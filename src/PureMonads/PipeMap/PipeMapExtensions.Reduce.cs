namespace PureMonads;

public static partial class PipeMapExtensions
{
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