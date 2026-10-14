#if NET10_0_OR_GREATER
using System.Globalization;

namespace Sagara.Core.Extensions;

public static class StringComparerExtensions
{
    // As of .NET 10 RC 2, the compiler gives us the following warning:
    //   warning CA1034: Do not nest type . Alternatively, change its accessibility so that it is not externally visible.
    //
    // We are properly using the new extension members. I think we just need to wait for the analyzer to be
    //   updated so that this warning goes away.
    // Suppress the .NET 8 compilation warning.
#pragma warning disable CA1034
    extension(StringComparer)
    {
        /// <summary>
        /// Gets a case-sensitive <see cref="StringComparer"/> that uses the current culture and
        /// <see cref="CompareOptions.NumericOrdering"/>, so that runs of digits are compared by
        /// numeric value rather than character-by-character (e.g., "file2" sorts before "file10").
        /// </summary>
        /// <remarks>
        /// Uses <see cref="CultureInfo.CurrentCulture"/> as of the moment the property is read. A new
        /// comparer is returned on each access, and that comparer retains the culture it was created
        /// with even if the current culture later changes.
        /// </remarks>
        public static StringComparer CurrentCultureNumeric
            => StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering);

        /// <summary>
        /// Gets a case-insensitive <see cref="StringComparer"/> that uses the current culture and
        /// <see cref="CompareOptions.NumericOrdering"/>, so that runs of digits are compared by
        /// numeric value rather than character-by-character (e.g., "File2" sorts before "file10").
        /// </summary>
        /// <remarks>
        /// Uses <see cref="CultureInfo.CurrentCulture"/> as of the moment the property is read. A new
        /// comparer is returned on each access, and that comparer retains the culture it was created
        /// with even if the current culture later changes.
        /// </remarks>
        public static StringComparer CurrentCultureNumericIgnoreCase
            => StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering | CompareOptions.IgnoreCase);

        /// <summary>
        /// Gets a case-sensitive <see cref="StringComparer"/> that uses the invariant culture and
        /// <see cref="CompareOptions.NumericOrdering"/>, so that runs of digits are compared by
        /// numeric value rather than character-by-character (e.g., "file2" sorts before "file10").
        /// </summary>
        /// <remarks>
        /// Results do not depend on <see cref="CultureInfo.CurrentCulture"/>, making this comparer
        /// suitable for stable, machine-independent ordering.
        /// </remarks>
        public static StringComparer InvariantCultureNumeric
            => StringComparer.Create(CultureInfo.InvariantCulture, CompareOptions.NumericOrdering);

        /// <summary>
        /// Gets a case-insensitive <see cref="StringComparer"/> that uses the invariant culture and
        /// <see cref="CompareOptions.NumericOrdering"/>, so that runs of digits are compared by
        /// numeric value rather than character-by-character (e.g., "File2" sorts before "file10").
        /// </summary>
        /// <remarks>
        /// Results do not depend on <see cref="CultureInfo.CurrentCulture"/>, making this comparer
        /// suitable for stable, machine-independent ordering.
        /// </remarks>
        public static StringComparer InvariantCultureNumericIgnoreCase
            => StringComparer.Create(CultureInfo.InvariantCulture, CompareOptions.NumericOrdering | CompareOptions.IgnoreCase);

        /// <summary>
        /// Obsolete. Use <see cref="extension(StringComparer).CurrentCultureNumeric"/> instead.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>StringComparer.CurrentCultureNumeric</c>.
        /// </remarks>
        [Obsolete("Obsolete. Use StringComparer.CurrentCultureNumeric instead. It makes the culture explicit.")]
        public static StringComparer Numeric
            => StringComparer.CurrentCultureNumeric;

        /// <summary>
        /// Obsolete. Use <see cref="extension(StringComparer).CurrentCultureNumericIgnoreCase"/> instead.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>StringComparer.CurrentCultureNumericIgnoreCase</c>.
        /// </remarks>
        [Obsolete("Obsolete. Use StringComparer.CurrentCultureNumericIgnoreCase instead. It makes the culture explicit.")]
        public static StringComparer NumericIgnoreCase
            => StringComparer.CurrentCultureNumericIgnoreCase;
    }
#pragma warning restore CA1034
}

#endif
