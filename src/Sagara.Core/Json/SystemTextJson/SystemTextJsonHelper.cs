using System.Text.Json;

namespace Sagara.Core.Json.SystemTextJson;

/// <summary>
/// Helper methods for serializing JSON with System.Text.Json.
/// </summary>
public static class SystemTextJsonHelper
{
    private static JsonSerializerOptions _serializeCamelCasePrettyPrint = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private static JsonSerializerOptions _serializeCamelCaseMinify = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private static JsonSerializerOptions _serializePascalCasePrettyPrint = new()
    {
        WriteIndented = true,
    };

    private static JsonSerializerOptions _serializePascalCaseMinify = new()
    {
        WriteIndented = false,
    };


    /// <summary>
    /// Convert the value into a JSON string. Default is camelCase property names and minified output.
    /// </summary>
    /// <param name="value">The value to convert to JSON.</param>
    /// <param name="camelCase">True to use camelCase property naming; false to use PascalCase. Default is true.</param>
    /// <param name="prettyPrint">True to pretty print the JSON; false to minify it. Default is false.</param>
    /// <returns>The serialized JSON string.</returns>
    public static string Serialize<TValue>(TValue value, bool camelCase = true, bool prettyPrint = false)
    {
        if (camelCase)
        {
            return prettyPrint
                ? JsonSerializer.Serialize(value, _serializeCamelCasePrettyPrint)
                : JsonSerializer.Serialize(value, _serializeCamelCaseMinify);
        }

        return prettyPrint
            ? JsonSerializer.Serialize(value, _serializePascalCasePrettyPrint)
            : JsonSerializer.Serialize(value, _serializePascalCaseMinify);
    }


    private static JsonSerializerOptions _deserializeCaseInsensitive = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private static JsonSerializerOptions _deserializeCaseSensitive = new()
    {
        PropertyNameCaseInsensitive = false,
    };

    /// <summary>
    /// Parse the JSON string into into an instance of the type specified by a generic type parameter. Default
    /// is to use case-insensitive property names.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="caseInsensitivePropertyNames">True to use case-insensitive property names; false to use
    /// case-sensitive property names. Default is true.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? Deserialize<T>(string json, bool caseInsensitivePropertyNames = true)
    {
        Check.ThrowIfNull(json);

        return caseInsensitivePropertyNames
            ? JsonSerializer.Deserialize<T>(json, _deserializeCaseInsensitive)
            : JsonSerializer.Deserialize<T>(json, _deserializeCaseSensitive);
    }
}
