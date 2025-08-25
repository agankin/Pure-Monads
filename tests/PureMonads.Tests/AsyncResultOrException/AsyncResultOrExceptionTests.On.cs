using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultOrExceptionTests
{
    [Test(Description = "Tests OnAsync.")]
    public async Task TestsOnAsync()
    {
        AsyncResult<string> AsyncValue(string val) => Value(Task.FromResult(val));
        var onResults = new List<string>();

        await AsyncValue("value")
            .OnAsync(
                _ => onResults.Add("On 1 invokes onValue"),
                _ => onResults.Add("On 1 invokes onError")
            );
        await Error<string>(new Exception())
            .OnAsync(
                _ => onResults.Add("On 2 invokes onValue"),
                _ => onResults.Add("On 2 invokes onError")
            );

        onResults.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnValueAsync.")]
    public async Task TestsOnValueAsync()
    {
        AsyncResult<string> AsyncValue(string val) => Value(Task.FromResult(val));
        var onValueResults = new List<string>();

        await AsyncValue("value")
            .OnValueAsync(_ => onValueResults.Add("OnValue 1 invokes onValue"));
        await Error<string>(new Exception())
            .OnValueAsync(_ => onValueResults.Add("OnValue 2 invokes onValue"));

        onValueResults.SequenceEqual(["OnValue 1 invokes onValue"]).ItIs(true);
    }

    [Test(Description = "Tests OnError.")]
    public void TestsOnError()
    {
        AsyncResult<string> AsyncValue(string val) => Value(Task.FromResult(val));
        var onErrorResults = new List<string>();

        AsyncValue("value")
            .OnError(_ => onErrorResults.Add("OnError 1 invokes onError"));
        Error<string>(new Exception()).OnError(_ => onErrorResults.Add("OnError 2 invokes onError"));

        onErrorResults.SequenceEqual(["OnError 2 invokes onError"]).ItIs(true);
    }
}