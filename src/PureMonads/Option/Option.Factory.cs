namespace PureMonads;

/// <summary>
/// Contains methods for creating options.
/// </summary>
public static class Option
{
    /// <summary>
    /// Wraps the value into Some option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="value">A value.</param>
    /// <returns>The original value wrapped into Some option.</returns>
    public static Option<TValue> Some<TValue>(this TValue value) => value;

    /// <summary>
    /// Creates an instance of None option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <returns>An instance of None option.</returns>
    public static Option<TValue> None<TValue>() => Option<TValue>.None();

    /// <summary>
    /// If the value is not null then wraps it into Some option otherwise returns None.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="value">A value.</param>
    /// <returns>The original value wrapped into Some option or None.</returns>
    public static Option<TValue> NullAsNone<TValue>(this TValue? value) =>
        value?.Some() ?? Option<TValue>.None();
}