using System.Threading.Tasks;

namespace PureMonads;

/// <summary>
/// Contains extension methods for Result monad.
/// </summary>
public static partial class ResultExtensions
{
    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TResult, TError> FlatMap<TValue, TResult, TError>(
        this Result<TValue, TError> result,
        Func<TValue, Result<TResult, TError>> map)
    {
        return result.Match(value => map(value), Result<TResult, TError>.Error);
    }

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TResult, TError> FlatMap<TValue, TResult, TError>(
        this Result<TValue, TError> result,
        Func<TValue, AsyncResult<TResult, TError>> map)
    {
        return result.Match(map, AsyncResult<TResult, TError>.Error);
    }

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="mapAsync">A mapping delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static async Task<Result<TResult, TError>> FlatMapAsync<TValue, TResult, TError>(
        this Result<TValue, TError> result,
        Func<TValue, Task<Result<TResult, TError>>> mapAsync)
    {
        return await result.Match(mapAsync, error => Result<TResult, TError>.Error(error).AsTask());
    }

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TResult> FlatMap<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, Result<TResult>> map)
    {
        return result.Match(map, Result<TResult>.Error);
    }

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TResult> FlatMap<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, AsyncResult<TResult>> map)
    {
        return result.Match(map, AsyncResult<TResult>.Error);
    }

    /// <summary>
    /// Maps a value if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="mapAsync">A mapping delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static async Task<Result<TResult>> FlatMapAsync<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, Task<Result<TResult>>> mapAsync)
    {
        return await result.Match(mapAsync, error => Result<TResult>.Error(error).AsTask());
    }
}