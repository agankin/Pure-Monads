using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

public partial class AsyncResultOrExceptionTests
{
    [Test(Description = "Tests to Option")]
    public async Task TestsToOption()
    {
        AsyncResult<int> AsyncValue(int val) => Value(val.AsTask());

        (await AsyncValue(1).AsyncValue())
            .IsSome(1);
        (await AsyncValue(1).Error())
            .IsNone();

        (await Error<int>(new TestException("err!")).AsyncValue())
            .IsNone();
        (await Error<int>(new TestException("err!")).Error())
            .IsSome(new TestException("err!"));
    }
}