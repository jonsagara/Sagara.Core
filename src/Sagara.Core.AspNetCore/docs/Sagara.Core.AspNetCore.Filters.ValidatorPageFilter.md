#### [Sagara.Core.AspNetCore](index.md 'index')
### [Sagara.Core.AspNetCore.Filters](index.md#Sagara.Core.AspNetCore.Filters 'Sagara.Core.AspNetCore.Filters')

## ValidatorPageFilter Class

Check the request's ModelState before the Page gets it. If invalid, return a 400 Bad Request. If  
it's not a GET, serialize ModelState as JSON and return it in the response body.

```csharp
public class ValidatorPageFilter :
Microsoft.AspNetCore.Mvc.Filters.IPageFilter,
Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ValidatorPageFilter

Implements [Microsoft.AspNetCore.Mvc.Filters.IPageFilter](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IPageFilter 'Microsoft.AspNetCore.Mvc.Filters.IPageFilter'), [Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata 'Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata')
### Methods

<a name='Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerExecuted(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext)'></a>

## ValidatorPageFilter.OnPageHandlerExecuted(PageHandlerExecutedContext) Method

Intentionally blank.

```csharp
public void OnPageHandlerExecuted(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext context);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerExecuted(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext).context'></a>

`context` [Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext 'Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext')

Implements [OnPageHandlerExecuted(PageHandlerExecutedContext)](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IPageFilter.OnPageHandlerExecuted#Microsoft_AspNetCore_Mvc_Filters_IPageFilter_OnPageHandlerExecuted_Microsoft_AspNetCore_Mvc_Filters_PageHandlerExecutedContext_ 'Microsoft.AspNetCore.Mvc.Filters.IPageFilter.OnPageHandlerExecuted(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext)')

<a name='Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext)'></a>

## ValidatorPageFilter.OnPageHandlerExecuting(PageHandlerExecutingContext) Method

If model state is valid, do nothing. Otherwise, if this is a GET request, return an empty 400 Bad Request.  
If this is not a GET request, serialize the ModeState as JSON and return it in the response as a 400 Bad Request.

```csharp
public void OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext context);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext).context'></a>

`context` [Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext 'Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext')

Implements [OnPageHandlerExecuting(PageHandlerExecutingContext)](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IPageFilter.OnPageHandlerExecuting#Microsoft_AspNetCore_Mvc_Filters_IPageFilter_OnPageHandlerExecuting_Microsoft_AspNetCore_Mvc_Filters_PageHandlerExecutingContext_ 'Microsoft.AspNetCore.Mvc.Filters.IPageFilter.OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext)')

<a name='Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerSelected(Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext)'></a>

## ValidatorPageFilter.OnPageHandlerSelected(PageHandlerSelectedContext) Method

Intentionally blank.

```csharp
public void OnPageHandlerSelected(Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext context);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerSelected(Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext).context'></a>

`context` [Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext 'Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext')

Implements [OnPageHandlerSelected(PageHandlerSelectedContext)](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IPageFilter.OnPageHandlerSelected#Microsoft_AspNetCore_Mvc_Filters_IPageFilter_OnPageHandlerSelected_Microsoft_AspNetCore_Mvc_Filters_PageHandlerSelectedContext_ 'Microsoft.AspNetCore.Mvc.Filters.IPageFilter.OnPageHandlerSelected(Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext)')