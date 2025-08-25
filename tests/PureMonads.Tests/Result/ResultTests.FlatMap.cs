using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultTests
{
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
}