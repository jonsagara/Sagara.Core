using Xunit;

namespace Sagara.Core.Tests.CheckTests;

public class CheckTests
{
    //
    // ThrowIfNull
    //

    [Fact]
    public void ThrowIfNull_NullReference_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull((object?)null));
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull((string?)null));
    }

    [Fact]
    public void ThrowIfNull_NonNullReference_DoesNotThrow()
    {
        Check.ThrowIfNull("ohio");
        Check.ThrowIfNull(new object());
    }

    [Fact]
    public void ThrowIfNull_NullNullableValueType_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull((int?)null));
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull((double?)null));
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull((decimal?)null));
    }

    [Fact]
    public void ThrowIfNull_NonNullNullableValueType_DoesNotThrow()
    {
        Check.ThrowIfNull((int?)42);
        Check.ThrowIfNull((double?)42.0);
        Check.ThrowIfNull((decimal?)42.0m);
    }


    //
    // ThrowIfNullOrEmpty
    //

    [Fact]
    public void ThrowIfNullOrEmpty_NullString_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNullOrEmpty(null));

        string? value = null;
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNullOrEmpty(value));
    }

    [Fact]
    public void ThrowIfNullOrEmpty_EmptyString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Check.ThrowIfNullOrEmpty(""));
    }

    [Fact]
    public void ThrowIfNullOrEmpty_NonEmptyString_DoesNotThrow()
    {
        Check.ThrowIfNullOrEmpty("ohai i'm jim from the office");
    }


    //
    // ThrowIfNullOrWhiteSpace
    //

    [Fact]
    public void ThrowIfNullOrWhiteSpace_NullString_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNullOrWhiteSpace(null));

        string? value = null;
        Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNullOrWhiteSpace(value));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\t\r\n\n\n\r\n")]
    public void ThrowIfNullOrWhiteSpace_WhiteSpaceString_ThrowsArgumentException(string? value)
    {
        Assert.Throws<ArgumentException>(() => Check.ThrowIfNullOrWhiteSpace(value));
    }

    [Fact]
    public void ThrowIfNullOrWhiteSpace_NonEmptyString_DoesNotThrow()
    {
        Check.ThrowIfNullOrWhiteSpace("ohai i'm jim from the office");
    }
}
