namespace PureMonads.Samples;

public static class PipeMapSamples
{
    public static async Task RunAsync()
    {
        // Functions used in examples
        int Square(int x) => x * x;
        int Half(int x) => x / 2;
        int Add5(int x) => x + 5;

        async Task<int> DoubleAsync(int x) => await Task.FromResult(x * 2);
        async Task<int> Subtract2Async(int x) => await Task.FromResult(x - 2);
        async Task<int> QubeAsync(int x) => await Task.FromResult(x * x * x);

        // Chaining functions:
        var result1 = 10                          // == "x = 10, (x * x / 2) + 5 = 55"
            .PipeMap(Square)
            .PipeMap(Half)
            .PipeMap(Add5)
            .Reduce((source, result) => $"x = {source}, (x * x / 2) + 5 = {result}");

        // Chaining async functions:
        var result2 = await 3                     // == 64
            .PipeMapAsync(DoubleAsync)
            .PipeMapAsync(Subtract2Async)
            .PipeMapAsync(QubeAsync)
            .ReduceAsync((source, result) =>  $"x = {source}, (x * 2 - 2) ^ 3 = {result}");
    }
}