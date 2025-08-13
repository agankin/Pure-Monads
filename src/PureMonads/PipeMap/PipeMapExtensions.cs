namespace PureMonads;

public static class PipeMapExtensions
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

    /// <summary>
    /// Tranforms the result from the previous PipeMap invocation by applying a transforming function
    /// and returns a new result along with the initial value.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Input result type.</typeparam>
    /// <typeparam name="TResult2">Output result type.</typeparam>
    /// <param name="pipeMapResult">The result from the previous PipeMap invocation.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>A result of the transforming function invocation along with the initial value.</returns>
    public static PipeMapResult<TSource, TResult2> PipeMap<TSource, TResult, TResult2>(
        this PipeMapResult<TSource, TResult> pipeMapResult,
        Func<TResult, TResult2> transform)
    {
        var (source, result) = pipeMapResult;
        var nextResult = transform(result);

        return new PipeMapResult<TSource, TResult2>(source, nextResult);
    }

    /// <summary>
    /// Tranforms the result from the previous PipeMap invocation by applying an async transforming function
    /// and returns a new result along with the initial value.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Input result type.</typeparam>
    /// <typeparam name="TResult2">Output result type.</typeparam>
    /// <param name="pipeMapResult">The result from the previous PipeMap invocation.</param>
    /// <param name="transformAsync">A transforming function.</param>
    /// <returns>
    /// A task representing a result of the transforming function invocation along with the initial value.
    /// </returns>
    public static async Task<PipeMapResult<TSource, TResult2>> PipeMapAsync<TSource, TResult, TResult2>(
        this PipeMapResult<TSource, TResult> pipeMapResult,
        Func<TResult, Task<TResult2>> transformAsync)
    {
        var (source, result) = pipeMapResult;
        var nextResult = await transformAsync(result);

        return new PipeMapResult<TSource, TResult2>(source, nextResult);
    }

    /// <summary>
    /// Tranforms the result from the previous PipeMap invocation by applying a transforming function
    /// and returns a new result along with the initial value.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Input result type.</typeparam>
    /// <typeparam name="TResult2">Output result type.</typeparam>
    /// <param name="pipeMapResultAsync">The task representing a result from the previous PipeMap invocation.</param>
    /// <param name="transform">A transforming function.</param>
    /// <returns>A result of the transforming function invocation along with the initial value.</returns>
    public static async Task<PipeMapResult<TSource, TResult2>> PipeMapAsync<TSource, TResult, TResult2>(
        this Task<PipeMapResult<TSource, TResult>> pipeMapResultAsync,
        Func<TResult, TResult2> transform)
    {
        var (source, result) = await pipeMapResultAsync;
        var nextResult = transform(result);

        return new PipeMapResult<TSource, TResult2>(source, nextResult);
    }

    /// <summary>
    /// Tranforms the result from the previous PipeMap invocation by applying an async transforming function
    /// and returns a new result along with the initial value.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Input result type.</typeparam>
    /// <typeparam name="TResult2">Output result type.</typeparam>
    /// <param name="pipeMapResultAsync">The task representing a result from the previous PipeMap invocation.</param>
    /// <param name="transformAsync">A transforming function.</param>
    /// <returns>
    /// A task representing a result of the transforming function invocation along with the initial value.
    /// </returns>
    public static async Task<PipeMapResult<TSource, TResult2>> PipeMapAsync<TSource, TResult, TResult2>(
        this Task<PipeMapResult<TSource, TResult>> pipeMapResultAsync,
        Func<TResult, Task<TResult2>> transformAsync)
    {
        var (source, result) = await pipeMapResultAsync;
        var nextResult = await transformAsync(result);

        return new PipeMapResult<TSource, TResult2>(source, nextResult);
    }

    /// <summary>
    /// Reduces the result from the previous PipeMap invocation.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Input result type.</typeparam>
    /// <typeparam name="TResult2">Output result type.</typeparam>
    /// <param name="pipeMapResult">The result from the previous PipeMap invocation.</param>
    /// <param name="reduce">A reducing function.</param>
    /// <returns>A result of the reducer invocation.</returns>
    public static TResult2 Reduce<TSource, TResult, TResult2>(
        this PipeMapResult<TSource, TResult> pipeMapResult,
        Func<TSource, TResult, TResult2> reduce)
    {
        var (source, result) = pipeMapResult;
        return reduce(source, result);
    }

    /// <summary>
    /// Reduces the result from the previous PipeMap invocation.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Input result type.</typeparam>
    /// <typeparam name="TResult2">Output result type.</typeparam>
    /// <param name="pipeMapResult">The result from the previous PipeMap invocation.</param>
    /// <param name="reduceAsync">A reducing function.</param>
    /// <returns>A task representing the result of the reducer invocation.</returns>
    public static Task<TResult2> ReduceAsync<TSource, TResult, TResult2>(
        this PipeMapResult<TSource, TResult> pipeMapResult,
        Func<TSource, TResult, Task<TResult2>> reduceAsync)
    {
        var (source, result) = pipeMapResult;
        return reduceAsync(source, result);
    }

    /// <summary>
    /// Reduces the result from the previous PipeMap invocation.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Input result type.</typeparam>
    /// <typeparam name="TResult2">Output result type.</typeparam>
    /// <param name="pipeMapResultAsync">The task representing a result from the previous PipeMap invocation.</param>
    /// <param name="reduce">A reducing function.</param>
    /// <returns>A task representing a result of the reducer invocation.</returns>
    public static async Task<TResult2> ReduceAsync<TSource, TResult, TResult2>(
        this Task<PipeMapResult<TSource, TResult>> pipeMapResultAsync,
        Func<TSource, TResult, TResult2> reduce)
    {
        var (source, result) = await pipeMapResultAsync;
        return reduce(source, result);
    }

    /// <summary>
    /// Reduces the result from the previous PipeMap invocation.
    /// </summary>
    /// <typeparam name="TSource">Initial value type.</typeparam>
    /// <typeparam name="TResult">Input result type.</typeparam>
    /// <typeparam name="TResult2">Output result type.</typeparam>
    /// <param name="pipeMapResultAsync">The task representing a result from the previous PipeMap invocation.</param>
    /// <param name="reduceAsync">A reducing function.</param>
    /// <returns>A task representing a result of the reducer invocation.</returns>
    public static async Task<TResult2> ReduceAsync<TSource, TResult, TResult2>(
        this Task<PipeMapResult<TSource, TResult>> pipeMapResultAsync,
        Func<TSource, TResult, Task<TResult2>> reduceAsync)
    {
        var (source, result) = await pipeMapResultAsync;
        return await reduceAsync(source, result);
    }
}