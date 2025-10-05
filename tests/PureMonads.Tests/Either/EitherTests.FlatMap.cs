using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Either;

public partial class EitherTests
{
    [Test(Description = "Tests FlatMapLeft")]
    public void TestsFlatMapLeft()
    {
        Left<int, string>(1)
            .FlatMapLeft(left => Left<int, string>(left + 1)).IsLeft(2);
        Left<int, string>(1)
            .FlatMapLeft(left => Right<int, string>($"Right: {left + 1}")).IsRight("Right: 2");

        Right<int, string>("2")
            .FlatMapLeft(_ => Left<int, string>(1)).IsRight("2");
        Right<int, string>("2")
            .FlatMapLeft(_ => Right<int, string>("Right: 2")).IsRight("2");
    }

    [Test(Description = "Tests FlatMapLeftAsync")]
    public async Task TestsFlatMapLeftAsync()
    {
        Task<Either<int, string>> Async(Either<int, string> either) => Task.FromResult(either);

        (
            await Left<int, string>(1)
                .FlatMapLeftAsync(left => Async(Left<int, string>(left + 1)))
        ).IsLeft(2);
        (
            await Left<int, string>(1)
                .FlatMapLeftAsync(left => Async(Right<int, string>($"Right: {left + 1}")))
        ).IsRight("Right: 2");

        (
            await Right<int, string>("2")
                .FlatMapLeftAsync(_ => Async(Left<int, string>(1)))
        ).IsRight("2");
        (
            await Right<int, string>("2")
                .FlatMapLeftAsync(_ => Async(Right<int, string>("Right: 2")))
        ).IsRight("2");
    }

    [Test(Description = "Tests FlatMapRight")]
    public void TestsFlatMapRight()
    {
        Left<int, string>(1)
            .FlatMapRight(_ => Left<int, string>(2)).IsLeft(1);
        Left<int, string>(1)
            .FlatMapRight(_ => Right<int, string>("Right: 2")).IsLeft(1);

        Right<int, string>("2")
            .FlatMapRight(_ => Left<int, string>(1)).IsLeft(1);
        Right<int, string>("2")
            .FlatMapRight(right => Right<int, string>($"Right: {right}")).IsRight("Right: 2");
    }

    [Test(Description = "Tests FlatMapRightAsync")]
    public async Task TestsFlatMapRightAsync()
    {
        Task<Either<int, string>> Async(Either<int, string> either) => Task.FromResult(either);
        
        (
            await Left<int, string>(1)
                .FlatMapRightAsync(_ => Async(Left<int, string>(2)))
        ).IsLeft(1);
        (
            await Left<int, string>(1)
                .FlatMapRightAsync(_ => Async(Right<int, string>("Right: 2")))
        ).IsLeft(1);

        (
            await Right<int, string>("2")
                .FlatMapRightAsync(_ => Async(Left<int, string>(1)))
        ).IsLeft(1);
        (
            await Right<int, string>("2")
                .FlatMapRightAsync(right => Async(Right<int, string>($"Right: {right}")))
        ).IsRight("Right: 2");
    }
}