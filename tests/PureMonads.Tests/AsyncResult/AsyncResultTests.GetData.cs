using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultTests
{
    [Test(Description = "Tests to AsyncOption")]
    public async Task TestsToAsyncOption()
    {
        var task1 = Task.FromResult(1);

        (
            await Value<int, string>(task1).AsyncValue()
        ).IsSome(1);

        (
            await Value<int, string>(task1).Error()
        ).IsNone();

        (
            await Error<int, string>("err!").AsyncValue()
        ).IsNone();

        (
            await Error<int, string>("err!").Error()
        ).IsSome("err!");
    }
}