#if NET10_0_OR_GREATER

using System.ComponentModel.DataAnnotations;
using Sagara.Core.Enums;

namespace Sagara.Core.Tests.EnumsTests;

public class EnumTraitsExtensionsTests
{
    //
    // IsEmpty
    //

    public enum EmptyEnum { }

    public enum NonEmptyEnum
    {
        SingleValue,
    }

    [Fact]
    public void IsEmpty_ReturnsTrue()
    {
        Assert.True(EmptyEnum.IsEmpty());
    }

    [Fact]
    public void IsEmpty_ReturnsFalse()
    {
        Assert.False(NonEmptyEnum.IsEmpty());
    }


    //
    // HasFlagsAttribute
    //

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
        Assert.True(HasFlagsEnum.HasFlagsAttribute());
    }

    [Fact]
    public void HasFlagsAttribute_ReturnsFalse()
    {
        Assert.False(NoFlagsEnum.HasFlagsAttribute());
    }


    //
    // GetEnumType
    //

    [Fact]
    public void GetEnumType_ReturnsCorrectType()
    {
        Assert.Equal(typeof(EmptyEnum), EmptyEnum.GetEnumType());
        Assert.Equal(typeof(NonEmptyEnum), NonEmptyEnum.GetEnumType());
        Assert.Equal(typeof(HasFlagsEnum), HasFlagsEnum.GetEnumType());
        Assert.Equal(typeof(NoFlagsEnum), NoFlagsEnum.GetEnumType());
    }


    //
    // GetAllValues / GetValidValues
    //

    public enum ValuesEnum
    {
        [InvalidEnumValue]
        Unknown = 0,
        Value1 = 1,
        Value2 = 2,
        Value3 = 3,
    }

    [Fact]
    public void GetAllValues_ReturnsAllValues()
    {
        var expected = Enum.GetValues<ValuesEnum>();
        Assert.Equal(expected, ValuesEnum.GetAllValues());
    }

    [Fact]
    public void GetValidValues_ReturnsValidValues()
    {
        var expected = new[] { ValuesEnum.Value1, ValuesEnum.Value2, ValuesEnum.Value3 };
        Assert.Equal(expected, ValuesEnum.GetValidValues());
    }


    //
    // IsValidValue
    //

    [Theory]
    [InlineData(ValuesEnum.Value1)]
    [InlineData(ValuesEnum.Value2)]
    [InlineData(ValuesEnum.Value3)]
    public void IsValidValue_ReturnsTrue(ValuesEnum value)
    {
        Assert.True(value.IsValidValue());
    }

    [Fact]
    public void IsValidValue_ReturnsFalse()
    {
        Assert.False(ValuesEnum.Unknown.IsValidValue());
    }


    //
    // EnsureNoDuplicateValues
    //

    [Fact]
    public void EnsureNoDuplicateValues_NoDuplicates_DoesNotThrow()
    {
        ValuesEnum.EnsureNoDuplicateValues();
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
            EnumWithDuplicateValues.EnsureNoDuplicateValues()
            );
    }


    //
    // GetDisplayName
    //

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
        Assert.Equal(expectedDisplayName, value.GetDisplayName());
    }

    [Fact]
    public void GetDisplayName_InvalidValue_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            ValuesWithDisplayNames.Unknown.GetDisplayName()
            );
    }


    //
    // GetDisplayNameOrDefault — non-nullable receiver (TEnum value)
    //

    [Theory]
    [InlineData(ValuesWithDisplayNames.SSN, "Social Security Number")]
    [InlineData(ValuesWithDisplayNames.FirstName, "First Name")]
    [InlineData(ValuesWithDisplayNames.Undecorated, "Undecorated")]
    public void GetDisplayNameOrDefault_NonNullable_ValidValue_ReturnsDisplayName(
        ValuesWithDisplayNames value, string expectedDisplayName)
    {
        Assert.Equal(expectedDisplayName, value.GetDisplayNameOrDefault());
    }

    [Fact]
    public void GetDisplayNameOrDefault_NonNullable_InvalidEnumValue_ReturnsNull()
    {
        // [InvalidEnumValue]-decorated member is not in the valid-values dictionary.
        Assert.Null(ValuesWithDisplayNames.Unknown.GetDisplayNameOrDefault());
    }

    [Fact]
    public void GetDisplayNameOrDefault_NonNullable_UndefinedCastValue_ReturnsNull()
    {
        // A cast-to-undefined numeric value is not in the valid-values dictionary.
        Assert.Null(((ValuesWithDisplayNames)99).GetDisplayNameOrDefault());
    }


    //
    // GetDisplayNameOrDefault — nullable receiver (TEnum? value)
    //

    [Fact]
    public void GetDisplayNameOrDefault_Nullable_NullValue_ReturnsNull()
    {
        ValuesWithDisplayNames? value = null;
        Assert.Null(value.GetDisplayNameOrDefault());
    }

    [Theory]
    [InlineData(ValuesWithDisplayNames.SSN, "Social Security Number")]
    [InlineData(ValuesWithDisplayNames.FirstName, "First Name")]
    [InlineData(ValuesWithDisplayNames.Undecorated, "Undecorated")]
    public void GetDisplayNameOrDefault_Nullable_ValidValue_ReturnsDisplayName(
        ValuesWithDisplayNames value, string expectedDisplayName)
    {
        ValuesWithDisplayNames? nullable = value;
        Assert.Equal(expectedDisplayName, nullable.GetDisplayNameOrDefault());
    }

    [Fact]
    public void GetDisplayNameOrDefault_Nullable_InvalidEnumValue_ReturnsNull()
    {
        // [InvalidEnumValue]-decorated member is not in the valid-values dictionary.
        ValuesWithDisplayNames? value = ValuesWithDisplayNames.Unknown;
        Assert.Null(value.GetDisplayNameOrDefault());
    }

    [Fact]
    public void GetDisplayNameOrDefault_Nullable_UndefinedCastValue_ReturnsNull()
    {
        // A cast-to-undefined numeric value is not in the valid-values dictionary.
        ValuesWithDisplayNames? value = (ValuesWithDisplayNames)99;
        Assert.Null(value.GetDisplayNameOrDefault());
    }
}

#endif
