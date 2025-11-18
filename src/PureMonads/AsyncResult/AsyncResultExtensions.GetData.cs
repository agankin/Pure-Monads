namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncResult monad.
/// </summary>
public static partial class AsyncResultExtensions
{
    /// <summary>
    /// Converts to AsyncOption monad returning Some if the result is Value or None if the result is Error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TValue> AsyncValue<TValue, TError>(this in AsyncResult<TValue, TError> result)
    {
        return result.Match(AsyncOption.Some, _ => AsyncOption.None<TValue>());
    }

    /// <summary>
    /// Converts to AsyncOption monad returning Some if the result is Value or None if the result is Error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TValue> AsyncValue<TValue>(this in AsyncResult<TValue> result)
    {
        return result.Match(AsyncOption.Some, _ => AsyncOption.None<TValue>());
    }

    /// <summary>
    /// Converts to AsyncOption monad returning Some if the result is Error or None if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TError> Error<TValue, TError>(this AsyncResult<TValue, TError> result)
    {
        return result.Match(_ => AsyncOption.None<TError>(), error => error.AsTask());
    }

    /// <summary>
    /// Converts to AsyncOption monad returning Some if the result is Error or None if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<Exception> Error<TValue>(this AsyncResult<TValue> result)
    {
        return result.Match(_ => AsyncOption.None<Exception>(), error => error.AsTask());
    }
}