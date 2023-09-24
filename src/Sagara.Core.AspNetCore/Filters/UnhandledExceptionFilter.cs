using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Sagara.Core.AspNetCore.Filters;

/// <summary>
/// <para>Trap and log exceptions that occur in the MVC pipeline so that we can add more context, e.g., controller,
/// action, and raw URL.</para>
/// <para>The docs say, &quot;Exception filters handle unhandled exceptions, including those that occur during 
/// controller creation and model binding. They are only called when an exception occurs in the pipeline. [...] 
/// Exception filters are good for trapping exceptions that occur within MVC actions.&quot;</para>
/// <para>See: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters#exception-filters</para>
/// </summary>
public class UnhandledExceptionFilter : IExceptionFilter
{
    private readonly ILogger _logger;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="logger"></param>
    public UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Write an informative, descriptive error message to the configured logger.
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        Check.NotNull(context);

        StringBuilder log = new();

        if (context.ActionDescriptor is ControllerActionDescriptor cad)
        {
            log.AppendLine($"*** Unhandled MVC exception caught in {nameof(UnhandledExceptionFilter)} ***");

            if (cad.RouteValues.TryGetValue("area", out string? area) && !string.IsNullOrWhiteSpace(area))
            {
                area += "/";
            }

            log.AppendLine(CultureInfo.InvariantCulture, $"    Request: {context.HttpContext?.Request?.Method} {area}{cad.ControllerName}/{cad.ActionName}");
        }
        else if (context.ActionDescriptor is CompiledPageActionDescriptor cpad)
        {
            log.AppendLine($"*** Unhandled Razor Pages exception caught in {nameof(UnhandledExceptionFilter)} ***");

            // ViewEnginePath already has a / in front, so no need to add one.
            cpad.RouteValues.TryGetValue("area", out string? area);

            log.AppendLine(CultureInfo.InvariantCulture, $"    Request: {context.HttpContext?.Request?.Method} {area}{cpad.ViewEnginePath}");
            log.AppendLine(CultureInfo.InvariantCulture, $"    ViewEnginePath: {cpad.RelativePath}");
        }
        else
        {
            log.AppendLine($"*** Unhandled exception caught in {nameof(UnhandledExceptionFilter)} ***");
        }

        if (context.HttpContext?.Request is not null)
        {
            log.AppendLine(CultureInfo.InvariantCulture, $"    Raw URL: {context.HttpContext.Request.GetEncodedUrl()}");
        }

        UnhandledExceptionFilterLogger.UnhandledException(_logger, context.Exception, log.ToString());

        // Don't set it to handled. Let it continue through the pipeline.
    }
}

/// <summary>
/// High-performance logging for ASP.NET Core. See: https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator
/// </summary>
internal static partial class UnhandledExceptionFilterLogger
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "{message}")]
    public static partial void UnhandledException(ILogger logger, Exception ex, string message);
}
