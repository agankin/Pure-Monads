using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultTests
{
    [Test(Description = "Tests On.")]
    public void TestsOn()
    {
        var results = new List<string>();

        Value<string, int>("value")
            .On(_ => results.Add("On 1 invokes onValue"), _ => results.Add("On 1 invokes onError"));
        Error<string, int>(1)
            .On(_ => results.Add("On 2 invokes onValue"), _ => results.Add("On 2 invokes onError"));

        results.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 1.")]
    public async Task TestsOnAsync1()
    {
        var results = new List<string>();

        Task AddToResultsAsync(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Value<string, int>("value")
            .OnAsync(
                _ => AddToResultsAsync("On 1 invokes onValue"),
                _ => AddToResultsAsync("On 1 invokes onError"));
        await Error<string, int>(1)
            .OnAsync(
                _ => AddToResultsAsync("On 2 invokes onValue"),
                _ => AddToResultsAsync("On 2 invokes onError"));

        results.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 2.")]
    public async Task TestsOnAsync2()
    {
        var results = new List<string>();

        Task AddToResultsAsync(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Value<string, int>("value")
            .OnAsync(
                _ => AddToResultsAsync("On 1 invokes onValue"),
                _ => results.Add("On 1 invokes onError"));
        await Error<string, int>(1)
            .OnAsync(
                _ => AddToResultsAsync("On 2 invokes onValue"),
                _ => results.Add("On 2 invokes onError"));

        results.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 3.")]
    public async Task TestsOnAsync3()
    {
        var results = new List<string>();

        Task AddToResultsAsync(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Value<string, int>("value")
            .OnAsync(
                _ => results.Add("On 1 invokes onValue"),
                _ => AddToResultsAsync("On 1 invokes onError"));
        await Error<string, int>(1)
            .OnAsync(
                _ => results.Add("On 2 invokes onValue"),
                _ => AddToResultsAsync("On 2 invokes onError"));

        results.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnValue.")]
    public void TestsOnValue()
    {
        var results = new List<string>();

        Value<string, int>("value").OnValue(_ => results.Add("OnValue 1 invokes onValue"));
        Error<string, int>(1).OnValue(_ => results.Add("OnValue 2 invokes onValue"));

        results.SequenceEqual(["OnValue 1 invokes onValue"]).ItIs(true);
    }

    [Test(Description = "Tests OnValueAsync.")]
    public async Task TestsOnValueAsync()
    {
        var results = new List<string>();

        Task AddToResultsAsync(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Value<string, int>("value")
            .OnValueAsync(_ => AddToResultsAsync("OnValue 1 invokes onValue"));
        await Error<string, int>(1)
            .OnValueAsync(_ => AddToResultsAsync("OnValue 2 invokes onValue"));

        results.SequenceEqual(["OnValue 1 invokes onValue"]).ItIs(true);
    }

    [Test(Description = "Tests OnError.")]
    public void TestsOnError()
    {
        var results = new List<string>();

        Value<string, int>("value").OnError(_ => results.Add("OnError 1 invokes onError"));
        Error<string, int>(1).OnError(_ => results.Add("OnError 2 invokes onError"));

        results.SequenceEqual(["OnError 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnErrorAsync.")]
    public async Task TestsOnErrorAsync()
    {
        var results = new List<string>();

        Task AddToResultsAsync(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await Value<string, int>("value").OnErrorAsync(_ => AddToResultsAsync("OnError 1 invokes onError"));
        await Error<string, int>(1).OnErrorAsync(_ => AddToResultsAsync("OnError 2 invokes onError"));

        results.SequenceEqual(["OnError 2 invokes onError"]).ItIs(true);
    }
}