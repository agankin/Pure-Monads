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
    public static Task<TValue> ValueOrFailureAsync<TValue>(this AsyncOption<TValue> asyncOption, string? message = null)
    {
        return asyncOption.OrAsync(new Func<TValue>(() => throw new Exception(message ?? "AsyncOption is None.")));
    }
}