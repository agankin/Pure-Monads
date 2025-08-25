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
}