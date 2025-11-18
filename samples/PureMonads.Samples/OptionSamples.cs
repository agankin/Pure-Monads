namespace PureMonads.Samples;

using static Option;

public static class OptionSamples
{
    public static async Task RunAsync()
    {
        // Option instances can be created:
        var some1 = Option<int>.Some(1);        // via factory method
        var some2 = 2.Some();                   // via extension method
        Option<int> some3 = 3;                  // by implicit conversion

        var none1 = Option<int>.None();         // via factory method
        var none2 = None<int>();                // via factory method in non-generic static Option class

        // Standard monad operations:
        var mapResult = some1.Map(value => value + 10);                     // == Some(11)
        var flatMapResult = some2.FlatMap(value => (value + 10).Some());    // == Some(12)

        // Standard monad operations supporting async mappers:
        var mapAsyncResult = some1.MapAsync(value => (value + 10).AsTask());     // == AsyncOption Some(11)

        AsyncOption<int> AsyncSome(int value) => AsyncOption.Some(value.AsTask());
        var flatMapAsyncResult = some2.FlatMap(value => AsyncSome(value + 10));        // == AsyncOption Some(12)

        var flatMapResult2 = await some2.FlatMapAsync(value => (value + 10).Some().AsTask()); // == Some(12)

        // Matching by invoking corresponding function:
        var matchResult = none1.Match(value => $"Some {value}!", () => "None!");   // == "None!"

        // Matching by invoking corresponding action: 
        void PrintSome<TValue>(TValue value) => Console.WriteLine($"Some: {value}!");
        void PrintNone() => Console.WriteLine($"None!");

        some1.On(PrintSome, PrintNone);         // Prints "Some: 1!"
        some1.OnSome(PrintSome);                // Prints "Some: 1!"
        some1.OnNone(PrintNone);                // Prints nothing
        none1.OnNone(PrintNone);                // Prints "None!"

        // The same with async actions:
        Task PrintSomeAsync<TValue>(TValue value)
        {
            Console.WriteLine($"Some: {value}!");
            return Task.CompletedTask;
        }
        Task PrintNoneAsync()
        {
            Console.WriteLine($"None!");
            return Task.CompletedTask;
        }
        
        await some1.OnAsync(PrintSomeAsync, PrintNoneAsync);    // All 3 print "Some: 1!"
        await some1.OnAsync(PrintSomeAsync, PrintNone);
        await some1.OnAsync(PrintSome, PrintNoneAsync);

        await some1.OnSomeAsync(PrintSomeAsync);                // Prints "Some: 1!"
        await some1.OnNoneAsync(PrintNoneAsync);                // Prints nothing
        await none1.OnNoneAsync(PrintNoneAsync);                // Prints "None!"

        // Converting a nullable value into an Option instance:
        var notNull = "I'm not null!";
        var someFromNotNull = notNull.NullAsNone();   // == Some("I'm not null!")

        string? itIsNull = null;
        var noneFromNull = itIsNull.NullAsNone();     // == None

        // Providing alternative values for None:
        int alt1 = none1.Or(100);                         // Directly passed alternative value
        int alt2 = none1.Or(() => 100);                   // Alternative value retrieved from a delegate
        Option<int> alt3 = none1.Or(100.Some());          // Directly passed alternative monad
        Option<int> alt4 = none1.Or(() => 100.Some());    // Alternative monad retrieved from a delegate

        // Extracting a value from Some or throwing an exception:
        try
        {
            none1.ValueOrFailure();     // Throws an exception
        }
        catch {}

        // The same with a custom exception:
        try
        {
            none1.ValueOrFailure(() => new InvalidOperationException());     // Throws InvalidOperationException
        }
        catch {}

        // Returning a Some value or None from dictionary:
        var dict = new Dictionary<int, string>
        {
            { 1, "One" },
            { 2, "Two" },
        };

        var dictValue1 = dict.GetOrNone(1);   // == Some("One")
        var dictValue3 = dict.GetOrNone(3);   // == None
    }
}