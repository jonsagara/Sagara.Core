#### [Sagara.Core.Logging.Serilog](index.md 'index')

## Sagara.Core.Logging.Serilog Assembly
### Namespaces

<a name='Sagara.Core.Logging.Serilog'></a>

## Sagara.Core.Logging.Serilog Namespace
- **[LoggerEnrichmentProperties](Sagara.Core.Logging.Serilog.LoggerEnrichmentProperties.md 'Sagara.Core.Logging.Serilog.LoggerEnrichmentProperties')** `Class` Constants to use when configuring Serilog enrichers.
  - **[Assembly](Sagara.Core.Logging.Serilog.LoggerEnrichmentProperties.md#Sagara.Core.Logging.Serilog.LoggerEnrichmentProperties.Assembly 'Sagara.Core.Logging.Serilog.LoggerEnrichmentProperties.Assembly')** `Field` Use the assembly name as a Serilog enrichment property.
- **[SerilogExtensions](Sagara.Core.Logging.Serilog.SerilogExtensions.md 'Sagara.Core.Logging.Serilog.SerilogExtensions')** `Class`
  - **[UseSerilog(this HostApplicationBuilder, Action&lt;IConfiguration,IServiceProvider,LoggerConfiguration&gt;, bool, bool)](Sagara.Core.Logging.Serilog.SerilogExtensions.md#Sagara.Core.Logging.Serilog.SerilogExtensions.UseSerilog(thisMicrosoft.Extensions.Hosting.HostApplicationBuilder,System.Action_Microsoft.Extensions.Configuration.IConfiguration,System.IServiceProvider,Serilog.LoggerConfiguration_,bool,bool) 'Sagara.Core.Logging.Serilog.SerilogExtensions.UseSerilog(this Microsoft.Extensions.Hosting.HostApplicationBuilder, System.Action<Microsoft.Extensions.Configuration.IConfiguration,System.IServiceProvider,Serilog.LoggerConfiguration>, bool, bool)')** `Method` Sets Serilog as the logging provider.
- **[UtcTimestampEnricher](Sagara.Core.Logging.Serilog.UtcTimestampEnricher.md 'Sagara.Core.Logging.Serilog.UtcTimestampEnricher')** `Class` Serilog enricher that converts the log event's timestamp to UTC.
  - **[Enrich(LogEvent, ILogEventPropertyFactory)](Sagara.Core.Logging.Serilog.UtcTimestampEnricher.md#Sagara.Core.Logging.Serilog.UtcTimestampEnricher.Enrich(Serilog.Events.LogEvent,Serilog.Core.ILogEventPropertyFactory) 'Sagara.Core.Logging.Serilog.UtcTimestampEnricher.Enrich(Serilog.Events.LogEvent, Serilog.Core.ILogEventPropertyFactory)')** `Method` Enrich the log event with a UTC timestamp.