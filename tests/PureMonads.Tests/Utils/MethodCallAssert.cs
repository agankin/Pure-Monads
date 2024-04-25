using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PureMonads.Tests.Utils
{
    internal class MethodCallAssert<TArg, TResult>
    {
        private readonly Func<TArg, TResult> _method;
        private readonly List<TArg> _calls = new();

        public MethodCallAssert(Func<TArg, TResult> method)
        {
            _method = method;
        }

        public TResult Method(TArg arg)
        {
            var result = _method(arg);
            _calls.Add(arg);

            return result;
        }

        public MethodCallAssert<TArg, TResult> CalledTimes(int times)
        {
            Assert.That(_calls.Count, Is.EqualTo(times));
            return this;
        }

        public MethodCallAssert<TArg, TResult> NthCall(int nth, TArg expectedArg)
        {
            var arg = _calls[nth];
            Assert.That(arg, Is.EqualTo(expectedArg));

            return this;
        }
    }
}