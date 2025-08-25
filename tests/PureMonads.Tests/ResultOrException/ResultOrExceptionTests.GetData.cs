using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultOrExceptionTests
{
    [Test(Description = "Tests to Option")]
    public void TestsToOption()
    {
        Value(1)
            .Value().IsSome(1);
        Value(1)
            .Error().IsNone();

        Error<int>(new TestException("err!"))
            .Value().IsNone();
        Error<int>(new TestException("err!"))
            .Error().IsSome(new TestException("err!"));
    }
}