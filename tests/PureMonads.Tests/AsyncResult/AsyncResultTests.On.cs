using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultTests
{
    [Test(Description = "Tests OnAsync.")]
    public async Task TestsOnAsync()
    {
        var onResults = new List<string>();

        await Value<string, int>("value".AsTask())
            .OnAsync(
                _ => onResults.Add("On 1 invokes onValue"),
                _ => onResults.Add("On 1 invokes onError"));
        await Error<string, int>(1)
            .OnAsync(
                _ => onResults.Add("On 2 invokes onValue"),
                _ => onResults.Add("On 2 invokes onError"));

        onResults.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnValue.")]
    public async Task TestsOnValue()
    {
        var onValueResults = new List<string>();

        await Value<string, int>("value".AsTask())
            .OnValueAsync(_ => onValueResults.Add("OnValue 1 invokes onValue"));
        await Error<string, int>(1)
            .OnValueAsync(_ => onValueResults.Add("OnValue 2 invokes onValue"));

        onValueResults.SequenceEqual(["OnValue 1 invokes onValue"]).ItIs(true);
    }

    [Test(Description = "Tests OnError.")]
    public void TestsOnError()
    {
        var onErrorResults = new List<string>();

        Value<string, int>("value".AsTask())
            .OnError(_ => onErrorResults.Add("OnError 1 invokes onError"));
        Error<string, int>(1)
            .OnError(_ => onErrorResults.Add("OnError 2 invokes onError"));

        onErrorResults.SequenceEqual(["OnError 2 invokes onError"]).ItIs(true);
    }
}