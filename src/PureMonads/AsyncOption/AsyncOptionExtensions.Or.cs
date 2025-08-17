namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncOption monad.
/// </summary>
public static partial class AsyncOptionExtensions
{
    /// <summary>
    /// Returns a value extracted from Some or <paramref name="alternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="alternativeValue">An alternative value.</param>
    /// <returns>A value extracted from Some or <paramref name="alternativeValue"/>.</returns>
    public static Task<TValue> OrAsync<TValue>(this in AsyncOption<TValue> asyncOption, TValue alternativeValue)
    {
        return asyncOption.Match(
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
    public static Task<TValue> OrAsync<TValue>(this in AsyncOption<TValue> asyncOption, Func<TValue> getAlternativeValue)
    {
        return asyncOption.Match(
            mapSome: task => task,
            onNone: () => Task.FromResult(getAlternativeValue()));
    }

    /// <summary>
    /// Returns a value extracted from Some or <paramref name="alternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <param name="alternativeValue">An alternative value.</param>
    /// <returns>A value extracted from Some or <paramref name="alternativeValue"/>.</returns>
    public static Task<TValue> OrAsync<TValue>(this in AsyncOption<TValue> asyncOption, Task<TValue> alternativeValue)
    {
        return asyncOption.Match(
            mapSome: task => task,
            onNone: () => alternativeValue);
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
    public static Task<TValue> OrAsync<TValue>(this in AsyncOption<TValue> asyncOption, Func<Task<TValue>> getAlternativeValue)
    {
        return asyncOption.Match(
            mapSome: task => task,
            onNone: getAlternativeValue);
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
}