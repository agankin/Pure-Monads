using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    [Test(Description = "Tests OnAsync 1.")]
    public async Task TestsOnAsync1()
    {
        var results = new List<string>();

        Task AddToResults(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Left<int, string>(1)
            .OnAsync(_ => AddToResults("onLeft1"), _ => results.Add("onRight1"));
        await Right<int, string>("2")
            .OnAsync(_ => AddToResults("onLeft2"), _ => results.Add("onRight2"));

        results.SequenceEqual(["onLeft1", "onRight2"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 2.")]
    public async Task TestsOnAsync2()
    {
        var results = new List<string>();

        Task AddToResults(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Left<int, string>(1)
            .OnAsync(_ => results.Add("onLeft1"), _ => AddToResults("onRight1"));
        await Right<int, string>("2")
            .OnAsync(_ => results.Add("onLeft2"), _ => AddToResults("onRight2"));

        results.SequenceEqual(["onLeft1", "onRight2"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 3.")]
    public async Task TestsOnAsync3()
    {
        var results = new List<string>();

        Task AddToResults(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Left<int, string>(1)
            .OnAsync(_ => AddToResults("onLeft1"), _ => AddToResults("onRight1"));
        await Right<int, string>("2")
            .OnAsync(_ => AddToResults("onLeft2"), _ => AddToResults("onRight2"));

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

    [Test(Description = "Tests OnLeftAsync.")]
    public async Task TestsOnLeftAsync()
    {
        var results = new List<string>();

        Task AddToResults(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Left<int, string>(1).OnLeftAsync(_ => AddToResults("onLeft1"));
        await Right<int, string>("2").OnLeftAsync(_ => AddToResults("onLeft2"));

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

    [Test(Description = "Tests OnRightAsync.")]
    public async Task TestsOnRightAsync()
    {
        var results = new List<string>();

        Task AddToResults(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Left<int, string>(1).OnRightAsync(_ => AddToResults("onRight1"));
        await Right<int, string>("2").OnRightAsync(_ => AddToResults("onRight2"));

        results.SequenceEqual(["onRight2"]).ItIs(true);
    }
}