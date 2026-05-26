using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Sagara.Core.Enums;
using Xunit;

namespace Sagara.Core.Tests.EnumsTests;

public class EnumTraitsTests
{
    public enum EmptyEnum { }

    public enum NonEmptyEnum
    {
        SingleValue,
    }

    [Fact]
    public void IsEmpty_ReturnsTrue()
    {
        Assert.True(EnumTraits<EmptyEnum>.IsEmpty);
    }

    [Fact]
    public void IsEmpty_ReturnsFalse()
    {
        Assert.False(EnumTraits<NonEmptyEnum>.IsEmpty);
    }


    [Flags]
    public enum HasFlagsEnum
    {
        Option1 = 0x1,
        Option2 = 0x2,
    }

    public enum NoFlagsEnum
    {
        [InvalidEnumValue]
        Unknown = 0,
        Option1,
        Option2,
    }

    [Fact]
    public void HasFlagsAttribute_ReturnsTrue()
    {
        Assert.True(EnumTraits<HasFlagsEnum>.HasFlagsAttribute);
    }

    [Fact]
    public void HasFlagsAttribute_ReturnsFalse()
    {
        Assert.False(EnumTraits<NoFlagsEnum>.HasFlagsAttribute);
    }

    [Fact]
    public void Type_ReturnsCorrectType()
    {
        Assert.Equal(typeof(EmptyEnum), EnumTraits<EmptyEnum>.EnumType);
        Assert.Equal(typeof(NonEmptyEnum), EnumTraits<NonEmptyEnum>.EnumType);
        Assert.Equal(typeof(HasFlagsEnum), EnumTraits<HasFlagsEnum>.EnumType);
        Assert.Equal(typeof(NoFlagsEnum), EnumTraits<NoFlagsEnum>.EnumType);
    }


    public enum ValuesEnum
    {
        [InvalidEnumValue]
        Unknown = 0,
        Value1 = 1,
        Value2 = 2,
        Value3 = 3,
    }

    [Fact]
    public void AllValues_ReturnsAllValues()
    {
        var expectedAllValues = EnumTraits<ValuesEnum>.AllValues;
        var actualAllValues = Enum.GetValues<ValuesEnum>();

        Assert.Equal(expectedAllValues, actualAllValues);
    }

    [Fact]
    public void ValidValues_ReturnsValidValues()
    {
        var expectedValidValues = EnumTraits<ValuesEnum>.ValidValues;
        var actualValidValues = Enum.GetValues<ValuesEnum>()
            .Where(v =>
            {
                // Exclude any values decorated with [InvalidEnumValue].
                var valueMemberInfo = EnumTraits<ValuesEnum>.EnumType
                    .GetMember(v.ToString())
                    .Single();

                return valueMemberInfo.GetCustomAttribute<InvalidEnumValueAttribute>() is null;
            })
            .ToArray();

        Assert.Equal(expectedValidValues, actualValidValues);
    }

    [Theory]
    [InlineData(ValuesEnum.Value1)]
    [InlineData(ValuesEnum.Value2)]
    [InlineData(ValuesEnum.Value3)]
    public void IsValidValue_ReturnsTrue(ValuesEnum value)
    {
        Assert.True(EnumTraits<ValuesEnum>.IsValidValue(value));
    }

    [Fact]
    public void IsValidValue_ReturnsFalse()
    {
        Assert.False(EnumTraits<ValuesEnum>.IsValidValue(ValuesEnum.Unknown));
    }

    [Fact]
    public void EnsureNoDuplicateValues_NoDuplicates_DoesNotThrow()
    {
        EnumTraits<ValuesEnum>.EnsureNoDuplicateValues();
    }

    public enum EnumWithDuplicateValues
    {
        Value1 = 1,
        Value2 = 1,
    }

    [Fact]
    public void EnsureNoDuplicateValues_HasDuplicates_Throws()
    {
        Assert.Throws<InvalidOperationException>(() =>
            EnumTraits<EnumWithDuplicateValues>.EnsureNoDuplicateValues()
            );
    }

    public enum ValuesWithDisplayNames
    {
        [InvalidEnumValue]
        Unknown = 0,

        [Display(Name = "Social Security Number")]
        SSN = 1,

        [Display(Name = "First Name")]
        FirstName = 2,

        Undecorated = 3,
    }

    [Theory]
    [InlineData(ValuesWithDisplayNames.SSN, "Social Security Number")]
    [InlineData(ValuesWithDisplayNames.FirstName, "First Name")]
    [InlineData(ValuesWithDisplayNames.Undecorated, "Undecorated")]
    public void GetDisplayName_ReturnsDisplayNames(ValuesWithDisplayNames value, string expectedDisplayName)
    {
        Assert.Equal(expectedDisplayName, EnumTraits<ValuesWithDisplayNames>.GetDisplayName(value));
    }

    [Fact]
    public void GetDisplayName_InvalidValue_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            EnumTraits<ValuesWithDisplayNames>.GetDisplayName(ValuesWithDisplayNames.Unknown)
            );
    }

    [Theory]
    [InlineData(ValuesWithDisplayNames.SSN, "Social Security Number")]
    [InlineData(ValuesWithDisplayNames.FirstName, "First Name")]
    [InlineData(ValuesWithDisplayNames.Undecorated, "Undecorated")]
    public void GetDisplayNameOrDefault_ReturnsDisplayNames(ValuesWithDisplayNames value, string expectedDisplayName)
    {
        Assert.Equal(expectedDisplayName, EnumTraits<ValuesWithDisplayNames>.GetDisplayNameOrDefault(value));
    }

    [Fact]
    public void GetDisplayNameOrDefault_InvalidValue_ReturnsNull()
    {
        Assert.Null(EnumTraits<ValuesWithDisplayNames>.GetDisplayNameOrDefault(ValuesWithDisplayNames.Unknown));
    }


    //
    // AllValues — additional edge cases
    //

    [Fact]
    public void AllValues_EmptyEnum_ReturnsEmpty()
    {
        Assert.Empty(EnumTraits<EmptyEnum>.AllValues);
    }

    [Fact]
    public void AllValues_IncludesValuesMarkedInvalid()
    {
        // AllValues must contain every defined member, including those marked [InvalidEnumValue].
        Assert.Contains(ValuesEnum.Unknown, EnumTraits<ValuesEnum>.AllValues);
    }


    //
    // ValidValues — additional edge cases
    //

    [Fact]
    public void ValidValues_EmptyEnum_ReturnsEmpty()
    {
        Assert.Empty(EnumTraits<EmptyEnum>.ValidValues);
    }

    public enum EnumWithNoInvalidValues
    {
        Alpha = 1,
        Beta = 2,
        Gamma = 3,
    }

    [Fact]
    public void ValidValues_NoInvalidValueAttributes_SameCountAsAllValues()
    {
        // When no [InvalidEnumValue] attributes are present, every value is valid.
        var allValues = EnumTraits<EnumWithNoInvalidValues>.AllValues;
        var validValues = EnumTraits<EnumWithNoInvalidValues>.ValidValues;

        Assert.Equal(allValues.Count, validValues.Count);
        Assert.All(allValues, v => Assert.True(EnumTraits<EnumWithNoInvalidValues>.IsValidValue(v)));
    }

    public enum AllInvalidEnum
    {
        [InvalidEnumValue]
        Sentinel1 = 0,
        [InvalidEnumValue]
        Sentinel2 = 1,
    }

    [Fact]
    public void ValidValues_AllMarkedInvalid_ReturnsEmpty()
    {
        Assert.Empty(EnumTraits<AllInvalidEnum>.ValidValues);
    }

    [Fact]
    public void ValidValues_AllMarkedInvalid_AllValuesStillReturnsAll()
    {
        // AllValues is unaffected by [InvalidEnumValue]; it always reflects Enum.GetValues<T>().
        Assert.Equal(2, EnumTraits<AllInvalidEnum>.AllValues.Count);
    }

    public enum MultipleInvalidSentinelsEnum
    {
        [InvalidEnumValue]
        Unknown = 0,
        [InvalidEnumValue]
        Invalid = -1,
        Valid1 = 1,
        Valid2 = 2,
    }

    [Fact]
    public void ValidValues_MultipleInvalidSentinels_ExcludesAll()
    {
        var validValues = EnumTraits<MultipleInvalidSentinelsEnum>.ValidValues;

        Assert.DoesNotContain(MultipleInvalidSentinelsEnum.Unknown, validValues);
        Assert.DoesNotContain(MultipleInvalidSentinelsEnum.Invalid, validValues);
        Assert.Equal(2, validValues.Count);
    }

    [Fact]
    public void ValidValues_IsSubsetOfAllValues()
    {
        var allValues = EnumTraits<ValuesEnum>.AllValues;
        Assert.All(
            EnumTraits<ValuesEnum>.ValidValues,
            v => Assert.Contains(v, allValues)
        );
    }


    //
    // IsValidValue — cast-to-undefined value
    //

    [Fact]
    public void IsValidValue_UndefinedCastValue_ReturnsFalse()
    {
        Assert.False(EnumTraits<ValuesEnum>.IsValidValue((ValuesEnum)99));
    }


    //
    // EnsureNoDuplicateValues — additional cases
    //

    [Fact]
    public void EnsureNoDuplicateValues_EmptyEnum_DoesNotThrow()
    {
        EnumTraits<EmptyEnum>.EnsureNoDuplicateValues();
    }

    [Fact]
    public void EnsureNoDuplicateValues_FlagsEnum_DoesNotThrow()
    {
        EnumTraits<HasFlagsEnum>.EnsureNoDuplicateValues();
    }

    [Fact]
    public void EnsureNoDuplicateValues_ExceptionMessage_ContainsDuplicateValue()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            EnumTraits<EnumWithDuplicateValues>.EnsureNoDuplicateValues()
        );
        Assert.Contains("1", ex.Message, StringComparison.Ordinal);
    }


    //
    // GetDisplayName — cast-to-undefined value and [Display] without Name
    //

    [Fact]
    public void GetDisplayName_UndefinedCastValue_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            EnumTraits<ValuesWithDisplayNames>.GetDisplayName((ValuesWithDisplayNames)99)
        );
    }

    public enum DisplayWithDescriptionOnlyEnum
    {
        [InvalidEnumValue]
        Unknown = 0,

        // [Display] with only Description (no Name) — GetName() returns null, must fall back to member name.
        [Display(Description = "A value with only a description")]
        NoNameValue = 1,
    }

    [Fact]
    public void GetDisplayName_DisplayAttributeWithNoName_FallsBackToMemberName()
    {
        Assert.Equal(
            nameof(DisplayWithDescriptionOnlyEnum.NoNameValue),
            EnumTraits<DisplayWithDescriptionOnlyEnum>.GetDisplayName(DisplayWithDescriptionOnlyEnum.NoNameValue)
        );
    }


    //
    // GetDisplayNameOrDefault — cast-to-undefined value and [Display] without Name
    //

    [Fact]
    public void GetDisplayNameOrDefault_UndefinedCastValue_ReturnsNull()
    {
        Assert.Null(EnumTraits<ValuesWithDisplayNames>.GetDisplayNameOrDefault((ValuesWithDisplayNames)99));
    }

    [Fact]
    public void GetDisplayNameOrDefault_DisplayAttributeWithNoName_FallsBackToMemberName()
    {
        Assert.Equal(
            nameof(DisplayWithDescriptionOnlyEnum.NoNameValue),
            EnumTraits<DisplayWithDescriptionOnlyEnum>.GetDisplayNameOrDefault(DisplayWithDescriptionOnlyEnum.NoNameValue)
        );
    }
}
