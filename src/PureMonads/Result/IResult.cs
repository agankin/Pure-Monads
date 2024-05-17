namespace PureMonads;

/// <summary>
/// Can be matched as a value or an error.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
/// <typeparam name="TError">Error type.</typeparam>
public interface IResult<TValue, TError>
{
    /// <summary>
    /// Matches as a value or an error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapValue">A delegate invoked for a value.</param>
    /// <param name="mapError">A delegate invoked for an error.</param>
    /// <returns>A result returned from the matched delegate.</returns>
    TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<TError, TResult> mapError);
}