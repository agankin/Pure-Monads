namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncResult monad.
/// </summary>
public static partial class AsyncResultExtensions
{
    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    public static Task OnAsync<TValue, TError>(
        this AsyncResult<TValue, TError> result,
        Action<TValue> onValue,
        Action<TError> onError)
    {
        return result.Match(
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
    public static Task OnAsync<TValue>(
        this AsyncResult<TValue> result,
        Action<TValue> onValue,
        Action<Exception> onError)
    {
        return result.Match(
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
    public static Task OnValueAsync<TValue, TError>(this AsyncResult<TValue, TError> result, Action<TValue> onValue)
    {
        return result.Match(
            task => task.Map(onValue.AsAsyncFunc()),
            _ => Task.CompletedTask);
    }

    /// <summary>
    /// If the result is Value invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="onValue">A delegate.</param>
    public static Task OnValueAsync<TValue>(this AsyncResult<TValue> result, Action<TValue> onValue)
    {
        return result.Match(
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