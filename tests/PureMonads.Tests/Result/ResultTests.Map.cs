using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultTests
{
    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        Value<int, string>(1)
            .Map(value => $"value: {value}").IsValue("value: 1");
        Error<int, string>("err!")
            .Map(value => $"value: {value}").IsError("err!");
    }
}