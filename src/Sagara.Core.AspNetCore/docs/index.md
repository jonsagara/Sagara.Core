#### [Sagara.Core.AspNetCore](index.md 'index')

## Sagara.Core.AspNetCore Assembly
### Namespaces

<a name='Sagara.Core.AspNetCore.Extensions'></a>

## Sagara.Core.AspNetCore.Extensions Namespace
- **[PageModelExtensions](Sagara.Core.AspNetCore.Extensions.PageModelExtensions.md 'Sagara.Core.AspNetCore.Extensions.PageModelExtensions')** `Class`
  - **[RedirectToPageJson&lt;TPageModel&gt;(this TPageModel, string, string, object)](Sagara.Core.AspNetCore.Extensions.PageModelExtensions.md#Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson_TPageModel_(thisTPageModel,string,string,object) 'Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson<TPageModel>(this TPageModel, string, string, object)')** `Method` Send a JsonResult whose object has a redirect property containing the URL that the client JavaScript should  
    redirect the browser to.

<a name='Sagara.Core.AspNetCore.Filters'></a>

## Sagara.Core.AspNetCore.Filters Namespace
- **[UnhandledExceptionFilter](Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.md 'Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter')** `Class`   
    
  Trap and log exceptions that occur in the MVC pipeline so that we can add more context, e.g., controller,  
              action, and raw URL.  
    
  The docs say, "Exception filters handle unhandled exceptions, including those that occur during   
              controller creation and model binding. They are only called when an exception occurs in the pipeline. [...]   
              Exception filters are good for trapping exceptions that occur within MVC actions."  
    
  See: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters#exception-filters
  - **[UnhandledExceptionFilter(ILogger&lt;UnhandledExceptionFilter&gt;)](Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.md#Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.UnhandledExceptionFilter(Microsoft.Extensions.Logging.ILogger_Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter_) 'Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.UnhandledExceptionFilter(Microsoft.Extensions.Logging.ILogger<Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter>)')** `Constructor` .ctor
  - **[OnException(ExceptionContext)](Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.md#Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext) 'Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilter.OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext)')** `Method` Write an informative, descriptive error message to the configured logger.
- **[UnhandledExceptionFilterLogger](Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilterLogger.md 'Sagara.Core.AspNetCore.Filters.UnhandledExceptionFilterLogger')** `Class` High-performance logging for ASP.NET Core. See: https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator
- **[ValidatorActionFilter](Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.md 'Sagara.Core.AspNetCore.Filters.ValidatorActionFilter')** `Class`   
    
  Validate non-GET requests from web pages. This is what enables the AJAX-y form submit behavior.
  - **[OnActionExecuted(ActionExecutedContext)](Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.md#Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext) 'Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext)')** `Method` Intentionally blank.
  - **[OnActionExecuting(ActionExecutingContext)](Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.md#Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext) 'Sagara.Core.AspNetCore.Filters.ValidatorActionFilter.OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext)')** `Method` Try to validate the request.
- **[ValidatorPageFilter](Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.md 'Sagara.Core.AspNetCore.Filters.ValidatorPageFilter')** `Class` Check the request's ModelState before the Page gets it. If invalid, return a 400 Bad Request. If  
  it's not a GET, serialize ModelState as JSON and return it in the response body.
  - **[OnPageHandlerExecuted(PageHandlerExecutedContext)](Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.md#Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerExecuted(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext) 'Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerExecuted(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext)')** `Method` Intentionally blank.
  - **[OnPageHandlerExecuting(PageHandlerExecutingContext)](Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.md#Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext) 'Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext)')** `Method` If model state is valid, do nothing. Otherwise, if this is a GET request, return an empty 400 Bad Request.  
    If this is not a GET request, serialize the ModeState as JSON and return it in the response as a 400 Bad Request.
  - **[OnPageHandlerSelected(PageHandlerSelectedContext)](Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.md#Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerSelected(Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext) 'Sagara.Core.AspNetCore.Filters.ValidatorPageFilter.OnPageHandlerSelected(Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext)')** `Method` Intentionally blank.

<a name='Sagara.Core.AspNetCore.ModelState'></a>

## Sagara.Core.AspNetCore.ModelState Namespace
- **[ModelStateDictionaryExtensions](Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.md 'Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions')** `Class` Extensions to convert ModelStateDictionary errors into JSON responses.
  - **[AddModelErrors(this ModelStateDictionary, IReadOnlyCollection&lt;string&gt;)](Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.md#Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.AddModelErrors(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary,System.Collections.Generic.IReadOnlyCollection_string_) 'Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.AddModelErrors(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary, System.Collections.Generic.IReadOnlyCollection<string>)')** `Method` Add a collection of error messages to ModelState, each with a blank key.
  - **[ToJsonErrorResult(this ModelStateDictionary, IReadOnlyCollection&lt;RequestError&gt;)](Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.md#Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary,System.Collections.Generic.IReadOnlyCollection_Sagara.Core.Validation.RequestError_) 'Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary, System.Collections.Generic.IReadOnlyCollection<Sagara.Core.Validation.RequestError>)')** `Method` Creates a 400 Bad Request ContentResult whose body is the JSON-serialized ModelStateDictionary.
  - **[ToJsonErrorResult(this ModelStateDictionary)](Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.md#Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary) 'Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToJsonErrorResult(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary)')** `Method` Creates a 400 Bad Request ContentResult whose body is the JSON-serialized ModelStateDictionary.
  - **[ToSlimModelStateDictionary(this ModelStateDictionary)](Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.md#Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToSlimModelStateDictionary(thisMicrosoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary) 'Sagara.Core.AspNetCore.ModelState.ModelStateDictionaryExtensions.ToSlimModelStateDictionary(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary)')** `Method` Only keep the Errors property, and of the errors, only keep the ErrorMessage.
- **[SlimModelStateEntry](Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.md 'Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry')** `Class` For sending model state errors back to the browser without all the extra, unnecessary fluff.
  - **[SlimModelStateEntry(IReadOnlyCollection&lt;SlimError&gt;)](Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.md#Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.SlimModelStateEntry(System.Collections.Generic.IReadOnlyCollection_Sagara.Core.AspNetCore.ModelState.SlimError_) 'Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.SlimModelStateEntry(System.Collections.Generic.IReadOnlyCollection<Sagara.Core.AspNetCore.ModelState.SlimError>)')** `Constructor` .ctor
  - **[Errors](Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.md#Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.Errors 'Sagara.Core.AspNetCore.ModelState.SlimModelStateEntry.Errors')** `Property` The collection of model state errors for the request.
- **[SlimError](Sagara.Core.AspNetCore.ModelState.SlimError.md 'Sagara.Core.AspNetCore.ModelState.SlimError')** `Struct` Used to reduce the content of a model state dictionary that's sent back to the browser.
  - **[SlimError(string)](Sagara.Core.AspNetCore.ModelState.SlimError.md#Sagara.Core.AspNetCore.ModelState.SlimError.SlimError(string) 'Sagara.Core.AspNetCore.ModelState.SlimError.SlimError(string)')** `Constructor` Used to reduce the content of a model state dictionary that's sent back to the browser.
  - **[ErrorMessage](Sagara.Core.AspNetCore.ModelState.SlimError.md#Sagara.Core.AspNetCore.ModelState.SlimError.ErrorMessage 'Sagara.Core.AspNetCore.ModelState.SlimError.ErrorMessage')** `Property` The error message for the request.