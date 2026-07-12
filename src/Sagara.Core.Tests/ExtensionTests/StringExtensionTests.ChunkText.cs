using Sagara.Core.Extensions;
using Xunit;

namespace Sagara.Core.Tests.ExtensionTests;

public partial class StringExtensionTests
{
    //
    // ChunkText - argument validation
    //

    [Fact]
    public void ChunkText_NullText_Throws()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        // Justification: testing that null throws.
        Assert.Throws<ArgumentNullException>(() =>
            ((string?)null).ChunkText(maxLengthInChars: 20, includeNumbersInChunks: true)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public void ChunkText_EmptyText_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            "".ChunkText(maxLengthInChars: 20, includeNumbersInChunks: true)
            );
    }

    [Fact]
    public void ChunkText_WhiteSpaceOnlyText_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            "   ".ChunkText(maxLengthInChars: 20, includeNumbersInChunks: true)
            );
    }

    [Theory]
    [InlineData(10)]
    [InlineData(0)]
    [InlineData(-1)]
    public void ChunkText_IncludeNumbers_MaxLengthLessThan11_Throws(int maxLengthInChars)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            "Hello world".ChunkText(maxLengthInChars: maxLengthInChars, includeNumbersInChunks: true)
            );
    }

    [Fact]
    public void ChunkText_IncludeNumbers_MaxLength11_DoesNotThrow()
    {
        // Boundary: 11 is the minimum allowed length when numbers are included.
        var exception = Record.Exception(() =>
            "Hello world".ChunkText(maxLengthInChars: 11, includeNumbersInChunks: true)
            );

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ChunkText_ExcludeNumbers_MaxLengthLessThan1_Throws(int maxLengthInChars)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            "Hello world".ChunkText(maxLengthInChars: maxLengthInChars, includeNumbersInChunks: false)
            );
    }

    [Fact]
    public void ChunkText_ExcludeNumbers_MaxLength1_DoesNotThrow()
    {
        // Boundary: 1 is the minimum allowed length when numbers are excluded.
        var exception = Record.Exception(() =>
            "Hello world".ChunkText(maxLengthInChars: 1, includeNumbersInChunks: false)
            );

        Assert.Null(exception);
    }


    //
    // ChunkText - single chunk (text fits within the limit)
    //

    [Fact]
    public void ChunkText_IncludeNumbers_TextFitsInSingleChunk_ReturnsWithoutNumberSuffix()
    {
        // 15 - 10 reserved for numbering == 5 usable chars, which exactly fits "Hello".
        var actual = "Hello".ChunkText(maxLengthInChars: 15, includeNumbersInChunks: true);

        Assert.Equal(expected: ["Hello"], actual: actual);
    }

    [Fact]
    public void ChunkText_ExcludeNumbers_TextFitsInSingleChunk_ReturnsSingleChunk()
    {
        var actual = "Hello".ChunkText(maxLengthInChars: 5, includeNumbersInChunks: false);

        Assert.Equal(expected: ["Hello"], actual: actual);
    }

    [Fact]
    public void ChunkText_TextShorterThanMaxLength_ReturnsSingleChunk()
    {
        var actual = "Hi".ChunkText(maxLengthInChars: 100, includeNumbersInChunks: false);

        Assert.Equal(expected: ["Hi"], actual: actual);
    }


    //
    // ChunkText - multiple chunks, word-boundary splitting
    //

    [Fact]
    public void ChunkText_IncludeNumbers_SplitsOnWordBoundaryAndAppendsNumbers()
    {
        // maxLengthInChars 20 - 10 reserved == 10 usable chars per chunk.
        // "Hello world" (11 chars) doesn't fit in 10, so it must split. The split point falls
        //   mid-word ("Hello worl|d"), so it backs up to the preceding space.
        var actual = "Hello world".ChunkText(maxLengthInChars: 20, includeNumbersInChunks: true);

        Assert.Equal(expected: ["Hello (1/2)", "world (2/2)"], actual: actual);
    }

    [Fact]
    public void ChunkText_ExcludeNumbers_SplitsOnWordBoundaryWithoutNumbers()
    {
        var actual = "Hello world".ChunkText(maxLengthInChars: 10, includeNumbersInChunks: false);

        Assert.Equal(expected: ["Hello", "world"], actual: actual);
    }

    [Fact]
    public void ChunkText_ManyWords_SplitsIntoMultipleNumberedChunks()
    {
        var actual = "The quick brown fox jumps over the lazy dog".ChunkText(maxLengthInChars: 21, includeNumbersInChunks: true);

        Assert.Equal(
            expected: ["The quick (1/5)", "brown fox (2/5)", "jumps over (3/5)", "the lazy (4/5)", "dog (5/5)"],
            actual: actual
            );
    }


    //
    // ChunkText - forced mid-word splitting when no space is available
    //

    [Fact]
    public void ChunkText_ExcludeNumbers_NoSpaces_SplitsMidWord()
    {
        var actual = "abcdefghi".ChunkText(maxLengthInChars: 3, includeNumbersInChunks: false);

        Assert.Equal(expected: ["abc", "def", "ghi"], actual: actual);
    }

    [Fact]
    public void ChunkText_ExcludeNumbers_MinimumMaxLength1_SplitsIntoIndividualCharacters()
    {
        var actual = "abc".ChunkText(maxLengthInChars: 1, includeNumbersInChunks: false);

        Assert.Equal(expected: ["a", "b", "c"], actual: actual);
    }

    [Fact]
    public void ChunkText_LeadingSpaceInRemainingText_IsNotIncludedAsOwnEmptyChunk()
    {
        // With maxLengthInChars 11 (statusMaxLengthInChars == 1), every other chunk boundary lands
        //   directly on a space character. Those whitespace-only chunks must be dropped rather than
        //   appearing as empty entries in the result, and the chunk numbering must not count them.
        var actual = "a b c d e".ChunkText(maxLengthInChars: 11, includeNumbersInChunks: true);

        Assert.Equal(
            expected: ["a (1/5)", "b (2/5)", "c (3/5)", "d (4/5)", "e (5/5)"],
            actual: actual
            );
    }


    //
    // ChunkText - no chunk has leading/trailing whitespace
    //

    [Fact]
    public void ChunkText_Chunks_HaveNoLeadingOrTrailingWhitespace()
    {
        var actual = "  Hello   world  ".ChunkText(maxLengthInChars: 11, includeNumbersInChunks: false);

        Assert.All(actual, chunk => Assert.Equal(expected: chunk.Trim(), actual: chunk));
        Assert.All(actual, chunk => Assert.NotEqual(expected: "", actual: chunk));
    }


    //
    // ChunkText - boundary: text length exactly equal to the usable chunk length
    //

    [Fact]
    public void ChunkText_ExcludeNumbers_TextLengthExactlyMaxLength_ReturnsSingleChunk()
    {
        var text = new string('a', 10);

        var actual = text.ChunkText(maxLengthInChars: 10, includeNumbersInChunks: false);

        Assert.Equal(expected: [text], actual: actual);
    }

    [Fact]
    public void ChunkText_ExcludeNumbers_TextLengthOneMoreThanMaxLength_ReturnsTwoChunks()
    {
        var text = new string('a', 11);

        var actual = text.ChunkText(maxLengthInChars: 10, includeNumbersInChunks: false);

        Assert.Equal(expected: [new string('a', 10), "a"], actual: actual);
    }


    //
    // ChunkText - round-trip: chunking must not lose or reorder any words
    //

    [Fact]
    public void ChunkText_IncludeNumbers_LongText_RoundTripsAllWordsInOrderWithSequentialNumbering()
    {
        var words = Enumerable.Range(1, 200).Select(i => $"word{i}").ToArray();
        var text = string.Join(" ", words);

        var actual = text.ChunkText(maxLengthInChars: 50, includeNumbersInChunks: true);

        Assert.True(actual.Length > 1, "Expected the long text to be split into multiple chunks.");

        // Every chunk must respect the requested max length.
        Assert.All(actual, chunk => Assert.True(chunk.Length <= 50, $"Chunk exceeds max length: \"{chunk}\""));

        // Every chunk must end with the correctly numbered "(n/total)" suffix, in sequence.
        var reconstructedWords = new List<string>();

        for (var i = 0; i < actual.Length; i++)
        {
            var expectedSuffix = $" ({i + 1}/{actual.Length})";

            Assert.EndsWith(expectedSuffix, actual[i], StringComparison.Ordinal);

            var chunkTextWithoutSuffix = actual[i][..^expectedSuffix.Length];
            reconstructedWords.AddRange(chunkTextWithoutSuffix.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        Assert.Equal(expected: words, actual: reconstructedWords);
    }

    [Fact]
    public void ChunkText_ExcludeNumbers_LongText_RoundTripsAllWordsInOrder()
    {
        var words = Enumerable.Range(1, 200).Select(i => $"word{i}").ToArray();
        var text = string.Join(" ", words);

        var actual = text.ChunkText(maxLengthInChars: 40, includeNumbersInChunks: false);

        Assert.True(actual.Length > 1, "Expected the long text to be split into multiple chunks.");

        Assert.All(actual, chunk => Assert.True(chunk.Length <= 40, $"Chunk exceeds max length: \"{chunk}\""));

        var reconstructedWords = actual
            .SelectMany(chunk => chunk.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        Assert.Equal(expected: words, actual: reconstructedWords);
    }


    //
    // ChunkText - single chunk never gets a number suffix, even for very small inputs
    //

    [Fact]
    public void ChunkText_IncludeNumbers_SingleCharacterText_ReturnsWithoutNumberSuffix()
    {
        var actual = "a".ChunkText(maxLengthInChars: 11, includeNumbersInChunks: true);

        Assert.Equal(expected: ["a"], actual: actual);
    }
}
