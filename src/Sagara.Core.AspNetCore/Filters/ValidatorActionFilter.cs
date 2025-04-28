using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sagara.Core.AspNetCore.ModelState;

namespace Sagara.Core.AspNetCore.Filters;

/// <summary>
/// <para>Validate non-GET requests from web pages. This is what enables the AJAX-y form submit behavior.</para>
/// </summary>
public class ValidatorActionFilter : IActionFilter
{
    /// <summary>
    /// Try to validate the request.
    /// </summary>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Check.ThrowIfNull(context);

        //if (context.HttpContext.Request.IsApiRequest())
        //{
        //    // Nothing to do. Don't validate API requests in this action filter. API responses use a separate response
        //    //   model for returning errors.
        //    return;
        //}

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
    public void OnActionExecuted(ActionExecutedContext context)
    { }
}
