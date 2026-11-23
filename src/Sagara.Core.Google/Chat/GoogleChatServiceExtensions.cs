using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Sagara.Core.Google.Chat;

public static class GoogleChatServiceExtensions
{
    /// <summary>
    /// Registers a <see cref="GoogleChatService"/>, configured in code. The webhook URL (or relay space name) is
    /// supplied per call, not at registration time, so a single registered instance can send to any number of
    /// Google Chat spaces.
    /// </summary>
    /// <param name="services">The DI services collection to add to.</param>
    /// <param name="configureOptions">
    /// An optional callback to configure <see cref="GoogleChatServiceOptions"/>. If not specified, the default
    /// options are used, which mention users by <see cref="GoogleChatMentionStyle.Id"/> and configure no relay.
    /// </param>
    /// <remarks>
    /// Options are validated when first resolved, and at host startup for hosted apps. Invalid options cause
    /// an <see cref="OptionsValidationException"/>.
    /// </remarks>
    public static IServiceCollection AddGoogleChatService(
        this IServiceCollection services,
        Action<GoogleChatServiceOptions>? configureOptions = null)
    {
        Check.ThrowIfNull(services);

        return AddGoogleChatServiceCore(services, bindConfiguration: null, configureOptions);
    }

    /// <summary>
    /// Registers a <see cref="GoogleChatService"/>, binding <see cref="GoogleChatServiceOptions"/> from the given
    /// configuration section.
    /// </summary>
    /// <param name="services">The DI services collection to add to.</param>
    /// <param name="configurationSection">
    /// The configuration section to bind, e.g. <c>builder.Configuration.GetSection("GoogleChat")</c>.
    /// </param>
    /// <param name="configureOptions">
    /// An optional callback that runs after binding, so values set in code override bound values.
    /// </param>
    /// <remarks>
    /// Options are validated when first resolved, and at host startup for hosted apps. Invalid options cause
    /// an <see cref="OptionsValidationException"/>.
    /// </remarks>
    public static IServiceCollection AddGoogleChatService(
        this IServiceCollection services,
        IConfiguration configurationSection,
        Action<GoogleChatServiceOptions>? configureOptions = null)
    {
        Check.ThrowIfNull(services);
        Check.ThrowIfNull(configurationSection);

        return AddGoogleChatServiceCore(
            services,
            bindConfiguration: optionsBuilder => optionsBuilder.Bind(configurationSection),
            configureOptions);
    }

    /// <summary>
    /// Registers a <see cref="GoogleChatService"/>, binding <see cref="GoogleChatServiceOptions"/> from the
    /// configuration section at the given path in the app's registered <see cref="IConfiguration"/>.
    /// </summary>
    /// <param name="services">The DI services collection to add to.</param>
    /// <param name="configSectionPath">The configuration section path to bind, e.g. <c>"GoogleChat"</c>.</param>
    /// <param name="configureOptions">
    /// An optional callback that runs after binding, so values set in code override bound values.
    /// </param>
    /// <remarks>
    /// Options are validated when first resolved, and at host startup for hosted apps. Invalid options cause
    /// an <see cref="OptionsValidationException"/>.
    /// </remarks>
    public static IServiceCollection AddGoogleChatService(
        this IServiceCollection services,
        string configSectionPath,
        Action<GoogleChatServiceOptions>? configureOptions = null)
    {
        Check.ThrowIfNull(services);
        Check.ThrowIfNullOrWhiteSpace(configSectionPath);

        return AddGoogleChatServiceCore(
            services,
            bindConfiguration: optionsBuilder => optionsBuilder.BindConfiguration(configSectionPath),
            configureOptions);
    }


    //
    // Private methods
    //

    private static IServiceCollection AddGoogleChatServiceCore(
        IServiceCollection services,
        Action<OptionsBuilder<GoogleChatServiceOptions>>? bindConfiguration,
        Action<GoogleChatServiceOptions>? configureOptions)
    {
        var optionsBuilder = services.AddOptions<GoogleChatServiceOptions>();

        // Bind first, then apply the callback, so code overrides configuration.
        bindConfiguration?.Invoke(optionsBuilder);

        if (configureOptions is not null)
        {
            optionsBuilder.Configure(configureOptions);
        }

        optionsBuilder.ValidateOnStart();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<GoogleChatServiceOptions>, GoogleChatServiceOptionsValidator>());

        services.AddHttpClient<GoogleChatService>();

        return services;
    }
}
