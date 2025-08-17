namespace PureMonads;

public static partial class PipeMapExtensions
{
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
}