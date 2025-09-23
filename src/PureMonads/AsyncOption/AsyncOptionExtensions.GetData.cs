namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncOption monad.
/// </summary>
public static partial class AsyncOptionExtensions
{
    /// <summary>
    /// Extracts a value from Some or throws an exception.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <returns>An extracted value.</returns>
    /// <exception cref="Exception">The exception thrown if the async option is None.</exception>
    public static Task<TValue> ValueOrFailureAsync<TValue>(this in AsyncOption<TValue> asyncOption, string? message = null)
    {
        return asyncOption.OrAsync(new Func<TValue>(() => throw new Exception(message ?? "AsyncOption is None.")));
    }

    /// <summary>
    /// Awaits and returns result representing an option or exception occured.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="asyncOption">The async option.</param>
    /// <returns>A task representing an async operation returning result.</returns>
    public static async Task<Result<Option<TValue>>> AsResultAsync<TValue>(this AsyncOption<TValue> asyncOption)
    {
        try
        {
            return await asyncOption;
        }
        catch (AggregateException ex)
        {
            var flattened = ex.Flatten();
            return flattened.InnerExceptions.Count > 1
                ? flattened
                : flattened.InnerExceptions.First();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}