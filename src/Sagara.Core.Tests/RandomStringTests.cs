using System.Buffers.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Sagara.Core.Tests;

public class RandomStringTests
{
    private const string Base64UrlAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
    private const string Base64StandardAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    //
    // GenerateRandomBase64UrlEncodedString
    //

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-128)]
    public void GenerateRandomBase64UrlEncodedString_WithNonPositiveByteCount_Throws(int byteCount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => RandomString.GenerateRandomBase64UrlEncodedString(byteCount));
    }

    [Theory]
    [InlineData(16)]
    [InlineData(32)]
    [InlineData(64)]
    public void GenerateRandomBase64UrlEncodedString_ReturnsNonEmptyString_ForValidByteCount(int byteCount)
    {
        // Act
        var result = RandomString.GenerateRandomBase64UrlEncodedString(byteCount);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GenerateRandomBase64UrlEncodedString_ReturnsValidBase64UrlString()
    {
        // Arrange
        const int byteCount = 32;

        // Act
        var result = RandomString.GenerateRandomBase64UrlEncodedString(byteCount);

        // Assert - Base64 URL encoding uses [A-Za-z0-9_-] without padding
        Assert.Matches("^[A-Za-z0-9_-]+$", result);
    }

    [Fact]
    public void GenerateRandomBase64UrlEncodedString_GeneratesDifferentStrings_OnMultipleCalls()
    {
        // Arrange
        const int byteCount = 32;

        // Act
        var result1 = RandomString.GenerateRandomBase64UrlEncodedString(byteCount);
        var result2 = RandomString.GenerateRandomBase64UrlEncodedString(byteCount);
        var result3 = RandomString.GenerateRandomBase64UrlEncodedString(byteCount);

        // Assert
        Assert.NotEqual(result1, result2);
        Assert.NotEqual(result2, result3);
        Assert.NotEqual(result1, result3);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(32)]
    public void GenerateRandomBase64UrlEncodedString_ProducesExpectedLengthAndAllowedCharacters(int byteCount)
    {
        var result = RandomString.GenerateRandomBase64UrlEncodedString(byteCount);

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        // Length = ceil(byteCount * 4 / 3), no padding for Base64Url
        var expectedLength = (int)Math.Ceiling(byteCount * 4.0 / 3.0);
        Assert.Equal(expectedLength, result.Length);

        // Base64Url should not contain '=' padding
        Assert.DoesNotContain('=', result);

        // Only characters from Base64Url alphabet
        var allowedChars = new HashSet<char>(Base64UrlAlphabet);
        foreach (var c in result)
        {
            Assert.True(allowedChars.Contains(c), $"Character '{c}' is not valid Base64Url.");
        }
    }


    //
    // GenerateRandomBase64EncodedString
    //

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-128)]
    public void GenerateRandomBase64EncodedString_WithNonPositiveByteCount_Throws(int byteCount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => RandomString.GenerateRandomBase64EncodedString(byteCount));
    }

    [Theory]
    [InlineData(16)]
    [InlineData(32)]
    [InlineData(64)]
    public void GenerateRandomBase64EncodedString_ReturnsNonEmptyString_ForValidByteCount(int byteCount)
    {
        // Act
        var result = RandomString.GenerateRandomBase64EncodedString(byteCount);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GenerateRandomBase64EncodedString_ReturnsValidBase64String()
    {
        // Arrange
        const int byteCount = 32;

        // Act
        var result = RandomString.GenerateRandomBase64EncodedString(byteCount);

        // Assert - Standard Base64 uses [A-Za-z0-9+/] with optional = padding
        Assert.Matches("^[A-Za-z0-9+/]+=*$", result);
    }

    [Fact]
    public void GenerateRandomBase64EncodedString_GeneratesDifferentStrings_OnMultipleCalls()
    {
        // Arrange
        const int byteCount = 32;

        // Act
        var result1 = RandomString.GenerateRandomBase64EncodedString(byteCount);
        var result2 = RandomString.GenerateRandomBase64EncodedString(byteCount);
        var result3 = RandomString.GenerateRandomBase64EncodedString(byteCount);

        // Assert
        Assert.NotEqual(result1, result2);
        Assert.NotEqual(result2, result3);
        Assert.NotEqual(result1, result3);
    }

    [Theory]
    [InlineData(15)] // Results in padding
    [InlineData(30)] // Results in padding
    public void GenerateRandomBase64EncodedString_IncludesPadding_WhenAppropriate(int byteCount)
    {
        // Act
        var result = RandomString.GenerateRandomBase64EncodedString(byteCount);

        // Assert
        // Try to decode it - should succeed if valid base64
        try
        {
            Convert.FromBase64String(result);
            Assert.True(true); // Decoding succeeded
        }
        catch
        {
            Assert.Fail("Generated string is not valid Base64");
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(32)]
    public void GenerateRandomBase64EncodedString_ProducesExpectedLengthAndAllowedCharacters(int byteCount)
    {
        var result = RandomString.GenerateRandomBase64EncodedString(byteCount);

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        // Base64: 4 chars per 3 bytes, with padding
        var expectedLength = (int)Math.Ceiling(byteCount / 3.0) * 4;
        Assert.Equal(expectedLength, result.Length);

        // Standard Base64 length is always a multiple of 4
        Assert.Equal(0, result.Length % 4);

        // Only characters from standard Base64 alphabet
        var allowedChars = new HashSet<char>(Base64StandardAlphabet);
        foreach (var c in result)
        {
            Assert.True(allowedChars.Contains(c), $"Character '{c}' is not valid Base64.");
        }

        // Also ensure it is decodable as Base64
        var decodedBytes = Convert.FromBase64String(result);
        Assert.Equal(byteCount, decodedBytes.Length);
    }


    //
    // GenerateUppercaseAlphanumericString
    //

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void GenerateUppercaseAlphanumericString_WithNonPositiveLength_Throws(int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => RandomString.GenerateUppercaseAlphanumericString(length));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(8)]
    [InlineData(10)]
    [InlineData(32)]
    [InlineData(50)]
    [InlineData(100)]
    public void GenerateUppercaseAlphanumericString_ProducesUppercaseLettersAndDigits(int length)
    {
        var result = RandomString.GenerateUppercaseAlphanumericString(length);

        Assert.NotNull(result);
        Assert.Equal(length, result.Length);

        foreach (var c in result)
        {
            Assert.True(char.IsUpper(c) || char.IsDigit(c), $"Character '{c}' is not upper-case alphanumeric.");
        }
    }

    [Fact]
    public void GenerateUppercaseAlphanumericString_GeneratesDifferentStrings_OnMultipleCalls()
    {
        // Arrange
        const int length = 50;

        // Act
        var result1 = RandomString.GenerateUppercaseAlphanumericString(length);
        var result2 = RandomString.GenerateUppercaseAlphanumericString(length);
        var result3 = RandomString.GenerateUppercaseAlphanumericString(length);

        // Assert
        Assert.NotEqual(result1, result2);
        Assert.NotEqual(result2, result3);
        Assert.NotEqual(result1, result3);
    }

    [Fact]
    public void GenerateUppercaseAlphanumericString_HasGoodDistribution()
    {
        // Arrange
        const int length = 1000;

        // Act
        var result = RandomString.GenerateUppercaseAlphanumericString(length);

        // Assert - Check that both letters and digits appear
        Assert.Matches("[A-Z]", result);
        Assert.Matches("[0-9]", result);
    }


    //
    // GenerateAlphanumericString
    //

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void GenerateAlphanumericString_WithNonPositiveLength_Throws(int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => RandomString.GenerateAlphanumericString(length));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(16)]
    [InlineData(64)]
    public void GenerateAlphanumericString_WithoutDashAndUnderscore_ContainsOnlyLettersAndDigits(int length)
    {
        var result = RandomString.GenerateAlphanumericString(length, includeDashAndUnderscore: false);

        Assert.NotNull(result);
        Assert.Equal(length, result.Length);

        foreach (var c in result)
        {
            Assert.True(char.IsLetterOrDigit(c), $"Character '{c}' is not a letter or digit.");
            Assert.NotEqual('-', c);
            Assert.NotEqual('_', c);
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(16)]
    [InlineData(64)]
    public void GenerateAlphanumericString_WithDashAndUnderscore_ContainsOnlyLettersDigitsDashUnderscore(int length)
    {
        var result = RandomString.GenerateAlphanumericString(length, includeDashAndUnderscore: true);

        Assert.NotNull(result);
        Assert.Equal(length, result.Length);

        foreach (var c in result)
        {
            Assert.True(char.IsLetterOrDigit(c) || c == '-' || c == '_', $"Character '{c}' is not a letter, digit, '-' or '_'.");
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public void GenerateAlphanumericString_ReturnsCorrectLength(int length)
    {
        // Act
        var result = RandomString.GenerateAlphanumericString(length);

        // Assert
        Assert.Equal(length, result.Length);
    }

    [Fact]
    public void GenerateAlphanumericString_WithDashAndUnderscore_CanIncludeDashAndUnderscore()
    {
        // Arrange - Generate many strings to ensure we get at least one dash/underscore
        const int length = 50;
        var hasDash = false;
        var hasUnderscore = false;

        // Act - Try up to 100 times
        for (int i = 0; i < 100; i++)
        {
            var result = RandomString.GenerateAlphanumericString(length, includeDashAndUnderscore: true);
            if (result.Contains('-', StringComparison.Ordinal))
            {
                hasDash = true;
            }

            if (result.Contains('_', StringComparison.Ordinal))
            {
                hasUnderscore = true;
            }

            if (hasDash && hasUnderscore)
            {
                break;
            }
        }

        // Assert - With enough attempts, we should see both characters
        Assert.True(hasDash || hasUnderscore, "Expected to see at least dash or underscore in generated strings");
    }

    [Fact]
    public void GenerateAlphanumericString_GeneratesDifferentStrings_OnMultipleCalls()
    {
        // Arrange
        const int length = 50;

        // Act
        var result1 = RandomString.GenerateAlphanumericString(length);
        var result2 = RandomString.GenerateAlphanumericString(length);
        var result3 = RandomString.GenerateAlphanumericString(length);

        // Assert
        Assert.NotEqual(result1, result2);
        Assert.NotEqual(result2, result3);
        Assert.NotEqual(result1, result3);
    }

    [Fact]
    public void GenerateAlphanumericString_ContainsBothUpperAndLowerCase()
    {
        // Arrange
        const int length = 100;

        // Act
        var result = RandomString.GenerateAlphanumericString(length, includeDashAndUnderscore: false);

        // Assert - With 100 characters, we should see both cases
        Assert.Matches("[a-z]", result);
        Assert.Matches("[A-Z]", result);
    }


    //
    // GenerateRandomString
    //

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void GenerateRandomString_WithNonPositiveLength_Throws(int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => RandomString.GenerateRandomString(length));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(16)]
    [InlineData(50)]
    [InlineData(64)]
    [InlineData(100)]
    public void GenerateRandomString_UsesInternalAlphanumericPlusSymbolsAlphabet(int length)
    {
        var result = RandomString.GenerateRandomString(length);

        Assert.NotNull(result);
        Assert.Equal(length, result.Length);

        // _alphanumericPlusSymbols is internal, so visible to tests
        var allowedChars = new HashSet<char>(RandomString._alphanumericPlusSymbols);
        foreach (var c in result)
        {
            Assert.True(allowedChars.Contains(c), $"Character '{c}' is not in _alphanumericPlusSymbols.");
        }
    }

    [Fact]
    public void GenerateRandomString_GeneratesDifferentStrings_OnMultipleCalls()
    {
        // Arrange
        const int length = 50;

        // Act
        var result1 = RandomString.GenerateRandomString(length);
        var result2 = RandomString.GenerateRandomString(length);
        var result3 = RandomString.GenerateRandomString(length);

        // Assert
        Assert.NotEqual(result1, result2);
        Assert.NotEqual(result2, result3);
        Assert.NotEqual(result1, result3);
    }

    [Fact]
    public void GenerateRandomString_CanIncludeSymbols()
    {
        // Arrange - Generate many strings to ensure we get at least one symbol
        const int length = 50;
        var hasSymbol = false;
        var symbolPattern = new Regex(@"[`~!@#$%^&*()\-_=+\[{\]}\\|;:'"",<.>/?]");

        // Act - Try up to 100 times
        for (int i = 0; i < 100; i++)
        {
            var result = RandomString.GenerateRandomString(length);
            if (symbolPattern.IsMatch(result))
            {
                hasSymbol = true;
                break;
            }
        }

        // Assert
        Assert.True(hasSymbol, "Expected to see at least one symbol character in generated strings");
    }

    [Fact]
    public void GenerateRandomString_IncludesAllCharacterTypes()
    {
        // Arrange
        const int length = 500; // Large enough to likely include all types

        // Act
        var result = RandomString.GenerateRandomString(length);

        // Assert
        Assert.Matches("[a-z]", result); // Lowercase
        Assert.Matches("[A-Z]", result); // Uppercase
        Assert.Matches("[0-9]", result); // Digits
        // At least one symbol should appear with 500 characters
        Assert.Matches(@"[`~!@#$%^&*()\-_=+\[{\]}\\|;:'"",<.>/?]", result);
    }


    //
    // Constants Validation Tests
    //

    [Fact]
    public void AlphabetLower_ContainsAllLowercaseLetters()
    {
        // Assert
        Assert.Equal(26, RandomString.AlphabetLower.Length);
        Assert.Equal("abcdefghijklmnopqrstuvwxyz", RandomString.AlphabetLower);
    }

    [Fact]
    public void Digits_ContainsAllDigits()
    {
        // Assert
        Assert.Equal(10, RandomString.Digits.Length);
        Assert.Equal("0123456789", RandomString.Digits);
    }

    [Fact]
    public void Symbols_ContainsExpectedSymbols()
    {
        // Assert
        Assert.Equal(32, RandomString.Symbols.Length);
        Assert.Contains('!', RandomString.Symbols);
        Assert.Contains('@', RandomString.Symbols);
        Assert.Contains('#', RandomString.Symbols);
        Assert.Contains('$', RandomString.Symbols);
        Assert.Contains('-', RandomString.Symbols);
        Assert.Contains('_', RandomString.Symbols);
    }


    //
    // Distribution Tests
    //

    [Fact]
    public void GenerateRandomString_HasReasonableDistribution()
    {
        // Arrange
        const int iterations = 10000;
        const int length = 1;
        var charCounts = new Dictionary<char, int>();

        // Act - Generate many single-character strings
        for (int i = 0; i < iterations; i++)
        {
            var result = RandomString.GenerateRandomString(length);
            var ch = result[0];
            charCounts[ch] = charCounts.GetValueOrDefault(ch, 0) + 1;
        }

        // Assert - We should see a variety of different characters
        Assert.True(charCounts.Count >= 50,
            $"Expected at least 50 different characters, got {charCounts.Count}");

        // No single character should dominate (appears more than 10% of the time)
        var maxCount = charCounts.Values.Max();
        var maxPercentage = (double)maxCount / iterations;
        Assert.True(maxPercentage < 0.1, $"Character distribution appears biased: {maxPercentage:P2}");
    }

    [Fact]
    public void GenerateUppercaseAlphanumericString_HasReasonableDistribution()
    {
        // Arrange
        const int iterations = 5000;
        const int length = 1;
        var charCounts = new Dictionary<char, int>();

        // Act
        for (int i = 0; i < iterations; i++)
        {
            var result = RandomString.GenerateUppercaseAlphanumericString(length);
            var ch = result[0];
            charCounts[ch] = charCounts.GetValueOrDefault(ch, 0) + 1;
        }

        // Assert - Should see variety across 36 possible characters (26 letters + 10 digits)
        Assert.True(charCounts.Count >= 25, $"Expected at least 25 different characters, got {charCounts.Count}");
    }
}
