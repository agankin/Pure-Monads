namespace PureMonads;

/// <summary>
/// Contains extension methods for Either monad.
/// </summary>
public static partial class EitherExtensions
{
    /// <summary>
    /// Converts to Option monad returning Some with a value extracted from Left or None if it's Right.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TLeft> Left<TLeft, TRight>(this in Either<TLeft, TRight> either)
    {
        return either.Match(Option.Some, _ => Option.None<TLeft>());
    }

    /// <summary>
    /// Converts to Option monad returning Some with a value extracted from Right or None if it's Left.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TRight> Right<TLeft, TRight>(this in Either<TLeft, TRight> either)
    {
        return either.Match(_ => Option.None<TRight>(), Option.Some);
    }
}