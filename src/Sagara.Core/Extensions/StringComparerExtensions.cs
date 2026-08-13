using System.Globalization;

namespace Sagara.Core.Extensions;

public static class StringComparerExtensions
{
    extension(StringComparer)
    {
        public static StringComparer Numeric
            => StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering);

        public static StringComparer NumericIgnoreCase
            => StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering | CompareOptions.IgnoreCase);
    }
}
