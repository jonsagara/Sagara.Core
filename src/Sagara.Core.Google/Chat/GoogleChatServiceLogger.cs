using System.Net;
using Microsoft.Extensions.Logging;

namespace Sagara.Core.Google.Chat;

internal static partial class GoogleChatServiceLogger
{
    [LoggerMessage(EventId = 1_000, Level = LogLevel.Error, Message = "Unhandled exception occurred while trying to send a Google Chat message. message={Message}")]
    public static partial void Error_UnhandledException(this ILogger logger, Exception? ex, string? message);

    [LoggerMessage(EventId = 1_001, Level = LogLevel.Error, Message = "Request to Google Chat API failed. payloadJson={PayloadJson}, statusCode={StatusCodeInt} {StatusCode}, responseBody={ResponseBody}")]
    public static partial void Error_RequestFailed(this ILogger logger, string? payloadJson, int statusCodeInt, HttpStatusCode statusCode, string? responseBody);

    [LoggerMessage(EventId = 1_002, Level = LogLevel.Error, Message = "Request to Google Chat throttling relay failed. space={SpaceName}, payloadJson={PayloadJson}, statusCode={StatusCodeInt} {StatusCode}, responseBody={ResponseBody}")]
    public static partial void Error_RelayRequestFailed(this ILogger logger, string spaceName, string? payloadJson, int statusCodeInt, HttpStatusCode statusCode, string? responseBody);

    [LoggerMessage(EventId = 1_003, Level = LogLevel.Error, Message = "Unhandled exception occurred while trying to enqueue a Google Chat message with the throttling relay. space={SpaceName}, message={Message}")]
    public static partial void Error_RelayUnhandledException(this ILogger logger, Exception? ex, string spaceName, string? message);
}
