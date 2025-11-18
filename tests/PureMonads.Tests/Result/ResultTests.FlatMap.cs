using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultTests
{
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

    [Test(Description = "Tests FlatMap (to AsyncResult)")]
    public async Task TestsFlatMapToAsyncResult()
    {
        AsyncResult<int, string> AsyncValue(int value) => AsyncResult<int, string>.Value(value.AsTask());
        AsyncResult<int, string> AsyncError(string error) => AsyncResult<int, string>.Error(error);

        await Value<int, string>(1)
            .FlatMap(value => AsyncValue(value + 1)).IsValueAsync(2);
        Value<int, string>(1)
            .FlatMap(_ => AsyncError("err!")).IsError("err!");

        Error<int, string>("err!")
            .FlatMap(value => AsyncValue(value + 1)).IsError("err!");
        Error<int, string>("err!")
            .FlatMap(_ => AsyncError("err2!")).IsError("err!");
    }

    [Test(Description = "Tests FlatMapAsync")]
    public async Task TestsFlatMapAsync()
    {
        Task<Result<int, string>> AsyncValue(int value) => Result<int, string>.Value(value).AsTask();
        Task<Result<int, string>> AsyncError(string error) => Result<int, string>.Error(error).AsTask();

        (
            await Value<int, string>(1)
                .FlatMapAsync(value => AsyncValue(value + 1))
        ).IsValue(2);
        (
            await Value<int, string>(1)
                .FlatMapAsync(_ => AsyncError("err!"))
        ).IsError("err!");

        (
            await Error<int, string>("err!")
                .FlatMapAsync(value => AsyncValue(value + 1))
        ).IsError("err!");
        (
            await Error<int, string>("err!")
                .FlatMapAsync(_ => AsyncError("err2!"))
        ).IsError("err!");
    }
}