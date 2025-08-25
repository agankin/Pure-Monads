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
}