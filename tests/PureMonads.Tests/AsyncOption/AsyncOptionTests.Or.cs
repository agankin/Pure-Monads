using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

public partial class AsyncOptionTests
{
    [Test(Description = "Tests Or")]
    public void TestsOr()
    {
        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        task1.Some()
            .Or(task2.Some()).ItIs(task1);
        None<int>()
            .Or(task2).ItIs(task2);

        task1.Some()
            .Or(() => task2).ItIs(task1);
        None<int>()
            .Or(() => task2).ItIs(task2);
    }

    [Test(Description = "Tests OrAsync")]
    public async Task TestsOrAsync()
    {
        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        (await task1.Some()
            .OrAsync(2)).ItIs(1);
        (await None<int>()
            .OrAsync(2)).ItIs(2);

        (await task1.Some()
            .OrAsync(() => 2)).ItIs(1);
        (await None<int>()
            .OrAsync(() => 2)).ItIs(2);
    }

    [Test(Description = "Tests OrAsync with async")]
    public async Task TestsOrAsyncWithAsync()
    {
        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        (await task1.Some()
            .OrAsync(task2)).ItIs(1);
        (await None<int>()
            .OrAsync(task2)).ItIs(2);

        (await task1.Some()
            .OrAsync(() => task2)).ItIs(1);
        (await None<int>()
            .OrAsync(() => task2)).ItIs(2);
    }
}