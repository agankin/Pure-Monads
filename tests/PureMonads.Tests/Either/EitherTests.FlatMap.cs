using NUnit.Framework;

namespace PureMonads.Tests;

using static Either;

public partial class EitherTests
{
    [Test(Description = "Tests FlatMapLeft")]
    public void TestsFlatMapLeft()
    {
        Left<int, string>(1)
            .FlatMapLeft(left => Left<string, string>($"Left: {left}")).IsLeft("Left: 1");
        Left<int, string>(1)
            .FlatMapLeft(_ => Right<int, string>("Right: 2")).IsRight("Right: 2");

        Right<int, string>("1")
            .FlatMapLeft(left => Left<string, string>($"Left: {left}")).IsRight("1");
        Right<int, string>("1")
            .FlatMapLeft(_ => Right<int, string>("Right: 2")).IsRight("1");
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
}