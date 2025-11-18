namespace PureMonads;

public static partial class PipeMapExtensions
{
    /// <summary>
    /// Tranforms the value by applying a transforming function and returns
    /// a result along with the initial value.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="source">The initial value.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>
    /// A result of the transforming function invocation along with the initial value.
    /// </returns>
    public static PipeMapResult<TSource, TResult> PipeMap<TSource, TResult>(
        this TSource source,
        Func<TSource, TResult> transform)
    {
        return new PipeMapResult<TSource, TResult>(source, transform(source));
    }

    /// <summary>
    /// Tranforms the value by applying an async transforming function and returns
    /// a result along with the initial value.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="source">The initial value.</param>
    /// <param name="transformAsync">A transforming function.</param>
    /// <returns>
    /// A task representing a result of the transforming function invocation along with the initial value.
    /// </returns>
    public static async Task<PipeMapResult<TSource, TResult>> PipeMapAsync<TSource, TResult>(
        this TSource source,
        Func<TSource, Task<TResult>> transformAsync)
    {
        var result = await transformAsync(source);
        
        return new PipeMapResult<TSource, TResult>(source, result);
    }

    /// <summary>
    /// Tranforms the value by applying a transforming function and returns
    /// a result along with the initial value.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="sourceAsync">The task representing an initial value.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>
    /// A task representing a result of the transforming function invocation along with the initial value.
    /// </returns>
    public static async Task<PipeMapResult<TSource, TResult>> PipeMapAsync<TSource, TResult>(
        this Task<TSource> sourceAsync,
        Func<TSource, TResult> transform)
    {
        var source = await sourceAsync;
        
        return new PipeMapResult<TSource, TResult>(source, transform(source));
    }

    /// <summary>
    /// Tranforms the value by applying an async transforming function and returns
    /// a result along with the initial value.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="sourceAsync">The task representing an initial value.</param>
    /// <param name="transformAsync">A transforming function.</param>
    /// <returns>
    /// A task representing a result of the transforming function invocation along with the initial value.
    /// </returns>
    public static async Task<PipeMapResult<TSource, TResult>> PipeMapAsync<TSource, TResult>(
        this Task<TSource> sourceAsync,
        Func<TSource, Task<TResult>> transformAsync)
    {
        var source = await sourceAsync;
        var result = await transformAsync(source);

        return new PipeMapResult<TSource, TResult>(source, result);
    }
}