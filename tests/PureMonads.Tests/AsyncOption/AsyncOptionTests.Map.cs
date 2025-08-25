using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

public partial class AsyncOptionTests
{
    [Test(Description = "Tests Map")]
    public async Task TestsMapAsync()
    {
        var task = Task.FromResult(1);

        var asyncOption1 = task.Some()
            .Map(value => value + 1);
        (await asyncOption1).IsSome(2);

        var asyncOption2 = task.Some()
            .Map(value => Task.FromResult(value + 2));
        (await asyncOption2).IsSome(3);

        var asyncOptionNone1 = None<int>()
            .Map(value => value + 1);
        (await asyncOptionNone1).IsNone();

        var asyncOptionNone2 = None<int>()
            .Map(value => Task.FromResult(value + 2));
        (await asyncOptionNone1).IsNone();
    }
}