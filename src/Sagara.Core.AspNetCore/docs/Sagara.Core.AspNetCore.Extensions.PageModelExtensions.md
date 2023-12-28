#### [Sagara.Core.AspNetCore](index.md 'index')
### [Sagara.Core.AspNetCore.Extensions](index.md#Sagara.Core.AspNetCore.Extensions 'Sagara.Core.AspNetCore.Extensions')

## PageModelExtensions Class

```csharp
public static class PageModelExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; PageModelExtensions
### Methods

<a name='Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson_TPageModel_(thisTPageModel,string,string,object)'></a>

## PageModelExtensions.RedirectToPageJson<TPageModel>(this TPageModel, string, string, object) Method

Send a JsonResult whose object has a redirect property containing the URL that the client JavaScript should  
redirect the browser to.

```csharp
public static Microsoft.AspNetCore.Mvc.JsonResult RedirectToPageJson<TPageModel>(this TPageModel pageModel, string pageName, string? pageHandler=null, object? values=null)
    where TPageModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel;
```
#### Type parameters

<a name='Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson_TPageModel_(thisTPageModel,string,string,object).TPageModel'></a>

`TPageModel`
#### Parameters

<a name='Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson_TPageModel_(thisTPageModel,string,string,object).pageModel'></a>

`pageModel` [TPageModel](Sagara.Core.AspNetCore.Extensions.PageModelExtensions.md#Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson_TPageModel_(thisTPageModel,string,string,object).TPageModel 'Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson<TPageModel>(this TPageModel, string, string, object).TPageModel')

The PageModel object.

<a name='Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson_TPageModel_(thisTPageModel,string,string,object).pageName'></a>

`pageName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The target page name.

<a name='Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson_TPageModel_(thisTPageModel,string,string,object).pageHandler'></a>

`pageHandler` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The page handler, if any.

<a name='Sagara.Core.AspNetCore.Extensions.PageModelExtensions.RedirectToPageJson_TPageModel_(thisTPageModel,string,string,object).values'></a>

`values` [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')

Route values, if any.

#### Returns
[Microsoft.AspNetCore.Mvc.JsonResult](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Mvc.JsonResult 'Microsoft.AspNetCore.Mvc.JsonResult')