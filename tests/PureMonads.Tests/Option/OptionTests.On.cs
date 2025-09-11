using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

public partial class OptionTests
{
    [Test(Description = "Tests On.")]
    public void TestsOn()
    {
        var onResults = new List<string>();

        "value".Some().On(
            _ => onResults.Add("On 1 invokes onSome"),
            () => onResults.Add("On 1 invokes onNone"));
        None<string>().On(
            _ => onResults.Add("On 2 invokes onSome"),
            () => onResults.Add("On 2 invokes onNone"));

        onResults.SequenceEqual(["On 1 invokes onSome", "On 2 invokes onNone"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 1.")]
    public async Task TestsOnAsync1()
    {
        var onResults = new List<string>();

        Task AddToResults(string value)
        {
            onResults.Add(value);
            return Task.CompletedTask;
        }

        await "value".Some().OnAsync(
            _ => AddToResults("On 1 invokes onSome"),
            () => AddToResults("On 1 invokes onNone"));
        await None<string>().OnAsync(
            _ => AddToResults("On 2 invokes onSome"),
            () => AddToResults("On 2 invokes onNone"));

        onResults.SequenceEqual(["On 1 invokes onSome", "On 2 invokes onNone"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 2.")]
    public async Task TestsOnAsync2()
    {
        var onResults = new List<string>();

        Task AddToResults(string value)
        {
            onResults.Add(value);
            return Task.CompletedTask;
        }

        await "value".Some().OnAsync(
            _ => AddToResults("On 1 invokes onSome"),
            () => onResults.Add("On 1 invokes onNone"));
        await None<string>().OnAsync(
            _ => AddToResults("On 2 invokes onSome"),
            () => onResults.Add("On 2 invokes onNone"));

        onResults.SequenceEqual(["On 1 invokes onSome", "On 2 invokes onNone"]).ItIs(true);
    }

    [Test(Description = "Tests OnAsync 3.")]
    public async Task TestsOnAsync3()
    {
        var onResults = new List<string>();

        Task AddToResults(string value)
        {
            onResults.Add(value);
            return Task.CompletedTask;
        }

        await "value".Some().OnAsync(
            _ => onResults.Add("On 1 invokes onSome"),
            () => AddToResults("On 1 invokes onNone"));
        await None<string>().OnAsync(
            _ => onResults.Add("On 2 invokes onSome"),
            () => AddToResults("On 2 invokes onNone"));

        onResults.SequenceEqual(["On 1 invokes onSome", "On 2 invokes onNone"]).ItIs(true);
    }

    [Test(Description = "Tests OnSome.")]
    public void TestsOnSome()
    {
        var onSomeResults = new List<string>();

        "value".Some().OnSome(_ => onSomeResults.Add("OnSome 1 invokes onSome"));
        None<string>().OnSome(_ => onSomeResults.Add("OnSome 2 invokes onSome"));

        onSomeResults.SequenceEqual(["OnSome 1 invokes onSome"]).ItIs(true);
    }

    [Test(Description = "Tests OnSomeAsync.")]
    public async Task TestsOnSomeAsync()
    {
        var onSomeResults = new List<string>();

        Task AddToResults(string value)
        {
            onSomeResults.Add(value);
            return Task.CompletedTask;
        }

        await "value".Some().OnSomeAsync(_ => AddToResults("OnSome 1 invokes onSome"));
        await None<string>().OnSomeAsync(_ => AddToResults("OnSome 2 invokes onSome"));

        onSomeResults.SequenceEqual(["OnSome 1 invokes onSome"]).ItIs(true);
    }

    [Test(Description = "Tests OnNone.")]
    public void TestsOnNone()
    {
        var onNoneResults = new List<string>();

        "value".Some().OnNone(() => onNoneResults.Add("OnNone 1 invokes onNone"));
        None<string>().OnNone(() => onNoneResults.Add("OnNone 2 invokes onNone"));

        onNoneResults.SequenceEqual(["OnNone 2 invokes onNone"]).ItIs(true);
    }

    [Test(Description = "Tests OnNoneAsync.")]
    public async Task TestsOnNoneAsync()
    {
        var onNoneResults = new List<string>();

        Task AddToResults(string value)
        {
            onNoneResults.Add(value);
            return Task.CompletedTask;
        }

        await "value".Some().OnNoneAsync(() => AddToResults("OnNone 1 invokes onNone"));
        await None<string>().OnNoneAsync(() => AddToResults("OnNone 2 invokes onNone"));

        onNoneResults.SequenceEqual(["OnNone 2 invokes onNone"]).ItIs(true);
    }
}