namespace PureMonads;

/// <summary>
/// Contains extension methods for Option monad.
/// </summary>
public static class OptionExtensions
{
    /// <summary>
    /// If the option is Some returns the result from <paramref name="mapSome"/> invocation wrapped into Some option.
    /// Otherwise returns None.
    /// </summary>
    /// <typeparam name="TResult">Result value type.</typeparam>
    /// <param name="map">A delegate invoked if the current option is Some.</param>
    /// <returns>Some result value or None.</returns>
    public static Option<TResult> Map<TValue, TResult>(this in Option<TValue> option, Func<TValue, TResult> map) =>
        option.Match(value => map(value), Option<TResult>.None);

    /// <summary>
    /// If the option is Some returns the result option from <paramref name="mapSome"/> invocation.
    /// Otherwise returns None.
    /// </summary>
    /// <typeparam name="TResult">Result value type.</typeparam>
    /// <param name="map">A delegate invoked if the current option is Some.</param>
    /// <returns>Some result value or None.</returns>
    public static Option<TResult> FlatMap<TValue, TResult>(this in Option<TValue> option, Func<TValue, Option<TResult>> map) =>
        option.Match(value => map(value), Option<TResult>.None);
         
    /// <summary>
    /// Returns a value extracted from Some option or <paramref name="alternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">An option.</param>
    /// <param name="alternativeValue">An alternative value.</param>
    /// <returns>A value extracted from Some option or <paramref name="alternativeValue"/>.</returns>
    public static TValue Or<TValue>(this in Option<TValue> option, TValue alternativeValue) =>
        option.Match(value => value, () => alternativeValue);

    /// <summary>
    /// Returns a value extracted from Some option or a value returned from <paramref name="getAlternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">An option.</param>
    /// <param name="getAlternativeValue">An alternative value factory.</param>
    /// <returns>
    /// A value extracted from Some option or a value returned from <paramref name="alternativeValue"/>.
    /// </returns>
    public static TValue Or<TValue>(this in Option<TValue> option, Func<TValue> getAlternativeValue) =>
        option.Match(value => value, getAlternativeValue);

    /// <summary>
    /// Returns the original option if it is Some or <paramref name="alternativeOption"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">An option.</param>
    /// <param name="alternativeOption">An alternative option.</param>
    /// <returns>Original option or <paramref name="alternativeOption"/>.</returns>
    public static Option<TValue> Or<TValue>(this in Option<TValue> option, Option<TValue> alternativeOption) =>
        option.Match(value => value, () => alternativeOption);

    /// <summary>
    /// Returns the original option if it is Some or a value from <paramref name="getAlternativeOption"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">An option.</param>
    /// <param name="getAlternativeOption">An alternative option factory.</param>
    /// <returns>
    /// Original option or a value from <paramref name="getAlternativeOption"/>.
    /// </returns>
    public static Option<TValue> Or<TValue>(this in Option<TValue> option, Func<Option<TValue>> getAlternativeOption) =>
        option.Match(value => value, getAlternativeOption);

    /// <summary>
    /// Returns a value from an option or throws a error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">An option.</param>
    /// <returns>A value.</returns>
    /// <exception cref="Exception"></exception>
    public static TValue ValueOrFailure<TValue>(this in Option<TValue> option, string? message = null) =>
        option.Or(() => throw new Exception(message ?? "Option is None."));
}