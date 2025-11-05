using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultTests
{
    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        Value<int, string>(1)
            .Map(value => $"value: {value}").IsValue("value: 1");
        Error<int, string>("err!")
            .Map(value => $"value: {value}").IsError("err!");
    }

    [Test(Description = "Tests MapAsync")]
    public async Task TestsMapAsync()
    {
        Task<string> AsyncValue(string value) => Task.FromResult(value);

        (
            await Value<int, string>(1)
                .MapAsync(value => AsyncValue($"value: {value}"))
        ).IsValue("value: 1");
        (
            await Error<int, string>("err!")
                .MapAsync(value => AsyncValue($"value: {value}"))
        ).IsError("err!");
    }
}