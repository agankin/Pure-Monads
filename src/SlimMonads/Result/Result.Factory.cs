namespace SlimMonads;

/// <summary>
/// Contains methods for creating instances of Result.
/// </summary>
public static class Result
{
    /// <summary>
    /// Executes a delegate and returns wrapped into Result a value or an occured exception.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="func">A delegate.</param>
    /// <returns>Value or Error result.</returns>
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
    /// Executes an async delegate and returns a task representing wrapped into Result a value or an occured exception.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="func">A delegate.</param>
    /// <returns>Value or Error result.</returns>
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