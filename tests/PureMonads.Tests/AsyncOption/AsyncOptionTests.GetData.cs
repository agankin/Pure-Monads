using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

public partial class AsyncOptionTests
{
    [Test(Description = "Tests ValueOrFailureAsync")]
    public async Task TestsValueOrFailureAsync()
    {
        var task1 = Task.FromResult(1);
        (await task1.Some().ValueOrFailureAsync()).ItIs(1);

        var exception = Assert.ThrowsAsync<Exception>(() =>
        {
            return None<int>().ValueOrFailureAsync("It is None.");
        });

        exception.NotNull().Message.ItIs("It is None.");
    }

    [Test(Description = "Tests AsResultAsync")]
    public async Task TestsAsResultAsync()
    {
        var task1 = Task.FromResult(1);
        var result1 = await task1.Some().AsResultAsync();
        result1.IsValue(Option.Some(1));

        var testException = new TestException("Exception!");
        var taskErr = Task.FromException<int>(testException);
        var errResult = await taskErr.Some().AsResultAsync();
        errResult.IsError(testException);
    }
}