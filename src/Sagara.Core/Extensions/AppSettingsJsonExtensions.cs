using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Sagara.Core.Extensions;

public static class AppSettingsJsonExtensions
{
    /// <summary>
    /// <para>When running in local dev, allow a non-committed appsettings.json file to override a project's
    /// regular appsettings.json. The former is located in a common directory outside of the project.</para>
    /// <para>If the file does not exist, the configuration remains unchanged.</para>
    /// <para>When running anywhere other than local dev, do not modify the configuration.</para>
    /// </summary>
    /// <remarks>
    /// <para>Each application (console or web) must copy the file from {solution root}/config/localdev/appsettings.development.{Environment.UserName.ToLowerInvariant()}.json</para>
    /// <para>Console applications must copy the file to the build output directory. Be sure to disable publishing of this file.</para>
    /// <para>Web applications must copy the file to the web application project root so that the values are picked up during local
    /// execution and debugging. This requires a custom build target. If you're someone other than Jon, ask Jon for a sample.</para>
    /// </remarks>
    public static IConfigurationBuilder AddJsonFileLoadDevOverrides(this IConfigurationBuilder builder, IHostEnvironment environment)
    {
        Check.ThrowIfNull(builder);
        Check.ThrowIfNull(environment);

        if (!environment.IsDevelopment())
        {
            // If we're running somewhere other than local dev, don't make any changes.
            return builder;
        }

        // Justification: I use lowercase for these file names.
#pragma warning disable CA1308 // Normalize strings to uppercase
        var localDevAppSettingsJsonFileName = $"appsettings.development.{Environment.UserName.ToLowerInvariant()}.json";
#pragma warning restore CA1308 // Normalize strings to uppercase

        var localDevAppSettingsJsonPath = Path.Combine(environment.ContentRootPath, localDevAppSettingsJsonFileName);

        if (!File.Exists(localDevAppSettingsJsonPath))
        {
            Log.Information("Skipping loading of {FileName} because the file does not exist: {FilePath}", localDevAppSettingsJsonFileName, localDevAppSettingsJsonPath);
            return builder;
        }

        Log.Warning("Loading local deveopment appsettings.json overrides from {FilePath}", localDevAppSettingsJsonPath);

        // Even though we checked above to ensure the file exists, make it optional.
        return builder.AddJsonFile(path: localDevAppSettingsJsonPath, optional: true);
    }
}
