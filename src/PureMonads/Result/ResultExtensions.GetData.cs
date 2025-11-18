namespace PureMonads;

/// <summary>
/// Contains extension methods for Result monad.
/// </summary>
public static partial class ResultExtensions
{
    /// <summary>
    /// Converts to Option monad returning Some if the result is Value or None if the result is Error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> Value<TValue, TError>(this in Result<TValue, TError> result)
    {
        return result.Match(Option.Some, _ => Option.None<TValue>());
    }

    /// <summary>
    /// Converts to Option monad returning Some if the result is Value or None if the result is Error.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TValue> Value<TValue>(this in Result<TValue> result)
    {
        return result.Match(Option.Some, _ => Option.None<TValue>());
    }

    /// <summary>
    /// Converts to Option monad returning Some if the result is Error or None if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TError">Error type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<TError> Error<TValue, TError>(this Result<TValue, TError> result)
    {
        return result.Match(_ => Option.None<TError>(), Option.Some);
    }

    /// <summary>
    /// Converts to Option monad returning Some if the result is Error or None if the result is Value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>An instance of Option monad.</returns>
    public static Option<Exception> Error<TValue>(this Result<TValue> result)
    {
        return result.Match(_ => Option.None<Exception>(), Option.Some);
    }

    /// <summary>
    /// Awaits and returns result representing a value or exception occured.
    /// </summary>
    /// <typeparam name="TValue">Result type.</typeparam>
    /// <param name="asyncValue">A task representing an async value.</param>
    /// <returns>>A task representing an async operation returning result.</returns>
    public static async Task<Result<TValue>> AsResultAsync<TValue>(this Task<TValue> asyncValue)
    {
        try
        {
            Result<TValue> valueResult = await asyncValue;

            return valueResult;
        }
        catch (AggregateException ex)
        {
            var flattened = ex.Flatten();

            return flattened.InnerExceptions.Count > 1
                ? flattened
                : flattened.InnerExceptions.First();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}