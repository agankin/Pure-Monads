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
        1.AsTask().Some().Equals(1.AsTask().Some()).ItIs(true);
        1.AsTask().Some().Equals(2.AsTask().Some()).ItIs(false);

        None<int>().Equals(None<int>()).ItIs(true);
        None<int>().Equals(1.AsTask().Some()).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        (1.AsTask().Some() == 1.AsTask().Some()).ItIs(true);
        (1.AsTask().Some() == 2.AsTask().Some()).ItIs(false);

        (None<int>() == None<int>()).ItIs(true);
        (None<int>() == 1.AsTask().Some()).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        (1.AsTask().Some() != 1.AsTask().Some()).ItIs(false);
        (1.AsTask().Some() != 2.AsTask().Some()).ItIs(true);

        (None<int>() != None<int>()).ItIs(false);
        (None<int>() != 1.AsTask().Some()).ItIs(true);
    }

    [Test(Description = "Tests Await")]
    public async Task TestsAwaitAsync()
    {
        var asyncOption1 = AsyncOption.Some(1.AsTask());
        (await asyncOption1).IsSome(1);

        var asyncOptionNone = None<int>();
        (await asyncOptionNone).IsNone();
    }
    
    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        1.AsTask().Some()
            .Match(task => task, () => 2.AsTask()).ItIs(1.AsTask());
        None<int>()
            .Match(task => task, () => 2.AsTask()).ItIs(2.AsTask());
    }
}