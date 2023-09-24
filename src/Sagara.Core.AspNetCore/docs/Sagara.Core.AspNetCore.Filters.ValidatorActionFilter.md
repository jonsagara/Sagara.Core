#### [Sagara.Core.AspNetCore](index.md 'index')
### [Sagara.Core.AspNetCore.Filters](index.md#Sagara.Core.AspNetCore.Filters 'Sagara.Core.AspNetCore.Filters')

## ValidatorActionFilter Class

  
Validate non-GET requests from web pages. This is what enables the AJAX-y form submit behavior.

```csharp
public class ValidatorActionFilter :
Microsoft.AspNetCore.Mvc.Filters.IActionFilter,
Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ValidatorActionFilter

Implements [Microsoft.AspNetCore.Mvc.Filters.IActionFilter](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IActionFilter 'Microsoft.AspNetCore.Mvc.Filters.IActionFilter'), [Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata 'Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata')
### Methods

<a name='Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext)'></a>

## ValidatorActionFilter.OnActionExecuted(ActionExecutedContext) Method

Intentionally blank.

```csharp
public void OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext context);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext).context'></a>

`context` [Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext 'Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext')

Implements [OnActionExecuted(ActionExecutedContext)](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IActionFilter.OnActionExecuted#Microsoft_AspNetCore_Mvc_Filters_IActionFilter_OnActionExecuted_Microsoft_AspNetCore_Mvc_Filters_ActionExecutedContext_ 'Microsoft.AspNetCore.Mvc.Filters.IActionFilter.OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext)')

<a name='Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext)'></a>

## ValidatorActionFilter.OnActionExecuting(ActionExecutingContext) Method

Try to validate the request.

```csharp
public void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext).context'></a>

`context` [Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext 'Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext')

Implements [OnActionExecuting(ActionExecutingContext)](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IActionFilter.OnActionExecuting#Microsoft_AspNetCore_Mvc_Filters_IActionFilter_OnActionExecuting_Microsoft_AspNetCore_Mvc_Filters_ActionExecutingContext_ 'Microsoft.AspNetCore.Mvc.Filters.IActionFilter.OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext)')