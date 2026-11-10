using System.Globalization;

namespace Sagara.Core.Extensions;

public static class StringComparerExtensions
{
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
}
