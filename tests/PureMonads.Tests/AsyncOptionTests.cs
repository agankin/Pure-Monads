using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

[TestFixture(TestName = "AsyncOption Tests")]
public class AsyncOptionTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        task1.Some().Equals(task1.Some()).ItIs(true);
        task1.Some().Equals(task2.Some()).ItIs(false);

        None<int>().Equals(None<int>()).ItIs(true);
        None<int>().Equals(task1.Some()).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        (task1.Some() == task1.Some()).ItIs(true);
        (task1.Some() == task2.Some()).ItIs(false);

        (None<int>() == None<int>()).ItIs(true);
        (None<int>() == task1.Some()).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        (task1.Some() != task1.Some()).ItIs(false);
        (task1.Some() != task2.Some()).ItIs(true);

        (None<int>() != None<int>()).ItIs(false);
        (None<int>() != task1.Some()).ItIs(true);
    }

    [Test(Description = "Tests Await")]
    public async Task TestsAwaitAsync()
    {
        var task1 = Task.FromResult(1);

        var asyncOption1 = AsyncOption.Some(task1);
        (await asyncOption1).IsSome(1);

        var asyncOptionNone = None<int>();
        (await asyncOptionNone).IsNone();
    }
    
    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        task1.Some()
            .Match(task => task, () => task2).ItIs(task1);
        None<int>()
            .Match(task => task, () => task2).ItIs(task2);
    }

    [Test(Description = "Tests Map")]
    public async Task TestsMapAsync()
    {
        var task = Task.FromResult(1);

        var asyncOption1 = task.Some()
            .Map(value => value + 1);
        (await asyncOption1).IsSome(2);

        var asyncOption2 = task.Some()
            .Map(value => Task.FromResult(value + 2));
        (await asyncOption2).IsSome(3);

        var asyncOptionNone1 = None<int>()
            .Map(value => value + 1);
        (await asyncOptionNone1).IsNone();

        var asyncOptionNone2 = None<int>()
            .Map(value => Task.FromResult(value + 2));
        (await asyncOptionNone1).IsNone();
    }

    [Test(Description = "Tests FlatMapAsync")]
    public async Task TestsFlatMapAsync()
    {
        var task = Task.FromResult(1);

        var asyncOptionSome1 = await task.Some()
            .FlatMapAsync<int, string>(value => Task.FromResult($"Some {value}"));
        (await asyncOptionSome1).IsSome("Some 1");

        var asyncOptionNone1 = await task.Some()
            .FlatMapAsync(value => None<string>());
        (await asyncOptionNone1).IsNone();

        var asyncOptionNone2 = await None<int>()
            .FlatMapAsync<int, string>(value => Task.FromResult($"Some {value}"));
        (await asyncOptionNone2).IsNone();
        
        var asyncOptionNone3 = await None<int>()
            .FlatMapAsync(value => None<string>());
        (await asyncOptionNone3).IsNone();
    }

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

    [Test(Description = "Tests ValueOrFailureAsync")]
    public async Task TestsValueOrFailureAsync()
    {
        var task1 = Task.FromResult(1);
        (await task1.Some().ValueOrFailureAsync()).ItIs(1);

        var exception = Assert.ThrowsAsync<Exception>(() =>
        {
            return None<int>().ValueOrFailureAsync("It is None.");
        });

        exception.NotNull().Message.ItIs("It is None.");
    }
}