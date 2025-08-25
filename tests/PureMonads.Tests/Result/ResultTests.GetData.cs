using NUnit.Framework;

namespace PureMonads.Tests;

using static Result;

public partial class ResultTests
{
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