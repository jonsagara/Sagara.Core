#### [Sagara.Core.Caching](index.md 'index')
### [Sagara.Core.Caching](index.md#Sagara.Core.Caching 'Sagara.Core.Caching')

## RedisCache Class

  
!!! IMPORTANT !!! This class is only intended to be used as a singleton because we construct the  
            multiplexer in our constructor, and constructing multiplexers is expensive.  
  
We have to use an instance created via DI instead of a static class because we need to be able   
            inject Configuration to have access to the connection string.

```csharp
public class RedisCache
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; RedisCache

Derived  
&#8627; [RedisAdminCache](Sagara.Core.Caching.RedisAdminCache.md 'Sagara.Core.Caching.RedisAdminCache')

### Remarks
NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.
### Constructors

<a name='Sagara.Core.Caching.RedisCache.RedisCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisCache_,string,bool)'></a>

## RedisCache(ILogger<RedisCache>, string, bool) Constructor

.ctor. Initializes the connection multiplexer.

```csharp
internal RedisCache(Microsoft.Extensions.Logging.ILogger<Sagara.Core.Caching.RedisCache> logger, string connectionString, bool allowAdmin);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.RedisCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisCache_,string,bool).logger'></a>

`logger` [Microsoft.Extensions.Logging.ILogger&lt;](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger-1 'Microsoft.Extensions.Logging.ILogger`1')[RedisCache](Sagara.Core.Caching.RedisCache.md 'Sagara.Core.Caching.RedisCache')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger-1 'Microsoft.Extensions.Logging.ILogger`1')

A logger from the DI container.

<a name='Sagara.Core.Caching.RedisCache.RedisCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisCache_,string,bool).connectionString'></a>

`connectionString` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The StackExchange.Redis connection string.

<a name='Sagara.Core.Caching.RedisCache.RedisCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisCache_,string,bool).allowAdmin'></a>

`allowAdmin` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

True to allow redis admin operations; false otherwise.
### Properties

<a name='Sagara.Core.Caching.RedisCache.Multiplexer'></a>

## RedisCache.Multiplexer Property

The multiplexer used to communicate with redis.

```csharp
protected StackExchange.Redis.ConnectionMultiplexer Multiplexer { get; set; }
```

#### Property Value
[StackExchange.Redis.ConnectionMultiplexer](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.ConnectionMultiplexer 'StackExchange.Redis.ConnectionMultiplexer')
### Methods

<a name='Sagara.Core.Caching.RedisCache.GetAsync_T_(string)'></a>

## RedisCache.GetAsync<T>(string) Method

Get the value of key. If the key does not exist the special value nil is returned.

```csharp
public System.Threading.Tasks.Task<T?> GetAsync<T>(string key);
```
#### Type parameters

<a name='Sagara.Core.Caching.RedisCache.GetAsync_T_(string).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.GetAsync_T_(string).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[T](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.GetAsync_T_(string).T 'Sagara.Core.Caching.RedisCache.GetAsync<T>(string).T')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')

<a name='Sagara.Core.Caching.RedisCache.GetSubscriber(object)'></a>

## RedisCache.GetSubscriber(object) Method

Obtain a pub/sub subscriber connection to the specified server.

```csharp
public StackExchange.Redis.ISubscriber GetSubscriber(object? asyncState=null);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.GetSubscriber(object).asyncState'></a>

`asyncState` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')

#### Returns
[StackExchange.Redis.ISubscriber](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.ISubscriber 'StackExchange.Redis.ISubscriber')

<a name='Sagara.Core.Caching.RedisCache.GetWithSlidingExpiration_T_(string,System.TimeSpan)'></a>

## RedisCache.GetWithSlidingExpiration<T>(string, TimeSpan) Method

Get the value of key. If the key does not exist the special value nil is returned. If the key does exist,  
set or update its expiration time.

```csharp
public T? GetWithSlidingExpiration<T>(string key, System.TimeSpan expiry);
```
#### Type parameters

<a name='Sagara.Core.Caching.RedisCache.GetWithSlidingExpiration_T_(string,System.TimeSpan).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.GetWithSlidingExpiration_T_(string,System.TimeSpan).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Caching.RedisCache.GetWithSlidingExpiration_T_(string,System.TimeSpan).expiry'></a>

`expiry` [System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')

#### Returns
[T](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.GetWithSlidingExpiration_T_(string,System.TimeSpan).T 'Sagara.Core.Caching.RedisCache.GetWithSlidingExpiration<T>(string, System.TimeSpan).T')

<a name='Sagara.Core.Caching.RedisCache.GetWithSlidingExpirationAsync_T_(string,System.TimeSpan)'></a>

## RedisCache.GetWithSlidingExpirationAsync<T>(string, TimeSpan) Method

Get the value of key. If the key does not exist the special value nil is returned. If the key does exist,  
set or update its expiration time.

```csharp
public System.Threading.Tasks.Task<T?> GetWithSlidingExpirationAsync<T>(string key, System.TimeSpan expiry);
```
#### Type parameters

<a name='Sagara.Core.Caching.RedisCache.GetWithSlidingExpirationAsync_T_(string,System.TimeSpan).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.GetWithSlidingExpirationAsync_T_(string,System.TimeSpan).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Caching.RedisCache.GetWithSlidingExpirationAsync_T_(string,System.TimeSpan).expiry'></a>

`expiry` [System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[T](Sagara.Core.Caching.RedisCache.md#Sagara.Core.Caching.RedisCache.GetWithSlidingExpirationAsync_T_(string,System.TimeSpan).T 'Sagara.Core.Caching.RedisCache.GetWithSlidingExpirationAsync<T>(string, System.TimeSpan).T')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')

<a name='Sagara.Core.Caching.RedisCache.IncrementAndExpireOnCreateAsync(string,System.TimeSpan)'></a>

## RedisCache.IncrementAndExpireOnCreateAsync(string, TimeSpan) Method

  
If the key doesn't exist, then create it, increment by one, and set the given expiration.  
  
If the key does exist, just increment the value without modifying the expiration.  
  
NOTE: this is how we implement rate limiting for Attachment downloads in the API, ala https://redis.io/commands/incr#pattern-rate-limiter

```csharp
public System.Threading.Tasks.Task<long> IncrementAndExpireOnCreateAsync(string key, System.TimeSpan expiry);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.IncrementAndExpireOnCreateAsync(string,System.TimeSpan).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The key of the value to increment.

<a name='Sagara.Core.Caching.RedisCache.IncrementAndExpireOnCreateAsync(string,System.TimeSpan).expiry'></a>

`expiry` [System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')

The absolute expiration of the key.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The value of key after incrementing.

<a name='Sagara.Core.Caching.RedisCache.InitializeConnectionMultiplexer(StackExchange.Redis.ConfigurationOptions)'></a>

## RedisCache.InitializeConnectionMultiplexer(ConfigurationOptions) Method

Initialize the connection multiplexer.

```csharp
protected StackExchange.Redis.ConnectionMultiplexer InitializeConnectionMultiplexer(StackExchange.Redis.ConfigurationOptions options);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.InitializeConnectionMultiplexer(StackExchange.Redis.ConfigurationOptions).options'></a>

`options` [StackExchange.Redis.ConfigurationOptions](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.ConfigurationOptions 'StackExchange.Redis.ConfigurationOptions')

#### Returns
[StackExchange.Redis.ConnectionMultiplexer](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.ConnectionMultiplexer 'StackExchange.Redis.ConnectionMultiplexer')

<a name='Sagara.Core.Caching.RedisCache.PublishAsync(StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue)'></a>

## RedisCache.PublishAsync(RedisChannel, RedisValue) Method

Posts a message to the given channel.

```csharp
public System.Threading.Tasks.Task<long> PublishAsync(StackExchange.Redis.RedisChannel channel, StackExchange.Redis.RedisValue message);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.PublishAsync(StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue).channel'></a>

`channel` [StackExchange.Redis.RedisChannel](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.RedisChannel 'StackExchange.Redis.RedisChannel')

<a name='Sagara.Core.Caching.RedisCache.PublishAsync(StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue).message'></a>

`message` [StackExchange.Redis.RedisValue](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.RedisValue 'StackExchange.Redis.RedisValue')

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')

<a name='Sagara.Core.Caching.RedisCache.RemoveAsync(string)'></a>

## RedisCache.RemoveAsync(string) Method

Removes the specified key. A key is ignored if it does not exist.

```csharp
public System.Threading.Tasks.Task<bool> RemoveAsync(string key);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.RemoveAsync(string).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')

<a name='Sagara.Core.Caching.RedisCache.RemoveAsync(System.Collections.Generic.IReadOnlyCollection_string_)'></a>

## RedisCache.RemoveAsync(IReadOnlyCollection<string>) Method

Removes the specified keys. A key is ignored if it does not exist. Skips the check if there are no keys in  
the collection. Throws if any of the keys are null or white space.

```csharp
public System.Threading.Tasks.Task<long> RemoveAsync(System.Collections.Generic.IReadOnlyCollection<string> keys);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.RemoveAsync(System.Collections.Generic.IReadOnlyCollection_string_).keys'></a>

`keys` [System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')

<a name='Sagara.Core.Caching.RedisCache.Set(string,object,System.Nullable_System.TimeSpan_)'></a>

## RedisCache.Set(string, object, Nullable<TimeSpan>) Method

Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type. Any previous   
time to live associated with the key is discarded on successful SET operation.

```csharp
public bool Set(string key, object value, System.Nullable<System.TimeSpan> expiry=null);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.Set(string,object,System.Nullable_System.TimeSpan_).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Caching.RedisCache.Set(string,object,System.Nullable_System.TimeSpan_).value'></a>

`value` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')

<a name='Sagara.Core.Caching.RedisCache.Set(string,object,System.Nullable_System.TimeSpan_).expiry'></a>

`expiry` [System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Sagara.Core.Caching.RedisCache.SetAsync(string,object,System.Nullable_System.TimeSpan_)'></a>

## RedisCache.SetAsync(string, object, Nullable<TimeSpan>) Method

Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type. Any previous   
time to live associated with the key is discarded on successful SET operation.

```csharp
public System.Threading.Tasks.Task<bool> SetAsync(string key, object value, System.Nullable<System.TimeSpan> expiry=null);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.SetAsync(string,object,System.Nullable_System.TimeSpan_).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Caching.RedisCache.SetAsync(string,object,System.Nullable_System.TimeSpan_).value'></a>

`value` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')

<a name='Sagara.Core.Caching.RedisCache.SetAsync(string,object,System.Nullable_System.TimeSpan_).expiry'></a>

`expiry` [System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')

<a name='Sagara.Core.Caching.RedisCache.SubscribeAsync(StackExchange.Redis.RedisChannel,System.Action_StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue_)'></a>

## RedisCache.SubscribeAsync(RedisChannel, Action<RedisChannel,RedisValue>) Method

Subscribe to perform some operation when a message to the preferred/active node is broadcast, without  
any guarantee of ordered handling.

```csharp
public System.Threading.Tasks.Task SubscribeAsync(StackExchange.Redis.RedisChannel channel, System.Action<StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue> handler);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisCache.SubscribeAsync(StackExchange.Redis.RedisChannel,System.Action_StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue_).channel'></a>

`channel` [StackExchange.Redis.RedisChannel](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.RedisChannel 'StackExchange.Redis.RedisChannel')

<a name='Sagara.Core.Caching.RedisCache.SubscribeAsync(StackExchange.Redis.RedisChannel,System.Action_StackExchange.Redis.RedisChannel,StackExchange.Redis.RedisValue_).handler'></a>

`handler` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-2 'System.Action`2')[StackExchange.Redis.RedisChannel](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.RedisChannel 'StackExchange.Redis.RedisChannel')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Action-2 'System.Action`2')[StackExchange.Redis.RedisValue](https://docs.microsoft.com/en-us/dotnet/api/StackExchange.Redis.RedisValue 'StackExchange.Redis.RedisValue')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-2 'System.Action`2')

#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')