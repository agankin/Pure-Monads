using NUnit.Framework;

namespace PureMonads.Tests;

using static Either;

[TestFixture(TestName = "Either Tests")]
public partial class EitherTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        var Left = Left<int, string>;
        var Right = Right<int, string>;

        Left(1).Equals(Left(1)).ItIs(true);
        Left(1).Equals(Left(2)).ItIs(false);

        Right("1").Equals(Right("1")).ItIs(true);
        Right("1").Equals(Right("2")).ItIs(false);

        Left(1).Equals(Right("1")).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var Left = Left<int, string>;
        var Right = Right<int, string>;

        (Left(1) == Left(1)).ItIs(true);
        (Left(1) == Left(2)).ItIs(false);

        (Right("1") == Right("1")).ItIs(true);
        (Right("1") == Right("2")).ItIs(false);

        (Left(1) == Right("1")).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var Left = Left<int, string>;
        var Right = Right<int, string>;

        (Left(1) != Left(1)).ItIs(false);
        (Left(1) != Left(2)).ItIs(true);

        (Right("1") != Right("1")).ItIs(false);
        (Right("1") != Right("2")).ItIs(true);

        (Left(1) != Right("1")).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        Left<int, string>(1)
            .Match(left => $"Left: {left}", right => $"Right: {right}").ItIs("Left: 1");
        Right<int, string>("2")
            .Match(left => $"Left: {left}", right => $"Right: {right}").ItIs("Right: 2");
    }
}