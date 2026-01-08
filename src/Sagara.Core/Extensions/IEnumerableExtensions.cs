namespace Sagara.Core;

public static class IEnumerableExtensions
{
#if NET8_0
    /// <summary>
    /// Returns an enumerable that incorporates the element's index into a tuple.
    /// </summary>
    /// <remarks>
    /// NOTE: This will be built into .NET 9 and above, so this method is for .NET 8.
    /// </remarks>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
    /// <param name="source">The source enumerable providing the elements.</param>
    public static IEnumerable<(int Index, TSource Item)> Index<TSource>(this IEnumerable<TSource> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (source is TSource[] { Length: 0 })
        {
            return [];
        }

        return IndexIterator(source);
    }

    private static IEnumerable<(int Index, TSource Item)> IndexIterator<TSource>(IEnumerable<TSource> source)
    {
        int index = -1;
        foreach (TSource element in source)
        {
            checked
            {
                index++;
            }

            yield return (index, element);
        }
    }
#endif

    /// <summary>
    /// Returns an enumerable that incorporates a 1-based numbering into a tuple.
    /// </summary>
    /// <remarks>
    /// This is similar to the built-in Index method, but uses 1-based numbering instead of 0-based indexing.
    /// </remarks>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">The source enumerable providing the elements.</param>
    /// <returns>An enumerable that incorporates each element 1-based number into a tuple.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    public static IEnumerable<(int Number, TSource Item)> Number<TSource>(this IEnumerable<TSource> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (source is TSource[] { Length: 0 })
        {
            return [];
        }

        return NumberIterator(source);
    }

    private static IEnumerable<(int Number, TSource Item)> NumberIterator<TSource>(IEnumerable<TSource> source)
    {
        int number = 0;
        foreach (TSource element in source)
        {
            checked
            {
                number++;
            }

            yield return (number, element);
        }
    }
}
