namespace PureMonads;

/// <summary>
/// Can either be a value or an error.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
/// <typeparam name="TError">Error type.</typeparam>
public readonly struct Result<TValue, TError> : IEquatable<Result<TValue, TError>>
{
    private readonly TValue _value;
    private readonly TError _error;

    private Result(TValue value)
    {
        _value = value;
        _error = default!;
        HasValue = true;
    }

    private Result(TError error)
    {
        _value = default!;
        _error = error;
        HasValue = false;
    }

    /// <summary>
    /// Returns true when it is a value.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Creates an instance of value result.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>An instance of value Result.</returns>
    public static Result<TValue, TError> Value(TValue value) => new(value);

    /// <summary>
    /// Creates an instance of error result.
    /// </summary>
    /// <param name="error">An error.</param>
    /// <returns>An instance of error Result.</returns>
    public static Result<TValue, TError> Error(TError error) => new(error);

    /// <summary>
    /// Matches as a value or an error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapValue">A delegate invoked for a value.</param>
    /// <param name="mapError">A delegate invoked for an error.</param>
    /// <returns>A result returned from the matched delegate.</returns>
    public TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<TError, TResult> mapError) =>
        HasValue ? mapValue(_value) : mapError(_error);

    /// <inheritdoc/>
    public override string ToString() => Match(value => $"Value({value})", error => $"Error({error})");

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is Result<TValue, TError> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HasValue
        ? _value?.GetHashCode() ?? 0
        : _error?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public bool Equals(Result<TValue, TError> other)
    {
        return HasValue == other.HasValue
            && (HasValue && EqualityComparer<TValue>.Default.Equals(_value, other._value)
                || !HasValue && EqualityComparer<TError>.Default.Equals(_error, other._error));
    }

    public static bool operator ==(Result<TValue, TError> first, Result<TValue, TError> second) => first.Equals(second);

    public static bool operator !=(Result<TValue, TError> first, Result<TValue, TError> second) => !first.Equals(second);
    
    public static implicit operator Result<TValue, TError>(TValue value) => Result<TValue, TError>.Value(value);

    public static implicit operator Result<TValue, TError>(TError error) => Result<TValue, TError>.Error(error);
}