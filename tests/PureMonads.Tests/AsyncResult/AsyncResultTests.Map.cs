using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultTests
{
    [Test(Description = "Tests MapAsync")]
    public async Task TestsMapAsync()
    {
        var task1 = Task.FromResult(1);

        await Value<int, string>(task1)
            .Map(value => $"value: {value}").IsValueAsync("value: 1");
        Error<int, string>("err!")
            .Map(value => $"value: {value}").IsError("err!");
    }
}