﻿namespace PureMonads.Samples;

public static class PipeSamples
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
        var result1 = 10                          // == 55
            .Pipe(Square)
            .Pipe(Half)
            .Pipe(Add5);

        // Chaining async functions:
        var result2 = await 3                     // == 64
            .PipeAsync(DoubleAsync)
            .PipeAsync(Subtract2Async)
            .PipeAsync(QubeAsync);
    }
}