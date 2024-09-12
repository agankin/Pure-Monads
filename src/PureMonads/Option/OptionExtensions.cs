namespace PureMonads;

/// <summary>
/// Contains extension methods for Option monad.
/// </summary>
public static class OptionExtensions
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

    /// <summary>
    /// Maps a value if the option is Some.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Mapped value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="map">A mapping delegate.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TResult> FlatMap<TValue, TResult>(this in Option<TValue> option, Func<TValue, Option<TResult>> map) =>
        option.Match(value => map(value), Option<TResult>.None);
         
    /// <summary>
    /// Returns a value extracted from Some or <paramref name="alternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="alternativeValue">An alternative value.</param>
    /// <returns>A value extracted from Some or <paramref name="alternativeValue"/>.</returns>
    public static TValue Or<TValue>(this in Option<TValue> option, TValue alternativeValue) =>
        option.Match(value => value, () => alternativeValue);

    /// <summary>
    /// Returns a value extracted from Some or returned from <paramref name="getAlternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="getAlternativeValue">An alternative value factory.</param>
    /// <returns>
    /// A value extracted from Some or returned from <paramref name="getAlternativeValue"/>.
    /// </returns>
    public static TValue Or<TValue>(this in Option<TValue> option, Func<TValue> getAlternativeValue) =>
        option.Match(value => value, getAlternativeValue);

    /// <summary>
    /// Returns the original option if it is Some or <paramref name="alternativeOption"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="alternativeOption">An alternative option.</param>
    /// <returns>The original option or <paramref name="alternativeOption"/>.</returns>
    public static Option<TValue> Or<TValue>(this in Option<TValue> option, Option<TValue> alternativeOption) =>
        option.Match(value => value, () => alternativeOption);

    /// <summary>
    /// Returns the original option if it is Some or an option returned from <paramref name="getAlternativeOption"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="getAlternativeOption">An alternative option factory.</param>
    /// <returns>
    /// The original option or an option returned from <paramref name="getAlternativeOption"/>.
    /// </returns>
    public static Option<TValue> Or<TValue>(this in Option<TValue> option, Func<Option<TValue>> getAlternativeOption) =>
        option.Match(value => value, getAlternativeOption);

    /// <summary>
    /// Extracts a value from Some or throws an exception.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <returns>An extracted value.</returns>
    /// <exception cref="Exception">The exception thrown if the option is None.</exception>
    public static TValue ValueOrFailure<TValue>(this in Option<TValue> option, string? message = null) =>
        option.Or(() => throw new Exception(message ?? "Option is None."));
}