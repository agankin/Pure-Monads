namespace PureMonads.Samples;

using static Result;

public static class ResultSamples
{
    public static async Task RunAsync()
    {
        // Result instances can be created:
        var value1 = Result<int, string>.Value(1);           // via factory method
        var value2 = Value<int, string>(2);                  // via factory method in non-generic static Result class
        Result<int, string> value3 = 3;                      // by implicit conversion

        var error1 = Result<int, string>.Error("Error 1");   // via factory method
        var error2 = Error<int, string>("Error 2");          // via factory method in non-generic static Result class
        Result<int, string> error3 = "Error 3";              // by implicit conversion

        // Standard monad operations:
        var mapResult = value1.Map(value => value + 10);                              // == Value(11)
        var flatMapResult = value2.FlatMap(value => Value<int, string>(value + 10));  // == Value(12)

        // Matching by invoking corresponding function:
        var matchResult = error1.Match(     // == "Error: Error 1"
            value => $"Value: {value}",
            err => $"Error: {err}");

        // Matching by invoking corresponding action: 
        void PrintValue<TValue>(TValue value) => Console.WriteLine($"Value: {value}");
        void PrintError<TError>(TError err) => Console.WriteLine($"Error: {err}");

        value1.On(PrintValue, PrintError);       // Prints "Value: 1"
        value1.OnValue(PrintValue);              // Prints "Value: 1"
        value1.OnError(PrintError);              // Prints nothing
        error1.OnError(PrintError);              // Prints "Error: Error 1"

        // Extracting a value or an error as Option<TValue> or Option<TError>:
        var someValue1 = value1.Value();     // == Some(1)
        var noneError1 = value1.Error();     // == None

        var noneValue2 = error2.Value();     // == None
        var someError2 = error2.Error();     // == Some("Error 2")

        // Result has methods to safely invoke delegates:
        var result1 = Result.From(() => "Value 1");   // == Value("Value 1)"
        var result2 = Result.From(() =>               // == Error(Exception)
        {
            throw new Exception("Error 1");
            return "Value 1";
        });

        // Async versions of the methods also exist:
        var result3 = await Result.FromAsync(async () =>    // == Value("Value 1")
        {
            await Task.CompletedTask;
            return "Value 1";
        });
        var result4 = await Result.FromAsync(async () =>    // == Error(Exception)
        {
            await Task.CompletedTask;
            
            throw new Exception("Error 1");
            return "Value 1";
        });
    }
}