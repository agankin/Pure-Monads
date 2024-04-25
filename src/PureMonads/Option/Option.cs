namespace PureMonads;

/// <summary>
/// An option that can be either Some containing a value or None without value.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct Option<TValue> : IOption<TValue>
{
    private readonly TValue? _value;

    private Option(bool hasValue, TValue? value) => (HasValue, _value) = (hasValue, value);

    /// <summary>
    /// Returns true when the option is Some.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Creates an instance of Some option.
    /// </summary>
    /// <param name="value">A value of the Some option.</param>
    /// <returns>An instance of Some option.</returns>
    public static Option<TValue> Some(TValue value) => new(hasValue: true, value);

    /// <summary>
    /// Creates an instance of None option.
    /// </summary>
    /// <returns>An instance of None option.</returns>
    public static Option<TValue> None() => new(hasValue: false, default);

    /// <inheritdoc/>
    public TResult Match<TResult>(Func<TValue, TResult> mapSome, Func<TResult> onNone) =>
        HasValue ? mapSome(_value.NotNull()) : onNone();

    /// <summary>
    /// If the current option is Some returns the result from <paramref name="mapSome"/> invocation wrapped into Some option.
    /// Otherwise returns None.
    /// </summary>
    /// <typeparam name="TResult">Result value type.</typeparam>
    /// <param name="mapSome">A delegate invoked if the current option is Some.</param>
    /// <returns>Some result value or None.</returns>
    public Option<TResult> Map<TResult>(Func<TValue, TResult> mapSome) =>
        Match(value => mapSome(value.NotNull()).Some(), Option<TResult>.None);

    /// <summary>
    /// If the current option is Some returns the result option from <paramref name="mapSome"/> invocation.
    /// Otherwise returns None.
    /// </summary>
    /// <typeparam name="TResult">Result value type.</typeparam>
    /// <param name="mapSome">A delegate invoked if the current option is Some.</param>
    /// <returns>Some result value or None.</returns>
    public Option<TResult> FlatMap<TResult>(Func<TValue, Option<TResult>> mapSome) =>
         Match(value => mapSome(value.NotNull()), Option<TResult>.None);

    public static implicit operator Option<TValue>(TValue value) => Option<TValue>.Some(value);
}