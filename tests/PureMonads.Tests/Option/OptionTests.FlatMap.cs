using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

public partial class OptionTests
{
    [Test(Description = "Tests FlatMap")]
    public void TestsFlatMap()
    {
        "value".Some()
            .FlatMap<string, string>(value => value + " 2").IsSome("value 2");
        "value".Some()
            .FlatMap(value => None<string>()).IsNone();

        None<string>()
            .FlatMap<string, string>(value => value + " 2").IsNone();
        None<string>()
            .FlatMap(value => None<string>()).IsNone();
    }
}