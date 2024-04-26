namespace PureMonads;

/// <summary>
/// Contains methods for creating instances of Result.
/// </summary>
public class Result
{
    private Result() {}

    /// <summary>
    /// Creates an instance of value result.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>A instance of value result.</returns>
    public static Result<TValue, TError> Value<TValue, TError>(TValue value) => new(value);

    /// <summary>
    /// Creates an instance of error result.
    /// </summary>
    /// <param name="error">An error.</param>
    /// <returns>An instance of error result.</returns>
    public static Result<TValue, TError> Error<TValue, TError>(TError error) => new(error);

    /// <summary>
    /// Creates an instance of value result.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>An instance of value result.</returns>
    public static Result<TValue> Value<TValue>(TValue value) => new(value);

    /// <summary>
    /// Creates an instance of exception result.
    /// </summary>
    /// <param name="error">An exception.</param>
    /// <returns>An instance of exception result.</returns>
    public static Result<TValue> Error<TValue>(Exception error) => new(error);
    
    /// <summary>
    /// Executes a delegate and returns a resulting value or an occured exception wrapped into Result.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="func">A delegate.</param>
    /// <returns>An instance of a value or an error result.</returns>
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
    /// Executes an async delegate and returns a task representing a resulting value or an occured exception wrapped into Result.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="func">A delegate.</param>
    /// <returns>An instance of a value or an error result.</returns>
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