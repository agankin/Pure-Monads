using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        Value(1)
            .Map(value => $"value: {value}").IsValue("value: 1");
        Error<int>(new TestException("err!"))
            .Map(value => $"value: {value}").IsError(new TestException("err!"));
    }

    [Test(Description = "Tests MapAsync")]
    public async Task TestsMapAsync()
    {
        Task<string> AsyncValue(string value) => Task.FromResult(value);

        (
            await Value(1)
                .MapAsync(value => AsyncValue($"value: {value}"))
        ).IsValue("value: 1");
        (
            await Error<int>(new TestException("err!"))
                .MapAsync(value => AsyncValue($"value: {value}"))
        ).IsError(new TestException("err!"));
    }
}