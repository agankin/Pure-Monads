namespace PureMonads;

public static class MonadExtensions
{
    /// <summary>
    /// Transforms a monad into a new one by applying a mapping function to a wrapped value.
    /// </summary>
    /// <typeparam name="T">Monad value type.</typeparam>
    /// <typeparam name="TResult">Result monad value type.</typeparam>
    /// <typeparam name="TMonad">Monad type.</typeparam>
    /// <param name="monad">A monad.</param>
    /// <param name="map">A mapping function.</param>
    /// <returns>A new monad.</returns>
    public static IMonad<TResult, TMonad> Map<T, TResult, TMonad>(this IMonad<T, TMonad> monad, Func<T, TResult> map)
    {
        var resultMonad = monad.FlatMap(value => map(value).Pipe(res => monad.Unit(res)));
        return resultMonad;
    }
}