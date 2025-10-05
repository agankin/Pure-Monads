namespace PureMonads;

/// <summary>
/// Contains extension methods for Either monad.
/// </summary>
public static partial class EitherExtensions
{
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
        return either.Match(mapLeft, Either<TResult, TRight>.Right);
    }

    /// <summary>
    /// Maps a left value if the Either is Left.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <typeparam name="TResult">Mapped left value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="mapLeftAsync">A mapping delegate.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Task<Either<TResult, TRight>> FlatMapLeftAsync<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> either,
        Func<TLeft, Task<Either<TResult, TRight>>> mapLeftAsync)
    {
        return either.Match(
            mapLeftAsync,
            right => Either<TResult, TRight>.Right(right).AsTask());
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
        return either.Match(Either<TLeft, TResult>.Left, mapRight);
    }

    /// <summary>
    /// Maps a right value if the Either is Right.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <typeparam name="TResult">Mapped right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="mapRightAsync">A mapping delegate.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Task<Either<TLeft, TResult>> FlatMapRightAsync<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> either,
        Func<TRight, Task<Either<TLeft, TResult>>> mapRightAsync)
    {
        return either.Match(
            left => Either<TLeft, TResult>.Left(left).AsTask(),
            mapRightAsync);
    }
}