using Microsoft.Extensions.DependencyInjection;
using Sagara.Core.Google.Chat;

namespace Sagara.Core.Google.Tests.Chat;

public class GoogleChatServiceExtensionsTests
{
    [Fact]
    public void AddGoogleChatService_NullServices_Throws()
    {
        IServiceCollection services = null!;

        Assert.Throws<ArgumentNullException>(() => services.AddGoogleChatService());
    }

    [Fact]
    public void AddGoogleChatService_ResolvesGoogleChatService()
    {
        var services = new ServiceCollection();
        services.AddGoogleChatService();

        using var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<GoogleChatService>();

        Assert.NotNull(service);
    }

    [Fact]
    public void AddGoogleChatService_NoConfigureOptions_DefaultsToMentionById()
    {
        var services = new ServiceCollection();
        services.AddGoogleChatService();

        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<GoogleChatServiceOptions>();

        Assert.Equal(GoogleChatMentionStyle.Id, options.MentionStyle);
    }

    [Fact]
    public void AddGoogleChatService_NoConfigureOptions_DefaultsToConvertBodyToClassicMarkup()
    {
        var services = new ServiceCollection();
        services.AddGoogleChatService();

        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<GoogleChatServiceOptions>();

        Assert.True(options.ConvertBodyToClassicMarkup);
    }

    [Fact]
    public void AddGoogleChatService_ConfigureOptions_AppliesConfiguredConvertBodyToClassicMarkup()
    {
        var services = new ServiceCollection();
        services.AddGoogleChatService(options => options.ConvertBodyToClassicMarkup = false);

        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<GoogleChatServiceOptions>();

        Assert.False(options.ConvertBodyToClassicMarkup);
    }

    [Fact]
    public void AddGoogleChatService_ConfigureOptions_AppliesConfiguredMentionStyle()
    {
        var services = new ServiceCollection();
        services.AddGoogleChatService(options => options.MentionStyle = GoogleChatMentionStyle.Email);

        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<GoogleChatServiceOptions>();

        Assert.Equal(GoogleChatMentionStyle.Email, options.MentionStyle);
    }
}
