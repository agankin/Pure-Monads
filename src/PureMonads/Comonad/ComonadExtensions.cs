namespace PureMonads;

public static class ComonadExtensions
{
    /// <summary>
    /// Transforms a comonad into a new one by applying a mapping function to a wrapped value.
    /// </summary>
    /// <typeparam name="T">Comonad value type.</typeparam>
    /// <typeparam name="TResult">Result comonad value type.</typeparam>
    /// <typeparam name="TComonad">Comonad type.</typeparam>
    /// <param name="comonad">A comonad.</param>
    /// <param name="map">A mapping function.</param>
    /// <returns>A new comonad.</returns>
    public static IComonad<TResult, TComonad> Map<T, TResult, TComonad>(this IComonad<T, TComonad> comonad, Func<T, TResult> map)
        where TComonad : IComonad<T, TComonad>
    {
        var value = comonad.Extract();
        var result = map(value);
        var resultComonad = result.Pipe(comonad.Unit);

        return resultComonad;
    }

    /// <summary>
    /// Transforms a comonad into a new one by applying a mapping function to a wrapped value.
    /// </summary>
    /// <typeparam name="T">Comonad value type.</typeparam>
    /// <typeparam name="TResult">Result comonad value type.</typeparam>
    /// <typeparam name="TComonad">Comonad type.</typeparam>
    /// <param name="comonad">A comonad.</param>
    /// <param name="map">A mapping function.</param>
    /// <returns>A new comonad.</returns>
    public static IComonad<TResult, TComonad> FlatMap<T, TResult, TComonad>(
        this IComonad<T, TComonad> comonad,
        Func<T, IComonad<TResult, TComonad>> map
    )
        where TComonad : IComonad<T, TComonad>
    {
        var value = comonad.Extract();
        var result = map(value).Extract();

        return comonad.Unit(result);
    }
}