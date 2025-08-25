using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

public partial class OptionTests
{
    [Test(Description = "Tests On.")]
    public void TestsOn()
    {
        var onResults = new List<string>();

        "value".Some().On(_ => onResults.Add("On 1 invokes onSome"), () => onResults.Add("On 1 invokes onNone"));
        None<string>().On(_ => onResults.Add("On 2 invokes onSome"), () => onResults.Add("On 2 invokes onNone"));

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

    [Test(Description = "Tests OnNone.")]
    public void TestsOnNone()
    {
        var onNoneResults = new List<string>();

        "value".Some().OnNone(() => onNoneResults.Add("OnNone 1 invokes onNone"));
        None<string>().OnNone(() => onNoneResults.Add("OnNone 2 invokes onNone"));

        onNoneResults.SequenceEqual(["OnNone 2 invokes onNone"]).ItIs(true);
    }
}