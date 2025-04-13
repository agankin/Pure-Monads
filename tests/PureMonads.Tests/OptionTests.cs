using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

[TestFixture(TestName = "Option Tests")]
public class OptionTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        1.Some().Equals(1.Some()).ItIs(true);
        1.Some().Equals(2.Some()).ItIs(false);

        None<int>().Equals(None<int>()).ItIs(true);
        None<int>().Equals(1.Some()).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        (1.Some() == 1.Some()).ItIs(true);
        (1.Some() == 2.Some()).ItIs(false);

        (None<int>() == None<int>()).ItIs(true);
        (None<int>() == 1.Some()).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        (1.Some() != 1.Some()).ItIs(false);
        (1.Some() != 2.Some()).ItIs(true);

        (None<int>() != None<int>()).ItIs(false);
        (None<int>() != 1.Some()).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        "value".Some()
            .Match(value => value + " 2", () => "none").ItIs("value 2");
        None<string>()
            .Match(value => value + " 2", () => "none").ItIs("none");
    }

    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        "value".Some()
            .Map(value => value + " 2").IsSome("value 2");
        None<string>()
            .Map(value => value + " 2").IsNone();
    }

    [Test(Description = "Tests FlatMap")]
    public void TestsFlatMap()
    {
        "value".Some()
            .FlatMap<string, string>(value => value + " 2").IsSome("value 2");
        "value".Some()
            .FlatMap(value => None<string>()).IsNone();

        None<string>()
            .FlatMap<string, string>(value => value + " 2").IsNone();
        None<string>()
            .FlatMap(value => None<string>()).IsNone();
    }

    [Test(Description = "Tests Or")]
    public void TestsOr()
    {
        // Some value or an alternative value.
        "value".Some()
            .Or("other").ItIs("value");
        None<string>()
            .Or("other").ItIs("other");

        // Some value or an alternative value from a factory function.
        "value".Some()
            .Or(() => "other").ItIs("value");
        None<string>()
            .Or(() => "other").ItIs("other");

        // Some value or an alternative option.
        "value".Some()
            .Or("other".Some()).IsSome("value");
        "value".Some()
            .Or(None<string>()).IsSome("value");
        None<string>()
            .Or("other".Some()).IsSome("other");
        None<string>()
            .Or(None<string>()).IsNone();

        // Some value or an alternative option from a factory function.
        "value".Some()
            .Or(() => "other".Some()).IsSome("value");
        "value".Some()
            .Or(() => None<string>()).IsSome("value");
        None<string>()
            .Or(() => "other".Some()).IsSome("other");
        None<string>()
            .Or(() => None<string>()).IsNone();
    }

    [Test(Description = "Tests NullAsNone")]
    public void TestsNullAsNone()
    {
        string? @null = null;

        "value".NullAsNone().IsSome("value");
        @null.NullAsNone().IsNone();
    }

    [Test(Description = "Tests Dictionary extensions.")]
    public void TestsDictionaryExtensions()
    {
        var dict = new Dictionary<int, string>
        {
            { 1, "One" },
            { 2, "Two" }
        };

        dict.GetOrNone(1).IsSome("One");
        dict.GetOrNone(3).IsNone();

        IDictionary<int, string> iDict = dict;

        iDict.GetOrNone(1).IsSome("One");
        iDict.GetOrNone(3).IsNone();

        IReadOnlyDictionary<int, string> roDict = dict;

        roDict.GetOrNone(1).IsSome("One");
        roDict.GetOrNone(3).IsNone();
    }

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

    [Test(Description = "Tests ValueOrFailure")]
    public void TestsValueOrFailure()
    {
        "value1".Some().ValueOrFailure().ItIs("value1");

        var exception = Assert.Throws<Exception>(() =>
        {
            None<string>().ValueOrFailure("It is None.");
        });

        exception.NotNull().Message.ItIs("It is None.");
    }
}