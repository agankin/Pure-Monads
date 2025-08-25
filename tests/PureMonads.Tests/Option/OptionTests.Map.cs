using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

public partial class OptionTests
{
    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        "value".Some()
            .Map(value => value + " 2").IsSome("value 2");
        None<string>()
            .Map(value => value + " 2").IsNone();
    }
}