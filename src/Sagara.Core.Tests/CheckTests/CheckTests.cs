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

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\t\r\n\n\n\r\n")]
    [InlineData("ohai i'm jim from the office")]
    public void ThrowIfNullOrEmpty_NonEmptyString_DoesNotThrow(string value)
    {
        Check.ThrowIfNullOrEmpty(value);
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


    //
    // ThrowIfEmptyGuid
    //

    [Fact]
    public void ThrowIfEmptyGuid_EmptyGuid_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Check.ThrowIfEmptyGuid(Guid.Empty));
    }

    [Fact]
    public void ThrowIfEmptyGuid_NonEmptyGuid_DoesNotThrow()
    {
        Check.ThrowIfEmptyGuid(Guid.Parse("c5c563a9-5f75-4518-947c-434d0fd5d89a"));
    }


    //
    // ThrowIfEmptyCollection
    //

    [Fact]
    public void ThrowIfEmptyCollection_EmptyCollection_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Check.ThrowIfEmptyCollection(Array.Empty<string>()));
        Assert.Throws<ArgumentException>(() => Check.ThrowIfEmptyCollection(new List<int>()));
        Assert.Throws<ArgumentException>(() => Check.ThrowIfEmptyCollection(new List<object>()));
    }

    [Fact]
    public void ThrowIfEmptyCollection_NonEmptyCollection_DoesNotThrow()
    {
        Check.ThrowIfEmptyCollection(["hi"]);
        Check.ThrowIfEmptyCollection(new List<int> { 42 });
        Check.ThrowIfEmptyCollection(new List<object> { new object() });
    }


    //
    // ThrowIfContainsNullOrWhiteSpaceValues
    //

    [Fact]
    public void ThrowIfContainsNullOrWhiteSpaceValues_ContainsNullOrWhiteSpaceValues_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Check.ThrowIfContainsNullOrWhiteSpaceValues(new string?[] { "", null, " ", "\t\r\n\r\r\n" }));
        Assert.Throws<ArgumentException>(() => Check.ThrowIfContainsNullOrWhiteSpaceValues(new List<string?>() { "", null, " ", "\t\r\n\r\r\n" }));
    }

    [Fact]
    public void ThrowIfContainsNullOrWhiteSpaceValues_ContainsNoNullOrWhiteSpaceValues_DoesNotThrow()
    {
        Check.ThrowIfContainsNullOrWhiteSpaceValues(new string?[] { "apple", "pear", "banana" });
        Check.ThrowIfContainsNullOrWhiteSpaceValues(new List<string?>() { "apple", "pear", "banana" });
    }


    //
    // ThrowIfContainsNullValues
    //

    [Fact]
    public void ThrowIfContainsNullValues_ContainsNullValues_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Check.ThrowIfContainsNullValues(new string?[] { "", null, " ", "California" }));
        Assert.Throws<ArgumentException>(() => Check.ThrowIfContainsNullValues(new List<Exception?>() { new Exception(), null, new ArgumentException() }));
    }

    [Fact]
    public void ThrowIfContainsNullValues_ContainsNoNullValues_DoesNotThrow()
    {
        Check.ThrowIfContainsNullValues(new string?[] { "apple", "pear", "banana" });
        Check.ThrowIfContainsNullValues(new List<Exception?>() { new Exception(), new NullReferenceException() });
    }


    //
    // ThrowIfLessThan
    //

    [Fact]
    public void ThrowIfLessThan_IntValueLessThan_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThan(0, 5));
    }

    [Fact]
    public void ThrowIfLessThan_DoubleValueLessThan_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThan(0.0, 5.0));
    }

    [Fact]
    public void ThrowIfLessThan_DecimalValueLessThan_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThan(0.0m, 5.0m));
    }

    [Fact]
    public void ThrowIfLessThan_IntValueGreaterThanOrEqual_DoesNotThrow()
    {
        Check.ThrowIfLessThan(500, 100);
        Check.ThrowIfLessThan(500, 500);
    }

    [Fact]
    public void ThrowIfLessThan_DoubleValueGreaterThanOrEqual_DoesNotThrow()
    {
        Check.ThrowIfLessThan(3.14, 1.57);
        Check.ThrowIfLessThan(3.14, 3.14);
    }

    [Fact]
    public void ThrowIfLessThan_DecimalValueGreaterThanOrEqual_DoesNotThrow()
    {
        Check.ThrowIfLessThan(900.12m, 900.11m);
        Check.ThrowIfLessThan(900.12m, 900.12m);
    }


    //
    // ThrowIfLessThanOrEqual
    //

    [Fact]
    public void ThrowIfLessThanOrEqual_IntValueLessThanOrEqual_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThanOrEqual(0, 5));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThanOrEqual(5, 5));
    }

    [Fact]
    public void ThrowIfLessThanOrEqual_DoubleValueLessThanOrEqual_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThanOrEqual(0.0, 5.0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThanOrEqual(5.0, 5.0));
    }

    [Fact]
    public void ThrowIfLessThanOrEqual_DecimalValueLessThanOrEqual_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThanOrEqual(0.0m, 5.0m));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfLessThanOrEqual(5.0m, 5.0m));
    }

    [Fact]
    public void ThrowIfLessThanOrEqual_IntValueGreaterThan_DoesNotThrow()
    {
        Check.ThrowIfLessThan(500, 100);
    }

    [Fact]
    public void ThrowIfLessThanOrEqual_DoubleValueGreaterThan_DoesNotThrow()
    {
        Check.ThrowIfLessThan(3.14, 1.57);
    }

    [Fact]
    public void ThrowIfLessThanOrEqual_DecimalValueGreaterThan_DoesNotThrow()
    {
        Check.ThrowIfLessThan(900.12m, 900.11m);
    }


    //
    // ThrowIfGreaterThan
    //

    [Fact]
    public void ThrowIfGreaterThan_IntValueGreaterThan_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThan(5, 1));
    }

    [Fact]
    public void ThrowIfGreaterThan_DoubleValueGreaterThan_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThan(5.0, 1.0));
    }

    [Fact]
    public void ThrowIfGreaterThan_DecimalValueGreaterThan_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThan(5.0m, 1.0m));
    }

    [Fact]
    public void ThrowIfGreaterThan_IntValueLessThanOrEqual_DoesNotThrow()
    {
        Check.ThrowIfGreaterThan(100, 500);
        Check.ThrowIfGreaterThan(500, 500);
    }

    [Fact]
    public void ThrowIfGreaterThan_DoubleValueLessThanOrEqual_DoesNotThrow()
    {
        Check.ThrowIfGreaterThan(1.57, 3.14);
        Check.ThrowIfGreaterThan(3.14, 3.14);
    }

    [Fact]
    public void ThrowIfGreaterThan_DecimalValueLessThanOrEqual_DoesNotThrow()
    {
        Check.ThrowIfGreaterThan(900.11m, 900.12m);
        Check.ThrowIfGreaterThan(900.12m, 900.12m);
    }


    //
    // ThrowIfGreaterThanOrEqual
    //

    [Fact]
    public void ThrowIfGreaterThanOrEqual_IntValueGreaterThanOrEqual_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThanOrEqual(5, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThanOrEqual(5, 5));
    }

    [Fact]
    public void ThrowIfGreaterThanOrEqual_DoubleValueGreaterThanOrEqual_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThanOrEqual(5.0, 1.0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThanOrEqual(5.0, 5.0));
    }

    [Fact]
    public void ThrowIfGreaterThanOrEqual_DecimalValueGreaterThanOrEqual_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThanOrEqual(5.0m, 1.0m));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfGreaterThanOrEqual(5.0m, 5.0m));
    }

    [Fact]
    public void ThrowIfGreaterThanOrEqual_IntValueLessThan_DoesNotThrow()
    {
        Check.ThrowIfGreaterThanOrEqual(100, 500);
    }

    [Fact]
    public void ThrowIfGreaterThanOrEqual_DoubleValueLessThan_DoesNotThrow()
    {
        Check.ThrowIfGreaterThanOrEqual(1.57, 3.14);
    }

    [Fact]
    public void ThrowIfGreaterThanOrEqual_DecimalValueLessThan_DoesNotThrow()
    {
        Check.ThrowIfGreaterThanOrEqual(900.11m, 900.12m);
    }


    //
    // ThrowIfEqual
    //

    [Fact]
    public void ThrowIfEqual_ValuesEqual_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfEqual(5, 5));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfEqual(5.0, 5.0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfEqual(5.0m, 5.0m));
    }

    [Fact]
    public void ThrowIfEqual_ValuesNotEqual_DoesNotThrow()
    {
        Check.ThrowIfEqual(5, -5);
        Check.ThrowIfEqual(5.0, -5.0);
        Check.ThrowIfEqual(5.0m, -5.0m);
    }


    //
    // ThrowIfNotEqual
    //

    [Fact]
    public void ThrowIfNotEqual_ValuesNotEqual_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfNotEqual(5, -5));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfNotEqual(5.0, -5.0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Check.ThrowIfNotEqual(5.0m, -5.0m));
    }

    [Fact]
    public void ThrowIfNotEqual_ValuesEqual_DoesNotThrow()
    {
        Check.ThrowIfNotEqual(5, 5);
        Check.ThrowIfNotEqual(5.0, 5.0);
        Check.ThrowIfNotEqual(5.0m, 5.0m);
    }
}
