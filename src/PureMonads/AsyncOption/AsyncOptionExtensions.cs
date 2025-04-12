namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncOption monad.
/// </summary>
public static class AsyncOptionExtensions
{
    /// <summary>
    /// Maps a value if the async option is Some.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TResult> Map<TValue, TResult>(this in AsyncOption<TValue> asyncOption, Func<TValue, TResult> map)
    {
        return asyncOption.Match(
            mapSome: task => AsyncOption.Some(task.Map(map)),
            onNone: AsyncOption<TResult>.None);
    }

    /// <summary>
    /// Maps a value if the async option is Some.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="asyncMap">A mapping delegate.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TResult> Map<TValue, TResult>(this in AsyncOption<TValue> asyncOption, Func<TValue, Task<TResult>> asyncMap)
    {
        return asyncOption.Match(
            mapSome: task => AsyncOption.Some(task.Map(asyncMap)),
            onNone: AsyncOption<TResult>.None);
    }

    /// <summary>
    /// Maps a value if the async option is Some.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static async Task<AsyncOption<TResult>> FlatMapAsync<TValue, TResult>(this AsyncOption<TValue> asyncOption, Func<TValue, AsyncOption<TResult>> map)
    {
        return await asyncOption.Match(
            mapSome: task => task.Map(map),
            onNone: () => Task.FromResult(AsyncOption<TResult>.None()));
    }
         
    /// <summary>
    /// Returns a value extracted from Some or <paramref name="alternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="alternativeValue">An alternative value.</param>
    /// <returns>A value extracted from Some or <paramref name="alternativeValue"/>.</returns>
    public static async Task<TValue> OrAsync<TValue>(this AsyncOption<TValue> asyncOption, TValue alternativeValue)
    {
        return await asyncOption.Match(
            mapSome: task => task,
            onNone: () => Task.FromResult(alternativeValue));
    }

    /// <summary>
    /// Returns a value extracted from Some or returned from <paramref name="getAlternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="getAlternativeValue">An alternative value factory.</param>
    /// <returns>
    /// A value extracted from Some or returned from <paramref name="getAlternativeValue"/>.
    /// </returns>
    public static async Task<TValue> OrAsync<TValue>(this AsyncOption<TValue> asyncOption, Func<TValue> getAlternativeValue)
    {
        return await asyncOption.Match(
            mapSome: task => task,
            onNone: () => Task.FromResult(getAlternativeValue()));
    }

    /// <summary>
    /// Returns the original async option if it is Some or <paramref name="alternativeOption"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="alternativeOption">An alternative option.</param>
    /// <returns>The original async option or <paramref name="alternativeOption"/>.</returns>
    public static AsyncOption<TValue> Or<TValue>(this in AsyncOption<TValue> asyncOption, AsyncOption<TValue> alternativeOption)
    {
        return asyncOption.Match(
            mapSome: task => task,
            onNone: () => alternativeOption);
    }

    /// <summary>
    /// Returns the original async option if it is Some or an async option returned from <paramref name="getAlternativeOption"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="getAlternativeOption">An alternative async option factory.</param>
    /// <returns>
    /// The original async option or an async option returned from <paramref name="getAlternativeOption"/>.
    /// </returns>
    public static AsyncOption<TValue> Or<TValue>(this in AsyncOption<TValue> asyncOption, Func<AsyncOption<TValue>> getAlternativeOption)
    {
        return asyncOption.Match(
            mapSome: task => task,
            onNone: getAlternativeOption);
    }

    /// <summary>
    /// Extracts a value from Some or throws an exception.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <returns>An extracted value.</returns>
    /// <exception cref="Exception">The exception thrown if the async option is None.</exception>
    public static async Task<TValue> ValueOrFailureAsync<TValue>(this AsyncOption<TValue> asyncOption, string? message = null)
    {
        return await asyncOption.OrAsync(() => throw new Exception(message ?? "AsyncOption is None."));
    }
}