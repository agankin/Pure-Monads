using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

[TestFixture(TestName = "AsyncResult or Exception Tests")]
public partial class AsyncResultOrExceptionTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        AsyncValue(1).Equals(AsyncValue(1)).ItIs(true);
        AsyncValue(1).Equals(AsyncValue(2)).ItIs(false);

        Error(err1).Equals(Error(err1)).ItIs(true);
        Error(err1).Equals(Error(err2)).ItIs(false);

        AsyncValue(1).Equals(Error(err1)).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        (AsyncValue(1) == AsyncValue(1)).ItIs(true);
        (AsyncValue(1) == AsyncValue(2)).ItIs(false);

        (Error(err1) == Error(err1)).ItIs(true);
        (Error(err1) == Error(err2)).ItIs(false);

        (AsyncValue(1) == Error(err1)).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        (AsyncValue(1) != AsyncValue(1)).ItIs(false);
        (AsyncValue(1) != AsyncValue(2)).ItIs(true);

        (Error(err1) != Error(err1)).ItIs(false);
        (Error(err1) != Error(err2)).ItIs(true);

        (AsyncValue(1) != Error(err1)).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public async Task TestsMatch()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));

        (
            await AsyncValue(1)
                .Match(
                    async value => $"value: {await value}",
                    error => Task.FromResult($"error: {error.Message}"))
        ).ItIs("value: 1");

        Error<int>(new TestException("err!"))
            .Match(value => $"value: {value}", error => $"error: {error.Message}").ItIs("error: err!");
    }

    [Test(Description = "Tests Map")]
    public async Task TestsMapAsync()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));

        await AsyncValue(1)
            .Map(value => $"value: {value}").IsValueAsync("value: 1");
        Error<int>(new TestException("err!"))
            .Map(value => $"value: {value}").IsError(new TestException("err!"));
    }

    [Test(Description = "Tests FlatMap")]
    public async Task TestsFlatMapAsync()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));

        await (
            await AsyncValue(1)
                .FlatMapAsync(value => AsyncValue(value + 1))
        ).IsValueAsync(2);

        (
            await AsyncValue(1)
                .FlatMapAsync(_ => Error<int>(new TestException("err!")))
        ).IsError(new TestException("err!"));

        (
            await Error<int>(new TestException("err!"))
                .FlatMapAsync(value => AsyncValue(value + 1))
        ).IsError(new TestException("err!"));

        (
            await Error<int>(new TestException("err!"))
                .FlatMapAsync(_ => Error<int>(new TestException("err2!")))
        ).IsError(new TestException("err!"));
    }

    [Test(Description = "Tests to Option")]
    public async Task TestsToOption()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));

        (await AsyncValue(1).AsyncValue())
            .IsSome(1);
        (await AsyncValue(1).Error())
            .IsNone();

        (await Error<int>(new TestException("err!")).AsyncValue())
            .IsNone();
        (await Error<int>(new TestException("err!")).Error())
            .IsSome(new TestException("err!"));
    }

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