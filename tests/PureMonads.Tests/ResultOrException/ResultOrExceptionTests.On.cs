using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
    [Test(Description = "Tests On.")]
    public void TestsOn()
    {
        var onResults = new List<string>();

        Value("value").On(_ => onResults.Add("On 1 invokes onValue"), _ => onResults.Add("On 1 invokes onError"));
        Error<string>(new Exception()).On(_ => onResults.Add("On 2 invokes onValue"), _ => onResults.Add("On 2 invokes onError"));

        onResults.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnValue.")]
    public void TestsOnValue()
    {
        var onValueResults = new List<string>();

        Value("value").OnValue(_ => onValueResults.Add("OnValue 1 invokes onValue"));
        Error<string>(new Exception()).OnValue(_ => onValueResults.Add("OnValue 2 invokes onValue"));

        onValueResults.SequenceEqual(["OnValue 1 invokes onValue"]).ItIs(true);
    }

    [Test(Description = "Tests OnError.")]
    public void TestsOnError()
    {
        var onErrorResults = new List<string>();

        Value("value").OnError(_ => onErrorResults.Add("OnError 1 invokes onError"));
        Error<string>(new Exception()).OnError(_ => onErrorResults.Add("OnError 2 invokes onError"));

        onErrorResults.SequenceEqual(["OnError 2 invokes onError"]).ItIs(true);
    }
}