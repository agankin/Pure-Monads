namespace PureMonads;

/// <summary>
/// Can either be Some value or None with no value.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct Option<TValue>
{
    private readonly TValue _value = default!;

    private Option(bool hasValue, TValue value) => (HasValue, _value) = (hasValue, value);

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
    public static Option<TValue> None() => new(hasValue: false, default!);

    /// <summary>
    /// Matches the option as Some with a value or None without value by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapSome">A delegate to be invoked with the value of Some.</param>
    /// <param name="onNone">A delegate to be invoked on None.</param>
    /// <returns>A result returned from the matched delegate invocation.</returns>
    public TResult Match<TResult>(Func<TValue, TResult> mapSome, Func<TResult> onNone) =>
        HasValue ? mapSome(_value) : onNone();

    public static implicit operator Option<TValue>(TValue value) => Option<TValue>.Some(value);

    /// <inheritdoc/>
    public override string ToString() => Match(value => $"Some({value})", () => "None");
}