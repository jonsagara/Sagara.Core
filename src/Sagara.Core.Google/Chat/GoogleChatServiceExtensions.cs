using Microsoft.Extensions.DependencyInjection;

namespace Sagara.Core.Google.Chat;

public static class GoogleChatServiceExtensions
{
    /// <summary>
    /// Registers a <see cref="GoogleChatService"/>. The webhook URL is supplied per call to
    /// <see cref="GoogleChatService.SendMessageAsync"/>, not at registration time, so a single registered instance
    /// can send to any number of Google Chat spaces.
    /// </summary>
    /// <param name="services">The DI services collection to add to.</param>
    public static IServiceCollection AddGoogleChatService(this IServiceCollection services)
    {
        Check.ThrowIfNull(services);

        services.AddHttpClient<GoogleChatService>();

        return services;
    }
}
