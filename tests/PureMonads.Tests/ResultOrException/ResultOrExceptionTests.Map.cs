using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        Value(1)
            .Map(value => $"value: {value}").IsValue("value: 1");
        Error<int>(new TestException("err!"))
            .Map(value => $"value: {value}").IsError(new TestException("err!"));
    }
}