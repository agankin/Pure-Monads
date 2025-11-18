using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

public partial class AsyncOptionTests
{
    [Test(Description = "Tests Or")]
    public void TestsOr()
    {
        1.AsTask().Some()
            .Or(2.AsTask().Some()).ItIs(1.AsTask());
        None<int>()
            .Or(2.AsTask()).ItIs(2.AsTask());

        1.AsTask().Some()
            .Or(() => 2.AsTask()).ItIs(1.AsTask());
        None<int>()
            .Or(() => 2.AsTask()).ItIs(2.AsTask());
    }

    [Test(Description = "Tests OrAsync")]
    public async Task TestsOrAsync()
    {
        (await 1.AsTask().Some()
            .OrAsync(2)).ItIs(1);
        (await None<int>()
            .OrAsync(2)).ItIs(2);

        (await 1.AsTask().Some()
            .OrAsync(() => 2)).ItIs(1);
        (await None<int>()
            .OrAsync(() => 2)).ItIs(2);
    }

    [Test(Description = "Tests OrAsync with async")]
    public async Task TestsOrAsyncWithAsync()
    {
        (await 1.AsTask().Some()
            .OrAsync(2.AsTask())).ItIs(1);
        (await None<int>()
            .OrAsync(2.AsTask())).ItIs(2);

        (await 1.AsTask().Some()
            .OrAsync(() => 2.AsTask())).ItIs(1);
        (await None<int>()
            .OrAsync(() => 2.AsTask())).ItIs(2);
    }
}