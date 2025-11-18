using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

public partial class AsyncOptionTests
{
    [Test(Description = "Tests OnAsync 1.")]
    public async Task TestsOnAsync1()
    {
        var results = new List<string>();

        await "value".AsTask().Some().OnAsync(
            _ => results.Add("OnAsync 1 invokes onSome"),
            () => results.Add("OnAsync 1 invokes onNone"));
        await None<string>().OnAsync(
            _ => results.Add("OnAsync 2 invokes onSome"),
            () => results.Add("OnAsync 2 invokes onNone"));

        results.SequenceEqual(["OnAsync 1 invokes onSome", "OnAsync 2 invokes onNone"]).ItIs(true);
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

        await "value".AsTask().Some().OnAsync(
            _ => AddToResults("OnAsync 1 invokes onSome"),
            () => results.Add("OnAsync 1 invokes onNone"));
        await None<string>().OnAsync(
            _ => AddToResults("OnAsync 2 invokes onSome"),
            () => results.Add("OnAsync 2 invokes onNone"));

        results.SequenceEqual(["OnAsync 1 invokes onSome", "OnAsync 2 invokes onNone"]).ItIs(true);
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

        await "value".AsTask().Some().OnAsync(
            _ => results.Add("OnAsync 1 invokes onSome"),
            () => AddToResults("OnAsync 1 invokes onNone"));
        await None<string>().OnAsync(
            _ => results.Add("OnAsync 2 invokes onSome"),
            () => AddToResults("OnAsync 2 invokes onNone"));

        results.SequenceEqual(["OnAsync 1 invokes onSome", "OnAsync 2 invokes onNone"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 4.")]
    public async Task TestsOnAsync4()
    {
        var results = new List<string>();

        Task AddToResults(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await "value".AsTask().Some().OnAsync(
            _ => AddToResults("OnAsync 1 invokes onSome"),
            () => AddToResults("OnAsync 1 invokes onNone"));
        await None<string>().OnAsync(
            _ => AddToResults("OnAsync 2 invokes onSome"),
            () => AddToResults("OnAsync 2 invokes onNone"));

        results.SequenceEqual(["OnAsync 1 invokes onSome", "OnAsync 2 invokes onNone"]).ItIs(true);
    }

    [Test(Description = "Tests OnSomeAsync 1.")]
    public async Task TestsOnSomeAsync1()
    {
        var results = new List<string>();

        await "value".AsTask().Some()
            .OnSomeAsync(_ => results.Add("OnSomeAsync 1 invokes onSome"));
        await None<string>()
            .OnSomeAsync(_ => results.Add("OnSomeAsync 2 invokes onSome"));

        results.SequenceEqual(["OnSomeAsync 1 invokes onSome"]).ItIs(true);
    }

    [Test(Description = "Tests OnSomeAsync 2.")]
    public async Task TestsOnSomeAsync2()
    {
        var results = new List<string>();

        Task AddToResults(string value)
        {
            results.Add(value);
            return Task.CompletedTask;
        }

        await "value".AsTask().Some()
            .OnSomeAsync(_ => AddToResults("OnSomeAsync 1 invokes onSome"));
        await None<string>()
            .OnSomeAsync(_ => AddToResults("OnSomeAsync 2 invokes onSome"));

        results.SequenceEqual(["OnSomeAsync 1 invokes onSome"]).ItIs(true);
    }

    [Test(Description = "Tests OnNoneAsync 1.")]
    public async Task TestsOnNoneAsync1()
    {
        var results = new List<string>();

        await "value".AsTask().Some()
            .OnNoneAsync(() => results.Add("OnNoneAsync 1 invokes onNone"));
        await None<string>()
            .OnNoneAsync(() => results.Add("OnNoneAsync 2 invokes onNone"));

        results.SequenceEqual(["OnNoneAsync 2 invokes onNone"]).ItIs(true);
    }

    [Test(Description = "Tests OnNoneAsync 2.")]
    public async Task TestsOnNoneAsync2()
    {
        var onNoneResults = new List<string>();

        Task AddToResults(string value)
        {
            onNoneResults.Add(value);
            return Task.CompletedTask;
        }

        await "value".AsTask().Some()
            .OnNoneAsync(() => AddToResults("OnNoneAsync 1 invokes onNone"));
        await None<string>()
            .OnNoneAsync(() => AddToResults("OnNoneAsync 2 invokes onNone"));

        onNoneResults.SequenceEqual(["OnNoneAsync 2 invokes onNone"]).ItIs(true);
    }
}