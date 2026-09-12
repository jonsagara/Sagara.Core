using Sagara.Core.Time;
using Xunit;

namespace Sagara.Core.Tests.TimeTests;

public class NodaTimeHelperTests
{
    //
    // ToLocal - argument validation
    //

    [Fact]
    public void ToLocal_NullIanaTimeZoneId_ThrowsArgumentNullException()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<ArgumentNullException>(() => utc.ToLocal(null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void ToLocal_WhiteSpaceIanaTimeZoneId_ThrowsArgumentException(string ianaTimeZoneId)
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<ArgumentException>(() => utc.ToLocal(ianaTimeZoneId));
    }

    [Fact]
    public void ToLocal_UnknownIanaTimeZoneId_ThrowsDateTimeZoneNotFoundException()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<NodaTime.TimeZones.DateTimeZoneNotFoundException>(() => utc.ToLocal("Not/A_Real_Zone"));
    }


    //
    // ToLocal - standard time
    //

    [Fact]
    public void ToLocal_LosAngelesInJanuary_ReturnsExpectedClockTimeAndOffset()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 1, 15, 4, 0, 0), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(-8), result.Offset);
    }

    [Fact]
    public void ToLocal_NewYorkInJanuary_ReturnsExpectedClockTimeAndOffset()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("America/New_York");

        Assert.Equal(new DateTime(2024, 1, 15, 7, 0, 0), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(-5), result.Offset);
    }


    //
    // ToLocal - daylight saving time
    //

    [Fact]
    public void ToLocal_LosAngelesInJuly_ReturnsExpectedClockTimeAndOffset()
    {
        var utc = new DateTime(2024, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 7, 15, 5, 0, 0), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(-7), result.Offset);
    }

    [Fact]
    public void ToLocal_NewYorkInJuly_ReturnsExpectedClockTimeAndOffset()
    {
        var utc = new DateTime(2024, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("America/New_York");

        Assert.Equal(new DateTime(2024, 7, 15, 8, 0, 0), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(-4), result.Offset);
    }


    //
    // ToLocal - zones without daylight saving time
    //

    [Fact]
    public void ToLocal_Utc_ReturnsSameClockTimeWithZeroOffset()
    {
        var utc = new DateTime(2024, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("UTC");

        Assert.Equal(utc, result.DateTime);
        Assert.Equal(TimeSpan.Zero, result.Offset);
    }

    [Fact]
    public void ToLocal_Tokyo_ReturnsExpectedClockTimeAndOffset()
    {
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("Asia/Tokyo");

        Assert.Equal(new DateTime(2024, 1, 15, 21, 0, 0), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(9), result.Offset);
    }


    //
    // ToLocal - the returned DateTimeOffset represents the same instant as the source UTC value
    //

    [Fact]
    public void ToLocal_ResultRepresentsSameInstantAsSourceUtc()
    {
        var utc = new DateTime(2024, 7, 15, 12, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("America/Los_Angeles");

        Assert.Equal(utc, result.UtcDateTime);
    }


    //
    // ToLocal - DateTimeKind coercion
    //

    [Fact]
    public void ToLocal_UnspecifiedKind_TreatsValueAsUtc()
    {
        var unspecified = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Unspecified);
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal(utc.ToLocal("America/Los_Angeles"), unspecified.ToLocal("America/Los_Angeles"));
    }

    [Fact]
    public void ToLocal_LocalKind_IsInterpretedAsUtcClockTime()
    {
        // Even though this instance is DateTimeKind.Local, the method re-marks it as UTC
        // rather than converting from the machine's local time zone.
        var local = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Local);
        var utc = new DateTime(2024, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        Assert.Equal(utc.ToLocal("America/Los_Angeles"), local.ToLocal("America/Los_Angeles"));
    }


    //
    // ToLocal - DST transition boundary (America/Los_Angeles, spring forward 2024-03-10 02:00 local -> 03:00 local)
    //

    [Fact]
    public void ToLocal_LosAngelesJustBeforeSpringForwardTransition_ReturnsPstOffset()
    {
        // 2024-03-10 09:59:59 UTC == 2024-03-10 01:59:59 PST, one second before the clocks spring forward.
        var utc = new DateTime(2024, 3, 10, 9, 59, 59, DateTimeKind.Utc);

        var result = utc.ToLocal("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 3, 10, 1, 59, 59), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(-8), result.Offset);
    }

    [Fact]
    public void ToLocal_LosAngelesAtSpringForwardTransition_ReturnsPdtOffset()
    {
        // 2024-03-10 10:00:00 UTC == 2024-03-10 03:00:00 PDT, the instant the clocks spring forward.
        var utc = new DateTime(2024, 3, 10, 10, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 3, 10, 3, 0, 0), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(-7), result.Offset);
    }

    [Fact]
    public void ToLocal_LosAngelesJustBeforeFallBackTransition_ReturnsPdtOffset()
    {
        // 2024-11-03 08:59:59 UTC == 2024-11-03 01:59:59 PDT, one second before the clocks fall back.
        var utc = new DateTime(2024, 11, 3, 8, 59, 59, DateTimeKind.Utc);

        var result = utc.ToLocal("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 11, 3, 1, 59, 59), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(-7), result.Offset);
    }

    [Fact]
    public void ToLocal_LosAngelesAtFallBackTransition_ReturnsPstOffset()
    {
        // 2024-11-03 09:00:00 UTC == 2024-11-03 01:00:00 PST (post fall-back), the instant the clocks fall back.
        var utc = new DateTime(2024, 11, 3, 9, 0, 0, DateTimeKind.Utc);

        var result = utc.ToLocal("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 11, 3, 1, 0, 0), result.DateTime);
        Assert.Equal(TimeSpan.FromHours(-8), result.Offset);
    }


    //
    // ToUtc - argument validation
    //

    [Fact]
    public void ToUtc_NullIanaTimeZoneId_ThrowsArgumentNullException()
    {
        var local = new DateTime(2024, 1, 15, 4, 0, 0);

        Assert.Throws<ArgumentNullException>(() => local.ToUtc(null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void ToUtc_WhiteSpaceIanaTimeZoneId_ThrowsArgumentException(string ianaTimeZoneId)
    {
        var local = new DateTime(2024, 1, 15, 4, 0, 0);

        Assert.Throws<ArgumentException>(() => local.ToUtc(ianaTimeZoneId));
    }

    [Fact]
    public void ToUtc_UnknownIanaTimeZoneId_ThrowsDateTimeZoneNotFoundException()
    {
        var local = new DateTime(2024, 1, 15, 4, 0, 0);

        Assert.Throws<NodaTime.TimeZones.DateTimeZoneNotFoundException>(() => local.ToUtc("Not/A_Real_Zone"));
    }


    //
    // ToUtc - standard time
    //

    [Fact]
    public void ToUtc_LosAngelesInJanuary_ReturnsExpectedUtc()
    {
        var local = new DateTime(2024, 1, 15, 4, 0, 0);

        var result = local.ToUtc("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 1, 15, 12, 0, 0), result);
    }

    [Fact]
    public void ToUtc_NewYorkInJanuary_ReturnsExpectedUtc()
    {
        var local = new DateTime(2024, 1, 15, 7, 0, 0);

        var result = local.ToUtc("America/New_York");

        Assert.Equal(new DateTime(2024, 1, 15, 12, 0, 0), result);
    }


    //
    // ToUtc - daylight saving time
    //

    [Fact]
    public void ToUtc_LosAngelesInJuly_ReturnsExpectedUtc()
    {
        var local = new DateTime(2024, 7, 15, 5, 0, 0);

        var result = local.ToUtc("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 7, 15, 12, 0, 0), result);
    }

    [Fact]
    public void ToUtc_NewYorkInJuly_ReturnsExpectedUtc()
    {
        var local = new DateTime(2024, 7, 15, 8, 0, 0);

        var result = local.ToUtc("America/New_York");

        Assert.Equal(new DateTime(2024, 7, 15, 12, 0, 0), result);
    }


    //
    // ToUtc - zones without daylight saving time
    //

    [Fact]
    public void ToUtc_Utc_ReturnsSameValue()
    {
        var local = new DateTime(2024, 7, 15, 12, 0, 0);

        var result = local.ToUtc("UTC");

        Assert.Equal(local, result);
    }

    [Fact]
    public void ToUtc_Tokyo_ReturnsExpectedUtc()
    {
        var local = new DateTime(2024, 1, 15, 21, 0, 0);

        var result = local.ToUtc("Asia/Tokyo");

        Assert.Equal(new DateTime(2024, 1, 15, 12, 0, 0), result);
    }


    //
    // ToUtc - returned DateTimeKind
    //

    [Fact]
    public void ToUtc_ReturnsUtcDateTimeKind()
    {
        var local = new DateTime(2024, 1, 15, 4, 0, 0);

        var result = local.ToUtc("America/Los_Angeles");

        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }


    //
    // ToUtc - leniency around DST transitions (America/Los_Angeles, spring forward/fall back 2024)
    //

    [Fact]
    public void ToUtc_SkippedLocalTimeDuringSpringForward_DoesNotThrow()
    {
        // 2024-03-10 02:30:00 local never occurred in America/Los_Angeles; clocks jumped from 02:00 to 03:00.
        // AtLeniently resolves this by shifting forward by the gap instead of throwing.
        var local = new DateTime(2024, 3, 10, 2, 30, 0);

        var result = local.ToUtc("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 3, 10, 10, 30, 0), result);
    }

    [Fact]
    public void ToUtc_AmbiguousLocalTimeDuringFallBack_DoesNotThrow()
    {
        // 2024-11-03 01:30:00 local occurred twice in America/Los_Angeles (once as PDT, once as PST).
        // AtLeniently resolves the ambiguity by choosing the earlier (pre-transition) offset instead of throwing.
        var local = new DateTime(2024, 11, 3, 1, 30, 0);

        var result = local.ToUtc("America/Los_Angeles");

        Assert.Equal(new DateTime(2024, 11, 3, 8, 30, 0), result);
    }


    //
    // ToUtc / ToLocal - round trip
    //

    [Fact]
    public void ToLocalThenToUtc_RoundTripsToOriginalUtcValue()
    {
        var utc = new DateTime(2024, 7, 15, 12, 34, 56, DateTimeKind.Utc);

        var local = utc.ToLocal("America/Los_Angeles");
        var roundTripped = local.DateTime.ToUtc("America/Los_Angeles");

        Assert.Equal(utc, roundTripped);
    }


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
