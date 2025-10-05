using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Either;

public partial class EitherTests
{
    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        Left<int, string>(1)
            .MapLeft(left => $"Left: {left}").IsLeft("Left: 1");
        Left<int, string>(1)
            .MapRight(right => $"Right: {right}").IsLeft(1);

        Right<int, string>("2")
            .MapRight(right => $"Right: {right}").IsRight("Right: 2");
        Right<int, string>("2")
            .MapLeft(left => $"Left: {left}").IsRight("2");
    }
    
    [Test(Description = "Tests MapAsync")]
    public async Task TestsMapAsync()
    {
        Task<string> AsyncValue(string value) => Task.FromResult(value);

        (
            await Left<int, string>(1)
                .MapLeftAsync(left => AsyncValue($"Left: {left}"))
        ).IsLeft("Left: 1");
        (
            await Left<int, string>(1)
                .MapRightAsync(right => AsyncValue($"Right: {right}"))
        ).IsLeft(1);
        
        (
            await Right<int, string>("2")
                .MapRightAsync(right => AsyncValue($"Right: {right}"))
        ).IsRight("Right: 2");
        (
            await Right<int, string>("2")
                .MapLeftAsync(left => AsyncValue($"Left: {left}"))
        ).IsRight("2");
    }
}