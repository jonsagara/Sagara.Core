using Microsoft.Extensions.DependencyInjection;

namespace Sagara.Core.Google.Chat;

public static class GoogleChatServiceExtensions
{
    /// <summary>
    /// Registers a <see cref="GoogleChatService"/>. The webhook URL is supplied per call to one of
    /// <see cref="GoogleChatService"/>'s SendMessageAsync overloads, not at registration time, so
    /// a single registered instance can send to any number of Google Chat spaces.
    /// </summary>
    /// <param name="services">The DI services collection to add to.</param>
    /// <param name="configureOptions">
    /// An optional callback to configure <see cref="GoogleChatServiceOptions"/>. If not specified, the
    /// default options are used, which mention users by <see cref="GoogleChatMentionStyle.Id"/>.
    /// </param>
    public static IServiceCollection AddGoogleChatService(
        this IServiceCollection services,
        Action<GoogleChatServiceOptions>? configureOptions = null)
    {
        Check.ThrowIfNull(services);

        var options = new GoogleChatServiceOptions();
        configureOptions?.Invoke(options);

        services.AddSingleton(options);
        services.AddHttpClient<GoogleChatService>();

        return services;
    }
}
