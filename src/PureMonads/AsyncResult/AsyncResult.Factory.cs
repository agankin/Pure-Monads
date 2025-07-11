namespace PureMonads;

/// <summary>
/// Contains methods for creating instances of AsyncResult monad.
/// </summary>
public static class AsyncResult
{
    /// <summary>
    /// Wraps a task in Value async result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="task">A task.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TValue, TError> Value<TValue, TError>(Task<TValue> task) => task;

    /// <summary>
    /// Wraps a task in Value async result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="task">A task.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TValue> Value<TValue>(Task<TValue> task) => task;

    /// <summary>
    /// Wraps an error in Error async result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="error">An error.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TValue, TError> Error<TValue, TError>(TError error) => error;

    /// <summary>
    /// Wraps an error in Error async result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="error">An exception.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TValue> Error<TValue>(Exception error) => error;
}