using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sagara.Core.AspNetCore.Extensions;

public static class PageModelExtensions
{
    /// <summary>
    /// Send a JsonResult whose object has a redirect property containing the URL that the client JavaScript should
    /// redirect the browser to.
    /// </summary>
    /// <param name="pageModel">The PageModel object.</param>
    /// <param name="pageName">The target page name.</param>
    /// <param name="pageHandler">The page handler, if any.</param>
    /// <param name="values">Route values, if any.</param>
    public static JsonResult RedirectToPageJson<TPageModel>(this TPageModel pageModel, string pageName, string? pageHandler = null, object? values = null)
        where TPageModel : PageModel
    {
        Check.NotNull(pageModel);

        return new JsonResult(new
        {
            redirect = pageModel.Url.Page(pageName: pageName, pageHandler: pageHandler, values: values)
        });
    }
}
