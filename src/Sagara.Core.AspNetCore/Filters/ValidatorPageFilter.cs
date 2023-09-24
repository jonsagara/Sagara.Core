using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sagara.Core.AspNetCore.ModelState;

namespace Sagara.Core.AspNetCore.Filters;

/// <summary>
/// Check the request's ModelState before the Page gets it. If invalid, return a 400 Bad Request. If
/// it's not a GET, serialize ModelState as JSON and return it in the response body.
/// </summary>
public class ValidatorPageFilter : IPageFilter
{
    /// <summary>
    /// Intentionally blank.
    /// </summary>
    /// <param name="context"></param>
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    { }

    /// <summary>
    /// If model state is valid, do nothing. Otherwise, if this is a GET request, return an empty 400 Bad Request.
    /// If this is not a GET request, serialize the ModeState as JSON and return it in the response as a 400 Bad Request.
    /// </summary>
    /// <param name="context"></param>
    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        Check.NotNull(context);

        if (context.ModelState.IsValid)
        {
            // Nothing to do. Request validation passes.
            return;
        }

        if (context.HttpContext.Request.Method == "GET")
        {
            context.Result = new BadRequestResult();
            return;
        }


        //
        // Send the model state error information back to the client as JSON in the body of a 400 Bad Request response.
        //

        context.Result = context.ModelState.ToJsonErrorResult();
    }

    /// <summary>
    /// Intentionally blank.
    /// </summary>
    /// <param name="context"></param>
    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    { }
}
