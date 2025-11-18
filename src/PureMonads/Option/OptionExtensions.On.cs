namespace PureMonads;

/// <summary>
/// Contains extension methods for Option monad.
/// </summary>
public static partial class OptionExtensions
{
    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSome">A delegate invoked on Some.</param>
    /// <param name="onNone">A delegate invoked on None.</param>
    /// <returns>The same option.</returns>
    public static Option<TValue> On<TValue>(this in Option<TValue> option, Action<TValue> onSome, Action onNone)
    {
        option.Match(onSome.AsFunc(), onNone.AsFunc());
        
        return option;
    }

    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSomeAsync">A delegate invoked on Some.</param>
    /// <param name="onNoneAsync">A delegate invoked on None.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue>(this in Option<TValue> option, Func<TValue, Task> onSomeAsync, Func<Task> onNoneAsync)
    {
        return option.Match(onSomeAsync, onNoneAsync);
    }

    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSomeAsync">A delegate invoked on Some.</param>
    /// <param name="onNone">A delegate invoked on None.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue>(this in Option<TValue> option, Func<TValue, Task> onSomeAsync, Action onNone)
    {
        return option.Match(onSomeAsync, onNone.AsAsyncFunc());
    }

    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSome">A delegate invoked on Some.</param>
    /// <param name="onNoneAsync">A delegate invoked on None.</param>
    /// <returns>A task.</returns>
    public static Task OnAsync<TValue>(this in Option<TValue> option, Action<TValue> onSome, Func<Task> onNoneAsync)
    {
        return option.Match(onSome.AsAsyncFunc(), onNoneAsync);
    }

    /// <summary>
    /// If the option is Some invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSome">A delegate.</param>
    /// <returns>The same option.</returns>
    public static Option<TValue> OnSome<TValue>(this in Option<TValue> option, Action<TValue> onSome)
    {
        option.Match(onSome.AsFunc(), () => new());
        
        return option;
    }

    /// <summary>
    /// If the option is Some invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSomeAsync">A delegate.</param>
    /// <returns>A task.</returns>
    public static Task OnSomeAsync<TValue>(this in Option<TValue> option, Func<TValue, Task> onSomeAsync)
    {
        return option.Match(onSomeAsync, () => Task.CompletedTask);
    }

    /// <summary>
    /// If the option is None invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onNone">A delegate.</param>
    /// <returns>The same option.</returns>
    public static Option<TValue> OnNone<TValue>(this in Option<TValue> option, Action onNone)
    {
        option.Match(_ => new(), onNone.AsFunc());
        
        return option;
    }
    
    /// <summary>
    /// If the option is None invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onNoneAsync">A delegate.</param>
    /// <returns>A task.</returns>
    public static Task OnNoneAsync<TValue>(this in Option<TValue> option, Func<Task> onNoneAsync)
    {
        return option.Match(_ => Task.CompletedTask, onNoneAsync);
    }
}