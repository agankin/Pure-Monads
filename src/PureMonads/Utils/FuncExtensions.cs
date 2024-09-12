namespace PureMonads;

internal static class FuncExtensions
{
    public static Func<Nothing> AsFunc(this Action action)
    {
        return () =>
        {
            action();
            return new();
        };
    }
    
    public static Func<TArg, Nothing> AsFunc<TArg>(this Action<TArg> action)
    {
        return arg =>
        {
            action(arg);
            return new();
        };
    }
}