using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using SR = Sagara.Core.Resources.Strings;

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
    public static void ThrowIfNull<T>([NotNull] T value, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        if (value is null)
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);
            throw new ArgumentNullException(paramName: callerArgExpression, message: $"{SR.ArgumentNull_Generic}{Environment.NewLine}{callerInfo}");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the string is null, or an <see cref="ArgumentException"/> if the
    /// string is empty.
    /// </summary>
    public static void ThrowIfNullOrEmpty([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // Throw if it's null.
            ThrowIfNull(value: value, callerArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

            // It's not null, so it's white space.
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);
            throw new ArgumentException(message: $"{SR.Argument_EmptyString}{Environment.NewLine}{callerInfo}", paramName: callerArgExpression);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the string is null, or an <see cref="ArgumentException"/> if the
    /// string is white space.
    /// </summary>
    public static void ThrowIfNullOrWhiteSpace([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // Throw if it's null.
            ThrowIfNull(value: value, callerArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

            // It's not null, so it's white space.
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);
            throw new ArgumentException(message: $"{SR.Argument_EmptyOrWhiteSpaceString}{Environment.NewLine}{callerInfo}", paramName: callerArgExpression);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the Guid equals Guid.Empty.
    /// </summary>
    public static void ThrowIfEmptyGuid(Guid value, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        if (value == Guid.Empty)
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);
            throw new ArgumentException(message: $"{SR.Argument_EmptyGuid}{Environment.NewLine}{callerInfo}", paramName: callerArgExpression);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="value"/> is null, or an <see cref="ArgumentException"/>
    /// if <paramref name="value"/> is empty.
    /// </summary>
    public static void ThrowIfEmptyCollection<T>([NotNull] IReadOnlyCollection<T> value, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        ThrowIfNull(value: value, callerArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

        if (value.Count == 0)
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);
            throw new ArgumentException(message: $"{SR.Argument_EmptyCollection}{Environment.NewLine}{callerInfo}", paramName: callerArgExpression);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="value"/> is null, or an <see cref="ArgumentException"/> 
    /// if <paramref name="value"/> contains one or more null or white space strings.
    /// </summary>
    public static void ThrowIfContainsNullOrWhiteSpaceValues([NotNull] IReadOnlyCollection<string?> value, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        // Make sure the collection itself is not null.
        ThrowIfNull(value: value, callerArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

        if (value.Any(v => string.IsNullOrWhiteSpace(v)))
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);
            throw new ArgumentException(message: $"{SR.Argument_CollectionNullOrWhiteSpaceValues}{Environment.NewLine}{callerInfo}", paramName: callerArgExpression);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="other"/>.
    /// </summary>
    public static void ThrowIfLessThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(other) < 0)
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            var formattedMessage = string.Format(CultureInfo.CurrentCulture, SR.ArgumentOutOfRange_Generic_MustBeGreaterOrEqual, callerArgExpression, value, other);
#pragma warning restore CA1863 // Use 'CompositeFormat'

            throw new ArgumentOutOfRangeException(paramName: callerArgExpression, actualValue: value, message: $"{formattedMessage}{Environment.NewLine}{callerInfo}");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than or equal <paramref name="other"/>.
    /// </summary>
    public static void ThrowIfLessThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
        where T : IComparable<T>
    {
        //ArgumentOutOfRangeException.ThrowIfGreaterThan(0, 1);
        if (value.CompareTo(other) <= 0)
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            var formattedMessage = string.Format(CultureInfo.CurrentCulture, SR.ArgumentOutOfRange_Generic_MustBeGreater, callerArgExpression, value, other);
#pragma warning restore CA1863 // Use 'CompositeFormat'

            throw new ArgumentOutOfRangeException(paramName: callerArgExpression, actualValue: value, message: $"{formattedMessage}{Environment.NewLine}{callerInfo}");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than <paramref name="other"/>.
    /// </summary>
    public static void ThrowIfGreaterThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(other) > 0)
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            var formattedMessage = string.Format(CultureInfo.CurrentCulture, SR.ArgumentOutOfRange_Generic_MustBeLessOrEqual, callerArgExpression, value, other);
#pragma warning restore CA1863 // Use 'CompositeFormat'

            throw new ArgumentOutOfRangeException(paramName: callerArgExpression, actualValue: value, message: $"{formattedMessage}{Environment.NewLine}{callerInfo}");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than or equal <paramref name="other"/>.
    /// </summary>
    public static void ThrowIfGreaterThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(other) >= 0)
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            var formattedMessage = string.Format(CultureInfo.CurrentCulture, SR.ArgumentOutOfRange_Generic_MustBeLess, callerArgExpression, value, other);
#pragma warning restore CA1863 // Use 'CompositeFormat'

            throw new ArgumentOutOfRangeException(paramName: callerArgExpression, actualValue: value, message: $"{formattedMessage}{Environment.NewLine}{callerInfo}");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is equal to <paramref name="other"/>.
    /// </summary>
    public static void ThrowIfEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
        where T : IEquatable<T>?
    {
        if (EqualityComparer<T>.Default.Equals(value, other))
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            var formattedMessage = string.Format(CultureInfo.CurrentCulture, SR.ArgumentOutOfRange_Generic_MustBeNotEqual, callerArgExpression, value, other);
#pragma warning restore CA1863 // Use 'CompositeFormat'

            throw new ArgumentOutOfRangeException(paramName: callerArgExpression, actualValue: value, message: $"{formattedMessage}{Environment.NewLine}{callerInfo}");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not equal to <paramref name="other"/>.
    /// </summary>
    public static void ThrowIfNotEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? callerArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
        where T : IEquatable<T>?
    {
        if (!EqualityComparer<T>.Default.Equals(value, other))
        {
            var callerInfo = FormatCallerInfo(valueArgExpression: callerArgExpression, memberName: memberName, sourceLineNumber: sourceLineNumber, sourceFilePath: sourceFilePath);

            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            var formattedMessage = string.Format(CultureInfo.CurrentCulture, SR.ArgumentOutOfRange_Generic_MustBeEqual, callerArgExpression, value, other);
#pragma warning restore CA1863 // Use 'CompositeFormat'

            throw new ArgumentOutOfRangeException(paramName: callerArgExpression, actualValue: value, message: $"{formattedMessage}{Environment.NewLine}{callerInfo}");
        }
    }


    //
    // Deprecated methods
    //

    /// <summary>
    /// Throws an <see cref="ArgumentNullException" /> if the value is null.
    /// </summary>
    [Obsolete($"Use {nameof(ThrowIfNull)}<T>")]
    public static void NotNull<T>([NotNull] T value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        if (value is null)
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw new ArgumentNullException(valueArgExpression, FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath));
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the string is null, or an <see cref="ArgumentException"/> if the
    /// string is null or white space.
    /// </summary>
    [Obsolete($"Use {nameof(ThrowIfNullOrWhiteSpace)}")]
    public static void NotEmpty([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        Exception? ex = null;

        if (value is null)
        {
            ex = new ArgumentNullException(valueArgExpression, FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath));
        }
        else if (value.Trim().Length == 0)
        {
            ex = new ArgumentException(
                message: $"{valueArgExpression} is empty or white space. Caller: {FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
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
    [Obsolete($"Use {nameof(ThrowIfEmptyGuid)}")]
    public static void NotEmpty(Guid value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message:
                $"{valueArgExpression} equals Guid.Empty. Caller: {FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the collection is null, or an <see cref="ArgumentException"/> if
    /// the collection is empty.
    /// </summary>
    [Obsolete($"Use {nameof(ThrowIfEmptyCollection)}")]
    public static void NotEmpty<T>([NotNull] IReadOnlyCollection<T> value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        NotNull(value, valueArgExpression, memberName, sourceLineNumber, sourceFilePath);

        if (value.Count == 0)
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw new ArgumentException(
                message: $"Collection argument is empty. Caller: {FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the collection is null, or an <see cref="ArgumentException"/> if
    /// the collection has one or more null values.
    /// </summary>
    [Obsolete("Don't use. This should be caught in validation and returned as an error message.", true)]
    public static void HasNoNulls<T>([NotNull] IReadOnlyList<T> value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        NotNull(value, valueArgExpression, memberName, sourceLineNumber, sourceFilePath);

        if (value.Any(v => v is null))
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw new ArgumentException(
                message: $"Collection has one or more null values. Caller: {FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is outside the range of 
    /// [<paramref name="rangeLo"/>, <paramref name="rangeHi"/>].
    /// </summary>
    [Obsolete("Use new ThrowIfLessThan/LessThanOrEqual/etc. overloads", true)]
    public static void NotOutOfRange<T>(T value, T rangeLo, T rangeHi, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
        where T : IComparable, IComparable<T>
    {
        if (rangeLo.CompareTo(rangeHi) > 0)
        {
            throw new ArgumentException($"{nameof(rangeLo)} must be less than or equal to {nameof(rangeHi)}. Caller: {FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}", nameof(rangeLo));
        }

        if (value.CompareTo(rangeLo) < 0 || value.CompareTo(rangeHi) > 0)
        {
            throw new ArgumentOutOfRangeException(valueArgExpression, value, $"{valueArgExpression} was outside the range of [{rangeLo}, {rangeHi}].  Caller: {FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}");
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the collection is null, or an <see cref="ArgumentException"/> if
    /// the collection has one or more null or white space strings.
    /// </summary>
    [Obsolete($"Use {nameof(ThrowIfContainsNullOrWhiteSpaceValues)}")]
    public static void HasNoEmpties([NotNull] IReadOnlyCollection<string> value, [CallerArgumentExpression(nameof(value))] string? valueArgExpression = null,
        [CallerMemberName] string? memberName = null, [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string? sourceFilePath = null)
    {
        NotNull(value, valueArgExpression, memberName, sourceLineNumber, sourceFilePath);

        if (value.Any(v => string.IsNullOrWhiteSpace(v)))
        {
            NotEmpty(valueArgExpression, nameof(valueArgExpression), memberName, sourceLineNumber, sourceFilePath);

            throw new ArgumentException(
                message: $"Collection of strings has one or more null or white space values. Caller: {FormatCallerInfoOld(valueArgExpression, memberName, sourceLineNumber, sourceFilePath)}",
                paramName: valueArgExpression
                );
        }
    }


    //
    // Private methods
    //

    [Obsolete($"Use {nameof(FormatCallerInfo)}")]
    private static string FormatCallerInfoOld(string? valueArgExpression, string? memberName, int sourceLineNumber, string? sourceFilePath)
    {
        return $"Failing argument expression '{valueArgExpression}' called by {memberName} at Line {sourceLineNumber} in '{sourceFilePath}'.";
    }

    private static string FormatCallerInfo(string? valueArgExpression, string? memberName, int sourceLineNumber, string? sourceFilePath)
        => $"[Caller argument expression: '{valueArgExpression}'; Caller member: '{memberName}'; Called at Line {sourceLineNumber} in File '{sourceFilePath}']";
}
