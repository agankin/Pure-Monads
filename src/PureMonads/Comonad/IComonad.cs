namespace PureMonads;

/// <summary>
/// Defines a comonad.
/// </summary>
/// <typeparam name="T">Comonad value type.</typeparam>
/// <typeparam name="TComonad">Comonad actual type.</typeparam>
public interface IComonad<T, TComonad>
{
    /// <summary>
    /// Creates a new comonad.
    /// </summary>
    /// <typeparam name="TNew">New comonad value type.</typeparam>
    /// <typeparam name="TComonad">Comonad actual type.</typeparam>
    /// <param name="value">A value to be wrapped into a comonad.</param>
    /// <returns>A new comonad instance.</returns>
    IComonad<TNew, TComonad> Unit<TNew>(TNew value);

    /// <summary>
    /// Extracts a value from a comonad.
    /// </summary>
    /// <returns>A value extracted from a comonad.</returns>
    T Extract();
}