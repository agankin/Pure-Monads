namespace PureMonads;

/// <summary>
/// Contains extensions for collections returning Option.
/// </summary>
public static class OptionCollectionExtensions
{
    /// <summary>
    /// Returns Some value from a dictionary if key exists, otherwise returns None.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="dict">A dictionary.</param>
    /// <param name="key">A key the value retrieved by.</param>
    /// <returns>Some value if the key exists or None.</returns>
    public static Option<TValue> GetOrNone<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key)
    {
        return dict.TryGetValue(key, out var value) ? value : Option.None<TValue>();
    }

    /// <summary>
    /// Returns Some value from a dictionary if key exists, otherwise returns None.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="dict">A dictionary.</param>
    /// <param name="key">A key the value retrieved by.</param>
    /// <returns>Some value if the key exists or None.</returns>
    public static Option<TValue> GetOrNone<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
    {
        return dict.TryGetValue(key, out var value) ? value : Option.None<TValue>();
    }

    /// <summary>
    /// Returns Some value from a dictionary if key exists, otherwise returns None.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="dict">A dictionary.</param>
    /// <param name="key">A key the value retrieved by.</param>
    /// <returns>Some value if the key exists or None.</returns>
    public static Option<TValue> GetOrNone<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        where TKey : notnull
    {
        return GetOrNone((IReadOnlyDictionary<TKey, TValue>)dict, key);
    }
}