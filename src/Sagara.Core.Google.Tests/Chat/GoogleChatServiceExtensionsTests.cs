using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
    public void AddGoogleChatService_NoConfigureOptions_UsesDefaults()
    {
        var options = ResolveOptions(services => services.AddGoogleChatService());

        Assert.Equal(GoogleChatMentionStyle.Id, options.MentionStyle);
        Assert.True(options.ConvertBodyToClassicMarkup);
        Assert.Null(options.Relay);
    }

    [Fact]
    public void AddGoogleChatService_ConfigureOptions_AppliesConfiguredValues()
    {
        var options = ResolveOptions(services => services.AddGoogleChatService(o =>
        {
            o.MentionStyle = GoogleChatMentionStyle.Email;
            o.ConvertBodyToClassicMarkup = false;
            o.Relay = new GoogleChatRelayOptions { BaseAddress = new Uri("https://relay.example.com"), ApiToken = "token" };
        }));

        Assert.Equal(GoogleChatMentionStyle.Email, options.MentionStyle);
        Assert.False(options.ConvertBodyToClassicMarkup);
        Assert.NotNull(options.Relay);
        Assert.Equal(new Uri("https://relay.example.com"), options.Relay.BaseAddress);
        Assert.Equal("token", options.Relay.ApiToken);
    }

    [Fact]
    public void AddGoogleChatService_ConfigurationSection_BindsAllOptions()
    {
        var configuration = BuildConfiguration(new()
        {
            ["GoogleChat:MentionStyle"] = "Email",
            ["GoogleChat:ConvertBodyToClassicMarkup"] = "false",
            ["GoogleChat:Relay:BaseAddress"] = "https://relay.example.com",
            ["GoogleChat:Relay:ApiToken"] = "token-from-config",
        });

        var options = ResolveOptions(services => services.AddGoogleChatService(configuration.GetSection("GoogleChat")));

        Assert.Equal(GoogleChatMentionStyle.Email, options.MentionStyle);
        Assert.False(options.ConvertBodyToClassicMarkup);
        Assert.NotNull(options.Relay);
        Assert.Equal(new Uri("https://relay.example.com"), options.Relay.BaseAddress);
        Assert.Equal("token-from-config", options.Relay.ApiToken);
    }

    [Fact]
    public void AddGoogleChatService_ConfigurationSectionWithoutRelay_LeavesRelayNull()
    {
        var configuration = BuildConfiguration(new() { ["GoogleChat:MentionStyle"] = "Email" });

        var options = ResolveOptions(services => services.AddGoogleChatService(configuration.GetSection("GoogleChat")));

        Assert.Null(options.Relay);
    }

    [Fact]
    public void AddGoogleChatService_ConfigurationSectionAndCallback_CallbackOverridesBoundValues()
    {
        var configuration = BuildConfiguration(new()
        {
            ["GoogleChat:MentionStyle"] = "Email",
            ["GoogleChat:Relay:BaseAddress"] = "https://relay.example.com",
        });

        // The token is supplied in code, e.g. from a secret store.
        var options = ResolveOptions(services => services.AddGoogleChatService(
            configuration.GetSection("GoogleChat"),
            o =>
            {
                o.MentionStyle = GoogleChatMentionStyle.Id;
                o.Relay!.ApiToken = "token-from-code";
            }));

        Assert.Equal(GoogleChatMentionStyle.Id, options.MentionStyle);
        Assert.Equal("token-from-code", options.Relay!.ApiToken);
    }

    [Fact]
    public void AddGoogleChatService_ConfigSectionPath_BindsFromRegisteredConfiguration()
    {
        var configuration = BuildConfiguration(new()
        {
            ["Notifications:GoogleChat:Relay:BaseAddress"] = "https://relay.example.com",
            ["Notifications:GoogleChat:Relay:ApiToken"] = "token-from-path",
        });

        var options = ResolveOptions(services =>
        {
            services.AddSingleton(configuration);
            services.AddGoogleChatService("Notifications:GoogleChat");
        });

        Assert.Equal("token-from-path", options.Relay!.ApiToken);
    }

    [Fact]
    public void AddGoogleChatService_NullConfigurationSection_Throws()
    {
        var services = new ServiceCollection();

        Assert.Throws<ArgumentNullException>(() => services.AddGoogleChatService((IConfiguration)null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void AddGoogleChatService_BlankConfigSectionPath_Throws(string configSectionPath)
    {
        var services = new ServiceCollection();

        Assert.Throws<ArgumentException>(() => services.AddGoogleChatService(configSectionPath));
    }

    [Fact]
    public void AddGoogleChatService_CalledTwice_RegistersValidatorOnce()
    {
        var services = new ServiceCollection();
        services.AddGoogleChatService();
        services.AddGoogleChatService();

        using var provider = services.BuildServiceProvider();

        Assert.Single(provider.GetServices<IValidateOptions<GoogleChatServiceOptions>>());
    }

    [Fact]
    public void AddGoogleChatService_RelayWithoutBaseAddress_FailsValidation()
    {
        var ex = AssertValidationFails(o => o.Relay = new GoogleChatRelayOptions { ApiToken = "token" });

        Assert.Contains("Relay.BaseAddress is required", ex.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("http://relay.example.com")]
    [InlineData("/relative")]
    public void AddGoogleChatService_RelayWithNonHttpsOrRelativeBaseAddress_FailsValidation(string baseAddress)
    {
        var ex = AssertValidationFails(o => o.Relay = new GoogleChatRelayOptions
        {
            BaseAddress = new Uri(baseAddress, UriKind.RelativeOrAbsolute),
            ApiToken = "token",
        });

        Assert.Contains("Relay.BaseAddress must be an absolute https URI", ex.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AddGoogleChatService_RelayWithoutApiToken_FailsValidation(string? apiToken)
    {
        var ex = AssertValidationFails(o => o.Relay = new GoogleChatRelayOptions
        {
            BaseAddress = new Uri("https://relay.example.com"),
            ApiToken = apiToken,
        });

        Assert.Contains("Relay.ApiToken is required", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AddGoogleChatService_RelayEmpty_ReportsAllFailures()
    {
        var ex = AssertValidationFails(o => o.Relay = new GoogleChatRelayOptions());

        Assert.Equal(2, ex.Failures.Count());
    }

    [Fact]
    public void AddGoogleChatService_UndefinedMentionStyle_FailsValidation()
    {
        var ex = AssertValidationFails(o => o.MentionStyle = (GoogleChatMentionStyle)999);

        Assert.Contains("MentionStyle", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AddGoogleChatService_InvalidOptions_ResolvingServiceThrows()
    {
        var services = new ServiceCollection();
        services.AddGoogleChatService(o => o.Relay = new GoogleChatRelayOptions());

        using var provider = services.BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(() => provider.GetRequiredService<GoogleChatService>());
    }


    //
    // Helpers
    //

    private static IConfiguration BuildConfiguration(Dictionary<string, string?> values)
        => new ConfigurationBuilder().AddInMemoryCollection(values).Build();

    private static GoogleChatServiceOptions ResolveOptions(Action<IServiceCollection> register)
    {
        var services = new ServiceCollection();
        register(services);

        using var provider = services.BuildServiceProvider();
        return provider.GetRequiredService<IOptions<GoogleChatServiceOptions>>().Value;
    }

    private static OptionsValidationException AssertValidationFails(Action<GoogleChatServiceOptions> configureOptions)
        => Assert.Throws<OptionsValidationException>(() => ResolveOptions(services => services.AddGoogleChatService(configureOptions)));
}
