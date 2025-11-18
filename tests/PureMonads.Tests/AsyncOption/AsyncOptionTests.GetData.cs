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
        (await 1.AsTask().Some().ValueOrFailureAsync()).ItIs(1);

        var exception = Assert.ThrowsAsync<Exception>(() =>
        {
            return None<int>().ValueOrFailureAsync("It is None.");
        });

        exception.NotNull().Message.ItIs("It is None.");
    }

    [Test(Description = "Tests AsResultAsync")]
    public async Task TestsAsResultAsync()
    {
        var result1 = await 1.AsTask().Some().AsResultAsync();
        result1.IsValue(Option.Some(1));

        var testException = new TestException("Exception!");
        var taskErr = testException.AsTaskException<int>();
        
        var errResult = await taskErr.Some().AsResultAsync();
        errResult.IsError(testException);
    }
}