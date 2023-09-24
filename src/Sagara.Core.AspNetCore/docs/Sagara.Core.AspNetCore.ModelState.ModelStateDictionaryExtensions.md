#### [Sagara.Core.AspNetCore](index.md 'index')
### [Sagara.Core.AspNetCore.ModelState](index.md#Sagara.Core.AspNetCore.ModelState 'Sagara.Core.AspNetCore.ModelState')

## ModelStateDictionaryExtensions Class

Extensions to convert ModelStateDictionary errors into JSON responses.

```csharp
public static class ModelStateDictionaryExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ModelStateDictionaryExtensions
### Methods

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.AddModelErrors(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary,System.Collections.Generic.IReadOnlyCollection_string_)'></a>

## ModelStateDictionaryExtensions.AddModelErrors(this ModelStateDictionary, IReadOnlyCollection<string>) Method

Add a collection of error messages to ModelState, each with a blank key.

```csharp
public static void AddModelErrors(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, System.Collections.Generic.IReadOnlyCollection<string> errors);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.AddModelErrors(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary,System.Collections.Generic.IReadOnlyCollection_string_).modelState'></a>

`modelState` [Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary 'Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary')

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.AddModelErrors(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary,System.Collections.Generic.IReadOnlyCollection_string_).errors'></a>

`errors` [System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary,System.Collections.Generic.IReadOnlyCollection_Sagara.Core.Validation.RequestError_)'></a>

## ModelStateDictionaryExtensions.ToJsonErrorResult(this ModelStateDictionary, IReadOnlyCollection<RequestError>) Method

Creates a 400 Bad Request ContentResult whose body is the JSON-serialized ModelStateDictionary.

```csharp
public static Microsoft.AspNetCore.Mvc.ContentResult ToJsonErrorResult(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, System.Collections.Generic.IReadOnlyCollection<Sagara.Core.Validation.RequestError> additionalErrors);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary,System.Collections.Generic.IReadOnlyCollection_Sagara.Core.Validation.RequestError_).modelState'></a>

`modelState` [Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary 'Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary')

The ModelState dictionary.

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary,System.Collections.Generic.IReadOnlyCollection_Sagara.Core.Validation.RequestError_).additionalErrors'></a>

`additionalErrors` [System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[Sagara.Core.Validation.RequestError](https://docs.microsoft.com/en-us/dotnet/api/Sagara.Core.Validation.RequestError 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')

A collection of property-specific error messages to add to model state.

#### Returns
[Microsoft.AspNetCore.Mvc.ContentResult](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.ContentResult 'Microsoft.AspNetCore.Mvc.ContentResult')

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary)'></a>

## ModelStateDictionaryExtensions.ToJsonErrorResult(this ModelStateDictionary) Method

Creates a 400 Bad Request ContentResult whose body is the JSON-serialized ModelStateDictionary.

```csharp
public static Microsoft.AspNetCore.Mvc.ContentResult ToJsonErrorResult(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary).modelState'></a>

`modelState` [Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary 'Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary')

The ModelState dictionary.

#### Returns
[Microsoft.AspNetCore.Mvc.ContentResult](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.ContentResult 'Microsoft.AspNetCore.Mvc.ContentResult')

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToSlimModelStateDictionary(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary)'></a>

## ModelStateDictionaryExtensions.ToSlimModelStateDictionary(this ModelStateDictionary) Method

Only keep the Errors property, and of the errors, only keep the ErrorMessage.

```csharp
public static System.Collections.Generic.Dictionary<string,Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry> ToSlimModelStateDictionary(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToSlimModelStateDictionary(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary).modelState'></a>

`modelState` [Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary 'Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary')

#### Returns
[System.Collections.Generic.Dictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[SlimModelStateEntry](Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.md 'Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')