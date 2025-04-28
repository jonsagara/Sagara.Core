namespace Sagara.Core.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Take the left-most <paramref name="length"/> characters of the given string.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <param name="length">The maximum string length of the returned string.</param>
    /// <returns>
    /// <para>If <paramref name="value"/>.Length &gt;= <paramref name="length"/>, return the left-most <paramref name="length"/> characters.</para>
    /// <para>If <paramref name="value"/>.Length &lt; length, return <paramref name="value"/> unchanged.</para>
    /// </returns>
    public static string Left(this string value, int length)
    {
        Check.ThrowIfNull(value);
        ArgumentOutOfRangeException.ThrowIfLessThan(length, 0);

        return value.Length >= length
            ? value.Substring(startIndex: 0, length: length)
            : value;
    }

    /// <summary>
    /// Take the right-most <paramref name="length"/> characters of the given string.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <param name="length">The maximum string length of the returned string.</param>
    /// <returns>
    /// <para>If <paramref name="value"/>.Length &gt;= <paramref name="length"/>, return the right-most <paramref name="length"/> characters.</para>
    /// <para>If <paramref name="value"/>.Length &lt; length, return <paramref name="value"/> unchanged.</para>
    /// </returns>
    public static string Right(this string value, int length)
    {
        Check.ThrowIfNull(value);
        ArgumentOutOfRangeException.ThrowIfLessThan(length, 0);

        return value.Length >= length
            ? value.Substring(startIndex: value.Length - length, length: length)
            : value;
    }
}
