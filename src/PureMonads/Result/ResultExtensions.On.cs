namespace PureMonads;

/// <summary>
/// Contains extension methods for Result monad.
/// </summary>
public static partial class ResultExtensions
{
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
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValueAsync">A delegate invoked on Value.</param>
    /// <param name="onErrorAsync">A delegate invoked on Error.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue, TError>(
        this in Result<TValue, TError> result,
        Func<TValue, Task> onValueAsync,
        Func<TError, Task> onErrorAsync)
    {
        return result.Match(onValueAsync, onErrorAsync);
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValueAsync">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue, TError>(
        this in Result<TValue, TError> result,
        Func<TValue, Task> onValueAsync,
        Action<TError> onError)
    {
        return result.Match(onValueAsync, onError.AsAsyncFunc());
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onErrorAsync">A delegate invoked on Error.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue, TError>(
        this in Result<TValue, TError> result,
        Action<TValue> onValue,
        Func<TError, Task> onErrorAsync)
    {
        return result.Match(onValue.AsAsyncFunc(), onErrorAsync);
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
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValueAsync">A delegate invoked on Value.</param>
    /// <param name="onErrorAsync">A delegate invoked on Error.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue>(
        this in Result<TValue> result,
        Func<TValue, Task> onValueAsync,
        Func<Exception, Task> onErrorAsync)
    {
        return result.Match(onValueAsync, onErrorAsync);
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValueAsync">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue>(
        this in Result<TValue> result,
        Func<TValue, Task> onValueAsync,
        Action<Exception> onError)
    {
        return result.Match(onValueAsync, onError.AsAsyncFunc());
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onErrorAsync">A delegate invoked on Error.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue>(
        this in Result<TValue> result,
        Action<TValue> onValue,
        Func<Exception, Task> onErrorAsync)
    {
        return result.Match(onValue.AsAsyncFunc(), onErrorAsync);
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
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValueAsync">A delegate.</param>
    /// <returns>A task.</returns>
    public static Task OnValueAsync<TValue, TError>(this in Result<TValue, TError> result, Func<TValue, Task> onValueAsync)
    {
        return result.Match(onValueAsync, _ => Task.CompletedTask);
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
    /// If the result is Value invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValueAsync">A delegate.</param>
    /// <returns>A task.</returns>
    public static Task OnValueAsync<TValue>(this in Result<TValue> result, Func<TValue, Task> onValueAsync)
    {
        return result.Match(onValueAsync, _ => Task.CompletedTask);
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
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onErrorAsync">A delegate.</param>
    /// <returns>A task.</returns>
    public static Task OnErrorAsync<TValue, TError>(this in Result<TValue, TError> result, Func<TError, Task> onErrorAsync)
    {
        return result.Match(_ => Task.CompletedTask, onErrorAsync);
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

    /// <summary>
    /// If the result is Error invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onErrorAsync">A delegate.</param>
    /// <returns>A task.</returns>
    public static Task OnErrorAsync<TValue>(this in Result<TValue> result, Func<Exception, Task> onErrorAsync)
    {
        return result.Match(_ => Task.CompletedTask, onErrorAsync);
    }
}