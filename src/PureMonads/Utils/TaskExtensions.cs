namespace PureMonads;

internal static class TaskExtensions
{
    public static Task<TResult> Map<TValue, TResult>(this Task<TValue> task, Func<TValue, TResult> map)
    {
        var taskScheduler = SynchronizationContext.Current != null
            ? TaskScheduler.FromCurrentSynchronizationContext()
            : TaskScheduler.Default;
        return task.ContinueWith(task => map(task.Result), taskScheduler);
    }

    public static Task<TResult> Map<TValue, TResult>(this Task<TValue> task, Func<TValue, Task<TResult>> asyncMap)
    {
        return task.Map<TValue, Task<TResult>>(asyncMap).Unwrap();
    }

    public static Task<TValue> AsTask<TValue>(this TValue value) => Task.FromResult(value);
}