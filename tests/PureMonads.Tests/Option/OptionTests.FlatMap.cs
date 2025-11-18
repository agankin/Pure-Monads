using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

public partial class OptionTests
{
    [Test(Description = "Tests FlatMap")]
    public void TestsFlatMap()
    {
        "value".Some()
            .FlatMap(value => (value + " 2").Some()).IsSome("value 2");
        "value".Some()
            .FlatMap(value => None<string>()).IsNone();

        None<string>()
            .FlatMap(value => (value + " 2").Some()).IsNone();
        None<string>()
            .FlatMap(value => None<string>()).IsNone();
    }

    [Test(Description = "Tests FlatMap (to AsyncOption)")]
    public async Task TestsFlatMapToAsyncOption()
    {
        AsyncOption<string> AsyncSome(string value) => AsyncOption.Some(value.AsTask());
        AsyncOption<string> AsyncNone() => AsyncOption.None<string>();

        await "value".Some()
            .FlatMap(value => AsyncSome(value + " 2")).IsSomeAsync("value 2");
        "value".Some()
            .FlatMap(value => AsyncNone()).IsNone();

        None<string>()
            .FlatMap(value => AsyncSome(value + " 2")).IsNone();
        None<string>()
            .FlatMap(value => AsyncNone()).IsNone();
    }

    [Test(Description = "Tests FlatMapAsync")]
    public async Task TestsFlatMapAsync()
    {
        Task<Option<string>> AsyncSome(string value) => Option.Some(value).AsTask();
        Task<Option<string>> AsyncNone() => None<string>().AsTask();

        (
            await "value".Some()
                .FlatMapAsync(value => AsyncSome(value + " 2"))
        ).IsSome("value 2");
        (
            await "value".Some()
                .FlatMapAsync(value => AsyncNone())
        ).IsNone();

        (
            await None<string>()
                .FlatMapAsync(value => AsyncSome(value + " 2"))
        ).IsNone();
        (
            await None<string>()
                .FlatMapAsync(value => AsyncNone())
        ).IsNone();
    }
}