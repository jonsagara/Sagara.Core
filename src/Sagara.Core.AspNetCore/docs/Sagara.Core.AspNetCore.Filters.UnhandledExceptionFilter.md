#### [Sagara.Core.AspNetCore](index.md 'index')
### [Sagara.Core.AspNetCore.Filters](index.md#Sagara.Core.AspNetCore.Filters 'Sagara.Core.AspNetCore.Filters')

## UnhandledExceptionFilter Class

  
Trap and log exceptions that occur in the MVC pipeline so that we can add more context, e.g., controller,  
            action, and raw URL.  
  
The docs say, "Exception filters handle unhandled exceptions, including those that occur during   
            controller creation and model binding. They are only called when an exception occurs in the pipeline. [...]   
            Exception filters are good for trapping exceptions that occur within MVC actions."  
  
See: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters#exception-filters

```csharp
public class UnhandledExceptionFilter :
Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter,
Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; UnhandledExceptionFilter

Implements [Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter 'Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter'), [Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata 'Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata')
### Constructors

<a name='Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.UnhandledExceptionFilter(Microsoft.Extensions.Logging.ILogger_Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter_)'></a>

## UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter>) Constructor

.ctor

```csharp
public UnhandledExceptionFilter(Microsoft.Extensions.Logging.ILogger<Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter> logger);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.UnhandledExceptionFilter(Microsoft.Extensions.Logging.ILogger_Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter_).logger'></a>

`logger` [Microsoft.Extensions.Logging.ILogger&lt;](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger-1 'Microsoft.Extensions.Logging.ILogger`1')[UnhandledExceptionFilter](Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.md 'Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger-1 'Microsoft.Extensions.Logging.ILogger`1')
### Methods

<a name='Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext)'></a>

## UnhandledExceptionFilter.OnException(ExceptionContext) Method

Write an informative, descriptive error message to the configured logger.

```csharp
public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context);
```
#### Parameters

<a name='Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext).context'></a>

`context` [Microsoft.AspNetCore.Mvc.Filters.ExceptionContext](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.ExceptionContext 'Microsoft.AspNetCore.Mvc.Filters.ExceptionContext')

Implements [OnException(ExceptionContext)](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter.OnException#Microsoft_AspNetCore_Mvc_Filters_IExceptionFilter_OnException_Microsoft_AspNetCore_Mvc_Filters_ExceptionContext_ 'Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter.OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext)')