namespace PureMonads;

public static class ResultExtensions
{
    /// <summary>
    /// If the current result is a value returns it wrapped into Some option, otherwise returns None option.
    /// </summary>
    /// <returns>Some value or None.</returns>
    public static Option<TValue> Value<TValue, TError>(this Result<TValue, TError> result) =>
        result.Match(Option.Some, _ => Option.None<TValue>());

    /// <summary>
    /// If the current result is a value returns it wrapped into Some option, otherwise returns None option.
    /// </summary>
    /// <returns>Some value or None.</returns>
    public static Option<TValue> Value<TValue>(this Result<TValue> result) =>
        result.Match(Option.Some, _ => Option.None<TValue>());

    /// <summary>
    /// If the current result is an error returns it wrapped into Some option, otherwise returns None option.
    /// </summary>
    /// <returns>Some error or None.</returns>
    public static Option<TError> Error<TValue, TError>(this Result<TValue, TError> result) =>
        result.Match(_ => Option.None<TError>(), _ => Option.None<TError>());

    /// <summary>
    /// If the current result is an exception returns it wrapped into Some option, otherwise returns None option.
    /// </summary>
    /// <returns>Some exception or None.</returns>
    public static Option<Exception> Error<TValue>(this Result<TValue> result) =>
        result.Match(_ => Option.None<Exception>(), _ => Option.None<Exception>());
}