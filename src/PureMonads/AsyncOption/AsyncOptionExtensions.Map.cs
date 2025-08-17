namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncOption monad.
/// </summary>
public static partial class AsyncOptionExtensions
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
}