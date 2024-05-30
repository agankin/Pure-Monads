namespace PureMonads.Samples;

using static Result;

public static class ResultSamples
{
    public static async Task RunAsync()
    {
        // Result instances can be created:
        var value1 = Result<int, string>.Value(1);           // via factory method
        var value2 = Value<int, string>(2);                  // via factory method from non-generic Result class
        Result<int, string> value3 = 3;                      // by implicit conversion

        var error1 = Result<int, string>.Error("Error 1");   // via factory method
        var error2 = Error<int, string>("Error 2");          // via factory method from non-generic Result class
        Result<int, string> error3 = "Error 3";              // by implicit conversion
        
        // Standard monad operations:
        var mapResult = value1.Map(value => value + 10);                              // mapResult is Value(11)
        var flatMapResult = value2.FlatMap(value => Value<int, string>(value + 10));  // flatMapResult is Value(12)

        // Matching can be done:
        var matchResult = error1.Match(     // matchResult is "Error: Error 1"
            value => $"Value: {value}",
            err => $"Error: {err}");

        // Extracting value or error as Option<TValue> or Option<TError>:
        var someValue1 = value1.Value();     // someValue1 is Some(1)
        var noneError1 = value1.Error();     // noneError1 is None

        var noneValue2 = error2.Value();     // noneValue2 is None
        var someError2 = error2.Error();     // someError2 is Some("Error 2")

        // Result can be used instead of try-catch:
        var result1 = Result.From(() => "Value 1");   // result1 is Value("Value 1)"
        var result2 = Result.From(() =>               // result2 is Error(Exception)
        {
            throw new Exception("Error 1");
            return "Value 1";
        });

        // Async versions of Result as try-catch also exist:
        var result3 = await Result.FromAsync(async () =>    // result3 is Value("Value 1)"
        {
            await Task.CompletedTask;
            return "Value 1";
        });
        var result4 = await Result.FromAsync(async () =>    // result4 is Error(Exception)
        {
            await Task.CompletedTask;
            
            throw new Exception("Error 1");
            return "Value 1";
        });
    }
}