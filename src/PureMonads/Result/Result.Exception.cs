namespace PureMonads;

/// <summary>
/// Represents a result that can either be a value or an exception.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct Result<TValue> : IResult<TValue>
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
    /// Creates an instance of a value result.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>A value result.</returns>
    public static Result<TValue> Value(TValue value) => new(value);

    /// <summary>
    /// Creates an instance of an exception result.
    /// </summary>
    /// <param name="error">An exception.</param>
    /// <returns>An exception result.</returns>
    public static Result<TValue> Error(Exception error) => new(error);

    /// <inheritdoc/>
    public TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<Exception, TResult> mapError) =>
        HasValue ? mapValue(_value) : mapError(_error);

    /// <summary>
    /// If the current result is a value then maps it with <paramref name="mapValue"/> and returns wrapped into Result,
    /// otherwise returns the same exception Result.
    /// </summary>
    /// <typeparam name="TResult">Result value type.</typeparam>
    /// <param name="mapSome">A delegate invoked if the current result is a value.</param>
    /// <returns>Value or Error result.</returns>
    public Result<TResult> Map<TResult>(Func<TValue, TResult> mapValue) =>
        Match(value => mapValue(value.NotNull()), Result<TResult>.Error);

    /// <summary>
    /// If the current result is a value then maps it with <paramref name="mapValue"/> and returns,
    /// otherwise returns the same exception Result.
    /// </summary>
    /// <typeparam name="TResult">Result value type.</typeparam>
    /// <param name="mapSome">A delegate invoked if the current result is a value.</param>
    /// <returns>Value or Error result.</returns>
    public Result<TResult> FlatMap<TResult>(Func<TValue, Result<TResult>> mapValue) =>
         Match(value => mapValue(value.NotNull()), Result<TResult>.Error);

    public static implicit operator Result<TValue>(TValue value) => Result<TValue>.Value(value);

    public static implicit operator Result<TValue>(Exception error) => Result<TValue>.Error(error);
}