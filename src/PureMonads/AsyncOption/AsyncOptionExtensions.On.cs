namespace PureMonads;

/// <summary>
/// Contains extension methods for AsyncOption monad.
/// </summary>
public static partial class AsyncOptionExtensions
{
    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSome">A delegate invoked on Some.</param>
    /// <param name="onNone">A delegate invoked on None.</param>
    /// <returns>A task.</returns>
    public static async Task OnAsync<TValue>(this AsyncOption<TValue> asyncOption, Action<TValue> onSome, Action onNone)
    {
        await asyncOption.Match(task => task.Map(onSome.AsAsyncFunc()), onNone.AsAsyncFunc());
    }

    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSomeAsync">A delegate invoked on Some.</param>
    /// <param name="onNone">A delegate invoked on None.</param>
    /// <returns>A task.</returns>
    public static async Task OnAsync<TValue>(this AsyncOption<TValue> asyncOption, Func<TValue, Task> onSomeAsync, Action onNone)
    {
        await asyncOption.Match(task => task.Map(onSomeAsync), onNone.AsAsyncFunc());
    }

    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSome">A delegate invoked on Some.</param>
    /// <param name="onNoneAsync">A delegate invoked on None.</param>
    /// <returns>A task.</returns>
    public static async Task OnAsync<TValue>(this AsyncOption<TValue> asyncOption, Action<TValue> onSome, Func<Task> onNoneAsync)
    {
        await asyncOption.Match(task => task.Map(onSome.AsAsyncFunc()), onNoneAsync);
    }

    /// <summary>
    /// Matches Some or None by invoking the corresponding delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSomeAsync">A delegate invoked on Some.</param>
    /// <param name="onNoneAsync">A delegate invoked on None.</param>
    /// <returns>A task.</returns>
    public static async Task OnAsync<TValue>(this AsyncOption<TValue> asyncOption, Func<TValue, Task> onSomeAsync, Func<Task> onNoneAsync)
    {
        await asyncOption.Match(task => task.Map(onSomeAsync), onNoneAsync);
    }

    /// <summary>
    /// If the option is Some invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSome">A delegate.</param>
    /// <returns>A task.</returns>
    public static async Task OnSomeAsync<TValue>(this AsyncOption<TValue> asyncOption, Action<TValue> onSome)
    {
        await asyncOption.Match(task => task.Map(onSome.AsAsyncFunc()), () => Task.CompletedTask);
    }

    /// <summary>
    /// If the option is Some invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onSomeAsync">A delegate.</param>
    /// <returns>A task.</returns>
    public static async Task OnSomeAsync<TValue>(this AsyncOption<TValue> asyncOption, Func<TValue, Task> onSomeAsync)
    {
        await asyncOption.Match(task => task.Map(onSomeAsync), () => Task.CompletedTask);
    }

    /// <summary>
    /// If the option is None invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onNone">A delegate.</param>
    /// <returns>A task.</returns>
    public static async Task OnNoneAsync<TValue>(this AsyncOption<TValue> asyncOption, Action onNone)
    {
        await asyncOption.Match(_ => Task.CompletedTask, onNone.AsAsyncFunc());
    }

    /// <summary>
    /// If the option is None invokes the given delegate.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="onNoneAsync">A delegate.</param>
    /// <returns>A task.</returns>
    public static async Task OnNoneAsync<TValue>(this AsyncOption<TValue> asyncOption, Func<Task> onNoneAsync)
    {
        await asyncOption.Match(_ => Task.CompletedTask, onNoneAsync);
    }
}