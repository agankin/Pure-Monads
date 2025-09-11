namespace PureMonads;

internal static class FuncExtensions
{
    public static Func<Nothing> AsFunc(this Action action)
    {
        return () =>
        {
            action();
            return default;
        };
    }

    public static Func<TArg, Nothing> AsFunc<TArg>(this Action<TArg> action)
    {
        return arg =>
        {
            action(arg);
            return default;
        };
    }

    public static Func<Task> AsAsyncFunc(this Action action)
    {
        return () =>
        {
            action();
            return Task.CompletedTask;
        };
    }
    
    public static Func<TArg, Task> AsAsyncFunc<TArg>(this Action<TArg> action)
    {
        return arg =>
        {
            action(arg);
            return Task.CompletedTask;
        };
    }
}