namespace PureMonads;

/// <summary>
/// Represents either a value or an error of Exception type.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct Result<TValue> : IEquatable<Result<TValue>>
{
    private readonly TValue _value;
    private readonly Exception _error;

    private Result(TValue value)
    {
        _value = value;
        _error = default!;
        HasValue = true;
    }

    private Result(Exception error)
    {
        _value = default!;
        _error = error;
        HasValue = false;
    }

    /// <summary>
    /// Contains true for Value result or false for Error result.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Wraps a value in Value result.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TValue> Value(TValue value) => new(value);

    /// <summary>
    /// Wraps an error in Error result.
    /// </summary>
    /// <param name="error">An error.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TValue> Error(Exception error) => new(error);

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapValue">A delegate invoked on Value.</param>
    /// <param name="mapError">A delegate invoked on Error.</param>
    /// <returns>A result returned from the matched delegate invocation.</returns>
    public TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<Exception, TResult> mapError)
    {
        return HasValue ? mapValue(_value) : mapError(_error);
    }

    /// <inheritdoc/>
    public override string ToString() => Match(
        value => $"Value({value})",
        ex => $"{ex.GetType().Name}({ex.Message})");

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is Result<TValue> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HasValue
        ? _value?.GetHashCode() ?? 0
        : _error?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public bool Equals(Result<TValue> other)
    {
        return HasValue == other.HasValue
            && (HasValue && EqualityComparer<TValue>.Default.Equals(_value, other._value)
                || !HasValue && EqualityComparer<Exception>.Default.Equals(_error, other._error));
    }

    public static bool operator ==(Result<TValue> first, Result<TValue> second) => first.Equals(second);

    public static bool operator !=(Result<TValue> first, Result<TValue> second) => !first.Equals(second);

    public static implicit operator Result<TValue>(TValue value) => Result<TValue>.Value(value);

    public static implicit operator Result<TValue>(Exception error) => Result<TValue>.Error(error);
}