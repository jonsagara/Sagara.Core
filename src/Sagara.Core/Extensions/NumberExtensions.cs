using System.Numerics;

namespace Sagara.Core.Extensions;

public static class NumberExtensions
{
    /// <summary>
    /// If <paramref name="value"/> is <c>TNumber.Zero</c>, return <c>null</c>. Otherwise, return
    /// <paramref name="value"/> unchanged.
    /// </summary>
    public static TNumber? ToNullIfZero<TNumber>(this TNumber value)
        where TNumber : struct, INumber<TNumber>
        => value == TNumber.Zero ? null : value;
}
