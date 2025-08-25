using System.Collections.Generic;
using NUnit.Framework;

namespace PureMonads.Tests;

public partial class OptionTests
{
    [Test(Description = "Tests Dictionary extensions.")]
    public void TestsDictionaryExtensions()
    {
        var dict = new Dictionary<int, string>
        {
            { 1, "One" },
            { 2, "Two" }
        };

        dict.GetOrNone(1).IsSome("One");
        dict.GetOrNone(3).IsNone();

        IDictionary<int, string> iDict = dict;

        iDict.GetOrNone(1).IsSome("One");
        iDict.GetOrNone(3).IsNone();

        IReadOnlyDictionary<int, string> roDict = dict;

        roDict.GetOrNone(1).IsSome("One");
        roDict.GetOrNone(3).IsNone();
    }
}