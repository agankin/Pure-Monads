using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

public partial class AsyncOptionTests
{
    [Test(Description = "Tests Map")]
    public async Task TestsMapAsync()
    {
        var asyncOption1 = 1.AsTask().Some()
            .Map(value => value + 1);
        (await asyncOption1).IsSome(2);

        var asyncOption2 = 1.AsTask().Some()
            .Map(value => (value + 2).AsTask());
        (await asyncOption2).IsSome(3);

        var asyncOptionNone1 = None<int>()
            .Map(value => value + 1);
        (await asyncOptionNone1).IsNone();

        var asyncOptionNone2 = None<int>()
            .Map(value => (value + 2).AsTask());
        (await asyncOptionNone1).IsNone();
    }
}