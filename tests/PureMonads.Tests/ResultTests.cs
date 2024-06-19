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

    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        Value<int, string>(1)
            .Map(value => $"value: {value}").IsValue("value: 1");
        Error<int, string>("err!")
            .Map(value => $"value: {value}").IsError("err!");
    }

    [Test(Description = "Tests FlatMap")]
    public void TestsFlatMap()
    {
        Value<int, string>(1)
            .FlatMap(value => Value<int, string>(value + 1)).IsValue(2);
        Value<int, string>(1)
            .FlatMap(_ => Error<int, string>("err!")).IsError("err!");

        Error<int, string>("err!")
            .FlatMap(value => Value<int, string>(value + 1)).IsError("err!");
        Error<int, string>("err!")
            .FlatMap(_ => Error<int, string>("err2!")).IsError("err!");
    }

    [Test(Description = "Tests to Option")]
    public void TestsToOption()
    {
        Value<int, string>(1)
            .Value().IsSome(1);
        Value<int, string>(1)
            .Error().IsNone();

        Error<int, string>("err!")
            .Value().IsNone();
        Error<int, string>("err!")
            .Error().IsSome("err!");
    }
}