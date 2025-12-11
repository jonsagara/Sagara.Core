using System.Buffers;
using System.Buffers.Text;
using System.Globalization;
using System.Security.Cryptography;

namespace Sagara.Core;

public static class RandomString
{
    // These are internal so that we can see them in unit tests.
    internal const string AlphabetLower = "abcdefghijklmnopqrstuvwxyz";
    internal const string Digits = "0123456789";
    internal const string Symbols = "`~!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?";

    private static readonly char[] _alphanumeric = [
        .. AlphabetLower,
        .. AlphabetLower.ToUpper(CultureInfo.InvariantCulture),
        .. Digits,
        ];

    private static readonly char[] _alphanumericPlusDashUnderscore = [
        .. _alphanumeric,
        '-',
        '_',
        ];

    // Symbols includes dash and underscore.
    internal static readonly char[] _alphanumericPlusSymbols = [
        .. _alphanumeric,
        .. Symbols,
        ];

    private static readonly char[] _lowercaseAlphanumeric = [
        .. AlphabetLower,
        .. Digits
        ];

    private static readonly char[] _lowercaseAlphanumericPlusDashUnderscore = [
        .. AlphabetLower,
        .. Digits,
        '-',
        '_',
        ];

    private static readonly char[] _uppercaseAlphanumeric = [
        .. AlphabetLower.ToUpper(CultureInfo.InvariantCulture),
        .. Digits
        ];

    private static readonly char[] _uppercaseAlphanumericPlusDashUnderscore = [
        .. AlphabetLower.ToUpper(CultureInfo.InvariantCulture),
        .. Digits,
        '-',
        '_',
        ];


    /// <summary>
    /// Generate a cryptographically-strong array of random bytes and return them encoded as a string that
    /// can contain the characters in [a-zA-Z0-9-_], as well as the various symbol characters.
    /// </summary>
    /// <param name="length">The length of the random string to generate.</param>
    /// <returns>The random bytes encoded as a string that can contain the characters in [a-zA-Z0-9-_], as 
    /// well as the various symbol characters.</returns>
    public static string Generate(int length)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(length, 0);

        return RandomNumberGenerator.GetString(_alphanumericPlusSymbols, length);
    }

    /// <summary>
    /// Generate a cryptographically-strong array of random bytes and return them encoded as a string that
    /// can contain the characters in [a-zA-Z0-9]. Optionally include dash ('-') and underscore ('_') as possible characters.
    /// </summary>
    /// <param name="length">The length of the random string to generate.</param>
    /// <param name="includeDashAndUnderscore">If true, include both dash ('-') and underscore ('_') as possible characters in the resulting string; otherwise, omit them. Default is false.</param>
    /// <returns>The random bytes encoded as a string that can contain the characters in [a-zA-Z0-9-_].</returns>
    public static string GenerateAlphanumeric(int length, bool includeDashAndUnderscore = false)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(length, 0);

        return includeDashAndUnderscore
            ? RandomNumberGenerator.GetString(_alphanumericPlusDashUnderscore, length)
            : RandomNumberGenerator.GetString(_alphanumeric, length);
    }

    /// <summary>
    /// Generates a cryptographically-strong array of random bytes and return them encoded as a string that
    /// can contain the characters in [a-z0-9]. Optionally include dash ('-') and underscore ('_') as possible characters.
    /// </summary>
    /// <param name="length">The length of the random string to generate.</param>
    /// <param name="includeDashAndUnderscore">If true, include both dash ('-') and underscore ('_') as possible characters in the resulting string; otherwise, omit them. Default is false.</param>
    /// <returns>The random bytes encoded as a string that can contain the characters in [a-z0-9].</returns>
    public static string GenerateLowercaseAlphanumeric(int length, bool includeDashAndUnderscore = false)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(length, 0);

        return includeDashAndUnderscore
            ? RandomNumberGenerator.GetString(_lowercaseAlphanumericPlusDashUnderscore, length)
            : RandomNumberGenerator.GetString(_lowercaseAlphanumeric, length);
    }

    /// <summary>
    /// Generates a cryptographically-strong array of random bytes and return them encoded as a string that
    /// can contain the characters in [A-Z0-9]. Optionally include dash ('-') and underscore ('_') as possible characters.
    /// </summary>
    /// <param name="length">The length of the random string to generate.</param>
    /// <param name="includeDashAndUnderscore">If true, include both dash ('-') and underscore ('_') as possible characters in the resulting string; otherwise, omit them. Default is false.</param>
    /// <returns>The random bytes encoded as a string that can contain the characters in [A-Z0-9].</returns>
    public static string GenerateUppercaseAlphanumeric(int length, bool includeDashAndUnderscore = false)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(length, 0);

        return includeDashAndUnderscore
            ? RandomNumberGenerator.GetString(_uppercaseAlphanumericPlusDashUnderscore, length)
            : RandomNumberGenerator.GetString(_uppercaseAlphanumeric, length);
    }

    /// <summary>
    /// Generate a cryptographically-strong array of random bytes and return them as a base64-encoded string. 
    /// </summary>
    /// <remarks>The underlying <see cref="Convert.ToBase64String(ReadOnlySpan{byte}, Base64FormattingOptions)"/> leaves
    /// any padding intact.</remarks>
    /// <param name="byteCount">The number of random bytes to generate.</param>
    /// <returns>The random bytes as a base64-encoded string.</returns>
    public static string GenerateBase64Encoded(int byteCount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(byteCount, 0);

        var randomStringBytes = ArrayPool<byte>.Shared.Rent(byteCount);
        try
        {
            GenerateRandomBytes(randomStringBytes, byteCount);

            return Convert.ToBase64String(randomStringBytes.AsSpan().Slice(0, byteCount));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(randomStringBytes, clearArray: true);
        }
    }

    /// <summary>
    /// Generates a cryptographically-strong array of random bytes and return them as a base64 url-encoded string. 
    /// </summary>
    /// <remarks>The underlying <see cref="Base64Url.EncodeToString(ReadOnlySpan{byte})"/> omits the optional padding characters.</remarks>
    /// <param name="byteCount">The number of random bytes to generate.</param>
    /// <returns>The random bytes as a base64 url-encoded string.</returns>
    // Justification: It's a base64 URL-encoded string, not a URI.
#pragma warning disable CA1055 // URI-like return values should not be strings
    public static string GenerateBase64UrlEncoded(int byteCount)
#pragma warning restore CA1055 // URI-like return values should not be strings
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(byteCount, 0);

        var randomStringBytes = ArrayPool<byte>.Shared.Rent(byteCount);
        try
        {
            GenerateRandomBytes(randomStringBytes, byteCount);

            return Base64Url.EncodeToString(randomStringBytes.AsSpan().Slice(0, byteCount));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(randomStringBytes, clearArray: true);
        }
    }


    //
    // Private methods
    //

    /// <summary>
    /// Fills a span with cryptographically strong random bytes.
    /// </summary>
    private static void GenerateRandomBytes(Span<byte> buffer, int length)
    {
        // A pooled array is likely larger than the requested size. Only generate the requested number
        //   of bytes.
        RandomNumberGenerator.Fill(buffer.Slice(0, length));
    }
}
