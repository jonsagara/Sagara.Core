using Microsoft.Extensions.Logging;

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
public class RedisAdminCache : RedisCache
{
    private readonly ILogger<RedisAdminCache> _logger;

    /// <summary>
    /// <para>!!! IMPORTANT !!! This class is only intended to be used as a singleton because we construct the
    /// multiplexer in our constructor, and constructing multiplexers is expensive.</para>
    /// <para>We have to use an instance created via DI instead of a static class because we need to be able 
    /// inject Configuration to have access to the connection string.</para>
    /// </summary>
    /// <remarks>
    /// <para>Same as <see cref="RedisCache"/>, but it supports protected operations, such as FLUSH.</para>
    /// <para>NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.</para>
    /// </remarks>
    public RedisAdminCache(ILogger<RedisAdminCache> logger, string connectionString)
        : base(logger, connectionString, allowAdmin: true)
    {
        _logger = logger;
    }


#pragma warning disable CA1031 // Do not catch general exception types

    /// <summary>
    /// Delete all the keys of all databases on the server.
    /// </summary>
    /// <returns></returns>
    public async Task FlushAllAsync()
    {
        try
        {
            foreach (var endPoint in Multiplexer.GetEndPoints())
            {
                var server = Multiplexer.GetServer(endPoint);
                await server
                    .FlushAllDatabasesAsync()
                    .ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            // Don't let the cache server bring down the application.
            _logger.UnhandledException(ex, command: "FLUSHALL", key: "(all keys)");
        }
    }

#pragma warning restore CA1031 // Do not catch general exception types
}
