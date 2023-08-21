﻿using Serilog.Core;
using Serilog.Events;

namespace Sagara.Core.Logging.Serilog;

/// <summary>
/// Serilog enricher that converts the log event's timestamp to UTC.
/// </summary>
public class UtcTimestampEnricher : ILogEventEnricher
{
    /// <summary>
    /// Enrich the log event with a UTC timestamp.
    /// </summary>
    /// <param name="logEvent">The log event to enrich.</param>
    /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        Check.NotNull(logEvent);
        Check.NotNull(propertyFactory);

        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UtcTimestamp", logEvent.Timestamp.UtcDateTime));
    }
}
