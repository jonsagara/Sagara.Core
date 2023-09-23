#### [Sagara.Core.Logging.Serilog](index.md 'index')
### [Sagara.Core.Logging.Serilog](index.md#Sagara.Core.Logging.Serilog 'Sagara.Core.Logging.Serilog')

## UtcTimestampEnricher Class

Serilog enricher that converts the log event's timestamp to UTC.

```csharp
public class UtcTimestampEnricher :
Serilog.Core.ILogEventEnricher
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; UtcTimestampEnricher

Implements [Serilog.Core.ILogEventEnricher](https://docs.microsoft.com/en-us/dotnet/api/Serilog.Core.ILogEventEnricher 'Serilog.Core.ILogEventEnricher')
### Methods

<a name='Sagara.Core.Logging.Serilog.UtcTimestampEnricher.Enrich(Serilog.Events.LogEvent,Serilog.Core.ILogEventPropertyFactory)'></a>

## UtcTimestampEnricher.Enrich(LogEvent, ILogEventPropertyFactory) Method

Enrich the log event with a UTC timestamp.

```csharp
public void Enrich(Serilog.Events.LogEvent logEvent, Serilog.Core.ILogEventPropertyFactory propertyFactory);
```
#### Parameters

<a name='Sagara.Core.Logging.Serilog.UtcTimestampEnricher.Enrich(Serilog.Events.LogEvent,Serilog.Core.ILogEventPropertyFactory).logEvent'></a>

`logEvent` [Serilog.Events.LogEvent](https://docs.microsoft.com/en-us/dotnet/api/Serilog.Events.LogEvent 'Serilog.Events.LogEvent')

The log event to enrich.

<a name='Sagara.Core.Logging.Serilog.UtcTimestampEnricher.Enrich(Serilog.Events.LogEvent,Serilog.Core.ILogEventPropertyFactory).propertyFactory'></a>

`propertyFactory` [Serilog.Core.ILogEventPropertyFactory](https://docs.microsoft.com/en-us/dotnet/api/Serilog.Core.ILogEventPropertyFactory 'Serilog.Core.ILogEventPropertyFactory')

Factory for creating new properties to add to the event.

Implements [Enrich(LogEvent, ILogEventPropertyFactory)](https://docs.microsoft.com/en-us/dotnet/api/Serilog.Core.ILogEventEnricher.Enrich#Serilog_Core_ILogEventEnricher_Enrich_Serilog_Events_LogEvent,Serilog_Core_ILogEventPropertyFactory_ 'Serilog.Core.ILogEventEnricher.Enrich(Serilog.Events.LogEvent,Serilog.Core.ILogEventPropertyFactory)')