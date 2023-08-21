using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sagara.Core.Caching;

public static class RedisCacheExtensions
{
    /// <summary>
    /// Add a <see cref="RedisCache"/> instance as a singleton.
    /// </summary>
    /// <param name="services">The DI services collection to add to.</param>
    /// <param name="connectionString">The StackExchange.Redis connection string to use.</param>
    public static IServiceCollection AddRedisCacheSingleton(this IServiceCollection services, string connectionString)
    {
        Check.NotNull(services);
        Check.NotEmpty(connectionString);

        return services
            .AddSingleton(serviceProvider =>
            {
                // We need to pass an ILogger<T> instance to RedisCache.
                var logger = serviceProvider.GetRequiredService<ILogger<RedisCache>>();

                return new RedisCache(logger, connectionString);
            });
    }
}
