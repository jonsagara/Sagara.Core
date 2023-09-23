#### [Sagara.Core.Caching](index.md 'index')
### [Sagara.Core.Caching](index.md#Sagara.Core.Caching 'Sagara.Core.Caching')

## RedisAdminCache Class

  
!!! IMPORTANT !!! This class is only intended to be used as a singleton because we construct the  
            multiplexer in our constructor, and constructing multiplexers is expensive.  
  
We have to use an instance created via DI instead of a static class because we need to be able   
            inject Configuration to have access to the connection string.

```csharp
public class RedisAdminCache : Sagara.Core.Caching.RedisCache
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [RedisCache](Sagara.Core.Caching.RedisCache.md 'Sagara.Core.Caching.RedisCache') &#129106; RedisAdminCache

### Remarks
NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.
### Constructors

<a name='Sagara.Core.Caching.RedisAdminCache.RedisAdminCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisAdminCache_,string)'></a>

## RedisAdminCache(ILogger<RedisAdminCache>, string) Constructor

  
!!! IMPORTANT !!! This class is only intended to be used as a singleton because we construct the  
            multiplexer in our constructor, and constructing multiplexers is expensive.  
  
We have to use an instance created via DI instead of a static class because we need to be able   
            inject Configuration to have access to the connection string.

```csharp
public RedisAdminCache(Microsoft.Extensions.Logging.ILogger<Sagara.Core.Caching.RedisAdminCache> logger, string connectionString);
```
#### Parameters

<a name='Sagara.Core.Caching.RedisAdminCache.RedisAdminCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisAdminCache_,string).logger'></a>

`logger` [Microsoft.Extensions.Logging.ILogger&lt;](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger-1 'Microsoft.Extensions.Logging.ILogger`1')[RedisAdminCache](Sagara.Core.Caching.RedisAdminCache.md 'Sagara.Core.Caching.RedisAdminCache')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger-1 'Microsoft.Extensions.Logging.ILogger`1')

<a name='Sagara.Core.Caching.RedisAdminCache.RedisAdminCache(Microsoft.Extensions.Logging.ILogger_Sagara.Core.Caching.RedisAdminCache_,string).connectionString'></a>

`connectionString` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

### Remarks
  
Same as [RedisCache](Sagara.Core.Caching.RedisCache.md 'Sagara.Core.Caching.RedisCache'), but it supports protected operations, such as FLUSH.  
  
NOTE: Be sure to set abortConnect=false in the connection string so that we gracefully handle connection failures.
### Methods

<a name='Sagara.Core.Caching.RedisAdminCache.FlushAllAsync()'></a>

## RedisAdminCache.FlushAllAsync() Method

Delete all the keys of all databases on the server.

```csharp
public System.Threading.Tasks.Task FlushAllAsync();
```

#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')