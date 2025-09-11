﻿namespace PureMonads.Samples;

public static class PipeSamples
{
    public static async Task RunAsync()
    {
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
    }
}