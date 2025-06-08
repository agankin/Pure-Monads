namespace PureMonads;

/// <summary>
/// Represents a Pipe result along with an initially provided value. 
/// </summary>
/// <typeparam name="TSource">Initial value type.</typeparam>
/// <typeparam name="TResult">Mapping result type.</typeparam>
/// <param name="Source">An initial value.</param>
/// <param name="Result">A mapping result.</param>
public readonly record struct PipeMapResult<TSource, TResult>(
    TSource Source,
    TResult Result
);