using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
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

    [Test(Description = "Tests AsResultAsync")]
    public async Task TestsAsResultAsync()
    {
        var result1 = await Task.FromResult(1).AsResultAsync();
        result1.IsValue(1);

        var testException = new TestException("Exception!");
        var resultEx = await Task.FromException<int>(testException).AsResultAsync();
        resultEx.IsError(testException);
    }
}