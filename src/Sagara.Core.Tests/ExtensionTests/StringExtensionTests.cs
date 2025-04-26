using Sagara.Core.Extensions;
using Xunit;

namespace Sagara.Core.Tests.ExtensionTests;

public class StringExtensionTests
{
    //
    // Left
    //

    [Theory]
    [InlineData("Hello")]
    [InlineData("a")]
    [InlineData("Hello, World! This is amazing!")]
    public void Left_LengthEquals_ReturnsWholeString(string value)
    {
        Assert.Equal(expected: value, actual: value.Left(value.Length));
    }

    [Theory]
    [InlineData("Hello", 30)]
    [InlineData("a", 52)]
    [InlineData("Hello, World! This is amazing!", int.MaxValue)]
    public void Left_LengthGreaterThanValue_ReturnsWholeString(string value, int length)
    {
        Assert.Equal(expected: value, actual: value.Left(length));
    }

    [Theory]
    [InlineData("Hello")]
    [InlineData("a")]
    [InlineData("Hello, World! This is amazing!")]
    public void Left_Length1_ReturnsFirstCharacter(string value)
    {
        Assert.Equal(expected: $"{value[0]}", actual: value.Left(1));
    }

    [Theory]
    [InlineData("Hello", 3)]
    [InlineData("a", 1)]
    [InlineData("Hello, World! This is amazing!", 10)]
    public void Left_LengthN_ReturnsFirstNCharacters(string value, int length)
    {
        Assert.Equal(expected: value[..length], actual: value.Left(length));
    }

    [Theory]
    [InlineData("Hello")]
    [InlineData("a")]
    [InlineData("Hello, World! This is amazing!")]
    public void Left_Length0_ReturnsEmptyString(string value)
    {
        Assert.Equal(expected: "", actual: value.Left(0));
    }

    [Theory]
    [InlineData("Hello", -1)]
    [InlineData("a", -2)]
    [InlineData("Hello, World! This is amazing!", int.MinValue)]
    public void Left_LengthLessThan0_Throws(string value, int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => value.Left(length));
    }

    [Fact]
    public void Left_NullValue_Throws()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        // Justification: testing that null throws.
        Assert.Throws<ArgumentNullException>(() => 
            ((string?)null).Left(12)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }


    //
    // Right
    //

    [Theory]
    [InlineData("Hello")]
    [InlineData("a")]
    [InlineData("Hello, World! This is amazing!")]
    public void Right_LengthEquals_ReturnsWholeString(string value)
    {
        Assert.Equal(expected: value, actual: value.Right(value.Length));
    }

    [Theory]
    [InlineData("Hello", 30)]
    [InlineData("a", 52)]
    [InlineData("Hello, World! This is amazing!", int.MaxValue)]
    public void Right_LengthGreaterThanValue_ReturnsWholeString(string value, int length)
    {
        Assert.Equal(expected: value, actual: value.Right(length));
    }

    [Theory]
    [InlineData("Hello")]
    [InlineData("a")]
    [InlineData("Hello, World! This is amazing!")]
    public void Right_Length1_ReturnsLastCharacter(string value)
    {
        Assert.Equal(expected: $"{value[^1]}", actual: value.Right(1));
    }

    [Theory]
    [InlineData("Hello", 3)]
    [InlineData("a", 1)]
    [InlineData("Hello, World! This is amazing!", 10)]
    public void Right_LengthN_ReturnsLastNCharacters(string value, int length)
    {
        Assert.Equal(expected: value[^length..], actual: value.Right(length));
    }

    [Theory]
    [InlineData("Hello")]
    [InlineData("a")]
    [InlineData("Hello, World! This is amazing!")]
    public void Right_Length0_ReturnsEmptyString(string value)
    {
        Assert.Equal(expected: "", actual: value.Right(0));
    }

    [Theory]
    [InlineData("Hello", -1)]
    [InlineData("a", -2)]
    [InlineData("Hello, World! This is amazing!", int.MinValue)]
    public void Right_LengthLessThan0_Throws(string value, int length)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => value.Right(length));
    }

    [Fact]
    public void Right_NullValue_Throws()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        // Justification: testing that null throws.
        Assert.Throws<ArgumentNullException>(() =>
            ((string?)null).Right(12)
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}
