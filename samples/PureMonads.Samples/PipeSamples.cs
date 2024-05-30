namespace PureMonads.Samples;

public static class PipeSamples
{
    public static async Task RunAsync()
    {
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
    }
}