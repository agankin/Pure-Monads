using System.Runtime.CompilerServices;

namespace PureMonads;

/// <summary>
/// Represents either an async operation that will complete with a value or no value.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct AsyncOption<TValue> : IEquatable<AsyncOption<TValue>>
{
    private readonly Task<TValue> _task = default!;

    private AsyncOption(bool hasValue, Task<TValue> task) => (HasValue, _task) = (hasValue, task);

    /// <summary>
    /// Contains true for Some async option or false for None async option.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Wraps a task in Some async option.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TValue> Some(Task<TValue> task) => new(hasValue: true, task);

    /// <summary>
    /// Creates an instance of None async option.
    /// </summary>
    /// <returns>An instance of AsyncOption monad.</returns>
    public static AsyncOption<TValue> None() => new(hasValue: false, default!);

    /// <summary>
    /// Returns an awaiter used to await for a value.
    /// </summary>
    public TaskAwaiter<Option<TValue>> GetAwaiter()
    {
        return HasValue
            ? _task.Map(Option.Some).GetAwaiter()
            : Task.FromResult(Option.None<TValue>()).GetAwaiter();
    }

    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="mapSome">A delegate invoked on Some.</param>
    /// <param name="onNone">A delegate invoked on None.</param>
    /// <returns>A result returned from the matched delegate invocation.</returns>
    public TResult Match<TResult>(Func<Task<TValue>, TResult> mapSome, Func<TResult> onNone)
    {
        return HasValue ? mapSome(_task) : onNone();
    }

    /// <inheritdoc/>
    public override string ToString() => HasValue ? $"AsyncSome" : "None";

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is AsyncOption<TValue> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HasValue ? _task?.GetHashCode() ?? 0 : 0;

    /// <inheritdoc/>
    public bool Equals(AsyncOption<TValue> other)
    {
        return HasValue == other.HasValue
            && (!HasValue || _task == other._task);
    }

    public static bool operator ==(AsyncOption<TValue> first, AsyncOption<TValue> second) => first.Equals(second);

    public static bool operator !=(AsyncOption<TValue> first, AsyncOption<TValue> second) => !first.Equals(second);

    public static implicit operator AsyncOption<TValue>(Task<TValue> task) => AsyncOption<TValue>.Some(task);
}