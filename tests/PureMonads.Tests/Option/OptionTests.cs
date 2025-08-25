using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

[TestFixture(TestName = "Option Tests")]
public partial class OptionTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        1.Some().Equals(1.Some()).ItIs(true);
        1.Some().Equals(2.Some()).ItIs(false);

        None<int>().Equals(None<int>()).ItIs(true);
        None<int>().Equals(1.Some()).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        (1.Some() == 1.Some()).ItIs(true);
        (1.Some() == 2.Some()).ItIs(false);

        (None<int>() == None<int>()).ItIs(true);
        (None<int>() == 1.Some()).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        (1.Some() != 1.Some()).ItIs(false);
        (1.Some() != 2.Some()).ItIs(true);

        (None<int>() != None<int>()).ItIs(false);
        (None<int>() != 1.Some()).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        "value".Some()
            .Match(value => value + " 2", () => "none").ItIs("value 2");
        None<string>()
            .Match(value => value + " 2", () => "none").ItIs("none");
    }
}