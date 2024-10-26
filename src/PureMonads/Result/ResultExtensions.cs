namespace PureMonads;

/// <summary>
/// Contains extension methods for Result monad.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts to Option monad returning Some if the result is Value or None if the result is Error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> Value<TValue, TError>(this in Result<TValue, TError> result) =>
        result.Match(Option.Some, _ => Option.None<TValue>());

    /// <summary>
    /// Converts to Option monad returning Some if the result is Value or None if the result is Error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> Value<TValue>(this in Result<TValue> result) =>
        result.Match(Option.Some, _ => Option.None<TValue>());

    /// <summary>
    /// Converts to Option monad returning Some if the result is Error or None if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TError> Error<TValue, TError>(this Result<TValue, TError> result) =>
        result.Match(_ => Option.None<TError>(), Option.Some);

    /// <summary>
    /// Converts to Option monad returning Some if the result is Error or None if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<Exception> Error<TValue>(this Result<TValue> result) =>
        result.Match(_ => Option.None<Exception>(), Option.Some);

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TResult, TError> Map<TValue, TResult, TError>(this Result<TValue, TError> result, Func<TValue, TResult> map) =>
        result.Match(value => map(value), Result<TResult, TError>.Error);

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TResult, TError> FlatMap<TValue, TResult, TError>(this Result<TValue, TError> result, Func<TValue, Result<TResult, TError>> map) =>
        result.Match(value => map(value), Result<TResult, TError>.Error);

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TResult> Map<TValue, TResult>(this Result<TValue> result, Func<TValue, TResult> map) =>
        result.Match(value => map(value), Result<TResult>.Error);

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TResult> FlatMap<TValue, TResult>(this Result<TValue> result, Func<TValue, Result<TResult>> map) =>
        result.Match(value => map(value), Result<TResult>.Error);

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue, TError> On<TValue, TError>(this in Result<TValue, TError> result, Action<TValue> onValue, Action<TError> onError)
    {
        result.Match(onValue.AsFunc(), onError.AsFunc());
        return result;
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue> On<TValue>(this in Result<TValue> result, Action<TValue> onValue, Action<Exception> onError)
    {
        result.Match(onValue.AsFunc(), onError.AsFunc());
        return result;
    }

    /// <summary>
    /// If the result is Value invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue, TError> OnValue<TValue, TError>(this in Result<TValue, TError> result, Action<TValue> onValue)
    {
        result.Match(onValue.AsFunc(), _ => new());
        return result;
    }

    /// <summary>
    /// If the result is Value invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue> OnValue<TValue>(this in Result<TValue> result, Action<TValue> onValue)
    {
        result.Match(onValue.AsFunc(), _ => new());
        return result;
    }

    /// <summary>
    /// If the result is Error invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onError">A delegate.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue, TError> OnError<TValue, TError>(this in Result<TValue, TError> result, Action<TError> onError)
    {
        result.Match(_ => new(), onError.AsFunc());
        return result;
    }

    /// <summary>
    /// If the result is Error invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onError">A delegate.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue> OnError<TValue>(this in Result<TValue> result, Action<Exception> onError)
    {
        result.Match(_ => new(), onError.AsFunc());
        return result;
    }
}