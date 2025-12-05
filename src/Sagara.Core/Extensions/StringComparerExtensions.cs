using System.Globalization;

namespace Sagara.Core.Extensions;

public static class StringComparerExtensions
{
    // As of .NET 10 RC 2, the compiler gives us the following warning:
    //   warning CA1034: Do not nest type . Alternatively, change its accessibility so that it is not externally visible.
    //
    // We are properly using the new extension members. I think we just need to wait for the analyzer to be
    //   updated so that this warning goes away.
#if NET10_0_OR_GREATER
    // Suppress the .NET 8 compilation warning.
#pragma warning disable CA1034
    extension(StringComparer)
    {
        public static StringComparer Numeric
            => StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering);

        public static StringComparer NumericIgnoreCase
            => StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering | CompareOptions.IgnoreCase);
    }
#pragma warning restore CA1034
#endif
}
