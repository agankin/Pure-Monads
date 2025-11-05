using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
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

    [Test(Description = "Tests FlatMap (to AsyncResult)")]
    public async Task TestsFlatMapToAsyncResult()
    {
        AsyncResult<int> AsyncValue(int value) => AsyncResult<int>.Value(Task.FromResult(value));
        AsyncResult<int> AsyncError(Exception error) => AsyncResult<int>.Error(error);

        await Value(1)
            .FlatMap(value => AsyncValue(value + 1)).IsValueAsync(2);
        Value(1)
            .FlatMap(_ => AsyncError(new TestException("err!"))).IsError(new TestException("err!"));

        Error<int>(new TestException("err!"))
            .FlatMap(value => AsyncValue(value + 1)).IsError(new TestException("err!"));
        Error<int>(new TestException("err!"))
            .FlatMap(_ => AsyncError(new TestException("err2!"))).IsError(new TestException("err!"));
    }

    [Test(Description = "Tests FlatMapAsync")]
    public async Task TestsFlatMapAsync()
    {
        Task<Result<int>> AsyncValue(int value) => Task.FromResult(Result<int>.Value(value));
        Task<Result<int>> AsyncError(Exception error) => Task.FromResult(Result<int>.Error(error));

        (
            await Value(1)
                .FlatMapAsync(value => AsyncValue(value + 1))
        ).IsValue(2);
        (
            await Value(1)
                .FlatMapAsync(_ => AsyncError(new TestException("err!")))
        ).IsError(new TestException("err!"));

        (
            await Error<int>(new TestException("err!"))
                .FlatMapAsync(value => AsyncValue(value + 1))
        ).IsError(new TestException("err!"));
        (
            await Error<int>(new TestException("err!"))
                .FlatMapAsync(_ => AsyncError(new TestException("err2!")))
        ).IsError(new TestException("err!"));
    }
}