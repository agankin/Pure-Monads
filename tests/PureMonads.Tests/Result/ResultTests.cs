using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

[TestFixture(TestName = "Result Tests")]
public partial class ResultTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        Value(1).Equals(Value(1)).ItIs(true);
        Value(1).Equals(Value(2)).ItIs(false);

        Error("err1").Equals(Error("err1")).ItIs(true);
        Error("err1").Equals(Error("err2")).ItIs(false);

        Value(1).Equals(Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        (Value(1) == Value(1)).ItIs(true);
        (Value(1) == Value(2)).ItIs(false);

        (Error("err1") == Error("err1")).ItIs(true);
        (Error("err1") == Error("err2")).ItIs(false);

        (Value(1) == Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        (Value(1) != Value(1)).ItIs(false);
        (Value(1) != Value(2)).ItIs(true);

        (Error("err1") != Error("err1")).ItIs(false);
        (Error("err1") != Error("err2")).ItIs(true);

        (Value(1) != Error("err1")).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        Value<int, string>(1)
            .Match(value => $"value: {value}", error => $"error: {error}").ItIs("value: 1");
        Error<int, string>("err!")
            .Match(value => $"value: {value}", error => $"error: {error}").ItIs("error: err!");
    }
}