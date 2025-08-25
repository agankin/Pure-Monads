using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultOrExceptionTests
{
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
}