using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Either;

[TestFixture(TestName = "Either Tests")]
public class EitherTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        var Left = Left<int, string>;
        var Right = Right<int, string>;

        Left(1).Equals(Left(1)).ItIs(true);
        Left(1).Equals(Left(2)).ItIs(false);

        Right("1").Equals(Right("1")).ItIs(true);
        Right("1").Equals(Right("2")).ItIs(false);

        Left(1).Equals(Right("1")).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var Left = Left<int, string>;
        var Right = Right<int, string>;

        (Left(1) == Left(1)).ItIs(true);
        (Left(1) == Left(2)).ItIs(false);

        (Right("1") == Right("1")).ItIs(true);
        (Right("1") == Right("2")).ItIs(false);

        (Left(1) == Right("1")).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var Left = Left<int, string>;
        var Right = Right<int, string>;

        (Left(1) != Left(1)).ItIs(false);
        (Left(1) != Left(2)).ItIs(true);

        (Right("1") != Right("1")).ItIs(false);
        (Right("1") != Right("2")).ItIs(true);

        (Left(1) != Right("1")).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        Left<int, string>(1)
            .Match(left => $"Left: {left}", right => $"Right: {right}").ItIs("Left: 1");
        Right<int, string>("2")
            .Match(left => $"Left: {left}", right => $"Right: {right}").ItIs("Right: 2");
    }

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

    [Test(Description = "Tests to Option")]
    public void TestsToOption()
    {
        Left<int, string>(1)
            .Left().IsSome(1);
        Left<int, string>(1)
            .Right().IsNone();

        Right<int, string>("2")
            .Left().IsNone();
        Right<int, string>("2")
            .Right().IsSome("2");
    }

    [Test(Description = "Tests On.")]
    public void TestsOn()
    {
        var results = new List<string>();

        Left<int, string>(1).On(_ => results.Add("onLeft1"), _ => results.Add("onRight1"));
        Right<int, string>("2").On(_ => results.Add("onLeft2"), _ => results.Add("onRight2"));

        results.SequenceEqual(["onLeft1", "onRight2"]).ItIs(true);
    }

    [Test(Description = "Tests OnLeft.")]
    public void TestsOnLeft()
    {
        var results = new List<string>();

        Left<int, string>(1).OnLeft(_ => results.Add("onLeft1"));
        Right<int, string>("2").OnLeft(_ => results.Add("onLeft2"));

        results.SequenceEqual(["onLeft1"]).ItIs(true);
    }

    [Test(Description = "Tests OnRight.")]
    public void TestsOnRight()
    {
        var results = new List<string>();

        Left<int, string>(1).OnRight(_ => results.Add("onRight1"));
        Right<int, string>("2").OnRight(_ => results.Add("onRight2"));

        results.SequenceEqual(["onRight2"]).ItIs(true);
    }
}