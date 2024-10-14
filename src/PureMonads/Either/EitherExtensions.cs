namespace PureMonads;

/// <summary>
/// Contains extension methods for Either monad.
/// </summary>
public static class EitherExtensions
{
    /// <summary>
    /// Converts to Option monad returning Some with a value extracted from Left or None if it's Right.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TLeft> Left<TLeft, TRight>(this in Either<TLeft, TRight> either) =>
        either.Match(Option.Some, _ => Option.None<TLeft>());

    /// <summary>
    /// Converts to Option monad returning Some with a value extracted from Right or None if it's Left.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TRight> Right<TLeft, TRight>(this in Either<TLeft, TRight> either) =>
        either.Match(_ => Option.None<TRight>(), Option.Some);

    /// <summary>
    /// Maps a left value if the Either is Left.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <typeparam name="TResult">Mapped left value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="mapLeft">A mapping delegate.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Either<TResult, TRight> MapLeft<TLeft, TRight, TResult>(
        this in Either<TLeft, TRight> either,
        Func<TLeft, TResult> mapLeft)
    {
        return either.Match(value => mapLeft(value), Either<TResult, TRight>.Right);
    }

    /// <summary>
    /// Maps a right value if the Either is Right.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <typeparam name="TResult">Mapped right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="mapRight">A mapping delegate.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Either<TLeft, TResult> MapRight<TLeft, TRight, TResult>(
        this in Either<TLeft, TRight> either,
        Func<TRight, TResult> mapRight)
    {
        return either.Match(Either<TLeft, TResult>.Left, value => mapRight(value));
    }

    /// <summary>
    /// Maps a left value if the Either is Left.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <typeparam name="TResult">Mapped left value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="mapLeft">A mapping delegate.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Either<TResult, TRight> FlatMapLeft<TLeft, TRight, TResult>(
        this in Either<TLeft, TRight> either,
        Func<TLeft, Either<TResult, TRight>> mapLeft)
    {
        return either.Match(value => mapLeft(value), Either<TResult, TRight>.Right);
    }

    /// <summary>
    /// Maps a right value if the Either is Right.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <typeparam name="TResult">Mapped right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="mapRight">A mapping delegate.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Either<TLeft, TResult> FlatMapRight<TLeft, TRight, TResult>(
        this in Either<TLeft, TRight> either,
        Func<TRight, Either<TLeft, TResult>> mapRight)
    {
        return either.Match(Either<TLeft, TResult>.Left, value => mapRight(value));
    }

    /// <summary>
    /// Matches Left or Right by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onLeft">A delegate invoked on Left.</param>
    /// <param name="onRight">A delegate invoked on Right.</param>
    public static void On<TLeft, TRight>(this in Either<TLeft, TRight> either, Action<TLeft> onLeft, Action<TRight> onRight) =>
        either.Match(onLeft.AsFunc(), onRight.AsFunc());

    /// <summary>
    /// If the Either is Left invokes the given delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onLeft">A delegate.</param>
    public static void OnLeft<TLeft, TRight>(this in Either<TLeft, TRight> either, Action<TLeft> onLeft) =>
        either.Match(onLeft.AsFunc(), _ => new());

    /// <summary>
    /// If the Either is Right invokes the given delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onRight">A delegate.</param>
    public static void OnRight<TLeft, TRight>(this in Either<TLeft, TRight> either, Action<TRight> onRight) =>
        either.Match(_ => new(), onRight.AsFunc());
}