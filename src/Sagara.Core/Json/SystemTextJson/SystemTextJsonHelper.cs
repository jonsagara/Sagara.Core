﻿using System.Text.Json;

namespace Sagara.Core.Json.SystemTextJson;

/// <summary>
/// Helper methods for serializing JSON with System.Text.Json.
/// </summary>
[Obsolete("In v5.0.0, the class name will be shortened to STJsonHelper.")]
public static class SystemTextJsonHelper
{
    /// <summary>
    /// Convert the value into a JSON string. Default is camelCase property names and minified output.
    /// </summary>
    /// <param name="value">The value to convert to JSON.</param>
    /// <param name="camelCase">True to use camelCase property naming; false to use PascalCase. Default is true.</param>
    /// <param name="prettyPrint">True to pretty print the JSON; false to minify it. Default is false.</param>
    /// <returns>The serialized JSON string.</returns>
    public static string Serialize<TValue>(TValue value, bool camelCase = true, bool prettyPrint = false)
        => STJsonHelper.Serialize(value, camelCase: camelCase, prettyPrint: prettyPrint);

    /// <summary>
    /// Asynchronously convert the provided value to UTF-8 encoded JSON text and write it to the <see cref="Stream"/>.
    /// </summary>
    /// <param name="utf8Json">The UTF-8 <see cref="Stream"/> to write to.</param>
    /// <param name="value">The value to convert to JSON.</param>
    /// <param name="camelCase">True to use camelCase property naming; false to use PascalCase. Default is true.</param>
    /// <param name="prettyPrint">True to pretty print the JSON; false to minify it. Default is false.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> that can be used to cancel the write operation.</param>
    public static async Task SerializeAsync<TValue>(Stream utf8Json, TValue value, bool camelCase = true, bool prettyPrint = false, CancellationToken cancellationToken = default)
        => await STJsonHelper.SerializeAsync(utf8Json, value, camelCase: camelCase, prettyPrint: prettyPrint, cancellationToken: cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Convert the value into a JSON string using the settings passed in by the caller.
    /// </summary>
    /// <remarks>
    /// This class's available configurations don't work for all cases. Allow the caller to completely customize
    /// the serialization behavior.
    /// </remarks>
    /// <param name="value">The value to convert to JSON.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> explicitly configured by the caller.</param>
    /// <returns>The serialized JSON string.</returns>
    public static string Serialize<TValue>(TValue value, JsonSerializerOptions options)
        => STJsonHelper.Serialize(value, options);

    /// <summary>
    /// Asynchronously convert the value into a JSON string using the settings passed in by the caller.
    /// </summary>
    /// <remarks>
    /// This class's available configurations don't work for all cases. Allow the caller to completely customize
    /// the serialization behavior.
    /// </remarks>
    /// <param name="utf8Json">The UTF-8 <see cref="Stream"/> to write to.</param>
    /// <param name="value">The value to convert to JSON.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> explicitly configured by the caller.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> that can be used to cancel the write operation.</param>
    public static async Task SerializeAsync<TValue>(Stream utf8Json, TValue value, JsonSerializerOptions options, CancellationToken cancellationToken = default)
        => await STJsonHelper.SerializeAsync(utf8Json, value, options, cancellationToken).ConfigureAwait(false);


    /// <summary>
    /// Parse the JSON string into into an instance of the type specified by a generic type parameter. Default
    /// is to use case-insensitive property names.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="caseInsensitivePropertyNames">True to use case-insensitive property names; false to use
    /// case-sensitive property names. Default is true.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? Deserialize<T>(string json, bool caseInsensitivePropertyNames = true)
        => STJsonHelper.Deserialize<T>(json, caseInsensitivePropertyNames);

    /// <summary>
    /// Parse the JSON string into into an instance of the type specified by a generic type parameter. Default
    /// is to use case-insensitive property names.
    /// </summary>
    /// <remarks>
    /// This class's available configurations don't work for all cases. Allow the caller to completely customize
    /// the deserialization behavior.
    /// </remarks>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> explicitly configured by the caller.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? Deserialize<T>(string json, JsonSerializerOptions options)
        => STJsonHelper.Deserialize<T>(json, options);

    /// <summary>
    /// Asynchronously reads the UTF-8 encoded text representing a single JSON value into a <typeparamref name="T"/>.
    /// The Stream will be read to completion.
    /// </summary>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="caseInsensitivePropertyNames">True to use case-insensitive property names; false to use
    /// case-sensitive property names. Default is true.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> that can be used to cancel the read operation.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static async ValueTask<T?> DeserializeAsync<T>(Stream utf8Json, bool caseInsensitivePropertyNames = true, CancellationToken cancellationToken = default)
        => await STJsonHelper.DeserializeAsync<T>(utf8Json, caseInsensitivePropertyNames, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously reads the UTF-8 encoded text representing a single JSON value into a <typeparamref name="T"/>.
    /// The Stream will be read to completion.
    /// </summary>
    /// <param name="utf8Json">JSON data to parse.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> explicitly configured by the caller.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> that can be used to cancel the read operation.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static async ValueTask<T?> DeserializeAsync<T>(Stream utf8Json, JsonSerializerOptions options, CancellationToken cancellationToken = default)
        => await STJsonHelper.DeserializeAsync<T>(utf8Json, options, cancellationToken).ConfigureAwait(false);
}
