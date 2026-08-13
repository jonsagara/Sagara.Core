namespace Sagara.Core;

public static class IEnumerableExtensions
{
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
