using System.Net;
using Microsoft.Extensions.Logging;

namespace Sagara.Core.Google.Chat;

internal static partial class GoogleChatServiceLogger
{
    [LoggerMessage(EventId = 1_000, Level = LogLevel.Error, Message = "Unhandled exception occurred while trying to send a Google Chat message. message={Message}")]
    public static partial void Error_UnhandledException(this ILogger logger, Exception? ex, string? message);

    [LoggerMessage(EventId = 1_001, Level = LogLevel.Error, Message = "Request to Google Chat API failed. payloadJson={PayloadJson}, statusCode={StatusCodeInt} {StatusCode}, responseBody={ResponseBody}")]
    public static partial void Error_RequestFailed(this ILogger logger, string? payloadJson, int statusCodeInt, HttpStatusCode statusCode, string? responseBody);
}
