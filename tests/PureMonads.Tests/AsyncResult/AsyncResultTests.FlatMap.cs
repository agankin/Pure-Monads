using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultTests
{
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
}