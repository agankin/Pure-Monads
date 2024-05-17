using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

[TestFixture(TestName = "Result Tests")]
public partial class ResultTests
{
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