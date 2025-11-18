using System.Threading.Tasks;
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

    [Test(Description = "Tests MapAsync")]
    public async Task TestsMapAsync()
    {
        await "value".Some()
            .MapAsync(value => (value + " 2").AsTask()).IsSomeAsync("value 2");
        None<string>()
            .MapAsync(value => (value + " 2").AsTask()).IsNone();
    }
}