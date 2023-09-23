#### [Sagara.Core](index.md 'index')
### [Sagara.Core.IO](index.md#Sagara.Core.IO 'Sagara.Core.IO')

## RecyclableMemoryStreamManagerHelper Class

Creates a [Microsoft.IO.RecyclableMemoryStreamManager](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.IO.RecyclableMemoryStreamManager 'Microsoft.IO.RecyclableMemoryStreamManager') that the caller can register as a singleton in   
a DI framework.

```csharp
public static class RecyclableMemoryStreamManagerHelper
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; RecyclableMemoryStreamManagerHelper
### Methods

<a name='Sagara.Core.IO.RecyclableMemoryStreamManagerHelper.Create()'></a>

## RecyclableMemoryStreamManagerHelper.Create() Method

  
Create a new [Microsoft.IO.RecyclableMemoryStreamManager](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.IO.RecyclableMemoryStreamManager 'Microsoft.IO.RecyclableMemoryStreamManager') instance that can be used by the DI framework.  
  
Uses the same defaults (as of Microsoft.IO.RecyclableMemoryStream v2.3.2) as the [Microsoft.IO.RecyclableMemoryStreamManager](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.IO.RecyclableMemoryStreamManager 'Microsoft.IO.RecyclableMemoryStreamManager') default ctor,  
            except we cap the max small and large pool free bytes at 12.5 MB and 512 MB, respectively. RMSM leaves them unbounded.

```csharp
public static Microsoft.IO.RecyclableMemoryStreamManager Create();
```

#### Returns
[Microsoft.IO.RecyclableMemoryStreamManager](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.IO.RecyclableMemoryStreamManager 'Microsoft.IO.RecyclableMemoryStreamManager')

### Remarks
  
See: https://github.com/microsoft/Microsoft.IO.RecyclableMemoryStream