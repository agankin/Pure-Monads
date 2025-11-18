using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultTests
{
    [Test(Description = "Tests FlatMapAsync")]
    public async Task TestsFlatMapAsync()
    {
        await (
            await Value<int, string>(1.AsTask())
                .FlatMapAsync(value => Value<int, string>(2.AsTask()))
        ).IsValueAsync(2);
        (
            await Value<int, string>(1.AsTask())
                .FlatMapAsync(_ => Error<int, string>("err!"))
        ).IsError("err!");

        (
            await Error<int, string>("err!")
                .FlatMapAsync(value => Value<int, string>(2.AsTask()))
        ).IsError("err!");
        (
            await Error<int, string>("err!")
                .FlatMapAsync(_ => Error<int, string>("err2!"))
        ).IsError("err!");
    }
}