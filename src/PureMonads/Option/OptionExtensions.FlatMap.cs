namespace PureMonads;

/// <summary>
/// Contains extension methods for Option monad.
/// </summary>
public static partial class OptionExtensions
{
    /// <summary>
    /// Maps a value if the option is Some.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TResult> FlatMap<TValue, TResult>(this in Option<TValue> option, Func<TValue, Option<TResult>> map) =>
        option.Match(map, Option<TResult>.None);

    /// <summary>
    /// Maps a value if the option is Some.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TResult> FlatMap<TValue, TResult>(this in Option<TValue> option, Func<TValue, AsyncOption<TResult>> map) =>
        option.Match(map, AsyncOption<TResult>.None);

    /// <summary>
    /// Maps a value if the option is Some.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>A task representing an async operation returning an instance of Option monad.</returns>
    public static Task<Option<TResult>> FlatMapAsync<TValue, TResult>(this in Option<TValue> option, Func<TValue, Task<Option<TResult>>> map) =>
        option.Match(map, () => Task.FromResult(Option<TResult>.None()));
}