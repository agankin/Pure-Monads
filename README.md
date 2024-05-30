# Pure Monads

Contains implementations of the most commonly used monads:

- **Option**
- **Result**

Also contains **Pipe extensions** for chaining function calls.

## Examples

### Option

```cs
using static Option;

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
    none1.ValueOrFailure();     // Throws exception as none1 is None
}
catch {}
```

### Result

The following code samples are for **Result&lt;TValue, TError&gt;** class.

A version with **TError** defaulted to Exception exists: **Result&lt;TValue&gt;**. This version has identical methods except only single generic type parameter is used.

```cs
using static Result;

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
```

### Pipe

```cs
// Used functions
int Square(int x) => x * x;
int Add5(int x) => x + 5;

async Task<int> DoubleAsync(int x) => await Task.FromResult(x * 2);
async Task<int> Subtract2Async(int x) => await Task.FromResult(x - 2);

// Chaining functions application:
var result1 = 10                          // result1 is 55
    .Pipe(Square)
    .Pipe(x => x / 2)
    .Pipe(Add5);

// Async functions application chaining:
var result2 = await 3                     // result2 is 64
    .PipeAsync(DoubleAsync)
    .PipeAsync(Subtract2Async)
    .PipeAsync(async x => await Task.FromResult(x * x * x));
```