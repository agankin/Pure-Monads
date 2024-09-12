namespace PureMonads;

/// <summary>
/// Contains methods for creating instances of Result monad.
/// </summary>
public static class Result
{
    /// <summary>
    /// Wraps a value in Value result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="value">A value.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TValue, TError> Value<TValue, TError>(TValue value) => value;

    /// <summary>
    /// Wraps a value in Value result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="value">A value.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TValue> Value<TValue>(TValue value) => value;

    /// <summary>
    /// Wraps an error in Error result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="error">An error.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TValue, TError> Error<TValue, TError>(TError error) => error;

    /// <summary>
    /// Wraps an error in Error result.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="error">An exception.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TValue> Error<TValue>(Exception error) => error;

    /// <summary>
    /// Executes a delegate and returns an instance of Result monad containing either
    /// a returned value or an exception occured.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="func">A delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static Result<TResult> From<TResult>(Func<TResult> func)
    {
        try
        {
            return func();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    /// <summary>
    /// Executes an async delegate and returns a task representing an instance of Result monad containing either
    /// a returned value or an exception occured.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="func">A delegate.</param>
    /// <returns>An instance of Result monad.</returns>
    public static async Task<Result<TResult>> FromAsync<TResult>(Func<Task<TResult>> asyncFunc)
    {
        try
        {
            Result<TResult> func = await asyncFunc();
            return func;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}