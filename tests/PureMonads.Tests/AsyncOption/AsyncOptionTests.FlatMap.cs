using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncOption;

public partial class AsyncOptionTests
{
    [Test(Description = "Tests FlatMapAsync")]
    public async Task TestsFlatMapAsync()
    {
        var task = Task.FromResult(1);

        var asyncOptionSome1 = await task.Some()
            .FlatMapAsync<int, string>(value => Task.FromResult($"Some {value}"));
        (await asyncOptionSome1).IsSome("Some 1");

        var asyncOptionNone1 = await task.Some()
            .FlatMapAsync(value => None<string>());
        (await asyncOptionNone1).IsNone();

        var asyncOptionNone2 = await None<int>()
            .FlatMapAsync<int, string>(value => Task.FromResult($"Some {value}"));
        (await asyncOptionNone2).IsNone();
        
        var asyncOptionNone3 = await None<int>()
            .FlatMapAsync(value => None<string>());
        (await asyncOptionNone3).IsNone();
    }
}