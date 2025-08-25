using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Either;

public partial class EitherTests
{
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