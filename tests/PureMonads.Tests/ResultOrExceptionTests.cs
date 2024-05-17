using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

[TestFixture(TestName = "Result or Exception Tests")]
public partial class ResultOrExceptionTests
{
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
    public async Task TestsFromAsync()
    {
        From(() => 1).IsValue(1);
        From(new Func<int>(() => throw new TestException("err!"))).IsError(new TestException("err!"));

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
}