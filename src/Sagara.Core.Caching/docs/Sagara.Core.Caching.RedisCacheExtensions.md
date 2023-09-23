#### [Sagara.Core.Caching](index.md 'index')
### [Sagara.Core.Caching](index.md#Sagara.Core.Caching 'Sagara.Core.Caching')

## RedisCacheExtensions Class

```csharp
public static class RedisCacheExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; RedisCacheExtensions
### Methods

<a name='Sagara.Core.Caching.RedisCacheExtensions.AddRedisAdminCacheSingleton(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,string)'></a>

## RedisCacheExtensions.AddRedisAdminCacheSingleton(this IServiceCollection, string) Method

Add a [RedisAdminCache](Sagara.Core.Caching.RedisAdminCache.md 'Sagara.Core.Caching.RedisAdminCache') instance as a singleton.

```csharp
public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddRedisAdminCacheSingleton(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, string connectionString);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCacheExtensions.AddRedisAdminCacheSingleton(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,string).services'></a>

`services` [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')

The DI services collection to add to.

<a name='Sagara.Core.Caching.RedisCacheExtensions.AddRedisAdminCacheSingleton(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,string).connectionString'></a>

`connectionString` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The StackExchange.Redis connection string to use.

#### Returns
[Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')

### Remarks
NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.

<a name='Sagara.Core.Caching.RedisCacheExtensions.AddRedisCacheSingleton(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,string)'></a>

## RedisCacheExtensions.AddRedisCacheSingleton(this IServiceCollection, string) Method

Add a [RedisCache](Sagara.Core.Caching.RedisCache.md 'Sagara.Core.Caching.RedisCache') instance as a singleton.

```csharp
public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddRedisCacheSingleton(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, string connectionString);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCacheExtensions.AddRedisCacheSingleton(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,string).services'></a>

`services` [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')

The DI services collection to add to.

<a name='Sagara.Core.Caching.RedisCacheExtensions.AddRedisCacheSingleton(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,string).connectionString'></a>

`connectionString` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The StackExchange.Redis connection string to use.

#### Returns
[Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')

### Remarks
NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.