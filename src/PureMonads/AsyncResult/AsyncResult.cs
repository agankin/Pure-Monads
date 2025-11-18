using System.Runtime.CompilerServices;

namespace PureMonads;

/// <summary>
/// Represents either an async operation returning a value or an error.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
/// <typeparam name="TError">Error type.</typeparam>
public readonly struct AsyncResult<TValue, TError> : IEquatable<AsyncResult<TValue, TError>>
{
    private readonly Task<TValue> _task;
    private readonly TError _error;

    private AsyncResult(Task<TValue> task)
    {
        _task = task;
        _error = default!;
        HasValue = true;
    }

    private AsyncResult(TError error)
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
    public static AsyncResult<TValue, TError> Value(Task<TValue> task) => new(task);

    /// <summary>
    /// Wraps an error in Error async result.
    /// </summary>
    /// <param name="error">An error.</param>
    /// <returns>An instance of AsyncResult monad.</returns>
    public static AsyncResult<TValue, TError> Error(TError error) => new(error);

    /// <summary>
    /// Returns an awaiter used to await for a value.
    /// </summary>
    public TaskAwaiter<Result<TValue, TError>> GetAwaiter()
    {
        return HasValue
            ? _task.Map(Result<TValue, TError>.Value).GetAwaiter()
            : Result.Error<TValue, TError>(_error).AsTask().GetAwaiter();
    }

    /// <summary>
    /// Matches Value or Error by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapValue">A delegate invoked on Value.</param>
    /// <param name="mapError">A delegate invoked on Error.</param>
    /// <returns>A result returned from the matched delegate invocation.</returns>
    public TResult Match<TResult>(Func<Task<TValue>, TResult> mapValue, Func<TError, TResult> mapError) =>
        HasValue ? mapValue(_task) : mapError(_error);

    /// <inheritdoc/>
    public override string ToString() => Match(value => $"AsyncValue", error => $"Error({error})");

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is AsyncResult<TValue, TError> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HasValue
        ? _task?.GetHashCode() ?? 0
        : _error?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public bool Equals(AsyncResult<TValue, TError> other)
    {
        return HasValue == other.HasValue
            && (HasValue && _task == other._task
                || !HasValue && EqualityComparer<TError>.Default.Equals(_error, other._error));
    }

    public static bool operator ==(AsyncResult<TValue, TError> first, AsyncResult<TValue, TError> second) => first.Equals(second);

    public static bool operator !=(AsyncResult<TValue, TError> first, AsyncResult<TValue, TError> second) => !first.Equals(second);
    
    public static implicit operator AsyncResult<TValue, TError>(Task<TValue> task) => AsyncResult<TValue, TError>.Value(task);

    public static implicit operator AsyncResult<TValue, TError>(TError error) => AsyncResult<TValue, TError>.Error(error);
}