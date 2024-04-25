namespace PureMonads;

/// <summary>
/// Defines a comonad wrapping a value having one of 2 possible types.
/// </summary>
/// <typeparam name="T1">First value type.</typeparam>
/// <typeparam name="T2">Second value type.</typeparam>
/// <typeparam name="TComonad">Comonad actual type.</typeparam>
public interface IComonad2<T1, T2, TComonad>
{
    /// <summary>
    /// Creates a new comonad.
    /// </summary>
    /// <typeparam name="TNew1">New comonad first value type.</typeparam>
    /// <typeparam name="TNew2">New comonad second value type.</typeparam>
    /// <typeparam name="TComonad">Comonad actual type.</typeparam>
    /// <param name="value">A value to be wrapped into a comonad.</param>
    /// <returns>A new comonad instance.</returns>
    IComonad2<TNew1, TNew2, TComonad> Unit<TNew1, TNew2>(TNew1 value);

    /// <summary>
    /// Creates a new comonad.
    /// </summary>
    /// <typeparam name="TNew1">New comonad first value type.</typeparam>
    /// <typeparam name="TNew2">New comonad second value type.</typeparam>
    /// <typeparam name="TComonad">Comonad actual type.</typeparam>
    /// <param name="value">A value to be wrapped into a comonad.</param>
    /// <returns>A new comonad instance.</returns>
    IComonad2<TNew1, TNew2, TComonad> Unit<TNew1, TNew2>(TNew2 value);

    /// <summary>
    /// Extracts a value from a comonad.
    /// As two value types possible it reduces to a single result type by applying one of 2 mapping functions.
    /// </summary>
    /// <returns>A value extracted from a comonad.</returns>
    TResult Extract<TResult>(Func<T1, TResult> mapValue1, Func<T2, TResult> mapValue2);
}