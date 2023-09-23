#### [Sagara.Core.Logging.Serilog](index.md 'index')
### [Sagara.Core.Logging.Serilog](index.md#Sagara.Core.Logging.Serilog 'Sagara.Core.Logging.Serilog')

## SerilogExtensions Class

```csharp
public static class SerilogExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; SerilogExtensions
### Methods

<a name='Sagara.Core.Logging.Serilog.SerilogExtensions.UseSerilog(thisMicrosoft.Extensions.Hosting.HostApplicationBuilder,System.Action_Microsoft.Extensions.Configuration.IConfiguration,System.IServiceProvider,Serilog.LoggerConfiguration_,bool,bool)'></a>

## SerilogExtensions.UseSerilog(this HostApplicationBuilder, Action<IConfiguration,IServiceProvider,LoggerConfiguration>, bool, bool) Method

Sets Serilog as the logging provider.

```csharp
public static Microsoft.Extensions.Hosting.HostApplicationBuilder UseSerilog(this Microsoft.Extensions.Hosting.HostApplicationBuilder builder, System.Action<Microsoft.Extensions.Configuration.IConfiguration,System.IServiceProvider,Serilog.LoggerConfiguration> configureLogger, bool preserveStaticLogger=false, bool writeToProviders=false);
```
#### Parameters

<a name='Sagara.Core.Logging.Serilog.SerilogExtensions.UseSerilog(thisMicrosoft.Extensions.Hosting.HostApplicationBuilder,System.Action_Microsoft.Extensions.Configuration.IConfiguration,System.IServiceProvider,Serilog.LoggerConfiguration_,bool,bool).builder'></a>

`builder` [Microsoft.Extensions.Hosting.HostApplicationBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Hosting.HostApplicationBuilder 'Microsoft.Extensions.Hosting.HostApplicationBuilder')

The [Microsoft.Extensions.Hosting.HostApplicationBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Hosting.HostApplicationBuilder 'Microsoft.Extensions.Hosting.HostApplicationBuilder') to configure.

<a name='Sagara.Core.Logging.Serilog.SerilogExtensions.UseSerilog(thisMicrosoft.Extensions.Hosting.HostApplicationBuilder,System.Action_Microsoft.Extensions.Configuration.IConfiguration,System.IServiceProvider,Serilog.LoggerConfiguration_,bool,bool).configureLogger'></a>

`configureLogger` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-3 'System.Action`3')[Microsoft.Extensions.Configuration.IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Configuration.IConfiguration 'Microsoft.Extensions.Configuration.IConfiguration')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Action-3 'System.Action`3')[System.IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider 'System.IServiceProvider')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Action-3 'System.Action`3')[Serilog.LoggerConfiguration](https://docs.microsoft.com/en-us/dotnet/api/Serilog.LoggerConfiguration 'Serilog.LoggerConfiguration')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-3 'System.Action`3')

The delegate for configuring the Serilog.LoggerConfiguration that will be used   
            to construct a Serilog.Core.Logger.

<a name='Sagara.Core.Logging.Serilog.SerilogExtensions.UseSerilog(thisMicrosoft.Extensions.Hosting.HostApplicationBuilder,System.Action_Microsoft.Extensions.Configuration.IConfiguration,System.IServiceProvider,Serilog.LoggerConfiguration_,bool,bool).preserveStaticLogger'></a>

`preserveStaticLogger` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

Indicates whether to preserve the value of Serilog.Log.Logger.

<a name='Sagara.Core.Logging.Serilog.SerilogExtensions.UseSerilog(thisMicrosoft.Extensions.Hosting.HostApplicationBuilder,System.Action_Microsoft.Extensions.Configuration.IConfiguration,System.IServiceProvider,Serilog.LoggerConfiguration_,bool,bool).writeToProviders'></a>

`writeToProviders` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

By default, Serilog does not write events to Microsoft.Extensions.Logging.ILoggerProviders   
            registered through the Microsoft.Extensions.Logging API. Normally, equivalent Serilog sinks are used in place of providers.   
            Specify true to write events to all providers.

#### Returns
[Microsoft.Extensions.Hosting.HostApplicationBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Hosting.HostApplicationBuilder 'Microsoft.Extensions.Hosting.HostApplicationBuilder')  
The host application builder.

### Remarks
  
This is (hopefully) a temporary replacement for `Serilog.Extensions.Hosting.UseSerilog`. It's necessary  
            to support working with the new `Host.CreateApplicationBuilder()` pattern being rolled out in .NET 7, which   
            does not currently support the `IHostBuilder` interface.  
  
See: https://github.com/dotnet/runtime/discussions/81090#discussioncomment-4784551