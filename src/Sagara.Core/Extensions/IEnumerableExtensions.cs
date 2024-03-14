namespace Sagara.Core;

public static class IEnumerableExtensions
{
#if NET6_0 || NET7_0 || NET8_0
    /// <summary>
    /// Returns an enumerable that incorporates the element's index into a tuple.
    /// </summary>
    /// <remarks>
    /// NOTE: This will be built into .NET 9 and above, so this method is for .NET 8 and below.
    /// </remarks>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">The source enumerable providing the elements.</param>
    public static IEnumerable<(int Index, TSource Item)> Index<TSource>(this IEnumerable<TSource> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return source.Select((item, index) => (index, item));
    }
#endif
}
