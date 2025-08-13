namespace PureMonads;

public static class PipeExtensions
{
    /// <summary>
    /// Tranforms the value by applying a transforming function.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>A result of the transforming function invocation.</returns>
    public static TResult Pipe<TValue, TResult>(this TValue value, Func<TValue, TResult> transform) => transform(value);

    /// <summary>
    /// Tranforms the value by applying an async transforming function.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="transformAsync">A transforming function.</param>
    /// <returns>A task representing a result of the transforming function invocation.</returns>
    public static Task<TResult> PipeAsync<TValue, TResult>(this TValue value, Func<TValue, Task<TResult>> transformAsync)
    {
        return transformAsync(value);
    }

    /// <summary>
    /// Tranforms the value from the task by applying a transforming function.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="task">The value task.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>A task representing a result of the transforming function invocation.</returns>
    public static async Task<TResult> PipeAsync<TValue, TResult>(this Task<TValue> task, Func<TValue, TResult> transform)
    {
        var value = await task;
        var result = transform(value);
        
        return result;
    }
    
    /// <summary>
    /// Tranforms the value from the task by applying an async transforming function.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="task">The value task.</param>
    /// <param name="transformAsync">A transforming function.</param>
    /// <returns>A task representing a result of the transforming function invocation.</returns>
    public static async Task<TResult> PipeAsync<TValue, TResult>(this Task<TValue> task, Func<TValue, Task<TResult>> transformAsync)
    {
        var value = await task;
        var result = await transformAsync(value);
        
        return result;
    }
}