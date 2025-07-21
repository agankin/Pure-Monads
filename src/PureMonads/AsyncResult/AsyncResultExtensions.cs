namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncResult monad.
/// </summary>
public static class AsyncResultExtensions
{
    /// <summary>
    /// Converts to AsyncOption monad returning Some if the result is Value or None if the result is Error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TValue> AsyncValue<TValue, TError>(this in AsyncResult<TValue, TError> result) =>
        result.Match(AsyncOption.Some, _ => AsyncOption.None<TValue>());

    /// <summary>
    /// Converts to AsyncOption monad returning Some if the result is Value or None if the result is Error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TValue> AsyncValue<TValue>(this in AsyncResult<TValue> result) =>
        result.Match(AsyncOption.Some, _ => AsyncOption.None<TValue>());

    /// <summary>
    /// Converts to AsyncOption monad returning Some if the result is Error or None if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TError> Error<TValue, TError>(this AsyncResult<TValue, TError> result) =>
        result.Match(_ => AsyncOption.None<TError>(), error => error.AsTask());

    /// <summary>
    /// Converts to AsyncOption monad returning Some if the result is Error or None if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<Exception> Error<TValue>(this AsyncResult<TValue> result) =>
        result.Match(_ => AsyncOption.None<Exception>(), error => error.AsTask());

    /// <summary>
    /// Maps a value if the async result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TResult, TError> Map<TValue, TResult, TError>(this AsyncResult<TValue, TError> result, Func<TValue, TResult> map) =>
        result.Match(
            task => task.Map(map),
            AsyncResult<TResult, TError>.Error);

    /// <summary>
    /// Maps a value if the async result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static async Task<AsyncResult<TResult, TError>> FlatMapAsync<TValue, TResult, TError>(
        this AsyncResult<TValue, TError> result,
        Func<TValue, AsyncResult<TResult, TError>> map)
    {
        return await result.Match(
            task => task.Map(map),
            error => Task.FromResult(AsyncResult<TResult, TError>.Error(error)));
    }

    /// <summary>
    /// Maps a value if the async result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TResult> Map<TValue, TResult>(this AsyncResult<TValue> result, Func<TValue, TResult> map) =>
        result.Match(task => task.Map(map), AsyncResult<TResult>.Error);

    /// <summary>
    /// Maps a value if the async result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static async Task<AsyncResult<TResult>> FlatMapAsync<TValue, TResult>(
        this AsyncResult<TValue> result,
        Func<TValue, AsyncResult<TResult>> map)
    {
        return await result.Match(
            task => task.Map(map),
            error => Task.FromResult(AsyncResult<TResult>.Error(error)));
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    public static async Task OnAsync<TValue, TError>(
        this AsyncResult<TValue, TError> result,
        Action<TValue> onValue,
        Action<TError> onError)
    {
        await result.Match(
            task => task.Map(onValue.AsFunc()),
            onError.AsAsyncFunc().Invoke);
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    public static async Task OnAsync<TValue>(
        this AsyncResult<TValue> result,
        Action<TValue> onValue,
        Action<Exception> onError)
    {
        await result.Match(
            task => task.Map(onValue.AsFunc()),
            onError.AsAsyncFunc().Invoke);
    }

    /// <summary>
    /// If the result is Value invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="onValue">A delegate.</param>
    public static async Task OnValueAsync<TValue, TError>(this AsyncResult<TValue, TError> result, Action<TValue> onValue)
    {
        await result.Match(
            task => task.Map(onValue.AsAsyncFunc()),
            _ => Task.CompletedTask);
    }

    /// <summary>
    /// If the result is Value invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="onValue">A delegate.</param>
    public static async Task OnValueAsync<TValue>(this AsyncResult<TValue> result, Action<TValue> onValue)
    {
        await result.Match(
            task => task.Map(onValue.AsAsyncFunc()),
            _ => Task.CompletedTask);
    }

    /// <summary>
    /// If the result is Error invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="onError">A delegate.</param>
    public static void OnError<TValue, TError>(
        this AsyncResult<TValue, TError> result,
        Action<TError> onError)
    {
        result.Match(
            _ => new(),
            error => onError.AsFunc().Invoke(error));
    }

    /// <summary>
    /// If the result is Error invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="onError">A delegate.</param>
    public static void OnError<TValue>(this AsyncResult<TValue> result, Action<Exception> onError)
    {
        result.Match(
            _ => new(),
            error => onError.AsFunc().Invoke(error));
    }
}