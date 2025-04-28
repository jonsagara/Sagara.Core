using NodaTime;

namespace Sagara.Core.Time;

/// <summary>
/// Wrappers around NodaTime helpers to convert between time zones.
/// </summary>
public static class NodaTimeHelper
{
    /// <summary>
    /// Convert the UTC date/time into the equivalent date/time of the given time zone specified by the 
    /// IANA time zone Id.
    /// </summary>
    public static DateTimeOffset ToLocal(this DateTime utc, string ianaTimeZoneId)
    {
        Check.ThrowIfNullOrWhiteSpace(ianaTimeZoneId);

        // Ensure the UTC date/time is marked as UTC.
        utc = DateTime.SpecifyKind(utc, DateTimeKind.Utc);

        // Convert the source UTC date/time into a NodaTime instant.
        var instant = Instant.FromDateTimeUtc(utc);

        // Get the target time zone information from Tzdb.
        var targetZoneInfo = DateTimeZoneProviders.Tzdb[ianaTimeZoneId];

        // Convert the instant to the zone's local time.
        var zonedDateTime = instant.InZone(targetZoneInfo);

        // Return it as a BCL DateTimeOffset so that the result has access to the offset.
        return zonedDateTime.ToDateTimeOffset();
    }

    /// <summary>
    /// Convert the local date/time (local to the time zone specified by the IANA time zone Id) into the
    /// equivalent UTC date/time.
    /// </summary>
    public static DateTime ToUtc(this DateTime local, string ianaTimeZoneId)
    {
        Check.ThrowIfNullOrWhiteSpace(ianaTimeZoneId);

        // Convert BCL date/time to a NodaTime local date/time.
        var localDateTime = LocalDateTime.FromDateTime(local);

        // Get the source time zone information from Tzdb.
        var sourceZoneInfo = DateTimeZoneProviders.Tzdb[ianaTimeZoneId];

        // Make a zoned date/time out of the local date/time and the source time zone info.
        var zonedDateTime = sourceZoneInfo.AtLeniently(localDateTime);

        // Now we have something NodaTime can work with. Convert it to UTC.
        return zonedDateTime.ToDateTimeUtc();
    }
}
