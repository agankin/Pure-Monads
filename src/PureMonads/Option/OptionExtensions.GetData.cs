namespace PureMonads;

/// <summary>
/// Contains extension methods for Option monad.
/// </summary>
public static partial class OptionExtensions
{
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