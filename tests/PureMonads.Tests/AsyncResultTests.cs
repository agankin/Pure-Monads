using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

[TestFixture(TestName = "AsyncResult Tests")]
public partial class AsyncResultTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        Value(task1).Equals(Value(task1)).ItIs(true);
        Value(task1).Equals(Value(task2)).ItIs(false);

        Error("err1").Equals(Error("err1")).ItIs(true);
        Error("err1").Equals(Error("err2")).ItIs(false);

        Value(task1).Equals(Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        (Value(task1) == Value(task1)).ItIs(true);
        (Value(task1) == Value(task2)).ItIs(false);

        (Error("err1") == Error("err1")).ItIs(true);
        (Error("err1") == Error("err2")).ItIs(false);

        (Value(task1) == Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        (Value(task1) != Value(task1)).ItIs(false);
        (Value(task1) != Value(task2)).ItIs(true);

        (Error("err1") != Error("err1")).ItIs(false);
        (Error("err1") != Error("err2")).ItIs(true);

        (Value(task1) != Error("err1")).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public async Task TestsMatch()
    {
        var task1 = Task.FromResult(1);

        (
            await Value<int, string>(task1)
                .Match(
                    async task => $"value: {await task}",
                    error => Task.FromResult($"error: {error}"))
        ).ItIs("value: 1");

        (
            await Error<int, string>("err!")
                .Match(
                    async task => $"value: {await task}",
                    error => Task.FromResult($"error: {error}"))
        ).ItIs("error: err!");
    }

    [Test(Description = "Tests MapAsync")]
    public async Task TestsMapAsync()
    {
        var task1 = Task.FromResult(1);

        await Value<int, string>(task1)
            .Map(value => $"value: {value}").IsValueAsync("value: 1");
        Error<int, string>("err!")
            .Map(value => $"value: {value}").IsError("err!");
    }

    [Test(Description = "Tests FlatMapAsync")]
    public async Task TestsFlatMapAsync()
    {
        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        await (
            await Value<int, string>(task1)
                .FlatMapAsync(value => Value<int, string>(task2))
        ).IsValueAsync(2);
        (
            await Value<int, string>(task1)
                .FlatMapAsync(_ => Error<int, string>("err!"))
        ).IsError("err!");

        (
            await Error<int, string>("err!")
                .FlatMapAsync(value => Value<int, string>(task2))
        ).IsError("err!");
        (
            await Error<int, string>("err!")
                .FlatMapAsync(_ => Error<int, string>("err2!"))
        ).IsError("err!");
    }

    [Test(Description = "Tests to AsyncOption")]
    public async Task TestsToAsyncOption()
    {
        var task1 = Task.FromResult(1);

        (
            await Value<int, string>(task1).AsyncValue()
        ).IsSome(1);

        (
            await Value<int, string>(task1).Error()
        ).IsNone();

        (
            await Error<int, string>("err!").AsyncValue()
        ).IsNone();

        (
            await Error<int, string>("err!").Error()
        ).IsSome("err!");
    }

    [Test(Description = "Tests OnAsync.")]
    public async Task TestsOnAsync()
    {
        var onResults = new List<string>();

        var strTask = Task.FromResult("value");

        await Value<string, int>(strTask)
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

        var strTask = Task.FromResult("value");

        await Value<string, int>(strTask)
            .OnValueAsync(_ => onValueResults.Add("OnValue 1 invokes onValue"));
        await Error<string, int>(1)
            .OnValueAsync(_ => onValueResults.Add("OnValue 2 invokes onValue"));

        onValueResults.SequenceEqual(["OnValue 1 invokes onValue"]).ItIs(true);
    }

    [Test(Description = "Tests OnError.")]
    public async Task TestsOnError()
    {
        var onErrorResults = new List<string>();

        var strTask = Task.FromResult("value");

        await Value<string, int>(strTask)
            .OnErrorAsync(_ => onErrorResults.Add("OnError 1 invokes onError"));
        await Error<string, int>(1)
            .OnErrorAsync(_ => onErrorResults.Add("OnError 2 invokes onError"));

        onErrorResults.SequenceEqual(["OnError 2 invokes onError"]).ItIs(true);
    }
}