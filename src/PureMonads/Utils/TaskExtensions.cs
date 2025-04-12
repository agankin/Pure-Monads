namespace PureMonads;

internal static class TaskExtensions
{
    public static Task<TResult> Map<TValue, TResult>(this Task<TValue> task, Func<TValue, TResult> map)
    {
        var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        return task.ContinueWith(task => map(task.Result), taskScheduler);
    }

    public static Task<TResult> Map<TValue, TResult>(this Task<TValue> task, Func<TValue, Task<TResult>> asyncMap)
    {
        return task.Map<TValue, Task<TResult>>(asyncMap).Unwrap();
    }
}