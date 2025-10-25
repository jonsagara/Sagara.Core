using Sagara.Core.Extensions;
using Xunit;

namespace Sagara.Core.Tests.ExtensionTests;

public partial class StringExtensionTests
{
    //
    // ReplaceLastOccurrence
    //

    [Theory]
    [MemberData(nameof(GetTestData_OrdinalIgnoreCase))]
    public void ReplaceLastOccurrence_OrdinalIgnoreCase(ReplaceLastOccurrence data)
    {
        var actual = data.Value.ReplaceLastOccurrence(oldValue: data.OldValue, newValue: data.NewValue, StringComparison.OrdinalIgnoreCase);

        Assert.Equal(expected: data.Expected, actual: actual);
    }

    public static IEnumerable<object[]> GetTestData_OrdinalIgnoreCase()
    {
        // Basic replacements
        yield return new object[] { new ReplaceLastOccurrence(Value: "foo.md", OldValue: ".md", NewValue: "", Expected: "foo") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "hello world", OldValue: "world", NewValue: "universe", Expected: "hello universe") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test", OldValue: "es", NewValue: "xx", Expected: "txxt") };

        // Multiple occurrences - should only replace last
        yield return new object[] { new ReplaceLastOccurrence(Value: "foo.bar.md", OldValue: ".", NewValue: "_", Expected: "foo.bar_md") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test test test", OldValue: "test", NewValue: "pass", Expected: "test test pass") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "ababab", OldValue: "ab", NewValue: "cd", Expected: "ababcd") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "hello hello hello", OldValue: "hello", NewValue: "goodbye", Expected: "hello hello goodbye") };

        // Single occurrence
        yield return new object[] { new ReplaceLastOccurrence(Value: "single", OldValue: "gle", NewValue: "ton", Expected: "sinton") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "replace me", OldValue: "me", NewValue: "you", Expected: "replace you") };

        // OldValue not found
        yield return new object[] { new ReplaceLastOccurrence(Value: "no match here", OldValue: "xyz", NewValue: "abc", Expected: "no match here") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test", OldValue: "missing", NewValue: "found", Expected: "test") };

        // Empty string replacements
        yield return new object[] { new ReplaceLastOccurrence(Value: "remove.suffix.", OldValue: ".", NewValue: "", Expected: "remove.suffix") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test  ", OldValue: " ", NewValue: "", Expected: "test ") };

        // Replacement with longer string
        yield return new object[] { new ReplaceLastOccurrence(Value: "a b c", OldValue: " ", NewValue: "---", Expected: "a b---c") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "short", OldValue: "t", NewValue: "tening", Expected: "shortening") };

        // At beginning, middle, and end
        yield return new object[] { new ReplaceLastOccurrence(Value: "prefix_test", OldValue: "prefix", NewValue: "new", Expected: "new_test") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test_middle_test", OldValue: "_test", NewValue: "_end", Expected: "test_middle_end") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test_suffix", OldValue: "_suffix", NewValue: "_end", Expected: "test_end") };

        // Full string replacement
        yield return new object[] { new ReplaceLastOccurrence(Value: "replace", OldValue: "replace", NewValue: "new", Expected: "new") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "abc", OldValue: "abc", NewValue: "xyz", Expected: "xyz") };

        // Overlapping patterns
        yield return new object[] { new ReplaceLastOccurrence(Value: "aaaa", OldValue: "aa", NewValue: "b", Expected: "aab") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "abababab", OldValue: "abab", NewValue: "cd", Expected: "ababcd") };

        // Single character
        yield return new object[] { new ReplaceLastOccurrence(Value: "a", OldValue: "a", NewValue: "b", Expected: "b") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "x", OldValue: "x", NewValue: "", Expected: "") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test", OldValue: "t", NewValue: "T", Expected: "tesT") };

        // Whitespace handling
        yield return new object[] { new ReplaceLastOccurrence(Value: "  spaces  ", OldValue: " ", NewValue: "_", Expected: "  spaces _") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "tab\there", OldValue: "\t", NewValue: " ", Expected: "tab here") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "line\nbreak\n", OldValue: "\n", NewValue: "", Expected: "line\nbreak") };

        // Special characters
        yield return new object[] { new ReplaceLastOccurrence(Value: "price: $100", OldValue: "$", NewValue: "£", Expected: "price: £100") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test [1] [2]", OldValue: "[", NewValue: "(", Expected: "test [1] (2]") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "path\\to\\file", OldValue: "\\", NewValue: "/", Expected: "path\\to/file") };

        // Case insensitivity tests (assuming OrdinalIgnoreCase comparison in some tests)
        yield return new object[] { new ReplaceLastOccurrence(Value: "Test test TEST", OldValue: "test", NewValue: "pass", Expected: "Test test pass") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "ABC abc ABC", OldValue: "ABC", NewValue: "xyz", Expected: "ABC abc xyz") };

        // Adjacent duplicates
        yield return new object[] { new ReplaceLastOccurrence(Value: "testtest", OldValue: "test", NewValue: "pass", Expected: "testpass") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "......", OldValue: ".", NewValue: "!", Expected: ".....!") };

        // Empty result after replacement
        yield return new object[] { new ReplaceLastOccurrence(Value: "x", OldValue: "x", NewValue: "", Expected: "") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "delete", OldValue: "delete", NewValue: "", Expected: "") };

        // Unicode and special characters
        yield return new object[] { new ReplaceLastOccurrence(Value: "emoji 😀 test 😀", OldValue: "😀", NewValue: "🎉", Expected: "emoji 😀 test 🎉") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "café café", OldValue: "café", NewValue: "coffee", Expected: "café coffee") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "Hello 世界 世界", OldValue: "世界", NewValue: "World", Expected: "Hello 世界 World") };

        // Very long strings
        yield return new object[] { new ReplaceLastOccurrence(Value: new string('a', 1000) + "b", OldValue: "b", NewValue: "c", Expected: new string('a', 1000) + "c") };

        // Identical value and oldValue but appearing multiple times in pattern
        yield return new object[] { new ReplaceLastOccurrence(Value: "the the the", OldValue: "the", NewValue: "a", Expected: "the the a") };

        // Newlines and complex whitespace
        yield return new object[] { new ReplaceLastOccurrence(Value: "line1\r\nline2\r\n", OldValue: "\r\n", NewValue: "\n", Expected: "line1\r\nline2\n") };
    }


    [Theory]
    [MemberData(nameof(GetTestData_Ordinal))]
    public void ReplaceLastOccurrence_Ordinal(ReplaceLastOccurrence data)
    {
        var actual = data.Value.ReplaceLastOccurrence(oldValue: data.OldValue, newValue: data.NewValue, StringComparison.Ordinal);

        Assert.Equal(expected: data.Expected, actual: actual);
    }

    public static IEnumerable<object[]> GetTestData_Ordinal()
    {
        // Basic replacements
        yield return new object[] { new ReplaceLastOccurrence(Value: "foo.md", OldValue: ".md", NewValue: "", Expected: "foo") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "hello world", OldValue: "world", NewValue: "universe", Expected: "hello universe") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test", OldValue: "es", NewValue: "xx", Expected: "txxt") };

        // Multiple occurrences - should only replace last
        yield return new object[] { new ReplaceLastOccurrence(Value: "foo.bar.md", OldValue: ".", NewValue: "_", Expected: "foo.bar_md") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test test test", OldValue: "test", NewValue: "pass", Expected: "test test pass") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "ababab", OldValue: "ab", NewValue: "cd", Expected: "ababcd") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "hello hello hello", OldValue: "hello", NewValue: "goodbye", Expected: "hello hello goodbye") };

        // Single occurrence
        yield return new object[] { new ReplaceLastOccurrence(Value: "single", OldValue: "gle", NewValue: "ton", Expected: "sinton") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "replace me", OldValue: "me", NewValue: "you", Expected: "replace you") };

        // OldValue not found
        yield return new object[] { new ReplaceLastOccurrence(Value: "no match here", OldValue: "xyz", NewValue: "abc", Expected: "no match here") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test", OldValue: "missing", NewValue: "found", Expected: "test") };

        // Empty string replacements
        yield return new object[] { new ReplaceLastOccurrence(Value: "remove.suffix.", OldValue: ".", NewValue: "", Expected: "remove.suffix") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test  ", OldValue: " ", NewValue: "", Expected: "test ") };

        // Replacement with longer string
        yield return new object[] { new ReplaceLastOccurrence(Value: "a b c", OldValue: " ", NewValue: "---", Expected: "a b---c") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "short", OldValue: "t", NewValue: "tening", Expected: "shortening") };

        // At beginning, middle, and end
        yield return new object[] { new ReplaceLastOccurrence(Value: "prefix_test", OldValue: "prefix", NewValue: "new", Expected: "new_test") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test_middle_test", OldValue: "_test", NewValue: "_end", Expected: "test_middle_end") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test_suffix", OldValue: "_suffix", NewValue: "_end", Expected: "test_end") };

        // Full string replacement
        yield return new object[] { new ReplaceLastOccurrence(Value: "replace", OldValue: "replace", NewValue: "new", Expected: "new") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "abc", OldValue: "abc", NewValue: "xyz", Expected: "xyz") };

        // Overlapping patterns
        yield return new object[] { new ReplaceLastOccurrence(Value: "aaaa", OldValue: "aa", NewValue: "b", Expected: "aab") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "abababab", OldValue: "abab", NewValue: "cd", Expected: "ababcd") };

        // Single character
        yield return new object[] { new ReplaceLastOccurrence(Value: "a", OldValue: "a", NewValue: "b", Expected: "b") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "x", OldValue: "x", NewValue: "", Expected: "") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test", OldValue: "t", NewValue: "T", Expected: "tesT") };

        // Whitespace handling
        yield return new object[] { new ReplaceLastOccurrence(Value: "  spaces  ", OldValue: " ", NewValue: "_", Expected: "  spaces _") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "tab\there", OldValue: "\t", NewValue: " ", Expected: "tab here") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "line\nbreak\n", OldValue: "\n", NewValue: "", Expected: "line\nbreak") };

        // Special characters
        yield return new object[] { new ReplaceLastOccurrence(Value: "price: $100", OldValue: "$", NewValue: "£", Expected: "price: £100") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "test [1] [2]", OldValue: "[", NewValue: "(", Expected: "test [1] (2]") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "path\\to\\file", OldValue: "\\", NewValue: "/", Expected: "path\\to/file") };

        // Case sensitivity tests (assuming Ordinal comparison in some tests)
        yield return new object[] { new ReplaceLastOccurrence(Value: "Test test TEST", OldValue: "test", NewValue: "pass", Expected: "Test pass TEST") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "ABC abc ABC", OldValue: "ABC", NewValue: "xyz", Expected: "ABC abc xyz") };

        // Adjacent duplicates
        yield return new object[] { new ReplaceLastOccurrence(Value: "testtest", OldValue: "test", NewValue: "pass", Expected: "testpass") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "......", OldValue: ".", NewValue: "!", Expected: ".....!") };

        // Empty result after replacement
        yield return new object[] { new ReplaceLastOccurrence(Value: "x", OldValue: "x", NewValue: "", Expected: "") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "delete", OldValue: "delete", NewValue: "", Expected: "") };

        // Unicode and special characters
        yield return new object[] { new ReplaceLastOccurrence(Value: "emoji 😀 test 😀", OldValue: "😀", NewValue: "🎉", Expected: "emoji 😀 test 🎉") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "café café", OldValue: "café", NewValue: "coffee", Expected: "café coffee") };
        yield return new object[] { new ReplaceLastOccurrence(Value: "Hello 世界 世界", OldValue: "世界", NewValue: "World", Expected: "Hello 世界 World") };

        // Very long strings
        yield return new object[] { new ReplaceLastOccurrence(Value: new string('a', 1000) + "b", OldValue: "b", NewValue: "c", Expected: new string('a', 1000) + "c") };

        // Identical value and oldValue but appearing multiple times in pattern
        yield return new object[] { new ReplaceLastOccurrence(Value: "the the the", OldValue: "the", NewValue: "a", Expected: "the the a") };

        // Newlines and complex whitespace
        yield return new object[] { new ReplaceLastOccurrence(Value: "line1\r\nline2\r\n", OldValue: "\r\n", NewValue: "\n", Expected: "line1\r\nline2\n") };
    }


    [Fact]
    public void ReplaceLastOccurrence_ValueNull_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            string? value = null;
            value!.ReplaceLastOccurrence(oldValue: "foo", newValue: "bar", StringComparison.OrdinalIgnoreCase);
        });
    }

    [Fact]
    public void ReplaceLastOccurrence_OldValueNull_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            "foo".ReplaceLastOccurrence(oldValue: null!, newValue: "bar", StringComparison.OrdinalIgnoreCase);
        });
    }

    [Fact]
    public void ReplaceLastOccurrence_OldValueEmpty_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            "foo".ReplaceLastOccurrence(oldValue: "", newValue: "bar", StringComparison.OrdinalIgnoreCase);
        });
    }


    //
    // Types
    //

    public record ReplaceLastOccurrence(string Value, string OldValue, string NewValue, string Expected);
}
