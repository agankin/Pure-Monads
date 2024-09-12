# Pure Monads

![NuGet Version](https://img.shields.io/nuget/v/Pure-Monads)

Contains implementations of the most commonly used monads:

- **Option**
- **Result**

In addition contains **Pipe extensions** for chaining function calls.

## Examples

- [Option](#option)
- [Result](#result)
- [Pipe extensions](#pipe-extensions)

### Option

```cs
using static Option;

// Option instances can be created:
var some1 = Option<int>.Some(1);        // via factory method
var some2 = 2.Some();                   // via extension method
Option<int> some3 = 3;                  // by implicit conversion

var none1 = Option<int>.None();         // via factory method
var none2 = None<int>();                // via factory method in non-generic static Option class

// Standard monad operations:
var mapResult = some1.Map(value => value + 10);                     // == Some(11)
var flatMapResult = some2.FlatMap(value => (value + 10).Some());    // == Some(12)

// Matching can be done:
var matchResult = none1.Match(value => $"Some {value}!", () => "None!");   // == "None!"

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
```

### Result

The following code samples are for **Result&lt;TValue, TError&gt;** type. **Result&lt;TValue&gt;** type exists with with **TError** defaulted to Exception. This version has identical methods.

```cs
using static Result;

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

// Matching can be done:
var matchResult = error1.Match(     // == "Error: Error 1"
    value => $"Value: {value}",
    err => $"Error: {err}");

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
```

### Pipe extensions

```cs
// Functions used in examples
int Square(int x) => x * x;
int Add5(int x) => x + 5;

async Task<int> DoubleAsync(int x) => await Task.FromResult(x * 2);
async Task<int> Subtract2Async(int x) => await Task.FromResult(x - 2);

// Chaining functions:
var result1 = 10                          // == 55
    .Pipe(Square)
    .Pipe(x => x / 2)
    .Pipe(Add5);

// Chaining async functions:
var result2 = await 3                     // == 64
    .PipeAsync(DoubleAsync)
    .PipeAsync(Subtract2Async)
    .PipeAsync(async x => await Task.FromResult(x * x * x));
```