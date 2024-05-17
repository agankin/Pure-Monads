namespace PureMonads;

public static class PipeExtensions
{
    /// <summary>
    /// Tranforms a value by applying a transforming function.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="value">A value.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>The result of the transforming function invocation.</returns>
    public static TResult Pipe<TValue, TResult>(this TValue value, Func<TValue, TResult> transform) => transform(value);

    /// <summary>
    /// Tranforms a value by applying an async transforming function.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="value">A value.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>A task representing a result of the transforming function invocation.</returns>
    public static async Task<TResult> PipeAsync<TValue, TResult>(this TValue value, Func<TValue, Task<TResult>> transformAsync)
    {
        var result = await transformAsync(value);
        return result;
    }

    /// <summary>
    /// Tranforms a value from the task by applying an async transforming function.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="valueTask">A value task.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>A task representing a result of the transforming function invocation.</returns>
    public static async Task<TResult> PipeAsync<TValue, TResult>(this Task<TValue> valueTask, Func<TValue, Task<TResult>> transformAsync)
    {
        var value = await valueTask;
        var result = await transformAsync(value);
        
        return result;
    }
}