using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sagara.Core;

/// <summary>
/// <para>Argument checker so that we don't have to keep writing the same guard clauses all over the place.</para>
/// <para>This updated version uses the new <see cref="CallerArgumentExpressionAttribute"/> to get the 
/// argument expression from the call site so that we don't have to manually pass that in.</para>
/// </summary>
/// <remarks>
/// <para>NOTE: F# does not yet support <see cref="CallerArgumentExpressionAttribute"/>, so F# callers will need to pass in
/// the second valArgExpression argument. C# callers can omit this second argument.</para>
/// <para>NOTE: Based on an older version of this utility class from EF Core: https://github.com/dotnet/efcore/blob/release/8.0/src/Shared/Check.cs</para>
/// </remarks>
public static class Check
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException" /> if the value is null.
    /// </summary>
    public static void NotNull<T>([NotNull] T value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        if (value is null)
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw new ArgumentNullException(valueArgExpression, FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath));
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the string is null, or an <see cref="ArgumentException"/> if the
    /// string is null or white space.
    /// </summary>
    public static void NotEmpty([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        Exception? ex = null;

        if (value is null)
        {
            ex = new ArgumentNullException(valueArgExpression, FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath));
        }
        else if (value.Trim().Length == 0)
        {
            ex = new ArgumentException(
                message: $"{valueArgExpression} is empty or white space. Caller: {FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }

        if (ex is not null)
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw ex;
        }

        // Justification: value will be non-null when exiting. If it is null, this method throws.
#pragma warning disable CS8777 // Parameter must have a non-null value when exiting.
    }
#pragma warning restore CS8777 // Parameter must have a non-null value when exiting.

    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the Guid equals Guid.Empty.
    /// </summary>
    public static void NotEmpty(Guid value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message:
                $"{valueArgExpression} equals Guid.Empty. Caller: {FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the collection is null, or an <see cref="ArgumentException"/> if
    /// the collection is empty.
    /// </summary>
    public static void NotEmpty<T>([NotNull] IReadOnlyCollection<T> value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        NotNull(value, valueArgExpression, memberName, sourceLineNumber, sourceFilePath);

        if (value.Count == 0)
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw new ArgumentException(
                message: $"Collection argument is empty. Caller: {FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the collection is null, or an <see cref="ArgumentException"/> if
    /// the collection has one or more null values.
    /// </summary>
    public static void HasNoNulls<T>([NotNull] IReadOnlyList<T> value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        NotNull(value, valueArgExpression, memberName, sourceLineNumber, sourceFilePath);

        if (value.Any(v => v is null))
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw new ArgumentException(
                message: $"Collection has one or more null values. Caller: {FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is outside the range of 
    /// [<paramref name="rangeLo"/>, <paramref name="rangeHi"/>].
    /// </summary>
    public static void NotOutOfRange<T>(T value, T rangeLo, T rangeHi, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
        where T : IComparable, IComparable<T>
    {
        if (rangeLo.CompareTo(rangeHi) > 0)
        {
            throw new ArgumentException($"{nameof(rangeLo)} must be less than or equal to {nameof(rangeHi)}. Caller: {FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}", nameof(rangeLo));
        }

        if (value.CompareTo(rangeLo) < 0 || value.CompareTo(rangeHi) > 0)
        {
            throw new ArgumentOutOfRangeException(valueArgExpression, value, $"{valueArgExpression} was outside the range of [{rangeLo}, {rangeHi}].  Caller: {FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the collection is null, or an <see cref="ArgumentException"/> if
    /// the collection has one or more null or white space strings.
    /// </summary>
    public static void HasNoEmpties([NotNull] IReadOnlyCollection<string> value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        NotNull(value, valueArgExpression, memberName, sourceLineNumber, sourceFilePath);

        if (value.Any(v => string.IsNullOrWhiteSpace(v)))
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw new ArgumentException(
                message: $"Collection of strings has one or more null or white space values. Caller: {FormatCallerInfo(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }
    }


    //
    // Private methods
    //

    private static string FormatCallerInfo(string? valueArgExpression, string? memberName, int sourceLineNumber, string? sourceFilePath)
    {
        return $"Failing argument expression '{valueArgExpression}' called by {memberName} at Line {sourceLineNumber} in '{sourceFilePath}'.";
    }
}
