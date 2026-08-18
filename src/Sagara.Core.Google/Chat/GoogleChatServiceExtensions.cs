using Microsoft.Extensions.DependencyInjection;

namespace Sagara.Core.Google.Chat;

public static class GoogleChatServiceExtensions
{
    /// <summary>
    /// Registers a <see cref="GoogleChatService"/> singleton that sends messages to the Google Chat space backed
    /// by <paramref name="webhookUrl"/>.
    /// </summary>
    /// <param name="services">The DI services collection to add to.</param>
    /// <param name="webhookUrl">The Google Chat incoming webhook URL to POST messages to.</param>
    public static IServiceCollection AddGoogleChatService(this IServiceCollection services, string webhookUrl)
    {
        Check.ThrowIfNull(services);
        Check.ThrowIfNullOrWhiteSpace(webhookUrl);

        // AddHttpClient<GoogleChatService>() can't be used directly: its factory resolves GoogleChatService's
        // constructor parameters from DI, and there's no clean way to also DI-inject a plain webhook URL string.
        // Registering the typed HttpClient separately and constructing GoogleChatService ourselves sidesteps that.
        services.AddHttpClient<GoogleChatService>();

        services.AddSingleton(serviceProvider =>
        {
            var httpClient = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(GoogleChatService));

            return new GoogleChatService(httpClient, webhookUrl);
        });

        return services;
    }
}
