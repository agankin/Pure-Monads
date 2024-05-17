namespace PureMonads;

/// <summary>
/// Represents a result that can either be a value or an exception.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct Result<TValue>
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
    /// Returns true when it is a value.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Creates an instance of value result.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>An instance of value result.</returns>
    public static Result<TValue> Value(TValue value) => new(value);

    /// <summary>
    /// Creates an instance of error result.
    /// </summary>
    /// <param name="error">An exception.</param>
    /// <returns>An instance of error result.</returns>
    public static Result<TValue> Error(Exception error) => new(error);

    /// <summary>
    /// Matches as a value or an exception by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapValue">A delegate invoked for a value.</param>
    /// <param name="mapError">A delegate invoked for an exception.</param>
    /// <returns>A result returned from the matched delegate.</returns>
    public TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<Exception, TResult> mapError) =>
        HasValue ? mapValue(_value) : mapError(_error);

    public static implicit operator Result<TValue>(TValue value) => Result<TValue>.Value(value);

    public static implicit operator Result<TValue>(Exception error) => Result<TValue>.Error(error);
}