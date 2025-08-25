using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

[TestFixture(TestName = "AsyncOption Tests")]
public partial class AsyncOptionTests
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
}