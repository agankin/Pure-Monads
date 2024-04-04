namespace SlimMonads;

/// <summary>
/// Can be matched as Some containing a value or None without value.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public interface IOption<TValue>
{
    /// <summary>
    /// Matches the option as Some containing a value or None without value by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapSome">A delegate to be invoked with the value of Some.</param>
    /// <param name="onNone">A delegate to be invoked on None.</param>
    /// <returns>A result returned from the matched delegate invocation.</returns>
    TResult Match<TResult>(Func<TValue, TResult> mapSome, Func<TResult> onNone);
}