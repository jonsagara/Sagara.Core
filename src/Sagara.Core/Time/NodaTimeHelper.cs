using NodaTime;

namespace Sagara.Core.Time;

public static class NodaTimeHelper
{
    /// <summary>
    /// Replace the &quot;if null&quot; version above with this version once we're sure that all accounts
    /// and users have TimeZoneIds set.
    /// </summary>
    public static DateTimeOffset ToLocal(this DateTime utc, string timeZoneId)
    {
        Check.NotEmpty(timeZoneId);

        // Ensure the UTC date/time is marked as UTC.
        utc = DateTime.SpecifyKind(utc, DateTimeKind.Utc);

        // Convert the source UTC date/time into a NodaTime instant.
        var instant = Instant.FromDateTimeUtc(utc);

        // Get the target time zone information from Tzdb.
        var targetZoneInfo = DateTimeZoneProviders.Tzdb[timeZoneId];

        // Convert the instant to the zone's local time.
        var zonedDateTime = instant.InZone(targetZoneInfo);

        // Return it as a BCL DateTimeOffset so that the result has access to the offset.
        return zonedDateTime.ToDateTimeOffset();
    }

    /// <summary>
    /// Replace the &quot;if null&quot; version above with this version once we're sure that all accounts
    /// and users have TimeZoneIds set.
    /// </summary>
    public static DateTime ToUtc(this DateTime local, string timeZoneId)
    {
        Check.NotEmpty(timeZoneId);

        // Convert BCL date/time to a NodaTime local date/time.
        var localDateTime = LocalDateTime.FromDateTime(local);

        // Get the source time zone information from Tzdb.
        var sourceZoneInfo = DateTimeZoneProviders.Tzdb[timeZoneId];

        // Make a zoned date/time out of the local date/time and the source time zone info.
        var zonedDateTime = sourceZoneInfo.AtLeniently(localDateTime);

        // Now we have something NodaTime can work with. Convert it to UTC.
        return zonedDateTime.ToDateTimeUtc();
    }
}
