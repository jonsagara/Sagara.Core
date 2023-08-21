using Microsoft.Extensions.Logging;

namespace Sagara.Core.Caching;

public class RedisAdminCache : RedisCache
{
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
    }


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
            _logger.LogError(ex, "Unhandled exception while trying to delete all the keys of all databases on the server.");
        }
    }
}
