#### [Sagara.Core.AspNetCore](index.md 'index')
### [Sagara.Core.AspNetCore.ModelState](index.md#Sagara.Core.AspNetCore.ModelState 'Sagara.Core.AspNetCore.ModelState')

## SlimError Struct

Used to reduce the content of a model state dictionary that's sent back to the browser.

```csharp
public readonly struct SlimError :
System.IEquatable<Sagara.Core.AspNetCore.ModelState.SlimError>
```

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[SlimError](Sagara.Core.AspNetCore.ModelState.SlimError.md 'Sagara.Core.AspNetCore.ModelState.SlimError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Constructors

<a name='Sagara.Core.AspNetCore.ModelState.SlimError.SlimError(string)'></a>

## SlimError(string) Constructor

Used to reduce the content of a model state dictionary that's sent back to the browser.

```csharp
public SlimError(string ErrorMessage);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.ModelState.SlimError.SlimError(string).ErrorMessage'></a>

`ErrorMessage` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The error message for the request.
### Properties

<a name='Sagara.Core.AspNetCore.ModelState.SlimError.ErrorMessage'></a>

## SlimError.ErrorMessage Property

The error message for the request.

```csharp
public string ErrorMessage { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')