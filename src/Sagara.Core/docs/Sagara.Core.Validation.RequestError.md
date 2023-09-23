#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Validation](index.md#Sagara.Core.Validation 'Sagara.Core.Validation')

## RequestError Struct

Used to pass validation error messages from validation logic back to the UI for display.

```csharp
public readonly struct RequestError :
System.IEquatable<Sagara.Core.Validation.RequestError>
```

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Constructors

<a name='Sagara.Core.Validation.RequestError.RequestError(string,string)'></a>

## RequestError(string, string) Constructor

Used to pass validation error messages from validation logic back to the UI for display.

```csharp
public RequestError(string PropertyName, string ErrorMessage);
```
#### Parameters

<a name='Sagara.Core.Validation.RequestError.RequestError(string,string).PropertyName'></a>

`PropertyName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The name of the property, if any, that generated the error. If there is no property,  
            use string.Empty.

<a name='Sagara.Core.Validation.RequestError.RequestError(string,string).ErrorMessage'></a>

`ErrorMessage` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

A message describing the error.
### Properties

<a name='Sagara.Core.Validation.RequestError.ErrorMessage'></a>

## RequestError.ErrorMessage Property

A message describing the error.

```csharp
public string ErrorMessage { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Validation.RequestError.PropertyName'></a>

## RequestError.PropertyName Property

The name of the property, if any, that generated the error. If there is no property,  
            use string.Empty.

```csharp
public string PropertyName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')