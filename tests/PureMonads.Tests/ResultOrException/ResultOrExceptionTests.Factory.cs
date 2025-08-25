using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
    [Test(Description = "Tests From")]
    public void TestsFrom()
    {
        From(() => 1).IsValue(1);
        From(new Func<int>(() => throw new TestException("err!"))).IsError(new TestException("err!"));
    }

    [Test(Description = "Tests FromAsync")]
    public async Task TestsFromAsync()
    {
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