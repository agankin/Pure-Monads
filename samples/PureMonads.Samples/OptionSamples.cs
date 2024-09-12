namespace PureMonads.Samples;

using static Option;

public static class OptionSamples
{
    public static void Run()
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

        // Matching by invoking corresponding function:
        var matchResult = none1.Match(value => $"Some {value}!", () => "None!");   // == "None!"

        // Matching by invoking corresponding action: 
        void PrintSome<TValue>(TValue value) => Console.WriteLine($"Some: {value}!");
        void PrintNone() => Console.WriteLine($"None!");

        some1.On(PrintSome, PrintNone);         // Prints "Some: 1!"
        some1.OnSome(PrintSome);                // Prints "Some: 1!"
        some1.OnNone(PrintNone);                // Prints nothing
        none1.OnNone(PrintNone);                // Prints "None!"

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