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
}