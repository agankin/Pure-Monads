# Pure Monads

![NuGet Version](https://img.shields.io/nuget/v/Pure-Monads)

Contains implementations of the most commonly used monads:

- **Option**
- **AsyncOption**
- **Result**
- **AsyncResult**
- **Either**

In addition contains **Pipe extensions** and **PipeMap extensions** for chaining function calls.

## Examples

- [Option](#option)
- [AsyncOption](#async-option)
- [Result](#result)
- [Either](#either)
- [Pipe extensions](#pipe-extensions)
- [PipeMap extensions](#pipemap-extensions)

### Option

**Option** monad represents 2 possible states: a value or no value.

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

// Standard monad operations supporting async mappers:
var mapAsyncResult = some1.MapAsync(value => Task.FromResult(value + 10));     // == AsyncOption Some(11)

AsyncOption<int> AsyncSome(int value) => AsyncOption.Some(Task.FromResult(value));
var flatMapAsyncResult = some2.FlatMap(value => AsyncSome(value + 10));        // == AsyncOption Some(12)

Task<Option<int>> Async(int value) => Task.FromResult(value.Some());
var flatMapResult2 = await some2.FlatMapAsync(value => Async(value + 10));     // == Some(12)

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
```

### AsyncOption

**AsyncOption** monad represents 2 possible states: an asynchronous operation value or no value.

```cs
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

// Matching by invoking corresponding delegate:
Task PrintSomeAsync<TValue>(TValue value)
{
    Console.WriteLine($"Async Some: {value}!");
    return Task.CompletedTask;
}

Task PrintNoneAsync()
{
    Console.WriteLine($"Async None!");
    return Task.CompletedTask;
}

await asyncSome1.OnAsync(PrintSomeAsync, PrintNoneAsync);    // Prints "Async Some: 1!"
await asyncNone1.OnAsync(PrintSomeAsync, PrintNoneAsync);    // Prints "Async None!"

await asyncSome1.OnSomeAsync(PrintSomeAsync);                // Prints "Async Some: 1!"
await asyncSome1.OnNoneAsync(PrintNoneAsync);                // Prints nothing
await asyncNone1.OnNoneAsync(PrintNoneAsync);                // Prints "Async None!"

// Providing alternative values for None:
AsyncOption<int> asyncSome100 = asyncNone1.Or(Async(100).Some());       // Directly passed alternative monad
AsyncOption<int> asyncSome200 = asyncNone1.Or(() => Async(200).Some()); // Alternative monad retrieved from a delegate

int value100 = await asyncNone1.OrAsync(100);                    // Directly passed alternative value
int value200 = await asyncNone1.OrAsync(() => 200);              // Alternative value retrieved from a delegate

int value300 = await asyncNone1.OrAsync(Async(300));             // Directly passed alternative async value
int value400 = await asyncNone1.OrAsync(() => Async(400));       // Alternative async value retrieved from a delegate

// Extracting a value from Some or throwing an exception:
try
{
    await asyncNone1.ValueOrFailureAsync();     // Throws an exception
}
catch { }

// Awaiting with wrapping value or exception into an instance of Result monad:
var asyncValue = Task.FromResult(1);
var asyncError = Task.FromException<int>(new Exception());
Result<Option<int>> valueResult = await AsyncOption.Some(asyncValue).AsResultAsync();     // Value(1)
Result<Option<int>> error = await AsyncOption.Some(asyncError).AsResultAsync();           // Error(Exception)
```

### Result

**Result** monad represents either a value or an error.

The following code samples are based on **Result&lt;TValue, TError&gt;** type. Another version with identical methods exists having **TError** type defaulted to **Exception**: **Result&lt;TValue&gt;**.

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
#pragma warning disable CS0162 // Unreachable code detected
    return "Value 1";
#pragma warning restore CS0162 // Unreachable code detected
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
#pragma warning disable CS0162 // Unreachable code detected
    return "Value 1";
#pragma warning restore CS0162 // Unreachable code detected
});

// Awaiting with wrapping value or exception into an instance of Result monad:
var asyncValue = Task.FromResult(1);
var asyncError = Task.FromException<int>(new Exception());
Result<int> valueResult = await asyncValue.AsResultAsync();     // Value(1)
Result<int> error = await asyncError.AsResultAsync();           // Error(Exception)
```

### Either

**Either** monad contains one of two possible values: left or right value.

```cs
// Either instances can be created:
var left1 = Either<int, string>.Left(1);             // via factory method
var left2 = Left<int, string>(2);                    // via factory method in non-generic static Either class
Either<int, string> left3 = 3;                       // by implicit conversion

var right4 = Either<int, string>.Right("4");         // via factory method
var right5 = Right<int, string>("5");                // via factory method in non-generic static Either class
Either<int, string> right6 = "6";                    // by implicit conversion

// Supports left and right Map/FlatMap:
var mapLeft1 = left1.MapLeft(left => left + 10);             // == Left(11)
var mapRight1 = left1.MapRight(right => $"Right: {right}");  // == Left(1)
var mapRight2 = right4.MapRight(right => $"Right: {right}"); // == Right("Right: 4")

var flatMapLeft1 = left1.FlatMapLeft(left => Left<int, string>(left + 10));              // == Left(11)
var flatMapRight1 = left1.FlatMapLeft(_ => Right<int, string>($"Right: 1"));             // == Right("Right: 1")
var flatMapRight2 = right4.FlatMapRight(right => Right<int, string>($"Right: {right}")); // == Right("Right: 5")

// Matching by invoking corresponding function:
var matchResult = left1.Match(                      // == "Left: 1"
    left => $"Left: {left}",
    right => $"Right: {right}");

// Matching by invoking corresponding action: 
void PrintLeft<TLeft>(TLeft left) => Console.WriteLine($"Left: {left}");
void PrintRight<TRight>(TRight right) => Console.WriteLine($"Right: {right}");

left1.On(PrintLeft, PrintRight);        // Prints "Left: 1"
left2.OnLeft(PrintLeft);                // Prints "Left: 2"
right5.OnRight(PrintRight);             // Prints "Right: 5"
right5.OnLeft(PrintLeft);               // PrintLeft isn't invoked.

// Extracting left and right values as Option<TLeft> or Option<TRight>:
var some1 = left1.Left();            // == Some(1)
var none1 = left1.Right();           // == None

var none2 = right5.Left();           // == None
var some2 = right5.Right();          // == Some("5")
```

### Pipe extensions

**Pipe extensions** allow chaining function calls.

```cs
// Functions used in examples
int Square(int x) => x * x;
int Half(int x) => x / 2;
int Add5(int x) => x + 5;

// Chaining functions:
var result1 = 10                          // == 55
    .Pipe(Square)
    .Pipe(Half)
    .Pipe(Add5);

// Async functions used in examples
async Task<int> Mul2Async(int x) => await Task.FromResult(x * 2);
async Task<int> Subtract2Async(int x) => await Task.FromResult(x - 2);
async Task<int> QubeAsync(int x) => await Task.FromResult(x * x * x);

// Chaining async and sync functions:
var result2 = await 1                     // == 125
    .Pipe(Add5)
    .PipeAsync(Mul2Async)
    .PipeAsync(Subtract2Async)
    .PipeAsync(Half)
    .PipeAsync(QubeAsync);
```

### PipeMap extensions

**PipeMap extensions** allow chaining function calls with the initial value stored in chaining context. Both initial and calculated values are passed to the final reduction call.

```cs
// Functions used in examples
int Square(int x) => x * x;
int Half(int x) => x / 2;
int Add5(int x) => x + 5;

// Chaining functions:
var result1 = 10                          // == "x = 10, (x * x / 2) + 5 = 55"
    .PipeMap(Square)
    .PipeMap(Half)
    .PipeMap(Add5)
    .Reduce((source, result) => $"x = {source}, (x * x / 2) + 5 = {result}");

// Async functions used in examples
async Task<int> Mul2Async(int x) => await Task.FromResult(x * 2);
async Task<int> Subtract2Async(int x) => await Task.FromResult(x - 2);
async Task<int> QubeAsync(int x) => await Task.FromResult(x * x * x);

// Chaining async functions:
var result2 = await 1                     // == "x = 1, (((x + 5) * 2 - 2) / 2 ) ^ 3 = 125"
    .PipeMap(Add5)
    .PipeMapAsync(Mul2Async)
    .PipeMapAsync(Subtract2Async)
    .PipeMapAsync(Half)
    .PipeMapAsync(QubeAsync)
    .ReduceAsync((source, result) =>  $"x = {source}, (((x + 5) * 2 - 2) / 2 ) ^ 3 = {result}");
```