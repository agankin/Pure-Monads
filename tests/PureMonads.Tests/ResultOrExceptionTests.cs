using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

[TestFixture(TestName = "Result or Exception Tests")]
public partial class ResultOrExceptionTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        var Value = Value<int>;
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        Value(1).Equals(Value(1)).ItIs(true);
        Value(1).Equals(Value(2)).ItIs(false);

        Error(err1).Equals(Error(err1)).ItIs(true);
        Error(err1).Equals(Error(err2)).ItIs(false);

        Value(1).Equals(Error(err1)).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var Value = Value<int>;
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        (Value(1) == Value(1)).ItIs(true);
        (Value(1) == Value(2)).ItIs(false);

        (Error(err1) == Error(err1)).ItIs(true);
        (Error(err1) == Error(err2)).ItIs(false);

        (Value(1) == Error(err1)).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var Value = Value<int>;
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        (Value(1) != Value(1)).ItIs(false);
        (Value(1) != Value(2)).ItIs(true);

        (Error(err1) != Error(err1)).ItIs(false);
        (Error(err1) != Error(err2)).ItIs(true);

        (Value(1) != Error(err1)).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        Value(1)
            .Match(value => $"value: {value}", error => $"error: {error.Message}").ItIs("value: 1");
        Error<int>(new TestException("err!"))
            .Match(value => $"value: {value}", error => $"error: {error.Message}").ItIs("error: err!");
    }

    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        Value(1)
            .Map(value => $"value: {value}").IsValue("value: 1");
        Error<int>(new TestException("err!"))
            .Map(value => $"value: {value}").IsError(new TestException("err!"));
    }

    [Test(Description = "Tests FlatMap")]
    public void TestsFlatMap()
    {
        Value(1)
            .FlatMap(value => Value(value + 1)).IsValue(2);
        Value(1)
            .FlatMap(_ => Error<int>(new TestException("err!"))).IsError(new TestException("err!"));

        Error<int>(new TestException("err!"))
            .FlatMap(value => Value(value + 1)).IsError(new TestException("err!"));
        Error<int>(new TestException("err!"))
            .FlatMap(_ => Error<int>(new TestException("err2!"))).IsError(new TestException("err!"));
    }

    [Test(Description = "Tests to Option")]
    public void TestsToOption()
    {
        Value(1)
            .Value().IsSome(1);
        Value(1)
            .Error().IsNone();

        Error<int>(new TestException("err!"))
            .Value().IsNone();
        Error<int>(new TestException("err!"))
            .Error().IsSome(new TestException("err!"));
    }

    [Test(Description = "Tests From")]
    public void TestsFrom()
    {
        From(() => 1).IsValue(1);
        From(new Func<int>(() => throw new TestException("err!"))).IsError(new TestException("err!"));
    }

    [Test(Description = "Tests FromAsync")]
    public async Task TestsFromAsync()
    {
        (await FromAsync(async () => 
        {
            await Task.CompletedTask;
            return 1;
        })).IsValue(1);
        (await FromAsync(new Func<Task<int>>(async () => 
        {
            await Task.CompletedTask;
            throw new TestException("err!");
        }))).IsError(new TestException("err!"));
    }

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