namespace PureMonads.Samples;

using static AsyncOption;

public static class AsyncOptionSamples
{
    public static async Task RunAsync()
    {
        Task<TValue> Async<TValue>(TValue value) => Task.FromResult(value);

        // Option instances can be created:
        var asyncSome1 = AsyncOption<int>.Some(Async(1));     // via factory method
        var asyncSome2 = Async(2).Some();                     // via extension method
        AsyncOption<int> asyncSome3 = Async(3);               // by implicit conversion

        var asyncNone1 = AsyncOption<int>.None();             // via factory method
        var asyncNone2 = None<int>();                         // via factory method in non-generic static AsyncOption class

        // Awaiting for Option result:
        var _some1 = await asyncSome1;                        // == Some(1)
        var _none1 = await asyncNone1;                        // == None()

        // Standard monad operations:
        var asyncMapResult2 = asyncSome1.Map(value => value + 1);          // == AsyncSome(2)
        var asyncMapResult3 = asyncSome2.Map(value => Async(value + 2));   // == AsyncSome(3)

        var asyncFlatMapResult = await asyncSome2.FlatMapAsync(            // == AsyncSome(3)
            value => Async(value + 2).Some());

        // Matching by invoking corresponding function:
        var matchResult = asyncNone1.Match(value => $"Async Some!", () => "None!");   // == "None!"

        // Providing alternative values for None:
        AsyncOption<int> asyncSome100 = asyncNone1.Or(Async(100));       // Directly passed alternative value
        AsyncOption<int> asyncSome200 = asyncNone1.Or(() => Async(200)); // Alternative value retrieved from a delegate

        int value100 = await asyncNone1.OrAsync(100);          // Directly passed alternative monad
        int value200 = await asyncNone1.OrAsync(() => 200);    // Alternative monad retrieved from a delegate

        // Extracting a value from Some or throwing an exception:
        try
        {
            await asyncNone1.ValueOrFailureAsync();     // Throws an exception
        }
        catch {}
    }
}