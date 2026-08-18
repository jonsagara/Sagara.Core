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
}
