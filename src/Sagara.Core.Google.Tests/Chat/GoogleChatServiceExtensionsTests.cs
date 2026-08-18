using Microsoft.Extensions.DependencyInjection;
using Sagara.Core.Google.Chat;

namespace Sagara.Core.Google.Tests.Chat;

public class GoogleChatServiceExtensionsTests
{
    [Fact]
    public void AddGoogleChatService_NullWebhookUrl_Throws()
    {
        var services = new ServiceCollection();

        Assert.Throws<ArgumentNullException>(() => services.AddGoogleChatService(null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void AddGoogleChatService_WhiteSpaceWebhookUrl_Throws(string webhookUrl)
    {
        var services = new ServiceCollection();

        Assert.Throws<ArgumentException>(() => services.AddGoogleChatService(webhookUrl));
    }

    [Fact]
    public void AddGoogleChatService_ValidWebhookUrl_ResolvesGoogleChatService()
    {
        var services = new ServiceCollection();
        services.AddGoogleChatService("https://chat.googleapis.com/v1/spaces/x/messages?key=y&token=z");

        using var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<GoogleChatService>();

        Assert.NotNull(service);
    }
}
