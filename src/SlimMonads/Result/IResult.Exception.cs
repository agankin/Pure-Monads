namespace SlimMonads;

/// <summary>
/// Can be matched as a value or an exception.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public interface IResult<TValue>
{
    /// <summary>
    /// Matches as a value or an exception by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapValue">A delegate invoked for a value.</param>
    /// <param name="mapError">A delegate invoked for an exception.</param>
    /// <returns>A result returned from the matched delegate.</returns>
    TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<Exception, TResult> mapError);
}