#### [Sagara.Core.Caching](index.md 'index')

## Sagara.Core.Caching Assembly
### Namespaces

<a name='Sagara.Core.Caching'></a>

## Sagara.Core.Caching Namespace
- **[RedisAdminCache](Sagara.Core.Caching.RedisAdminCache.md 'Sagara.Core.Caching.RedisAdminCache')** `Class`   
    
  !!! IMPORTANT !!! This class is only intended to be used as a singleton because we construct the  
              multiplexer in our constructor, and constructing multiplexers is expensive.  
    
  We have to use an instance created via DI instead of a static class because we need to be able   
              inject Configuration to have access to the connection string.
  - **[RedisAdminCache(ILogger&lt;RedisAdminCache&gt;, string)](Sagara.Core.Caching.RedisAdminCache.md#Sagara.Core.Caching.RedisAdminCache.RedisAdminCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisAdminCache_,string) 'Sagara.Core.Caching.RedisAdminCache.RedisAdminCache(Microsoft.Extensions.Logging.ILogger<Sagara.Core.Caching.RedisAdminCache>, string)')** `Constructor`   
      
    !!! IMPORTANT !!! This class is only intended to be used as a singleton because we construct the  
                multiplexer in our constructor, and constructing multiplexers is expensive.  
      
    We have to use an instance created via DI instead of a static class because we need to be able   
                inject Configuration to have access to the connection string.
  - **[FlushAllAsync()](Sagara.Core.Caching.RedisAdminCache.md#Sagara.Core.Caching.RedisAdminCache.FlushAllAsync() 'Sagara.Core.Caching.RedisAdminCache.FlushAllAsync()')** `Method` Delete all the keys of all databases on the server.
- **[RedisCache](Sagara.Core.Caching.RedisCache.md 'Sagara.Core.Caching.RedisCache')** `Class`   
    
  !!! IMPORTANT !!! This class is only intended to be used as a singleton because we construct the  
              multiplexer in our constructor, and constructing multiplexers is expensive.  
    
  We have to use an instance created via DI instead of a static class because we need to be able   
              inject Configuration to have access to the connection string.
  - **[RedisCache(ILogger&lt;RedisCache&gt;, string, bool)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.RedisCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisCache_,string,bool) 'Sagara.Core.Caching.RedisCache.RedisCache(Microsoft.Extensions.Logging.ILogger<Sagara.Core.Caching.RedisCache>, string, bool)')** `Constructor` .ctor. Initializes the connection multiplexer.
  - **[Multiplexer](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.Multiplexer 'Sagara.Core.Caching.RedisCache.Multiplexer')** `Property` The multiplexer used to communicate with redis.
  - **[GetAsync&lt;T&gt;(string)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.GetAsync_T_(string) 'Sagara.Core.Caching.RedisCache.GetAsync<T>(string)')** `Method` Get the value of key. If the key does not exist the special value nil is returned.
  - **[GetSubscriber(object)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.GetSubscriber(object) 'Sagara.Core.Caching.RedisCache.GetSubscriber(object)')** `Method` Obtain a pub/sub subscriber connection to the specified server.
  - **[GetWithSlidingExpiration&lt;T&gt;(string, TimeSpan)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.GetWithSlidingExpiration_T_(string,System.TimeSpan) 'Sagara.Core.Caching.RedisCache.GetWithSlidingExpiration<T>(string, System.TimeSpan)')** `Method` Get the value of key. If the key does not exist the special value nil is returned. If the key does exist,  
    set or update its expiration time.
  - **[GetWithSlidingExpirationAsync&lt;T&gt;(string, TimeSpan)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.GetWithSlidingExpirationAsync_T_(string,System.TimeSpan) 'Sagara.Core.Caching.RedisCache.GetWithSlidingExpirationAsync<T>(string, System.TimeSpan)')** `Method` Get the value of key. If the key does not exist the special value nil is returned. If the key does exist,  
    set or update its expiration time.
  - **[IncrementAndExpireOnCreateAsync(string, TimeSpan)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.IncrementAndExpireOnCreateAsync(string,System.TimeSpan) 'Sagara.Core.Caching.RedisCache.IncrementAndExpireOnCreateAsync(string, System.TimeSpan)')** `Method`   
      
    If the key doesn't exist, then create it, increment by one, and set the given expiration.  
      
    If the key does exist, just increment the value without modifying the expiration.  
      
    NOTE: this is how we implement rate limiting for Attachment downloads in the API, ala https://redis.io/commands/incr#pattern-rate-limiter
  - **[InitializeConnectionMultiplexer(ConfigurationOptions)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.InitializeConnectionMultiplexer(StackExchange.Redis.ConfigurationOptions) 'Sagara.Core.Caching.RedisCache.InitializeConnectionMultiplexer(StackExchange.Redis.ConfigurationOptions)')** `Method` Initialize the connection multiplexer.
  - **[PublishAsync(RedisChannel, RedisValue)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.PublishAsync(StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue) 'Sagara.Core.Caching.RedisCache.PublishAsync(StackExchange.Redis.RedisChannel, StackExchange.Redis.RedisValue)')** `Method` Posts a message to the given channel.
  - **[RemoveAsync(string)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.RemoveAsync(string) 'Sagara.Core.Caching.RedisCache.RemoveAsync(string)')** `Method` Removes the specified key. A key is ignored if it does not exist.
  - **[RemoveAsync(IReadOnlyCollection&lt;string&gt;)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.RemoveAsync(System.Collections.Generic.IReadOnlyCollection_string_) 'Sagara.Core.Caching.RedisCache.RemoveAsync(System.Collections.Generic.IReadOnlyCollection<string>)')** `Method` Removes the specified keys. A key is ignored if it does not exist. Skips the check if there are no keys in  
    the collection. Throws if any of the keys are null or white space.
  - **[Set(string, object, Nullable&lt;TimeSpan&gt;)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.Set(string,object,System.Nullable_System.TimeSpan_) 'Sagara.Core.Caching.RedisCache.Set(string, object, System.Nullable<System.TimeSpan>)')** `Method` Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type. Any previous   
    time to live associated with the key is discarded on successful SET operation.
  - **[SetAsync(string, object, Nullable&lt;TimeSpan&gt;)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.SetAsync(string,object,System.Nullable_System.TimeSpan_) 'Sagara.Core.Caching.RedisCache.SetAsync(string, object, System.Nullable<System.TimeSpan>)')** `Method` Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type. Any previous   
    time to live associated with the key is discarded on successful SET operation.
  - **[SubscribeAsync(RedisChannel, Action&lt;RedisChannel,RedisValue&gt;)](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.SubscribeAsync(StackExchange.Redis.RedisChannel,System.Action_StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue_) 'Sagara.Core.Caching.RedisCache.SubscribeAsync(StackExchange.Redis.RedisChannel, System.Action<StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue>)')** `Method` Subscribe to perform some operation when a message to the preferred/active node is broadcast, without  
    any guarantee of ordered handling.
- **[RedisCacheExtensions](Sagara.Core.Caching.RedisCacheExtensions.md 'Sagara.Core.Caching.RedisCacheExtensions')** `Class`
  - **[AddRedisAdminCacheSingleton(this IServiceCollection, string)](Sagara.Core.Caching.RedisCacheExtensions.md#Sagara.Core.Caching.RedisCacheExtensions.AddRedisAdminCacheSingleton(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,string) 'Sagara.Core.Caching.RedisCacheExtensions.AddRedisAdminCacheSingleton(this Microsoft.Extensions.DependencyInjection.IServiceCollection, string)')** `Method` Add a [RedisAdminCache](Sagara.Core.Caching.RedisAdminCache.md 'Sagara.Core.Caching.RedisAdminCache') instance as a singleton.
  - **[AddRedisCacheSingleton(this IServiceCollection, string)](Sagara.Core.Caching.RedisCacheExtensions.md#Sagara.Core.Caching.RedisCacheExtensions.AddRedisCacheSingleton(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,string) 'Sagara.Core.Caching.RedisCacheExtensions.AddRedisCacheSingleton(this Microsoft.Extensions.DependencyInjection.IServiceCollection, string)')** `Method` Add a [RedisCache](Sagara.Core.Caching.RedisCache.md 'Sagara.Core.Caching.RedisCache') instance as a singleton.