namespace PureMonads;

/// <summary>
/// Defines a monad.
/// </summary>
/// <typeparam name="T">Monad value type.</typeparam>
/// <typeparam name="TMonad">Monad actual type.</typeparam>
public interface IMonad<T, TMonad>
{
    /// <summary>
    /// Creates a new monad.
    /// </summary>
    /// <typeparam name="TNew">New monad value type.</typeparam>
    /// <param name="value">A value to be wrapped into a monad.</param>
    /// <returns>A new monad instance.</returns>
    IMonad<TNew, TMonad> Unit<TNew>(TNew value);

    /// <summary>
    /// Transforms the monad into a new one by applying a mapping function to a wrapped value.
    /// </summary>
    /// <typeparam name="TResult">Result monad value type.</typeparam>
    /// <param name="map">A mapping function.</param>
    /// <returns>A new monad.</returns>
    public IMonad<TResult, TMonad> FlatMap<TResult>(Func<T, IMonad<TResult, TMonad>> map);
}