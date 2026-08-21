using Sagara.Core.Time;
using Xunit;

namespace Sagara.Core.Tests.TimeTests;

public class NodaTimeHelperTests
{
    //
    // GetTimeZoneName - argument validation
    //

    [Fact]
    public void GetTimeZoneName_NullIanaTimeZoneId_ThrowsArgumentNullException()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<ArgumentNullException>(() => utc.GetTimeZoneName(null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void GetTimeZoneName_WhiteSpaceIanaTimeZoneId_ThrowsArgumentException(string ianaTimeZoneId)
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<ArgumentException>(() => utc.GetTimeZoneName(ianaTimeZoneId));
    }

    [Fact]
    public void GetTimeZoneName_UnknownIanaTimeZoneId_ThrowsDateTimeZoneNotFoundException()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<NodaTime.TimeZones.DateTimeZoneNotFoundException>(() => utc.GetTimeZoneName("Not/A_Real_Zone"));
    }


    //
    // GetTimeZoneName - standard time
    //

    [Fact]
    public void GetTimeZoneName_LosAngelesInJanuary_ReturnsPST()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("PST", utc.GetTimeZoneName("America/Los_Angeles"));
    }

    [Fact]
    public void GetTimeZoneName_NewYorkInJanuary_ReturnsEST()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("EST", utc.GetTimeZoneName("America/New_York"));
    }

    [Fact]
    public void GetTimeZoneName_ChicagoInJanuary_ReturnsCST()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("CST", utc.GetTimeZoneName("America/Chicago"));
    }

    [Fact]
    public void GetTimeZoneName_DenverInJanuary_ReturnsMST()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("MST", utc.GetTimeZoneName("America/Denver"));
    }


    //
    // GetTimeZoneName - daylight saving time
    //

    [Fact]
    public void GetTimeZoneName_LosAngelesInJuly_ReturnsPDT()
    {
        var utc = new DateTime(2024, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("PDT", utc.GetTimeZoneName("America/Los_Angeles"));
    }

    [Fact]
    public void GetTimeZoneName_NewYorkInJuly_ReturnsEDT()
    {
        var utc = new DateTime(2024, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("EDT", utc.GetTimeZoneName("America/New_York"));
    }


    //
    // GetTimeZoneName - zones without daylight saving time
    //

    [Fact]
    public void GetTimeZoneName_TokyoInJanuary_ReturnsJST()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("JST", utc.GetTimeZoneName("Asia/Tokyo"));
    }

    [Fact]
    public void GetTimeZoneName_TokyoInJuly_ReturnsJST()
    {
        var utc = new DateTime(2024, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("JST", utc.GetTimeZoneName("Asia/Tokyo"));
    }

    [Fact]
    public void GetTimeZoneName_Utc_ReturnsUTC()
    {
        var utc = new DateTime(2024, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal("UTC", utc.GetTimeZoneName("UTC"));
    }


    //
    // GetTimeZoneName - DateTimeKind coercion
    //

    [Fact]
    public void GetTimeZoneName_UnspecifiedKind_TreatsValueAsUtc()
    {
        var unspecified = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Unspecified);
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal(utc.GetTimeZoneName("America/Los_Angeles"), unspecified.GetTimeZoneName("America/Los_Angeles"));
    }

    [Fact]
    public void GetTimeZoneName_LocalKind_IsInterpretedAsUtcClockTime()
    {
        // Even though this instance is DateTimeKind.Local, the method re-marks it as UTC
        // rather than converting from the machine's local time zone.
        var local = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Local);
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal(utc.GetTimeZoneName("America/Los_Angeles"), local.GetTimeZoneName("America/Los_Angeles"));
    }


    //
    // GetTimeZoneName - DST transition boundary (America/Los_Angeles, spring forward 2024-03-10 02:00 local -> 03:00 local)
    //

    [Fact]
    public void GetTimeZoneName_LosAngelesJustBeforeSpringForwardTransition_ReturnsPST()
    {
        // 2024-03-10 09:59:59 UTC == 2024-03-10 01:59:59 PST, one second before the clocks spring forward.
        var utc = new DateTime(2024, 3, 10, 9, 59, 59, DateTimeKind.Utc);

        Assert.Equal("PST", utc.GetTimeZoneName("America/Los_Angeles"));
    }

    [Fact]
    public void GetTimeZoneName_LosAngelesAtSpringForwardTransition_ReturnsPDT()
    {
        // 2024-03-10 10:00:00 UTC == 2024-03-10 03:00:00 PDT, the instant the clocks spring forward.
        var utc = new DateTime(2024, 3, 10, 10, 0, 0, DateTimeKind.Utc);

        Assert.Equal("PDT", utc.GetTimeZoneName("America/Los_Angeles"));
    }

    [Fact]
    public void GetTimeZoneName_LosAngelesJustBeforeFallBackTransition_ReturnsPDT()
    {
        // 2024-11-03 08:59:59 UTC == 2024-11-03 01:59:59 PDT, one second before the clocks fall back.
        var utc = new DateTime(2024, 11, 3, 8, 59, 59, DateTimeKind.Utc);

        Assert.Equal("PDT", utc.GetTimeZoneName("America/Los_Angeles"));
    }

    [Fact]
    public void GetTimeZoneName_LosAngelesAtFallBackTransition_ReturnsPST()
    {
        // 2024-11-03 09:00:00 UTC == 2024-11-03 01:00:00 PST (post fall-back), the instant the clocks fall back.
        var utc = new DateTime(2024, 11, 3, 9, 0, 0, DateTimeKind.Utc);

        Assert.Equal("PST", utc.GetTimeZoneName("America/Los_Angeles"));
    }
}
