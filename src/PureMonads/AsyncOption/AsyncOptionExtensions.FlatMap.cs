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
    public static Task<AsyncOption<TResult>> FlatMapAsync<TValue, TResult>(
        this in AsyncOption<TValue> asyncOption,
        Func<TValue, AsyncOption<TResult>> map)
    {
        return asyncOption.Match(
            mapSome: task => task.Map(map),
            onNone: () => AsyncOption<TResult>.None().AsTask());
    }
}