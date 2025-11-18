namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncResult monad.
/// </summary>
public static partial class AsyncResultExtensions
{
    /// <summary>
    /// Maps a value if the async result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static Task<AsyncResult<TResult, TError>> FlatMapAsync<TValue, TResult, TError>(
        this in AsyncResult<TValue, TError> result,
        Func<TValue, AsyncResult<TResult, TError>> map)
    {
        return result.Match(
            task => task.Map(map),
            error => AsyncResult<TResult, TError>.Error(error).AsTask());
    }

    /// <summary>
    /// Maps a value if the async result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static Task<AsyncResult<TResult>> FlatMapAsync<TValue, TResult>(
        this in AsyncResult<TValue> result,
        Func<TValue, AsyncResult<TResult>> map)
    {
        return result.Match(
            task => task.Map(map),
            error => AsyncResult<TResult>.Error(error).AsTask());
    }
}