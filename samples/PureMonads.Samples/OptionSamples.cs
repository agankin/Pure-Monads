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
        var none2 = None<int>();                // via static method in non-generic Option class

        // Standard monad operations:
        var mapResult = some1.Map(value => value + 10);                     // mapResult is Some(11)
        var flatMapResult = some2.FlatMap(value => (value + 10).Some());    // flatMapResult is Some(12)

        // Matching can be done:
        var matchResult = none1.Match(value => $"Some {value}!", () => "None!");   // matchResult is "None!"

        // Making nullable value into Option instance:
        var notNull = "I'm not null!";
        var someFromNotNull = notNull.NullAsNone();   // Now it's Some("I'm not null!")

        string? itIsNull = null;
        var noneFromNull = itIsNull.NullAsNone();     // Now it's None

        // Providing alternative values for None:
        int alt1 = none1.Or(100);                         // Directly passed alternative value
        int alt2 = none1.Or(() => 100);                   // Alternative value retrieved from delegate
        Option<int> alt3 = none1.Or(100.Some());          // Directly passed alternative monad
        Option<int> alt4 = none1.Or(() => 100.Some());    // Alternative monad retrieved from delegate
        
        // Extracting a value from Some or throwing Exception for None:
        try
        {
            none1.ValueOrFailure();     // Throws exception as none1 is None.
        }
        catch {}
    }
}