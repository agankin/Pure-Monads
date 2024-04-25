namespace PureMonads;

/// <summary>
/// Represents a result that can either be a value or an error.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
/// <typeparam name="TError">Error type.</typeparam>
public readonly struct Result<TValue, TError> : IResult<TValue, TError>
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
    /// Creates an instance of a value result.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>A value result.</returns>
    public static Result<TValue, TError> Value(TValue value) => new(value);

    /// <summary>
    /// Creates an instance of an error result.
    /// </summary>
    /// <param name="error">An error.</param>
    /// <returns>An error result.</returns>
    public static Result<TValue, TError> Error(TError error) => new(error);

    /// <inheritdoc/>
    public TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<TError, TResult> mapError) =>
        HasValue ? mapValue(_value) : mapError(_error);

    /// <summary>
    /// If the current result is a value then maps it with <paramref name="mapValue"/> and returns wrapped into Result,
    /// otherwise returns the same error Result.
    /// </summary>
    /// <typeparam name="TResult">Result value type.</typeparam>
    /// <param name="mapSome">A delegate invoked if the current result is a value.</param>
    /// <returns>Value or Error result.</returns>
    public Result<TResult, TError> Map<TResult>(Func<TValue, TResult> mapValue) =>
        Match(value => mapValue(value.NotNull()), Result<TResult, TError>.Error);

    /// <summary>
    /// If the current result is a value then maps it with <paramref name="mapValue"/> and returns,
    /// otherwise returns the same error Result.
    /// </summary>
    /// <typeparam name="TResult">Result value type.</typeparam>
    /// <param name="mapSome">A delegate invoked if the current result is a value.</param>
    /// <returns>Value or Error result.</returns>
    public Result<TResult, TError> FlatMap<TResult>(Func<TValue, Result<TResult, TError>> mapValue) =>
         Match(value => mapValue(value.NotNull()), Result<TResult, TError>.Error);

    public static implicit operator Result<TValue, TError>(TValue value) => Result<TValue, TError>.Value(value);

    public static implicit operator Result<TValue, TError>(TError error) => Result<TValue, TError>.Error(error);
}