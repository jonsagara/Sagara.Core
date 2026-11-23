using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Sagara.Core.Google.Tests.Chat;

internal sealed class RecordingLogger<T> : ILogger<T>
{
    public List<(LogLevel LogLevel, string Message)> Entries { get; } = [];

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        => Entries.Add((logLevel, formatter(state, exception)));
}

/// <summary>
/// Captures the last request and returns a canned response, or throws <paramref name="exceptionToThrow"/> if set.
/// </summary>
internal sealed class CapturingHttpMessageHandler(
    HttpStatusCode statusCode,
    string? responseBody = null,
    Exception? exceptionToThrow = null) : HttpMessageHandler
{
    private string? _requestBody;

    public HttpMethod? Method { get; private set; }

    public Uri? RequestUri { get; private set; }

    public AuthenticationHeaderValue? Authorization { get; private set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Method = request.Method;
        RequestUri = request.RequestUri;
        Authorization = request.Headers.Authorization;
        _requestBody = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        if (exceptionToThrow is not null)
        {
            throw exceptionToThrow;
        }

        var response = new HttpResponseMessage(statusCode);
        if (responseBody is not null)
        {
            response.Content = new StringContent(responseBody, Encoding.UTF8, "application/json");
        }

        return response;
    }

    public Task<JsonElement> GetRequestJsonAsync()
    {
        Assert.NotNull(_requestBody);

        using var document = JsonDocument.Parse(_requestBody);
        return Task.FromResult(document.RootElement.Clone());
    }
}
