namespace PureMonads;

/// <summary>
/// Contains extension methods for Result monad.
/// </summary>
public static partial class ResultExtensions
{
    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue, TError> On<TValue, TError>(this in Result<TValue, TError> result, Action<TValue> onValue, Action<TError> onError)
    {
        result.Match(onValue.AsFunc(), onError.AsFunc());
        return result;
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate invoked on Value.</param>
    /// <param name="onError">A delegate invoked on Error.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue> On<TValue>(this in Result<TValue> result, Action<TValue> onValue, Action<Exception> onError)
    {
        result.Match(onValue.AsFunc(), onError.AsFunc());
        return result;
    }

    /// <summary>
    /// If the result is Value invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue, TError> OnValue<TValue, TError>(this in Result<TValue, TError> result, Action<TValue> onValue)
    {
        result.Match(onValue.AsFunc(), _ => new());
        return result;
    }

    /// <summary>
    /// If the result is Value invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onValue">A delegate.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue> OnValue<TValue>(this in Result<TValue> result, Action<TValue> onValue)
    {
        result.Match(onValue.AsFunc(), _ => new());
        return result;
    }

    /// <summary>
    /// If the result is Error invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onError">A delegate.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue, TError> OnError<TValue, TError>(this in Result<TValue, TError> result, Action<TError> onError)
    {
        result.Match(_ => new(), onError.AsFunc());
        return result;
    }

    /// <summary>
    /// If the result is Error invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="onError">A delegate.</param>
    /// <returns>The same result.</returns>
    public static Result<TValue> OnError<TValue>(this in Result<TValue> result, Action<Exception> onError)
    {
        result.Match(_ => new(), onError.AsFunc());
        return result;
    }
}