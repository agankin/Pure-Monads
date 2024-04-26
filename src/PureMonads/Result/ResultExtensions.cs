namespace PureMonads;

public static class ResultExtensions
{
    /// <summary>
    /// Returns a value from result wrapped into Some option or returns None option.
    /// </summary>
    /// <returns>An instance of Option.</returns>
    public static Option<TValue> Value<TValue, TError>(this Result<TValue, TError> result) =>
        result.Extract(Option.Some, _ => Option.None<TValue>());

    /// <summary>
    /// Returns a value from result wrapped into Some option or returns None option.
    /// </summary>
    /// <returns>An instance of Option.</returns>
    public static Option<TValue> Value<TValue>(this Result<TValue> result) =>
        result.Extract(Option.Some, _ => Option.None<TValue>());

    /// <summary>
    /// Returns a error from result wrapped into Some option or returns None option.
    /// </summary>
    /// <returns>An instance of Option.</returns>
    public static Option<TError> Error<TValue, TError>(this Result<TValue, TError> result) =>
        result.Extract(_ => Option.None<TError>(), Option.Some);

    /// <summary>
    /// Returns a error from result wrapped into Some option or returns None option.
    /// </summary>
    /// <returns>An instance of Option.</returns>
    public static Option<Exception> Error<TValue>(this Result<TValue> result) =>
        result.Extract(_ => Option.None<Exception>(), Option.Some);
}