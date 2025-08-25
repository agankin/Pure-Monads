using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
    [Test(Description = "Tests FlatMap")]
    public void TestsFlatMap()
    {
        Value(1)
            .FlatMap(value => Value(value + 1)).IsValue(2);
        Value(1)
            .FlatMap(_ => Error<int>(new TestException("err!"))).IsError(new TestException("err!"));

        Error<int>(new TestException("err!"))
            .FlatMap(value => Value(value + 1)).IsError(new TestException("err!"));
        Error<int>(new TestException("err!"))
            .FlatMap(_ => Error<int>(new TestException("err2!"))).IsError(new TestException("err!"));
    }
}