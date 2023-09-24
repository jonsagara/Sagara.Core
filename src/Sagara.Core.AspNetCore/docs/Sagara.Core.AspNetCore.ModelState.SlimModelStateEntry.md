#### [Sagara.Core.AspNetCore](index.md 'index')
### [Sagara.Core.AspNetCore.ModelState](index.md#Sagara.Core.AspNetCore.ModelState 'Sagara.Core.AspNetCore.ModelState')

## SlimModelStateEntry Class

For sending model state errors back to the browser without all the extra, unnecessary fluff.

```csharp
public class SlimModelStateEntry :
System.IEquatable<Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; SlimModelStateEntry

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[SlimModelStateEntry](Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.md 'Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Constructors

<a name='Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.SlimModelStateEntry(System.Collections.Generic.IReadOnlyCollection_Sagara.Core.AspNetCore.ModelState.SlimError_)'></a>

## SlimModelStateEntry(IReadOnlyCollection<SlimError>) Constructor

.ctor

```csharp
public SlimModelStateEntry(System.Collections.Generic.IReadOnlyCollection<Sagara.Core.AspNetCore.ModelState.SlimError> errors);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.SlimModelStateEntry(System.Collections.Generic.IReadOnlyCollection_Sagara.Core.AspNetCore.ModelState.SlimError_).errors'></a>

`errors` [System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[SlimError](Sagara.Core.AspNetCore.ModelState.SlimError.md 'Sagara.Core.AspNetCore.ModelState.SlimError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')
### Properties

<a name='Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.Errors'></a>

## SlimModelStateEntry.Errors Property

The collection of model state errors for the request.

```csharp
public System.Collections.Generic.IReadOnlyCollection<Sagara.Core.AspNetCore.ModelState.SlimError> Errors { get; set; }
```

#### Property Value
[System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[SlimError](Sagara.Core.AspNetCore.ModelState.SlimError.md 'Sagara.Core.AspNetCore.ModelState.SlimError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')