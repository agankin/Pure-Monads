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
    public static Result<TResult, TError> Map<TValue, TResult, TError>(this Result<TValue, TError> result, Func<TValue, TResult> map) =>
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
}