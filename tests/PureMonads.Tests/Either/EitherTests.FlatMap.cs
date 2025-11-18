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
        (
            await Left<int, string>(1)
                .FlatMapLeftAsync(left => Left<int, string>(left + 1).AsTask())
        ).IsLeft(2);
        (
            await Left<int, string>(1)
                .FlatMapLeftAsync(left => Right<int, string>($"Right: {left + 1}").AsTask())
        ).IsRight("Right: 2");

        (
            await Right<int, string>("2")
                .FlatMapLeftAsync(_ => Left<int, string>(1).AsTask())
        ).IsRight("2");
        (
            await Right<int, string>("2")
                .FlatMapLeftAsync(_ => Right<int, string>("Right: 2").AsTask())
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
        (
            await Left<int, string>(1)
                .FlatMapRightAsync(_ => Left<int, string>(2).AsTask())
        ).IsLeft(1);
        (
            await Left<int, string>(1)
                .FlatMapRightAsync(_ => Right<int, string>("Right: 2").AsTask())
        ).IsLeft(1);

        (
            await Right<int, string>("2")
                .FlatMapRightAsync(_ => Left<int, string>(1).AsTask())
        ).IsLeft(1);
        (
            await Right<int, string>("2")
                .FlatMapRightAsync(right => Right<int, string>($"Right: {right}").AsTask())
        ).IsRight("Right: 2");
    }
}