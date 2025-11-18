namespace PureMonads;

/// <summary>
/// Contains extension methods for Option monad.
/// </summary>
public static partial class OptionExtensions
{
    /// <summary>
    /// Returns a value extracted from Some or <paramref name="alternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="alternativeValue">An alternative value.</param>
    /// <returns>A value extracted from Some or <paramref name="alternativeValue"/>.</returns>
    public static TValue Or<TValue>(this in Option<TValue> option, TValue alternativeValue)
    {
        return option.Match(value => value, () => alternativeValue);
    }

    /// <summary>
    /// Returns a value extracted from Some or returned from <paramref name="getAlternativeValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="getAlternativeValue">An alternative value factory.</param>
    /// <returns>
    /// A value extracted from Some or returned from <paramref name="getAlternativeValue"/>.
    /// </returns>
    public static TValue Or<TValue>(this in Option<TValue> option, Func<TValue> getAlternativeValue)
    {
        return option.Match(value => value, getAlternativeValue);
    }

    /// <summary>
    /// Returns the original option if it is Some or <paramref name="alternativeOption"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="alternativeOption">An alternative option.</param>
    /// <returns>The original option or <paramref name="alternativeOption"/>.</returns>
    public static Option<TValue> Or<TValue>(this in Option<TValue> option, Option<TValue> alternativeOption)
    {
        return option.Match(value => value, () => alternativeOption);
    }

    /// <summary>
    /// Returns the original option if it is Some or an option returned from <paramref name="getAlternativeOption"/>.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="option">The option.</param>
    /// <param name="getAlternativeOption">An alternative option factory.</param>
    /// <returns>
    /// The original option or an option returned from <paramref name="getAlternativeOption"/>.
    /// </returns>
    public static Option<TValue> Or<TValue>(this in Option<TValue> option, Func<Option<TValue>> getAlternativeOption)
    {
        return option.Match(value => value, getAlternativeOption);
    }
}