namespace PureMonads;

public static class Comonad2Extensions
{
    /// <summary>
    /// Transforms a comonad into a new one by applying a mapping function to a wrapped value.
    /// </summary>
    /// <typeparam name="T1">First value type.</typeparam>
    /// <typeparam name="T2">Second value type.</typeparam>
    /// <typeparam name="TResult">Result comonad value type.</typeparam>
    /// <typeparam name="TComonad">Comonad type.</typeparam>
    /// <param name="comonad">A comonad.</param>
    /// <param name="map">A mapping function.</param>
    /// <returns>A new comonad.</returns>
    public static IComonad2<TResult, T2, TComonad> Map<T1, T2, TResult, TComonad>(
        this IComonad2<T1, T2, TComonad> comonad,
        Func<T1, TResult> map
    )
    {
        var result = comonad.Extract(
            value1 => value1.Pipe(map).Pipe(comonad.Unit<TResult, T2>),
            value2 => value2.Pipe(comonad.Unit<TResult, T2>));

        return result;
    }

    /// <summary>
    /// Transforms a comonad into a new one by applying a mapping function to a wrapped value.
    /// </summary>
    /// <typeparam name="T1">First value type.</typeparam>
    /// <typeparam name="T2">Second value type.</typeparam>
    /// <typeparam name="TResult">Result comonad value type.</typeparam>
    /// <typeparam name="TComonad">Comonad type.</typeparam>
    /// <param name="comonad">A comonad.</param>
    /// <param name="map">A mapping function.</param>
    /// <returns>A new comonad.</returns>
    public static IComonad2<T1, TResult, TComonad> Map2<T1, T2, TResult, TComonad>(
        this IComonad2<T1, T2, TComonad> comonad,
        Func<T2, TResult> map
    )
    {
        var result = comonad.Extract(
            value1 => value1.Pipe(comonad.Unit<T1, TResult>),
            value2 => value2.Pipe(map).Pipe(comonad.Unit<T1, TResult>));

        return result;
    }

    /// <summary>
    /// Transforms a comonad into a new one by applying a mapping function to a wrapped value.
    /// </summary>
    /// <typeparam name="T1">First value type.</typeparam>
    /// <typeparam name="T2">Second value type.</typeparam>
    /// <typeparam name="TResult">Result comonad value type.</typeparam>
    /// <typeparam name="TComonad">Comonad type.</typeparam>
    /// <param name="comonad">A comonad.</param>
    /// <param name="map">A mapping function.</param>
    /// <returns>A new comonad.</returns>
    public static IComonad2<TResult, T2, TComonad> FlatMap<T1, T2, TResult, TComonad>(
        this IComonad2<T1, T2, TComonad> comonad,
        Func<T1, IComonad2<TResult, T2, TComonad>> map
    )
    {
        var resultComonad = comonad.Extract(
            value1 => value1.Pipe(map),
            value2 => value2.Pipe(comonad.Unit<TResult, T2>));

        return resultComonad;
    }
    
    /// <summary>
    /// Transforms a comonad into a new one by applying a mapping function to a wrapped value.
    /// </summary>
    /// <typeparam name="T1">First value type.</typeparam>
    /// <typeparam name="T2">Second value type.</typeparam>
    /// <typeparam name="TResult">Result comonad value type.</typeparam>
    /// <typeparam name="TComonad">Comonad type.</typeparam>
    /// <param name="comonad">A comonad.</param>
    /// <param name="map">A mapping function.</param>
    /// <returns>A new comonad.</returns>
    public static IComonad2<T1, TResult, TComonad> FlatMap2<T1, T2, TResult, TComonad>(
        this IComonad2<T1, T2, TComonad> comonad,
        Func<T2, IComonad2<T1, TResult, TComonad>> map
    )
    {
        var resultComonad = comonad.Extract(
            value1 => value1.Pipe(comonad.Unit<T1, TResult>),
            value2 => value2.Pipe(map));

        return resultComonad;
    }
}