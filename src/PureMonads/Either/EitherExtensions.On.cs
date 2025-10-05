using System.Threading.Tasks;

namespace PureMonads;

/// <summary>
/// Contains extension methods for Either monad.
/// </summary>
public static partial class EitherExtensions
{
    /// <summary>
    /// Matches Left or Right by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onLeft">A delegate invoked on Left.</param>
    /// <param name="onRight">A delegate invoked on Right.</param>
    /// <returns>The same Either monad.</returns>
    public static Either<TLeft, TRight> On<TLeft, TRight>(this in Either<TLeft, TRight> either, Action<TLeft> onLeft, Action<TRight> onRight)
    {
        either.Match(onLeft.AsFunc(), onRight.AsFunc());
        return either;
    }

    /// <summary>
    /// Matches Left or Right by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onLeftAsync">A delegate invoked on Left.</param>
    /// <param name="onRight">A delegate invoked on Right.</param>
    /// <returns>The same Either monad.</returns>
    public static async Task<Either<TLeft, TRight>> OnAsync<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task> onLeftAsync, Action<TRight> onRight)
    {
        await either.Match(onLeftAsync, onRight.AsAsyncFunc());
        return either;
    }

    /// <summary>
    /// Matches Left or Right by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onLeft">A delegate invoked on Left.</param>
    /// <param name="onRightAsync">A delegate invoked on Right.</param>
    /// <returns>The same Either monad.</returns>
    public static async Task<Either<TLeft, TRight>> OnAsync<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> onLeft, Func<TRight, Task> onRightAsync)
    {
        await either.Match(onLeft.AsAsyncFunc(), onRightAsync);
        return either;
    }

    /// <summary>
    /// Matches Left or Right by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onLeftAsync">A delegate invoked on Left.</param>
    /// <param name="onRightAsync">A delegate invoked on Right.</param>
    /// <returns>The same Either monad.</returns>
    public static async Task<Either<TLeft, TRight>> OnAsync<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TLeft, Task> onLeftAsync,
        Func<TRight, Task> onRightAsync)
    {
        await either.Match(onLeftAsync, onRightAsync);
        return either;
    }

    /// <summary>
    /// If the Either is Left invokes the given delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onLeft">A delegate.</param>
    /// <returns>The same Either monad.</returns>
    public static Either<TLeft, TRight> OnLeft<TLeft, TRight>(this in Either<TLeft, TRight> either, Action<TLeft> onLeft)
    {
        either.Match(onLeft.AsFunc(), _ => new());
        return either;
    }

    /// <summary>
    /// If the Either is Left invokes the given delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onLeftAsync">A delegate.</param>
    /// <returns>The same Either monad.</returns>
    public static async Task<Either<TLeft, TRight>> OnLeftAsync<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Task> onLeftAsync)
    {
        await either.Match(onLeftAsync, _ => Task.CompletedTask);
        return either;
    }

    /// <summary>
    /// If the Either is Right invokes the given delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onRight">A delegate.</param>
    /// <returns>The same Either monad.</returns>
    public static Either<TLeft, TRight> OnRight<TLeft, TRight>(this in Either<TLeft, TRight> either, Action<TRight> onRight)
    {
        either.Match(_ => new(), onRight.AsFunc());
        return either;
    }

    /// <summary>
    /// If the Either is Right invokes the given delegate.
    /// </summary>
    /// <typeparam name="TLeft">Left value type.</typeparam>
    /// <typeparam name="TRight">Right value type.</typeparam>
    /// <param name="either">The Either.</param>
    /// <param name="onRightAsync">A delegate.</param>
    /// <returns>The same Either monad.</returns>
    public static async Task<Either<TLeft, TRight>> OnRightAsync<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight, Task> onRightAsync)
    {
        await either.Match(_ => Task.CompletedTask, onRightAsync);
        return either;
    }
}