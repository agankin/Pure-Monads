using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultTests
{
    [Test(Description = "Tests to AsyncOption")]
    public async Task TestsToAsyncOption()
    {
        (
            await Value<int, string>(1.AsTask()).AsyncValue()
        ).IsSome(1);

        (
            await Value<int, string>(1.AsTask()).Error()
        ).IsNone();

        (
            await Error<int, string>("err!").AsyncValue()
        ).IsNone();

        (
            await Error<int, string>("err!").Error()
        ).IsSome("err!");
    }
}