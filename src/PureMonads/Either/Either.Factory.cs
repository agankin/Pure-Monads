namespace PureMonads;

/// <summary>
/// Contains methods for creating instances of Either monad.
/// </summary>
public static class Either
{
    /// <summary>
    /// Wraps a value into Left Either.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="value">A value.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft left) => left;

    /// <summary>
    /// Wraps a value into Right Either.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="value">A value.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight right) => right;
}