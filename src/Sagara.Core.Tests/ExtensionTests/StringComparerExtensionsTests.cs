#if NET10_0_OR_GREATER

using System.Globalization;
using System.Reflection;
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

    /// <summary>
    /// Maps a property name to the comparer it returns, so that theories can display a readable
    /// comparer name and run the same assertions against every property.
    /// </summary>
    private static StringComparer GetComparer(string name)
    {
        // Justification: the obsolete properties must keep working until they are removed.
#pragma warning disable CS0618 // Type or member is obsolete
        return name switch
        {
            "CurrentCultureNumeric" => StringComparer.CurrentCultureNumeric,
            "CurrentCultureNumericIgnoreCase" => StringComparer.CurrentCultureNumericIgnoreCase,
            "InvariantCultureNumeric" => StringComparer.InvariantCultureNumeric,
            "InvariantCultureNumericIgnoreCase" => StringComparer.InvariantCultureNumericIgnoreCase,
            "Numeric" => StringComparer.Numeric,
            "NumericIgnoreCase" => StringComparer.NumericIgnoreCase,
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, "Unknown comparer name."),
        };
#pragma warning restore CS0618 // Type or member is obsolete
    }

    private static readonly string[] _caseSensitiveComparerNames =
    [
        "CurrentCultureNumeric",
        "InvariantCultureNumeric",
        "Numeric",
    ];

    private static readonly string[] _caseInsensitiveComparerNames =
    [
        "CurrentCultureNumericIgnoreCase",
        "InvariantCultureNumericIgnoreCase",
        "NumericIgnoreCase",
    ];

    private static readonly string[] _allComparerNames = [.. _caseSensitiveComparerNames, .. _caseInsensitiveComparerNames];

    private static TheoryData<string> ToTheoryData(string[] comparerNames)
        => new(comparerNames);

    private static TheoryData<string, string, string> CrossJoin(string[] comparerNames, (string, string)[] pairs)
    {
        var data = new TheoryData<string, string, string>();

        foreach (var comparerName in comparerNames)
        {
            foreach (var (x, y) in pairs)
            {
                data.Add(comparerName, x, y);
            }
        }

        return data;
    }

    private static TheoryData<string, string> CrossJoin(string[] comparerNames, string[] values)
    {
        var data = new TheoryData<string, string>();

        foreach (var comparerName in comparerNames)
        {
            foreach (var value in values)
            {
                data.Add(comparerName, value);
            }
        }

        return data;
    }


    //
    // Test data
    //

    public static TheoryData<string> AllComparers
        => ToTheoryData(_allComparerNames);

    public static TheoryData<string> CaseSensitiveComparers
        => ToTheoryData(_caseSensitiveComparerNames);

    public static TheoryData<string> CaseInsensitiveComparers
        => ToTheoryData(_caseInsensitiveComparerNames);

    public static TheoryData<string, string, string> NumericOrderingPairs
        => CrossJoin(_allComparerNames,
        [
            ("file2", "file10"),
            ("1", "2"),
            ("9", "10"),
            ("99", "100"),
            ("a9b", "a10b"),
            ("v1.9.0", "v1.10.0"),
            ("v1.10.9", "v1.10.10"),
            ("Chapter 2", "Chapter 12"),
            ("a99999999999999999999", "a100000000000000000000"),
        ]);

    public static TheoryData<string, string, string> MixedCaseNumericOrderingPairs
        => CrossJoin(_caseInsensitiveComparerNames,
        [
            ("File2", "file10"),
            ("file2", "FILE10"),
            ("v1.9.0", "V1.10.0"),
            ("a99999999999999999999", "A100000000000000000000"),
        ]);

    public static TheoryData<string, string, string> NonDigitOrderingPairs
        => CrossJoin(_allComparerNames,
        [
            ("apple", "banana"),
            ("a", "b"),
            ("abc", "abcd"),
            ("", "a"),
        ]);

    public static TheoryData<string, string> IdenticalValues
        => CrossJoin(_allComparerNames,
        [
            "",
            "file10",
            "hello world",
            "123",
        ]);

    private static readonly (string, string)[] _caseOnlyDifferences =
    [
        ("FILE10", "file10"),
        ("Apple", "apple"),
        ("ABC123def", "abc123DEF"),
    ];

    public static TheoryData<string, string, string> CaseOnlyDifferencesForCaseSensitiveComparers
        => CrossJoin(_caseSensitiveComparerNames, _caseOnlyDifferences);

    public static TheoryData<string, string, string> CaseOnlyDifferencesForCaseInsensitiveComparers
        => CrossJoin(_caseInsensitiveComparerNames, _caseOnlyDifferences);

    public static TheoryData<string, string, string> DifferentValues
        => CrossJoin(_allComparerNames,
        [
            ("file2", "file10"),
            ("apple", "banana"),
        ]);


    //
    // Behavior shared by all comparers
    //

    [Theory]
    [MemberData(nameof(AllComparers))]
    public void AllComparers_ReturnNonNullComparer(string comparerName)
    {
        Assert.NotNull(GetComparer(comparerName));
    }

    [Theory]
    [MemberData(nameof(AllComparers))]
    public void AllComparers_ReturnNewInstanceOnEachAccess(string comparerName)
    {
        Assert.NotSame(GetComparer(comparerName), GetComparer(comparerName));
    }

    [Theory]
    [MemberData(nameof(NumericOrderingPairs))]
    public void AllComparers_CompareDigitRunsByNumericValue(string comparerName, string smaller, string larger)
    {
        WithCulture("en-US", () =>
        {
            var comparer = GetComparer(comparerName);

            Assert.True(comparer.Compare(smaller, larger) < 0);
            Assert.True(comparer.Compare(larger, smaller) > 0);
        });
    }

    [Theory]
    [MemberData(nameof(AllComparers))]
    public void AllComparers_DifferFromOrdinalAndDefaultCultureComparison(string comparerName)
    {
        WithCulture("en-US", () =>
        {
            // Sanity check: without numeric ordering, "file10" sorts before "file2".
            Assert.True(StringComparer.Ordinal.Compare("file10", "file2") < 0);
            Assert.True(StringComparer.CurrentCulture.Compare("file10", "file2") < 0);
            Assert.True(StringComparer.InvariantCulture.Compare("file10", "file2") < 0);

            Assert.True(GetComparer(comparerName).Compare("file10", "file2") > 0);
        });
    }

    [Theory]
    [MemberData(nameof(AllComparers))]
    public void AllComparers_SortListInNaturalOrder(string comparerName)
    {
        WithCulture("en-US", () =>
        {
            string[] unsorted = ["file10", "file2", "file1", "file20", "file3"];

            var sorted = unsorted.Order(GetComparer(comparerName)).ToArray();

            Assert.Equal(["file1", "file2", "file3", "file10", "file20"], sorted);
        });
    }

    [Theory]
    [MemberData(nameof(NonDigitOrderingPairs))]
    public void AllComparers_CompareNonDigitTextNormally(string comparerName, string smaller, string larger)
    {
        WithCulture("en-US", () =>
        {
            var comparer = GetComparer(comparerName);

            Assert.True(comparer.Compare(smaller, larger) < 0);
            Assert.True(comparer.Compare(larger, smaller) > 0);
        });
    }

    [Theory]
    [MemberData(nameof(IdenticalValues))]
    public void AllComparers_IdenticalStrings_AreEqual(string comparerName, string value)
    {
        WithCulture("en-US", () =>
        {
            var comparer = GetComparer(comparerName);

            // Use a distinct instance so we're not just testing reference equality.
            var copy = new string(value.AsSpan());

            Assert.Equal(0, comparer.Compare(value, copy));
            Assert.True(comparer.Equals(value, copy));
            Assert.Equal(comparer.GetHashCode(value), comparer.GetHashCode(copy));
        });
    }

    [Theory]
    [MemberData(nameof(DifferentValues))]
    public void AllComparers_DifferentStrings_AreNotEqual(string comparerName, string x, string y)
    {
        WithCulture("en-US", () =>
        {
            var comparer = GetComparer(comparerName);

            Assert.NotEqual(0, comparer.Compare(x, y));
            Assert.False(comparer.Equals(x, y));
        });
    }

    [Theory]
    [MemberData(nameof(AllComparers))]
    public void AllComparers_HandleNulls(string comparerName)
    {
        var comparer = GetComparer(comparerName);

        Assert.Equal(0, comparer.Compare(null, null));
        Assert.True(comparer.Compare(null, "a") < 0);
        Assert.True(comparer.Compare("a", null) > 0);
        Assert.True(comparer.Equals(null, null));
        Assert.False(comparer.Equals(null, "a"));
        Assert.False(comparer.Equals("a", null));
    }

    [Theory]
    [MemberData(nameof(AllComparers))]
    public void AllComparers_WorkAsDictionaryComparer(string comparerName)
    {
        WithCulture("en-US", () =>
        {
            var dict = new Dictionary<string, int>(GetComparer(comparerName))
            {
                ["Item10"] = 10,
            };

            Assert.True(dict.TryGetValue("Item10", out var value));
            Assert.Equal(10, value);
            Assert.False(dict.ContainsKey("Item2"));
        });
    }


    //
    // Case-sensitive comparers
    //

    [Theory]
    [MemberData(nameof(CaseOnlyDifferencesForCaseSensitiveComparers))]
    public void CaseSensitiveComparers_StringsDifferingOnlyByCase_AreNotEqual(string comparerName, string x, string y)
    {
        WithCulture("en-US", () =>
        {
            var comparer = GetComparer(comparerName);

            Assert.NotEqual(0, comparer.Compare(x, y));
            Assert.False(comparer.Equals(x, y));
        });
    }

    [Theory]
    [MemberData(nameof(CaseSensitiveComparers))]
    public void CaseSensitiveComparers_WorkAsHashSetComparer(string comparerName)
    {
        WithCulture("en-US", () =>
        {
            var set = new HashSet<string>(GetComparer(comparerName)) { "file2", "file2", "File2", "file10" };

            Assert.Equal(3, set.Count);
            Assert.Contains("file2", set);
            Assert.Contains("File2", set);
            Assert.DoesNotContain("FILE10", set);
        });
    }


    //
    // Case-insensitive comparers
    //

    [Theory]
    [MemberData(nameof(CaseOnlyDifferencesForCaseInsensitiveComparers))]
    public void CaseInsensitiveComparers_StringsDifferingOnlyByCase_AreEqual(string comparerName, string x, string y)
    {
        WithCulture("en-US", () =>
        {
            var comparer = GetComparer(comparerName);

            Assert.Equal(0, comparer.Compare(x, y));
            Assert.True(comparer.Equals(x, y));
            Assert.Equal(comparer.GetHashCode(x), comparer.GetHashCode(y));
        });
    }

    [Theory]
    [MemberData(nameof(MixedCaseNumericOrderingPairs))]
    public void CaseInsensitiveComparers_CompareMixedCaseDigitRunsByNumericValue(string comparerName, string smaller, string larger)
    {
        WithCulture("en-US", () =>
        {
            var comparer = GetComparer(comparerName);

            Assert.True(comparer.Compare(smaller, larger) < 0);
            Assert.True(comparer.Compare(larger, smaller) > 0);
        });
    }

    [Theory]
    [MemberData(nameof(CaseInsensitiveComparers))]
    public void CaseInsensitiveComparers_SortMixedCaseListInNaturalOrder(string comparerName)
    {
        WithCulture("en-US", () =>
        {
            string[] unsorted = ["FILE10", "file2", "File1", "file20", "FILE3"];

            var sorted = unsorted.Order(GetComparer(comparerName)).ToArray();

            Assert.Equal(["File1", "file2", "FILE3", "FILE10", "file20"], sorted);
        });
    }

    [Theory]
    [MemberData(nameof(CaseInsensitiveComparers))]
    public void CaseInsensitiveComparers_WorkAsHashSetComparer(string comparerName)
    {
        WithCulture("en-US", () =>
        {
            var set = new HashSet<string>(GetComparer(comparerName)) { "file2", "File2", "FILE2", "file10" };

            Assert.Equal(2, set.Count);
            Assert.Contains("FiLe2", set);
            Assert.Contains("FILE10", set);
        });
    }

    [Theory]
    [MemberData(nameof(CaseInsensitiveComparers))]
    public void CaseInsensitiveComparers_WorkAsCaseInsensitiveDictionaryComparer(string comparerName)
    {
        WithCulture("en-US", () =>
        {
            var dict = new Dictionary<string, int>(GetComparer(comparerName))
            {
                ["Item10"] = 10,
            };

            Assert.True(dict.TryGetValue("ITEM10", out var value));
            Assert.Equal(10, value);
        });
    }


    //
    // CurrentCultureNumeric
    //

    [Fact]
    public void CurrentCultureNumeric_UsesCurrentCulture()
    {
        // In Swedish, "ä" is a distinct letter that sorts after "z". In English, it sorts with "a".
        var swedishResult = 0;
        var englishResult = 0;

        WithCulture("sv-SE", () => swedishResult = StringComparer.CurrentCultureNumeric.Compare("ä", "z"));
        WithCulture("en-US", () => englishResult = StringComparer.CurrentCultureNumeric.Compare("ä", "z"));

        Assert.True(swedishResult > 0);
        Assert.True(englishResult < 0);
    }

    [Fact]
    public void CurrentCultureNumeric_RetainsCultureFromTimeOfAccess()
    {
        StringComparer? comparer = null;
        WithCulture("sv-SE", () => comparer = StringComparer.CurrentCultureNumeric);

        WithCulture("en-US", () =>
        {
            // Still Swedish ordering, even though the current culture has changed.
            Assert.True(comparer!.Compare("ä", "z") > 0);
        });
    }


    //
    // CurrentCultureNumericIgnoreCase
    //

    [Fact]
    public void CurrentCultureNumericIgnoreCase_UsesCurrentCulture()
    {
        // In Turkish, the uppercase of "i" is "İ" (dotted), not "I", so "i" and "I" are not
        //   case-insensitively equal. In English, they are.
        var turkishEqual = true;
        var englishEqual = false;

        WithCulture("tr-TR", () => turkishEqual = StringComparer.CurrentCultureNumericIgnoreCase.Equals("file1i", "FILE1I"));
        WithCulture("en-US", () => englishEqual = StringComparer.CurrentCultureNumericIgnoreCase.Equals("file1i", "FILE1I"));

        Assert.False(turkishEqual);
        Assert.True(englishEqual);
    }

    [Fact]
    public void CurrentCultureNumericIgnoreCase_RetainsCultureFromTimeOfAccess()
    {
        StringComparer? comparer = null;
        WithCulture("tr-TR", () => comparer = StringComparer.CurrentCultureNumericIgnoreCase);

        WithCulture("en-US", () =>
        {
            // Still Turkish casing rules, even though the current culture has changed.
            Assert.False(comparer!.Equals("i", "I"));
            Assert.True(comparer.Equals("i", "İ"));
        });
    }


    //
    // InvariantCultureNumeric
    //

    [Theory]
    [InlineData("en-US")]
    [InlineData("sv-SE")]
    [InlineData("tr-TR")]
    [InlineData("ja-JP")]
    public void InvariantCultureNumeric_IgnoresCurrentCulture(string cultureName)
    {
        // Under the invariant culture, "ä" sorts with "a", i.e., before "z", regardless of the
        //   current culture. Contrast with CurrentCultureNumeric_UsesCurrentCulture.
        WithCulture(cultureName, () =>
        {
            var comparer = StringComparer.InvariantCultureNumeric;

            Assert.True(comparer.Compare("ä", "z") < 0);
            Assert.True(comparer.Compare("file2", "file10") < 0);
            Assert.False(comparer.Equals("i", "I"));
        });
    }

    [Fact]
    public void InvariantCultureNumeric_SortsIdenticallyAcrossCultures()
    {
        string[] unsorted = ["z10", "ä2", "Z2", "a10", "file10", "File2", "ä10", "a2"];

        string[]? englishSorted = null;
        string[]? swedishSorted = null;
        string[]? turkishSorted = null;

        WithCulture("en-US", () => englishSorted = [.. unsorted.Order(StringComparer.InvariantCultureNumeric)]);
        WithCulture("sv-SE", () => swedishSorted = [.. unsorted.Order(StringComparer.InvariantCultureNumeric)]);
        WithCulture("tr-TR", () => turkishSorted = [.. unsorted.Order(StringComparer.InvariantCultureNumeric)]);

        Assert.Equal(englishSorted, swedishSorted);
        Assert.Equal(englishSorted, turkishSorted);
    }


    //
    // InvariantCultureNumericIgnoreCase
    //

    [Theory]
    [InlineData("en-US")]
    [InlineData("sv-SE")]
    [InlineData("tr-TR")]
    [InlineData("ja-JP")]
    public void InvariantCultureNumericIgnoreCase_IgnoresCurrentCulture(string cultureName)
    {
        // Under the invariant culture, "i" and "I" are case-insensitively equal, even when the
        //   current culture is Turkish. Contrast with CurrentCultureNumericIgnoreCase_UsesCurrentCulture.
        WithCulture(cultureName, () =>
        {
            var comparer = StringComparer.InvariantCultureNumericIgnoreCase;

            Assert.True(comparer.Equals("file1i", "FILE1I"));
            Assert.True(comparer.Compare("ä", "Z") < 0);
            Assert.True(comparer.Compare("File2", "file10") < 0);
        });
    }

    [Fact]
    public void InvariantCultureNumericIgnoreCase_SortsIdenticallyAcrossCultures()
    {
        // Values are distinct ignoring case, so the sort is fully determined.
        string[] unsorted = ["z10", "ä2", "Z2", "a10", "file10", "File2", "ä10", "I3", "i20"];

        string[]? englishSorted = null;
        string[]? swedishSorted = null;
        string[]? turkishSorted = null;

        WithCulture("en-US", () => englishSorted = [.. unsorted.Order(StringComparer.InvariantCultureNumericIgnoreCase)]);
        WithCulture("sv-SE", () => swedishSorted = [.. unsorted.Order(StringComparer.InvariantCultureNumericIgnoreCase)]);
        WithCulture("tr-TR", () => turkishSorted = [.. unsorted.Order(StringComparer.InvariantCultureNumericIgnoreCase)]);

        Assert.Equal(englishSorted, swedishSorted);
        Assert.Equal(englishSorted, turkishSorted);
    }


    //
    // Numeric / NumericIgnoreCase (obsolete)
    //

    /// <summary>
    /// Finds the declaration of a C# 14 extension property. The compiler emits a static getter on
    /// <see cref="StringComparerExtensions"/> for the implementation, but attributes such as
    /// <see cref="ObsoleteAttribute"/> are placed on the property in a compiler-generated nested
    /// "skeleton" type, which is what the compiler consults when callers use the member.
    /// </summary>
    private static PropertyInfo GetExtensionPropertyDeclaration(string propertyName)
    {
        var properties = typeof(StringComparerExtensions)
            .GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)
            .Select(t => t.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            .OfType<PropertyInfo>()
            .ToArray();

        return Assert.Single(properties);
    }

    public static TheoryData<string, string> ObsoleteMembers
        => new()
        {
            { "Numeric", "CurrentCultureNumeric" },
            { "NumericIgnoreCase", "CurrentCultureNumericIgnoreCase" },
        };

    [Theory]
    [MemberData(nameof(ObsoleteMembers))]
    public void ObsoleteMembers_AreMarkedObsolete_AndNameTheirReplacement(string propertyName, string replacementName)
    {
        var obsolete = GetExtensionPropertyDeclaration(propertyName).GetCustomAttribute<ObsoleteAttribute>();

        Assert.NotNull(obsolete);
        Assert.False(obsolete.IsError);
        Assert.Contains($"StringComparer.{replacementName} ", obsolete.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("CurrentCultureNumeric")]
    [InlineData("CurrentCultureNumericIgnoreCase")]
    [InlineData("InvariantCultureNumeric")]
    [InlineData("InvariantCultureNumericIgnoreCase")]
    public void NewMembers_AreNotMarkedObsolete(string propertyName)
    {
        Assert.Null(GetExtensionPropertyDeclaration(propertyName).GetCustomAttribute<ObsoleteAttribute>());
    }

    [Theory]
    [InlineData("CurrentCultureNumeric")]
    [InlineData("CurrentCultureNumericIgnoreCase")]
    [InlineData("InvariantCultureNumeric")]
    [InlineData("InvariantCultureNumericIgnoreCase")]
    [InlineData("Numeric")]
    [InlineData("NumericIgnoreCase")]
    public void AllMembers_AreStaticExtensionPropertiesOfStringComparer(string propertyName)
    {
        // The implementation is a public static getter on the containing class.
        var getter = typeof(StringComparerExtensions).GetMethod($"get_{propertyName}", BindingFlags.Public | BindingFlags.Static, Type.EmptyTypes);
        Assert.NotNull(getter);
        Assert.Equal(typeof(StringComparer), getter.ReturnType);

        // The declaration is a read-only property.
        var property = GetExtensionPropertyDeclaration(propertyName);
        Assert.Equal(typeof(StringComparer), property.PropertyType);
        Assert.False(property.CanWrite);
    }

    [Theory]
    [InlineData("en-US")]
    [InlineData("sv-SE")]
    [InlineData("tr-TR")]
    public void Numeric_BehavesLikeCurrentCultureNumeric(string cultureName)
    {
        string[] unsorted = ["z10", "ä2", "Z2", "a10", "file10", "File2", "ä10", "i3", "I3"];

        WithCulture(cultureName, () =>
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var obsoleteSorted = unsorted.Order(StringComparer.Numeric).ToArray();
            var obsoleteEqual = StringComparer.Numeric.Equals("file10", "FILE10");
#pragma warning restore CS0618 // Type or member is obsolete

            var replacementSorted = unsorted.Order(StringComparer.CurrentCultureNumeric).ToArray();
            var replacementEqual = StringComparer.CurrentCultureNumeric.Equals("file10", "FILE10");

            Assert.Equal(replacementSorted, obsoleteSorted);
            Assert.Equal(replacementEqual, obsoleteEqual);
        });
    }

    [Theory]
    [InlineData("en-US")]
    [InlineData("sv-SE")]
    [InlineData("tr-TR")]
    public void NumericIgnoreCase_BehavesLikeCurrentCultureNumericIgnoreCase(string cultureName)
    {
        // Values are distinct ignoring case (under every tested culture), so the sort is fully determined.
        string[] unsorted = ["z10", "ä2", "Z2", "a10", "file10", "File2", "ä10", "I3"];

        WithCulture(cultureName, () =>
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var obsoleteSorted = unsorted.Order(StringComparer.NumericIgnoreCase).ToArray();
            var obsoleteEqual = StringComparer.NumericIgnoreCase.Equals("file1i", "FILE1I");
#pragma warning restore CS0618 // Type or member is obsolete

            var replacementSorted = unsorted.Order(StringComparer.CurrentCultureNumericIgnoreCase).ToArray();
            var replacementEqual = StringComparer.CurrentCultureNumericIgnoreCase.Equals("file1i", "FILE1I");

            Assert.Equal(replacementSorted, obsoleteSorted);
            Assert.Equal(replacementEqual, obsoleteEqual);
        });
    }

    [Fact]
    public void Numeric_StillUsesCurrentCulture()
    {
        var swedishResult = 0;
        var englishResult = 0;

#pragma warning disable CS0618 // Type or member is obsolete
        WithCulture("sv-SE", () => swedishResult = StringComparer.Numeric.Compare("ä", "z"));
        WithCulture("en-US", () => englishResult = StringComparer.Numeric.Compare("ä", "z"));
#pragma warning restore CS0618 // Type or member is obsolete

        Assert.True(swedishResult > 0);
        Assert.True(englishResult < 0);
    }

    [Fact]
    public void NumericIgnoreCase_StillUsesCurrentCulture()
    {
        var turkishEqual = true;
        var englishEqual = false;

#pragma warning disable CS0618 // Type or member is obsolete
        WithCulture("tr-TR", () => turkishEqual = StringComparer.NumericIgnoreCase.Equals("file1i", "FILE1I"));
        WithCulture("en-US", () => englishEqual = StringComparer.NumericIgnoreCase.Equals("file1i", "FILE1I"));
#pragma warning restore CS0618 // Type or member is obsolete

        Assert.False(turkishEqual);
        Assert.True(englishEqual);
    }
}

#endif
