namespace PureMonads;

public static class ResultExtensions
{
    /// <summary>
    /// If the result is a value returns it wrapped into Some option.
    /// Otherwise returns None option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">A result.</param>
    /// <returns>Some value or None.</returns>
    public static Option<TValue> Value<TValue, TError>(this in Result<TValue, TError> result) =>
        result.Match(Option.Some, _ => Option.None<TValue>());

    /// <summary>
    /// If the result is a value returns it wrapped into Some option.
    /// Otherwise returns None option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">A result.</param>
    /// <returns>Some value or None.</returns>
    public static Option<TValue> Value<TValue>(this in Result<TValue> result) =>
        result.Match(Option.Some, _ => Option.None<TValue>());

    /// <summary>
    /// If the result is an error returns it wrapped into Some option.
    /// Otherwise returns None option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">A result.</param>
    /// <returns>Some error or None.</returns>
    public static Option<TError> Error<TValue, TError>(this Result<TValue, TError> result) =>
        result.Match(_ => Option.None<TError>(), Option.Some);

    /// <summary>
    /// If the current result is an exception returns it wrapped into Some option.
    /// Otherwise returns None option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">A result.</param>
    /// <returns>Some exception or None.</returns>
    public static Option<Exception> Error<TValue>(this Result<TValue> result) =>
        result.Match(_ => Option.None<Exception>(), Option.Some);

    /// <summary>
    /// If the result is a value then maps it with <paramref name="map"/> and returns wrapped into Result.
    /// Otherwise returns the same error Result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result.</returns>
    public static Result<TResult, TError> Map<TValue, TResult, TError>(this Result<TValue, TError> result, Func<TValue, TResult> map) =>
        result.Match(value => map(value), Result<TResult, TError>.Error);

    /// <summary>
    /// If the result is a value then maps it with <paramref name="map"/> and returns.
    /// Otherwise returns the same error Result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result.</returns>
    public static Result<TResult, TError> FlatMap<TValue, TResult, TError>(this Result<TValue, TError> result, Func<TValue, Result<TResult, TError>> map) =>
        result.Match(value => map(value), Result<TResult, TError>.Error);

    /// <summary>
    /// If the result is a value then maps it with <paramref name="map"/> and returns wrapped into Result.
    /// Otherwise returns the same error Result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result.</returns>
    public static Result<TResult> Map<TValue, TResult>(this Result<TValue> result, Func<TValue, TResult> map) =>
        result.Match(value => map(value), Result<TResult>.Error);

    /// <summary>
    /// If the result is a value then maps it with <paramref name="map"/> and returns.
    /// Otherwise returns the same error Result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result.</returns>
    public static Result<TResult> FlatMap<TValue, TResult>(this Result<TValue> result, Func<TValue, Result<TResult>> map) =>
        result.Match(value => map(value), Result<TResult>.Error);
}