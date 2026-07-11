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
    /// <param name="comparisonType">The type of <see cref="StringComparison"/> to use while performing the replacement.</param>
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

    /// <summary>
    /// <para>If <paramref name="text" />.Length &gt; <paramref name="maxLengthInChars"/>, split the text into 
    /// multiple chunks, each of which is &lt;= <paramref name="maxLengthInChars"/>.</para>
    /// <para>There will always be at least one chunk returned.</para>
    /// </summary>
    /// <param name="text">The full text.</param>
    /// <param name="maxLengthInChars">The maximum length of a chunk of text. Must be &gt;= 11 to allow
    /// for the appended chunk numbers.</param>
    public static string[] ChunkText(this string text, int maxLengthInChars)
    {
        Check.ThrowIfNullOrWhiteSpace(text);
        Check.ThrowIfLessThan(maxLengthInChars, 11);

        List<string> statusChunks = [];

        // Mastodon rate limiting provides a theoretical maximum of 300 posts in 5 minutes. With a 500 character
        //   limit, that would amount to 300 posts * 500 characters/post == 150,000 characters maximum that we
        //   could split up into separate chunks and submit.
        // This means our status numbering, when including the leading space, would have a maximum length of 10
        //   characters: " (300/300)"
        // Allow for that extra 10 characters when computing each status chunk's actual size.
        const int statusNumbersMaxLength = 10;
        var statusMaxLengthInChars = maxLengthInChars - statusNumbersMaxLength;

        var ixCurrentChunkStart = 0;

        while (ixCurrentChunkStart < text.Length)
        {
            // Find the next chunk's length, which is the minimum of the per-post character limit minus the
            //   status numbering, -OR- the length of the remaining text.
            var chunkLength = Math.Min(statusMaxLengthInChars, text.Length - ixCurrentChunkStart);

            // Calculate the starting index of the NEXT chunk (i.e., one character past the end of the current chunk).
            var ixNextChunkStart = ixCurrentChunkStart + chunkLength;

            // Try to avoid splitting in the middle of a word, but if:
            //
            // * The CURRENT chunk end position and NEXT chunk start position both fall within the full text
            // -AND-
            // * The last character of the CURRENT chunk is NOT a space
            // -AND-
            // * The first character of the NEXT chunk is NOT a space
            //
            // ... then we're in the middle of a word, and we must search for the closest preceding space character
            //   to split on.
            if (ixNextChunkStart < text.Length && text[ixNextChunkStart - 1] != ' ' && text[ixNextChunkStart] != ' ')
            {
                // Starting at the end of the current chunk, search backward for the closest preceding space character.
                var ixClosestPrecedingSpace = text.LastIndexOf(value: ' ', startIndex: ixNextChunkStart - 1);

                if (ixClosestPrecedingSpace == -1 || ixClosestPrecedingSpace <= ixCurrentChunkStart)
                {
                    // * No space character found: this chunk is string of non-white space characters, and we
                    //   have to break it up to adhere to the post size limit.
                    // -OR-
                    // * The chunk starts with a space character, and it doesn't make sense to split the
                    //   string there because that would result in an empty status chunk.
                    // -OR-
                    // * The space we found is in a preceding chunk, and is of no use to us as a split point.
                    //
                    // Regardless, keep the original calculated chunk size for splitting the string.
                }
                else
                {
                    // We found the position of the space character closest to the end of the current chunk, and
                    //   it is NOT the first character of the current chunk. Subtract the current position to
                    //   get the new, shorter chunk length.
                    chunkLength = ixClosestPrecedingSpace - ixCurrentChunkStart;
                }
            }

            // This can be white space if maxLengthInChars is 11 (i.e., the status will only contain a single
            //   character) and the first character of the chunk is a space.
            var statusChunk = text.Substring(startIndex: ixCurrentChunkStart, length: chunkLength).Trim();

            if (!string.IsNullOrWhiteSpace(statusChunk))
            {
                statusChunks.Add(statusChunk);
            }

            // Advance to the first character of the next chunk, if any.
            ixCurrentChunkStart += chunkLength;
        }

        return statusChunks
            .Select((statusChunk, ixChunk) => $"{statusChunk}{GetChunkNumbers(chunkNumber: ixChunk + 1, totalChunksCount: statusChunks.Count)}")
            .ToArray();
    }


    //
    // Private methods
    //

    private static string GetChunkNumbers(int chunkNumber, int totalChunksCount)
        => totalChunksCount == 1
        ? ""
        : $" ({chunkNumber}/{totalChunksCount})";


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
