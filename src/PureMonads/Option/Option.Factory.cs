namespace PureMonads;

/// <summary>
/// Contains methods for creating Option instances.
/// </summary>
public static class Option
{
    /// <summary>
    /// Wraps the value in Some option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> Some<TValue>(this TValue value) => value;

    /// <summary>
    /// Creates an instance of None option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> None<TValue>() => Option<TValue>.None();

    /// <summary>
    /// Returns the value wrapped in Some option if it's non null or None option if otherwise.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> NullAsNone<TValue>(this TValue? value) => value != null ? Some(value) : None<TValue>();
}