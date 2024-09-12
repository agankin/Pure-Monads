namespace PureMonads;

/// <summary>
/// Contains extensions for collections.
/// </summary>
public static class OptionCollectionExtensions
{
    /// <summary>
    /// Returns a value wrapped in Some option if a key exists or None option if a key doesn't exist.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="dict">The dictionary.</param>
    /// <param name="key">A key.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> GetOrNone<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key)
    {
        return dict.TryGetValue(key, out var value) ? value : Option.None<TValue>();
    }

    /// <summary>
    /// Returns a value wrapped in Some option if a key exists or None option if a key doesn't exist.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="dict">The dictionary.</param>
    /// <param name="key">A key.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> GetOrNone<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
    {
        return dict.TryGetValue(key, out var value) ? value : Option.None<TValue>();
    }

    /// <summary>
    /// Returns a value wrapped in Some option if a key exists or None option if a key doesn't exist.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="dict">The dictionary.</param>
    /// <param name="key">A key.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> GetOrNone<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        where TKey : notnull
    {
        return GetOrNone((IReadOnlyDictionary<TKey, TValue>)dict, key);
    }
}