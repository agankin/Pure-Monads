namespace PureMonads;

/// <summary>
/// Represents either a value or no value.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct Option<TValue> : IEquatable<Option<TValue>>
{
    private readonly TValue _value = default!;

    private Option(bool hasValue, TValue value) => (HasValue, _value) = (hasValue, value);

    /// <summary>
    /// Contains true for Some option or false for None option.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Wraps a value in Some option.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> Some(TValue value) => new(hasValue: true, value);

    /// <summary>
    /// Creates an instance of None option.
    /// </summary>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> None() => new(hasValue: false, default!);

    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapSome">A delegate invoked on Some.</param>
    /// <param name="onNone">A delegate invoked on None.</param>
    /// <returns>A result returned from the matched delegate invocation.</returns>
    public TResult Match<TResult>(Func<TValue, TResult> mapSome, Func<TResult> onNone) =>
        HasValue ? mapSome(_value) : onNone();

    /// <inheritdoc/>
    public override string ToString() => Match(value => $"Some({value})", () => "None");

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is Option<TValue> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HasValue ? _value?.GetHashCode() ?? 0 : 0;

    /// <inheritdoc/>
    public bool Equals(Option<TValue> other)
    {
        return HasValue == other.HasValue
            && (!HasValue || EqualityComparer<TValue>.Default.Equals(_value, other._value));
    }

    public static bool operator ==(Option<TValue> first, Option<TValue> second) => first.Equals(second);

    public static bool operator !=(Option<TValue> first, Option<TValue> second) => !first.Equals(second);

    public static implicit operator Option<TValue>(TValue value) => Option<TValue>.Some(value);
}