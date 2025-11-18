using System.Runtime.CompilerServices;

namespace PureMonads;

/// <summary>
/// Represents either an async operation returning a value or an error of Exception type.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct AsyncResult<TValue> : IEquatable<AsyncResult<TValue>>
{
    private readonly Task<TValue> _task;
    private readonly Exception _error;

    private AsyncResult(Task<TValue> task)
    {
        _task = task;
        _error = default!;
        HasValue = true;
    }

    private AsyncResult(Exception error)
    {
        _task = null!;
        _error = error;
        HasValue = false;
    }

    /// <summary>
    /// Contains true for Value async result or false for Error result.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Wraps a task in Value async result.
    /// </summary>
    /// <param name="task">A task.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TValue> Value(Task<TValue> task) => new(task);

    /// <summary>
    /// Wraps an error in Error async result.
    /// </summary>
    /// <param name="error">An error.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TValue> Error(Exception error) => new(error);

    /// <summary>
    /// Returns an awaiter used to await for a value.
    /// </summary>
    public TaskAwaiter<Result<TValue>> GetAwaiter()
    {
        return HasValue
            ? _task.Map(Result<TValue>.Value).GetAwaiter()
            : Result.Error<TValue>(_error).AsTask().GetAwaiter();
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapValue">A delegate invoked on Value.</param>
    /// <param name="mapError">A delegate invoked on Error.</param>
    /// <returns>A result returned from the matched delegate invocation.</returns>
    public TResult Match<TResult>(Func<Task<TValue>, TResult> mapValue, Func<Exception, TResult> mapError)
    {
        return HasValue ? mapValue(_task) : mapError(_error);
    }

    /// <inheritdoc/>
    public override string ToString() => Match(value => $"AsyncValue", error => $"Error({error})");

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is AsyncResult<TValue> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HasValue
        ? _task?.GetHashCode() ?? 0
        : _error?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public bool Equals(AsyncResult<TValue> other)
    {
        return HasValue == other.HasValue
            && (HasValue && _task == other._task
                || !HasValue && _error == other._error);
    }

    public static bool operator ==(AsyncResult<TValue> first, AsyncResult<TValue> second) => first.Equals(second);

    public static bool operator !=(AsyncResult<TValue> first, AsyncResult<TValue> second) => !first.Equals(second);
    
    public static implicit operator AsyncResult<TValue>(Task<TValue> task) => AsyncResult<TValue>.Value(task);

    public static implicit operator AsyncResult<TValue>(Exception error) => AsyncResult<TValue>.Error(error);
}