namespace PureMonads.Samples;

public static class PipeMapSamples
{
    public static async Task RunAsync()
    {
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
        async Task<int> Mul2Async(int x) => await (x * 2).AsTask();
        async Task<int> Subtract2Async(int x) => await (x - 2).AsTask();
        async Task<int> QubeAsync(int x) => await (x * x * x).AsTask();

        // Chaining async functions:
        var result2 = await 1                     // == "x = 1, (((x + 5) * 2 - 2) / 2 ) ^ 3 = 125"
            .PipeMap(Add5)
            .PipeMapAsync(Mul2Async)
            .PipeMapAsync(Subtract2Async)
            .PipeMapAsync(Half)
            .PipeMapAsync(QubeAsync)
            .ReduceAsync((source, result) =>  $"x = {source}, (((x + 5) * 2 - 2) / 2 ) ^ 3 = {result}");
    }
}