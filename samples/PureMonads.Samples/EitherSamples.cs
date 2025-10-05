namespace PureMonads.Samples;

using System.Threading.Tasks;
using static Either;

public static class EitherSamples
{
    public static async Task RunAsync()
    {
        // Either instances can be created:
        var left1 = Either<int, string>.Left(1);             // via factory method
        var left2 = Left<int, string>(2);                    // via factory method in non-generic static Either class
        Either<int, string> left3 = 3;                       // by implicit conversion

        var right4 = Either<int, string>.Right("4");         // via factory method
        var right5 = Right<int, string>("5");                // via factory method in non-generic static Either class
        Either<int, string> right6 = "6";                    // by implicit conversion

        // Supports left and right Map/FlatMap:
        var mapLeft1 = left1.MapLeft(left => left + 10);             // == Left(11)
        var mapRight1 = left1.MapRight(right => $"Right: {right}");  // == Still Left(1)
        var mapRight2 = right4.MapRight(right => $"Right: {right}"); // == Right("Right: 4")
        
        var flatMapLeft1 = left1.FlatMapLeft(left => Left<int, string>(left + 10));              // == Left(11)
        var flatMapRight1 = left1.FlatMapLeft(_ => Right<int, string>($"Right: 1"));             // == Right("Right: 1")
        var flatMapRight2 = right4.FlatMapRight(right => Right<int, string>($"Right: {right}")); // == Right("Right: 4")

        // Supports left and right Map/FlatMap with async mappers:
        Task<TValue> Async<TValue>(TValue value) => Task.FromResult(value);

        var mapLeft1Async = await left1.MapLeftAsync(left => Async(left + 10));             // == Left(11)
        var mapRight1Async = await left1.MapRightAsync(right => Async($"Right: {right}"));  // == Still Left(1)
        var mapRight2Async = await right4.MapRightAsync(right => Async($"Right: {right}")); // == Right("Right: 4")
        
        var flatMapLeft1Async = await left1.FlatMapLeftAsync(                               // == Left(11)
            left => Async(Left<int, string>(left + 10)));
        var flatMapRight1Async = await left1.FlatMapLeftAsync(                              // == Right("Right: 1")
            _ => Async(Right<int, string>($"Right: 1")));
        var flatMapRight2Async = await right4.FlatMapRightAsync(                            // == Right("Right: 4")
            right => Async(Right<int, string>($"Right: {right}")));

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
        right5.OnLeft(PrintLeft);               // Prints nothing.

        // The same with async actions:
        Task PrintLeftAsync<TLeft>(TLeft left)
        {
            Console.WriteLine($"Left: {left}");
            return Task.CompletedTask;
        }

        Task PrintRightAsync<TRight>(TRight right)
        {
            Console.WriteLine($"Right: {right}");
            return Task.CompletedTask;
        }

        await left1.OnAsync(PrintLeftAsync, PrintRightAsync);   // Prints "Left: 1"
        await left2.OnLeftAsync(PrintLeftAsync);                // Prints "Left: 2"
        await right5.OnRightAsync(PrintRightAsync);             // Prints "Right: 5"
        await right5.OnLeftAsync(PrintLeftAsync);               // Prints nothing.

        // Extracting left and right values as Option<TLeft> or Option<TRight>:
        var some1 = left1.Left();            // == Some(1)
        var none1 = left1.Right();           // == None

        var none2 = right5.Left();           // == None
        var some2 = right5.Right();          // == Some("5")
    }
}