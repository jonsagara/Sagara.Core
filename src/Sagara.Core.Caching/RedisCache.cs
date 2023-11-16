using Microsoft.Extensions.Logging;
using Sagara.Core.Json.SystemTextJson;
using StackExchange.Redis;

namespace Sagara.Core.Caching;

/// <summary>
/// <para>!!! IMPORTANT !!! This class is only intended to be used as a singleton because we construct the
/// multiplexer in our constructor, and constructing multiplexers is expensive.</para>
/// <para>We have to use an instance created via DI instead of a static class because we need to be able 
/// inject Configuration to have access to the connection string.</para>
/// </summary>
/// <remarks>
/// NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.
/// </remarks>
public class RedisCache
{
    private readonly ILogger<RedisCache> _logger;

    /// <summary>
    /// The multiplexer used to communicate with redis.
    /// </summary>
    protected ConnectionMultiplexer Multiplexer { get; set; }

    /// <summary>
    /// .ctor. Initializes the connection multiplexer.
    /// </summary>
    /// <param name="logger">A logger from the DI container.</param>
    /// <param name="connectionString">The StackExchange.Redis connection string.</param>
    /// <param name="allowAdmin">True to allow redis admin operations; false otherwise.</param>
    internal RedisCache(ILogger<RedisCache> logger, string connectionString, bool allowAdmin)
    {
        Check.NotEmpty(connectionString);

        _logger = logger;

        var configOptions = ConfigurationOptions.Parse(connectionString);
        configOptions.AllowAdmin = allowAdmin;

        Multiplexer = InitializeConnectionMultiplexer(configOptions);
    }

    /// <summary>
    /// Initialize the connection multiplexer.
    /// </summary>
    protected ConnectionMultiplexer InitializeConnectionMultiplexer(ConfigurationOptions options)
    {
        Check.NotNull(options);

        // Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.
        var multiplexer = ConnectionMultiplexer.Connect(options);

        // So we can tell which one failed - regular or admin.
        var logPrefix = options.AllowAdmin ? "[Admin] " : string.Empty;

        // Log connection and error events.
        multiplexer.ConnectionFailed += (sender, e) =>
        {
            RedisCacheLogger.OnConnectionFailed(_logger, e.Exception, logPrefix, e.ConnectionType, e.EndPoint, e.FailureType);
        };

        multiplexer.ConnectionRestored += (sender, e) =>
        {
            RedisCacheLogger.OnConnectionRestored(_logger, e.Exception, logPrefix, e.ConnectionType, e.EndPoint, e.FailureType);
        };

        multiplexer.ErrorMessage += (sender, e) =>
        {
            RedisCacheLogger.OnErrorMessage(_logger, logPrefix, e.EndPoint, e.Message);
        };

        multiplexer.ServerMaintenanceEvent += (sender, e) =>
        {
            RedisCacheLogger.OnServerMaintenanceEvent(_logger, logPrefix, e.ReceivedTimeUtc, e.StartTimeUtc, e.RawMessage);
        };

        return multiplexer;
    }


    // Justification: Anything can happen on the network. Catch them all.
#pragma warning disable CA1031 // Do not catch general exception types

    /// <summary>
    /// Get the value of key. If the key does not exist the special value nil is returned.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string key)
    {
        Check.NotEmpty(key);

        try
        {
            var db = GetDatabase();

            var value = await db
                .StringGetAsync(key)
                .ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(value))
            {
                return SystemTextJsonHelper.Deserialize<T>(value!);
            }
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledException(_logger, ex, command: "GET", key: key);
        }

        return default;
    }


    #region Sliding Expiration Lua script

    // If the value exists, update the key's expiration.
    //   * KEYS[1]: the key
    //   * ARGV[1]: the new expiration time in seconds
    private const string SLIDING_EXPIRATION_LUA_SCRIPT = """
local val = redis.call("GET", KEYS[1])
if val ~= false then
	redis.call("EXPIRE", KEYS[1], ARGV[1])
end

return val
""";

    #endregion

    /// <summary>
    /// Get the value of key. If the key does not exist the special value nil is returned. If the key does exist,
    /// set or update its expiration time.
    /// </summary>
    public async Task<T?> GetWithSlidingExpirationAsync<T>(string key, TimeSpan expiry)
    {
        Check.NotEmpty(key);

        try
        {
            var db = GetDatabase();

            var redisResult = await db
                .ScriptEvaluateAsync(SLIDING_EXPIRATION_LUA_SCRIPT, new RedisKey[] { key }, new RedisValue[] { expiry.TotalSeconds })
                .ConfigureAwait(false);

            var value = (string?)redisResult;
            if (!string.IsNullOrWhiteSpace(value))
            {
                return SystemTextJsonHelper.Deserialize<T>(value);
            }
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledException(_logger, ex, command: "GET/EXPIRE", key: key);
        }

        return default;
    }

    /// <summary>
    /// Get the value of key. If the key does not exist the special value nil is returned. If the key does exist,
    /// set or update its expiration time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="expiry"></param>
    /// <returns></returns>
    public T? GetWithSlidingExpiration<T>(string key, TimeSpan expiry)
    {
        Check.NotEmpty(key);

        try
        {
            var db = GetDatabase();

            var redisResult = db
                .ScriptEvaluate(SLIDING_EXPIRATION_LUA_SCRIPT, new RedisKey[] { key }, new RedisValue[] { expiry.TotalSeconds });

            var value = (string?)redisResult;
            if (!string.IsNullOrWhiteSpace(value))
            {
                return SystemTextJsonHelper.Deserialize<T>(value);
            }
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledException(_logger, ex, command: "GET/EXPIRE", key: key);
        }

        return default;
    }

    /// <summary>
    /// Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type. Any previous 
    /// time to live associated with the key is discarded on successful SET operation.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiry"></param>
    /// <returns></returns>
    public async Task<bool> SetAsync(string key, object value, TimeSpan? expiry = null)
    {
        Check.NotEmpty(key);
        Check.NotNull(value);

        try
        {
            var db = GetDatabase();

            return await db
                .StringSetAsync(key, SystemTextJsonHelper.Serialize(value), expiry)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledException(_logger, ex, command: "SET", key: key);
        }

        return false;
    }

    /// <summary>
    /// Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type. Any previous 
    /// time to live associated with the key is discarded on successful SET operation.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiry"></param>
    /// <returns></returns>
    public bool Set(string key, object value, TimeSpan? expiry = null)
    {
        Check.NotEmpty(key);
        Check.NotNull(value);

        try
        {
            var db = GetDatabase();

            return db.StringSet(key, SystemTextJsonHelper.Serialize(value), expiry);
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledException(_logger, ex, command: "SET", key: key);
        }

        return false;
    }

    ///// <summary>
    ///// Increments the number stored at key by increment. If the key does not exist, it is set to 0 before performing the 
    ///// operation. An error is returned if the key contains a value of the wrong type or contains a string that is not 
    ///// representable as integer. This operation is limited to 64 bit signed integers.
    ///// </summary>
    ///// <returns>the value of key after the increment</returns>
    //public async Task<long> IncrementAsync(string key, long increment = 1)
    //{
    //    Check.NotEmpty(key);

    //    try
    //    {
    //        var db = GetDatabase();

    //        return await db
    //            .StringIncrementAsync(key, increment)
    //            .ConfigureAwait(false);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Don't let cache server unavailability bring down the application.
    //        RedisCacheLogger.UnhandledException(_logger, ex, command: "INCR", key: key);
    //    }

    //    return 0L;
    //}

    /// <summary>
    /// Removes the specified key. A key is ignored if it does not exist.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<bool> RemoveAsync(string key)
    {
        Check.NotEmpty(key);

        try
        {
            var db = GetDatabase();

            return await db
                .KeyDeleteAsync(key)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledException(_logger, ex, command: "DEL", key: key);
        }

        return false;
    }

    /// <summary>
    /// Removes the specified keys. A key is ignored if it does not exist. Skips the check if there are no keys in
    /// the collection. Throws if any of the keys are null or white space.
    /// </summary>
    public async Task<long> RemoveAsync(IReadOnlyCollection<string> keys)
    {
        Check.HasNoEmpties(keys, nameof(keys));

        if (keys.Count > 0)
        {
            try
            {
                var db = GetDatabase();

                var redisKeys = keys
                    .Select(k => (RedisKey)k)
                    .ToArray();

                return await db
                    .KeyDeleteAsync(redisKeys)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Don't let cache server unavailability bring down the application.
                RedisCacheLogger.UnhandledException(_logger, ex, command: "DEL", key: string.Join(" ", keys));
            }
        }

        return 0L;
    }


    #region Increment and Expire on Create Lua script

    // Create the value if it doesn't exist, then increment it by one. If we create the key in this call,
    //   also set the the key's expiration so that the key is eventually evicted.
    //   * KEYS[1]: the key
    //   * ARGV[1]: the new expiration time in seconds
    private const string INCREMENT_AND_EXPIRE_ON_CREATE_LUA_SCRIPT = """
local val = redis.call("INCR", KEYS[1])
if tonumber(val) == 1 then
	redis.call("EXPIRE", KEYS[1], ARGV[1])
end

return val
""";

    #endregion


    /// <summary>
    /// <para>If the key doesn't exist, then create it, increment by one, and set the given expiration.</para>
    /// <para>If the key does exist, just increment the value without modifying the expiration.</para>
    /// <para>NOTE: this is how we implement rate limiting for Attachment downloads in the API, ala https://redis.io/commands/incr#pattern-rate-limiter </para>
    /// </summary>
    /// <param name="key">The key of the value to increment.</param>
    /// <param name="expiry">The absolute expiration of the key.</param>
    /// <returns>The value of key after incrementing.</returns>
    public async Task<long> IncrementAndExpireOnCreateAsync(string key, TimeSpan expiry)
    {
        try
        {
            var db = GetDatabase();

            // INCR and return the counter value. Will return 1 if it didn't already exist. Expire is only
            //   called if the key didn't already exist.
            return (long)await db
                .ScriptEvaluateAsync(INCREMENT_AND_EXPIRE_ON_CREATE_LUA_SCRIPT, new RedisKey[] { key }, new RedisValue[] { expiry.TotalSeconds })
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledException(_logger, ex, command: "INCR and EXPIRE On Create", key: key);
        }

        return 0L;
    }


    //
    // Pub/Sub
    //

    /// <summary>
    /// Obtain a pub/sub subscriber connection to the specified server.
    /// </summary>
    public ISubscriber GetSubscriber(object? asyncState = null)
        => Multiplexer.GetSubscriber(asyncState);

    /// <summary>
    /// Subscribe to perform some operation when a message to the preferred/active node is broadcast, without
    /// any guarantee of ordered handling.
    /// </summary>
    public async Task SubscribeAsync(RedisChannel channel, Action<RedisChannel, RedisValue> handler)
    {
        Check.NotNull(handler);

        try
        {
            await GetSubscriber()
                .SubscribeAsync(channel, handler)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledSubscribeException(_logger, ex, channel);
        }
    }

    /// <summary>
    /// Posts a message to the given channel.
    /// </summary>
    public async Task<long> PublishAsync(RedisChannel channel, RedisValue message)
    {
        try
        {
            return await GetSubscriber()
                .PublishAsync(channel, message)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Don't let cache server unavailability bring down the application.
            RedisCacheLogger.UnhandledPublishException(_logger, ex, channel, message);
        }

        return 0L;
    }

#pragma warning restore CA1031 // Do not catch general exception types


    //
    // Private methods
    //

    private IDatabase GetDatabase()
    {
        return Multiplexer.GetDatabase();
    }
}
