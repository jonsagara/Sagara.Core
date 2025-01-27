using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Sagara.Core.Collections;

/// <summary>
/// Provides a cached, read-only instance for the specified key and value types.
/// </summary>
/// <remarks>
/// <para>We're implementing <see cref="IReadOnlyDictionary{TKey, TValue}"/> to get a true read-only dictionary without
/// risking various extension methods leading to runtime exceptions. For example, <see cref="ReadOnlyDictionary{TKey, TValue}"/>
/// implements <see cref="IDictionary{TKey, TValue}"/>, which in turn lights up the <see cref="CollectionExtensions.TryAdd{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/>
/// extension method that will throw at runtime if called on a <see cref="ReadOnlyDictionary{TKey, TValue}"/>.</para>
/// </remarks>
/// <typeparam name="TKey">The type of keys in the read-only dictionary.</typeparam>
/// <typeparam name="TValue">The type of values in the read-only dictionary.</typeparam>
public sealed class EmptyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    where TKey : notnull
{
    /// <summary>
    /// The one and only instance of <see cref="EmptyDictionary{TKey, TValue}"/> for this key/value type pair.
    /// </summary>
    public static readonly IReadOnlyDictionary<TKey, TValue> Instance = ReadOnlyDictionary<TKey, TValue>.Empty;

    /// <summary>
    /// Don't let callers create their own instances.
    /// </summary>
    private EmptyDictionary()
    {
        // Intentionally blank.
    }

    /// <inheritdoc/>
    public TValue this[TKey key]
        => Instance[key];

    /// <inheritdoc/>
    public IEnumerable<TKey> Keys
        => Instance.Keys;

    /// <inheritdoc/>
    public IEnumerable<TValue> Values
        => Instance.Values;

    /// <inheritdoc/>
    public int Count
        => Instance.Count;

    /// <inheritdoc/>
    public bool ContainsKey(TKey key)
        => Instance.ContainsKey(key);

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        => Instance.GetEnumerator();

    /// <inheritdoc/>
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        => Instance.TryGetValue(key, out value);

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)Instance).GetEnumerator();
}
