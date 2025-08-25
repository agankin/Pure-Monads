using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultOrExceptionTests
{
    [Test(Description = "Tests Map")]
    public async Task TestsMapAsync()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));

        await AsyncValue(1)
            .Map(value => $"value: {value}").IsValueAsync("value: 1");
        Error<int>(new TestException("err!"))
            .Map(value => $"value: {value}").IsError(new TestException("err!"));
    }
}