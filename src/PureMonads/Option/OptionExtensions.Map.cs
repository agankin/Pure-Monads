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
    public static Option<TResult> Map<TValue, TResult>(this in Option<TValue> option, Func<TValue, TResult> map) =>
        option.Match(value => map(value), Option<TResult>.None);
}