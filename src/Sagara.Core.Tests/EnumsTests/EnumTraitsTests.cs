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
    public void IsValid_ReturnsTrue(ValuesEnum value)
    {
        Assert.True(EnumTraits<ValuesEnum>.IsValid(value));
    }

    [Fact]
    public void IsValid_ReturnsFalse()
    {
        Assert.False(EnumTraits<ValuesEnum>.IsValid(ValuesEnum.Unknown));
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
}
