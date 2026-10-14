#if NET10_0_OR_GREATER

using System.Globalization;
using Sagara.Core.Extensions;

namespace Sagara.Core.Tests.ExtensionTests;

public class StringComparerExtensionsTests
{
    //
    // Helpers
    //

    /// <summary>
    /// Runs <paramref name="action"/> with <see cref="CultureInfo.CurrentCulture"/> set to
    /// <paramref name="cultureName"/>, restoring the original culture afterward.
    /// </summary>
    private static void WithCulture(string cultureName, Action action)
    {
        var originalCulture = CultureInfo.CurrentCulture;

        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            action();
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    private static readonly string[] _unsortedFileNames = ["file10", "file2", "file1", "file20", "file3"];
    private static readonly string[] _sortedFileNames = ["file1", "file2", "file3", "file10", "file20"];


    //
    // Numeric
    //

    [Fact]
    public void Numeric_ReturnsNonNullComparer()
    {
        Assert.NotNull(StringComparer.Numeric);
    }

    [Theory]
    [InlineData("file2", "file10")]
    [InlineData("1", "2")]
    [InlineData("9", "10")]
    [InlineData("99", "100")]
    [InlineData("a9b", "a10b")]
    [InlineData("v1.9.0", "v1.10.0")]
    [InlineData("v1.10.9", "v1.10.10")]
    [InlineData("Chapter 2", "Chapter 12")]
    [InlineData("a99999999999999999999", "a100000000000000000000")]
    public void Numeric_ComparesDigitRunsByNumericValue(string smaller, string larger)
    {
        WithCulture("en-US", () =>
        {
            var comparer = StringComparer.Numeric;

            Assert.True(comparer.Compare(smaller, larger) < 0);
            Assert.True(comparer.Compare(larger, smaller) > 0);
        });
    }

    [Fact]
    public void Numeric_DiffersFromOrdinalAndDefaultCultureComparison()
    {
        WithCulture("en-US", () =>
        {
            // Sanity check: without numeric ordering, "file10" sorts before "file2".
            Assert.True(StringComparer.Ordinal.Compare("file10", "file2") < 0);
            Assert.True(StringComparer.CurrentCulture.Compare("file10", "file2") < 0);

            Assert.True(StringComparer.Numeric.Compare("file10", "file2") > 0);
        });
    }

    [Fact]
    public void Numeric_SortsListInNaturalOrder()
    {
        WithCulture("en-US", () =>
        {
            var sorted = _unsortedFileNames.Order(StringComparer.Numeric).ToArray();

            Assert.Equal(_sortedFileNames, sorted);
        });
    }

    [Theory]
    [InlineData("apple", "banana")]
    [InlineData("a", "b")]
    [InlineData("abc", "abcd")]
    public void Numeric_ComparesNonDigitTextNormally(string smaller, string larger)
    {
        WithCulture("en-US", () =>
        {
            var comparer = StringComparer.Numeric;

            Assert.True(comparer.Compare(smaller, larger) < 0);
            Assert.True(comparer.Compare(larger, smaller) > 0);
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData("file10")]
    [InlineData("hello world")]
    [InlineData("123")]
    public void Numeric_IdenticalStrings_AreEqual(string value)
    {
        WithCulture("en-US", () =>
        {
            var comparer = StringComparer.Numeric;

            // Use a distinct instance so we're not just testing reference equality.
            var copy = new string(value.AsSpan());

            Assert.Equal(0, comparer.Compare(value, copy));
            Assert.True(comparer.Equals(value, copy));
            Assert.Equal(comparer.GetHashCode(value), comparer.GetHashCode(copy));
        });
    }

    [Theory]
    [InlineData("FILE10", "file10")]
    [InlineData("Apple", "apple")]
    public void Numeric_IsCaseSensitive(string x, string y)
    {
        WithCulture("en-US", () =>
        {
            var comparer = StringComparer.Numeric;

            Assert.NotEqual(0, comparer.Compare(x, y));
            Assert.False(comparer.Equals(x, y));
        });
    }

    [Fact]
    public void Numeric_HandlesNulls()
    {
        var comparer = StringComparer.Numeric;

        Assert.Equal(0, comparer.Compare(null, null));
        Assert.True(comparer.Compare(null, "a") < 0);
        Assert.True(comparer.Compare("a", null) > 0);
        Assert.True(comparer.Equals(null, null));
        Assert.False(comparer.Equals(null, "a"));
    }

    [Fact]
    public void Numeric_WorksAsHashSetComparer()
    {
        WithCulture("en-US", () =>
        {
            var set = new HashSet<string>(StringComparer.Numeric) { "file2", "file2", "File2", "file10" };

            Assert.Equal(3, set.Count);
            Assert.Contains("file2", set);
            Assert.Contains("File2", set);
            Assert.DoesNotContain("FILE10", set);
        });
    }

    [Fact]
    public void Numeric_UsesCurrentCulture()
    {
        // In Swedish, "ä" is a distinct letter that sorts after "z". In English, it sorts with "a".
        var swedishResult = 0;
        var englishResult = 0;

        WithCulture("sv-SE", () => swedishResult = StringComparer.Numeric.Compare("ä", "z"));
        WithCulture("en-US", () => englishResult = StringComparer.Numeric.Compare("ä", "z"));

        Assert.True(swedishResult > 0);
        Assert.True(englishResult < 0);
    }

    [Fact]
    public void Numeric_RetainsCultureFromTimeOfAccess()
    {
        StringComparer? comparer = null;
        WithCulture("sv-SE", () => comparer = StringComparer.Numeric);

        WithCulture("en-US", () =>
        {
            // Still Swedish ordering, even though the current culture has changed.
            Assert.True(comparer!.Compare("ä", "z") > 0);
        });
    }


    //
    // NumericIgnoreCase
    //

    [Fact]
    public void NumericIgnoreCase_ReturnsNonNullComparer()
    {
        Assert.NotNull(StringComparer.NumericIgnoreCase);
    }

    [Theory]
    [InlineData("file2", "file10")]
    [InlineData("File2", "file10")]
    [InlineData("file2", "FILE10")]
    [InlineData("9", "10")]
    [InlineData("v1.9.0", "V1.10.0")]
    [InlineData("a99999999999999999999", "A100000000000000000000")]
    public void NumericIgnoreCase_ComparesDigitRunsByNumericValue(string smaller, string larger)
    {
        WithCulture("en-US", () =>
        {
            var comparer = StringComparer.NumericIgnoreCase;

            Assert.True(comparer.Compare(smaller, larger) < 0);
            Assert.True(comparer.Compare(larger, smaller) > 0);
        });
    }

    [Fact]
    public void NumericIgnoreCase_SortsMixedCaseListInNaturalOrder()
    {
        WithCulture("en-US", () =>
        {
            string[] unsorted = ["FILE10", "file2", "File1", "file20", "FILE3"];

            var sorted = unsorted.Order(StringComparer.NumericIgnoreCase).ToArray();

            Assert.Equal(["File1", "file2", "FILE3", "FILE10", "file20"], sorted);
        });
    }

    [Theory]
    [InlineData("FILE10", "file10")]
    [InlineData("Apple", "apple")]
    [InlineData("ABC123def", "abc123DEF")]
    [InlineData("", "")]
    public void NumericIgnoreCase_StringsDifferingOnlyByCase_AreEqual(string x, string y)
    {
        WithCulture("en-US", () =>
        {
            var comparer = StringComparer.NumericIgnoreCase;

            Assert.Equal(0, comparer.Compare(x, y));
            Assert.True(comparer.Equals(x, y));
            Assert.Equal(comparer.GetHashCode(x), comparer.GetHashCode(y));
        });
    }

    [Theory]
    [InlineData("file2", "file10")]
    [InlineData("apple", "banana")]
    public void NumericIgnoreCase_DifferentStrings_AreNotEqual(string x, string y)
    {
        WithCulture("en-US", () =>
        {
            var comparer = StringComparer.NumericIgnoreCase;

            Assert.NotEqual(0, comparer.Compare(x, y));
            Assert.False(comparer.Equals(x, y));
        });
    }

    [Fact]
    public void NumericIgnoreCase_HandlesNulls()
    {
        var comparer = StringComparer.NumericIgnoreCase;

        Assert.Equal(0, comparer.Compare(null, null));
        Assert.True(comparer.Compare(null, "a") < 0);
        Assert.True(comparer.Compare("a", null) > 0);
        Assert.True(comparer.Equals(null, null));
        Assert.False(comparer.Equals(null, "a"));
    }

    [Fact]
    public void NumericIgnoreCase_WorksAsHashSetComparer()
    {
        WithCulture("en-US", () =>
        {
            var set = new HashSet<string>(StringComparer.NumericIgnoreCase) { "file2", "File2", "FILE2", "file10" };

            Assert.Equal(2, set.Count);
            Assert.Contains("FiLe2", set);
            Assert.Contains("FILE10", set);
        });
    }

    [Fact]
    public void NumericIgnoreCase_WorksAsDictionaryComparer()
    {
        WithCulture("en-US", () =>
        {
            var dict = new Dictionary<string, int>(StringComparer.NumericIgnoreCase)
            {
                ["Item10"] = 10,
            };

            Assert.True(dict.TryGetValue("ITEM10", out var value));
            Assert.Equal(10, value);
            Assert.False(dict.ContainsKey("item2"));
        });
    }

    [Fact]
    public void NumericIgnoreCase_UsesCurrentCulture()
    {
        // In Turkish, the uppercase of "i" is "İ" (dotted), not "I", so "i" and "I" are not
        //   case-insensitively equal. In English, they are.
        var turkishEqual = true;
        var englishEqual = false;

        WithCulture("tr-TR", () => turkishEqual = StringComparer.NumericIgnoreCase.Equals("file1i", "FILE1I"));
        WithCulture("en-US", () => englishEqual = StringComparer.NumericIgnoreCase.Equals("file1i", "FILE1I"));

        Assert.False(turkishEqual);
        Assert.True(englishEqual);
    }

    [Fact]
    public void NumericIgnoreCase_RetainsCultureFromTimeOfAccess()
    {
        StringComparer? comparer = null;
        WithCulture("tr-TR", () => comparer = StringComparer.NumericIgnoreCase);

        WithCulture("en-US", () =>
        {
            // Still Turkish casing rules, even though the current culture has changed.
            Assert.False(comparer!.Equals("i", "I"));
            Assert.True(comparer.Equals("i", "İ"));
        });
    }
}

#endif
