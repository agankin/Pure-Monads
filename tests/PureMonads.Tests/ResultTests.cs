using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

[TestFixture(TestName = "Result Tests")]
public partial class ResultTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        Value(1).Equals(Value(1)).ItIs(true);
        Value(1).Equals(Value(2)).ItIs(false);

        Error("err1").Equals(Error("err1")).ItIs(true);
        Error("err1").Equals(Error("err2")).ItIs(false);

        Value(1).Equals(Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        (Value(1) == Value(1)).ItIs(true);
        (Value(1) == Value(2)).ItIs(false);

        (Error("err1") == Error("err1")).ItIs(true);
        (Error("err1") == Error("err2")).ItIs(false);

        (Value(1) == Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        (Value(1) != Value(1)).ItIs(false);
        (Value(1) != Value(2)).ItIs(true);

        (Error("err1") != Error("err1")).ItIs(false);
        (Error("err1") != Error("err2")).ItIs(true);

        (Value(1) != Error("err1")).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        Value<int, string>(1)
            .Match(value => $"value: {value}", error => $"error: {error}").ItIs("value: 1");
        Error<int, string>("err!")
            .Match(value => $"value: {value}", error => $"error: {error}").ItIs("error: err!");
    }

    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        Value<int, string>(1)
            .Map(value => $"value: {value}").IsValue("value: 1");
        Error<int, string>("err!")
            .Map(value => $"value: {value}").IsError("err!");
    }

    [Test(Description = "Tests FlatMap")]
    public void TestsFlatMap()
    {
        Value<int, string>(1)
            .FlatMap(value => Value<int, string>(value + 1)).IsValue(2);
        Value<int, string>(1)
            .FlatMap(_ => Error<int, string>("err!")).IsError("err!");

        Error<int, string>("err!")
            .FlatMap(value => Value<int, string>(value + 1)).IsError("err!");
        Error<int, string>("err!")
            .FlatMap(_ => Error<int, string>("err2!")).IsError("err!");
    }

    [Test(Description = "Tests to Option")]
    public void TestsToOption()
    {
        Value<int, string>(1)
            .Value().IsSome(1);
        Value<int, string>(1)
            .Error().IsNone();

        Error<int, string>("err!")
            .Value().IsNone();
        Error<int, string>("err!")
            .Error().IsSome("err!");
    }

    [Test(Description = "Tests On.")]
    public void TestsOn()
    {
        var onResults = new List<string>();

        Value<string, int>("value").On(_ => onResults.Add("On 1 invokes onValue"), _ => onResults.Add("On 1 invokes onError"));
        Error<string, int>(1).On(_ => onResults.Add("On 2 invokes onValue"), _ => onResults.Add("On 2 invokes onError"));

        onResults.SequenceEqual(["On 1 invokes onValue", "On 2 invokes onError"]).ItIs(true);
    }

    [Test(Description = "Tests OnValue.")]
    public void TestsOnValue()
    {
        var onValueResults = new List<string>();

        Value<string, int>("value").OnValue(_ => onValueResults.Add("OnValue 1 invokes onValue"));
        Error<string, int>(1).OnValue(_ => onValueResults.Add("OnValue 2 invokes onValue"));

        onValueResults.SequenceEqual(["OnValue 1 invokes onValue"]).ItIs(true);
    }

    [Test(Description = "Tests OnError.")]
    public void TestsOnError()
    {
        var onErrorResults = new List<string>();

        Value<string, int>("value").OnError(_ => onErrorResults.Add("OnError 1 invokes onError"));
        Error<string, int>(1).OnError(_ => onErrorResults.Add("OnError 2 invokes onError"));

        onErrorResults.SequenceEqual(["OnError 2 invokes onError"]).ItIs(true);
    }
}