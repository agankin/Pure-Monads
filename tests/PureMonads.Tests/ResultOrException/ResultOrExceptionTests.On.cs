using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
    [Test(Description = "Tests On.")]
    public void TestsOn()
    {
        var results = new List<string>();

        Value("value").On(_ => results.Add("On 1 invokes onValue"), _ => results.Add("On 1 invokes onError"));
        Error<string>(new Exception()).On(_ => results.Add("On 2 invokes onValue"), _ => results.Add("On 2 invokes onError"));

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

        await Value("value")
            .OnAsync(
                _ => AddToResultsAsync("On 1 invokes onValue"),
                _ => AddToResultsAsync("On 1 invokes onError"));
        await Error<string>(new Exception())
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

        await Value("value")
            .OnAsync(
                _ => AddToResultsAsync("On 1 invokes onValue"),
                _ => results.Add("On 1 invokes onError"));
        await Error<string>(new Exception())
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

        await Value("value")
            .OnAsync(
                _ => results.Add("On 1 invokes onValue"),
                _ => AddToResultsAsync("On 1 invokes onError"));
        await Error<string>(new Exception())
            .OnAsync(
                _ => results.Add("On 2 invokes onValue"),
                _ => AddToResultsAsync("On 2 invokes onError"));

        results.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnValue.")]
    public void TestsOnValue()
    {
        var results = new List<string>();

        Value("value").OnValue(_ => results.Add("OnValue 1 invokes onValue"));
        Error<string>(new Exception()).OnValue(_ => results.Add("OnValue 2 invokes onValue"));

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

        await Value("value").OnValueAsync(_ => AddToResultsAsync("OnValue 1 invokes onValue"));
        await Error<string>(new Exception()).OnValueAsync(_ => AddToResultsAsync("OnValue 2 invokes onValue"));

        results.SequenceEqual(["OnValue 1 invokes onValue"]).ItIs(true);
    }

    [Test(Description = "Tests OnError.")]
    public void TestsOnError()
    {
        var results = new List<string>();

        Value("value").OnError(_ => results.Add("OnError 1 invokes onError"));
        Error<string>(new Exception()).OnError(_ => results.Add("OnError 2 invokes onError"));

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

        await Value("value").OnErrorAsync(_ => AddToResultsAsync("OnError 1 invokes onError"));
        await Error<string>(new Exception()).OnErrorAsync(_ => AddToResultsAsync("OnError 2 invokes onError"));

        results.SequenceEqual(["OnError 2 invokes onError"]).ItIs(true);
    }
}