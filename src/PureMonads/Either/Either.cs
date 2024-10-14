namespace PureMonads;

/// <summary>
/// Contains either one of two possible values: Left or Right.
/// </summary>
/// <typeparam name="TLeft">Left value type.</typeparam>
/// <typeparam name="TRight">Right value type.</typeparam>
public readonly struct Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>
{
    private readonly TLeft _left = default!;
    private readonly TRight _right = default!;

    public Either(TLeft left) => (_left, IsLeft) = (left, true);

    public Either(TRight right) => (_right, IsLeft) = (right, false);

    /// <summary>
    /// Contains true if the Either is Left or false if it's Right.
    /// </summary>
    public bool IsLeft { get; }

    /// <summary>
    /// Wraps a value into Left Either.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Either<TLeft, TRight> Left(TLeft left) => new(left);

    /// <summary>
    /// Wraps a value into Right Either.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>An instance of Either monad.</returns>
    public static Either<TLeft, TRight> Right(TRight right) => new(right);

    /// <summary>
    /// Matches Left or Right by invoking the corresponding delegate.
    /// </summary>
    /// <param name="onLeft">A delegate invoked on Left.</param>
    /// <param name="onRight">A delegate invoked on Right.</param>
    /// <returns>A result returned from the matched delegate invocation.</returns>
    public TResult Match<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight)
    {
        return IsLeft ? onLeft(_left) : onRight(_right);
    }

    /// <inheritdoc/>
    public override string ToString() => Match(left => $"Left({left})", right => $"Right({right})");

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is Either<TLeft, TRight> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => IsLeft
        ? _left?.GetHashCode() ?? 0
        : _right?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public bool Equals(Either<TLeft, TRight> other)
    {
        return IsLeft == other.IsLeft
            && IsLeft 
                ? EqualityComparer<TLeft>.Default.Equals(_left, other._left)
                : EqualityComparer<TRight>.Default.Equals(_right, other._right);
    }

    public static bool operator ==(Either<TLeft, TRight> first, Either<TLeft, TRight> second) => first.Equals(second);

    public static bool operator !=(Either<TLeft, TRight> first, Either<TLeft, TRight> second) => !first.Equals(second);

    public static implicit operator Either<TLeft, TRight>(TLeft value) => new Either<TLeft, TRight>(value);

    public static implicit operator Either<TLeft, TRight>(TRight value) => new Either<TLeft, TRight>(value);
}