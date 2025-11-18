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
    public static AsyncResult<TResult, TError> Map<TValue, TResult, TError>(
        this AsyncResult<TValue, TError> result,
        Func<TValue, TResult> map)
    {
        return result.Match(task => task.Map(map), AsyncResult<TResult, TError>.Error);
    }

    /// <summary>
    /// Maps a value if the async result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The async result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TResult> Map<TValue, TResult>(this AsyncResult<TValue> result, Func<TValue, TResult> map)
    {
        return result.Match(task => task.Map(map), AsyncResult<TResult>.Error);
    }
}