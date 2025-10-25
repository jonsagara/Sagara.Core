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

    /// <summary>
    /// <para>Returns a new string in which the last occurrence of a specified substring in the current instance
    /// is replaced with another specified string, using the <see cref="StringComparison.Ordinal" /> comparison 
    /// type.</para>
    /// </summary>
    /// <param name="value">The string to operate on.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The string to replace the last occurence of <paramref name="oldValue"/>.</param>
    /// <returns>A string that is equivalent to the current string except that the last instance
    /// of <paramref name="oldValue"/> is replaced with <paramref name="newValue"/>. If <paramref name="oldValue"/>
    /// is not found in the current instance, the method returns the current instance unchanged.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is null or <paramref name="oldValue"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="oldValue"/> is the empty string ("").</exception>
    public static string ReplaceLastOccurrence(this string value, string oldValue, string? newValue)
        => ReplaceLastOccurrence(value: value, oldValue: oldValue, newValue: newValue, comparisonType: StringComparison.Ordinal);

    /// <summary>
    /// <para>Returns a new string in which the last occurrence of a specified substring in the current instance
    /// is replaced with another specified string, using the provided comparison type.</para>
    /// </summary>
    /// <param name="value">The string to operate on.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The string to replace the last occurence of <paramref name="oldValue"/>.</param>
    /// <returns>A string that is equivalent to the current string except that the last instance
    /// of <paramref name="oldValue"/> is replaced with <paramref name="newValue"/>. If <paramref name="oldValue"/>
    /// is not found in the current instance, the method returns the current instance unchanged.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is null or <paramref name="oldValue"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="oldValue"/> is the empty string ("").</exception>
    public static string ReplaceLastOccurrence(this string value, string oldValue, string? newValue, StringComparison comparisonType)
    {
        Check.ThrowIfNull(value);
        Check.ThrowIfNullOrEmpty(oldValue);

        // If the source value is the empty string, return the value as-is. There's nothing to replace.
        if (value.Length == 0)
        {
            return value;
        }

        // If newValue is null, treat it as the empty string. Callers use this to remove the oldValue.
        newValue ??= string.Empty;

        // Get the starting index of the last occurrence of oldValue in value.
        var ixLastOccurrence = value.LastIndexOf(value: oldValue, comparisonType: comparisonType);

        // If there are no occurrences of oldValue, return the string as-is.
        if (ixLastOccurrence == -1)
        {
            return value;
        }


        //
        // Replace the last occurrence of oldValue with newValue.
        //

        // Calculate the length of the result string.
        var resultStringLength = value.Length - oldValue.Length + newValue.Length;

        // Use string.Create to build the result string without intermediate allocations.
        var state = new ReplaceLastOccurrenceState(
            Value: value,
            IxLastOccurrence: ixLastOccurrence,
            OldValueLength: oldValue.Length,
            NewValue: newValue
            );

        return string.Create(length: resultStringLength, state: state, static (resultSpan, state) =>
        {
            var valueSpan = state.Value.AsSpan();

            // Copy everything up to, but no including, the last occurrence of oldValue.
            valueSpan[0..state.IxLastOccurrence].CopyTo(resultSpan);

            // Write to the buffer starting at the position just after the last character of value we copied
            //   into the buffer.
            var currentPosition = state.IxLastOccurrence;

            if (state.NewValue.Length > 0)
            {
                // Copy newValue into the buffer starting just after the last character of value that we copied
                //   into the buffer.
                state.NewValue.AsSpan().CopyTo(resultSpan[currentPosition..]);

                // Advance the current position to just after the last character of newValue we copied into the buffer.
                currentPosition += state.NewValue.Length;
            }

            // Copy the remainder of value, starting at the position just after the last occurrence of oldValue.
            // Remember that Range syntax allows you to start at the end of the string without throwing,
            //   so if a string is of length 3, the range [3..] is valid because you're starting the range at the
            //   end of the string. This will return an empty span because there are no subsequent characters.
            var afterOldValuePosition = state.IxLastOccurrence + state.OldValueLength;
            valueSpan[afterOldValuePosition..].CopyTo(resultSpan[currentPosition..]);
        });
    }


    //
    // Types
    //

    private readonly record struct ReplaceLastOccurrenceState(
        string Value,
        int IxLastOccurrence,
        int OldValueLength,
        string NewValue
        );
}
