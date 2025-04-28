using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sagara.Core.Caching;

public static class RedisCacheExtensions
{
    /// <summary>
    /// Add a <see cref="RedisCache"/> instance as a singleton.
    /// </summary>
    /// <remarks>
    /// NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.
    /// </remarks>
    /// <param name="services">The DI services collection to add to.</param>
    /// <param name="connectionString">The StackExchange.Redis connection string to use.</param>
    public static IServiceCollection AddRedisCacheSingleton(this IServiceCollection services, string connectionString)
    {
        Check.ThrowIfNull(services);
        Check.ThrowIfNullOrWhiteSpace(connectionString);

        return services
            .AddSingleton(serviceProvider =>
            {
                // We need to pass an ILogger<T> instance to RedisCache.
                var logger = serviceProvider.GetRequiredService<ILogger<RedisCache>>();

                return new RedisCache(logger, connectionString, allowAdmin: false);
            });
    }

    /// <summary>
    /// Add a <see cref="RedisAdminCache"/> instance as a singleton.
    /// </summary>
    /// <remarks>
    /// NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.
    /// </remarks>
    /// <param name="services">The DI services collection to add to.</param>
    /// <param name="connectionString">The StackExchange.Redis connection string to use.</param>
    public static IServiceCollection AddRedisAdminCacheSingleton(this IServiceCollection services, string connectionString)
    {
        Check.ThrowIfNull(services);
        Check.ThrowIfNullOrWhiteSpace(connectionString);

        return services
            .AddSingleton(serviceProvider =>
            {
                // We need to pass an ILogger<T> instance to RedisAdminCache.
                var logger = serviceProvider.GetRequiredService<ILogger<RedisAdminCache>>();

                return new RedisAdminCache(logger, connectionString);
            });
    }
}
