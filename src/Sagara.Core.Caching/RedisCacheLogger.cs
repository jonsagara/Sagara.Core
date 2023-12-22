using System.Net;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Sagara.Core.Caching;

/// <summary>
/// High-performance logging for ASP.NET Core. See: https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator
/// </summary>
internal static partial class RedisCacheLogger
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "{LogPrefix}ConnectionFailed: Connection type '{ConnectionType}' on EndPoint '{EndPoint}' reported ConnectionFailureType '{FailureType}'")]
    public static partial void OnConnectionFailed(this ILogger logger, Exception? ex, string logPrefix, ConnectionType connectionType, EndPoint? endPoint, ConnectionFailureType failureType);

    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "{LogPrefix}ConnectionRestored: Connection type '{ConnectionType}' on EndPoint '{EndPoint}' reported ConnectionFailureType '{FailureType}'")]
    public static partial void OnConnectionRestored(this ILogger logger, Exception? ex, string logPrefix, ConnectionType connectionType, EndPoint? endPoint, ConnectionFailureType failureType);

    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "{LogPrefix}ErrorMessage: Server '{EndPoint}' reported this error message: {Message}")]
    public static partial void OnErrorMessage(this ILogger logger, string logPrefix, EndPoint? endPoint, string message);

    [LoggerMessage(EventId = 0, Level = LogLevel.Warning, Message = "{LogPrefix}ServerMaintenanceEvent: Server maintenance event received at {ReceivedTimeUtc}. Expected start time is {StartTimeUtc}. Raw message: {RawMessage}")]
    public static partial void OnServerMaintenanceEvent(this ILogger logger, string logPrefix, DateTime receivedTimeUtc, DateTime? startTimeUtc, string? rawMessage);

    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "Unhandled exception trying to async {Command} {Key}")]
    public static partial void UnhandledException(this ILogger logger, Exception ex, string command, string key);

    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "Unhandled exception trying to SUBSCRIBE {Channel}")]
    public static partial void UnhandledSubscribeException(this ILogger logger, Exception ex, RedisChannel channel);

    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "Unhandled exception trying to async PUBLISH {Channel} {Message}")]
    public static partial void UnhandledPublishException(this ILogger logger, Exception ex, RedisChannel channel, RedisValue message);
}
