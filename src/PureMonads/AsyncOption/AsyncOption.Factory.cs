namespace PureMonads;

/// <summary>
/// Contains methods for creating AsyncOption instances.
/// </summary>
public static class AsyncOption
{
    /// <summary>
    /// Wraps the task in Some async option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="task">The task.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TValue> Some<TValue>(this Task<TValue> task) => task;

    /// <summary>
    /// Creates an instance of None async option.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TValue> None<TValue>() => AsyncOption<TValue>.None();
}