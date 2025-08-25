using NUnit.Framework;

namespace PureMonads.Tests;

using static Either;

public partial class EitherTests
{
    [Test(Description = "Tests to Option")]
    public void TestsToOption()
    {
        Left<int, string>(1)
            .Left().IsSome(1);
        Left<int, string>(1)
            .Right().IsNone();

        Right<int, string>("2")
            .Left().IsNone();
        Right<int, string>("2")
            .Right().IsSome("2");
    }
}