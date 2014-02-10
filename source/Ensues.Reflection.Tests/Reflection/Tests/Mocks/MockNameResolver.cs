using System;
using System.Linq.Expressions;

namespace Ensues.Reflection.Tests.Mocks {

    internal class MockNameResolver : MemberNameResolver {

        public Func<LambdaExpression, string> MockResolve { get; set; }

        public override string Resolve(LambdaExpression expression) {
            var local = MockResolve;
            if (local == null) throw new InvalidOperationException();
            return local(expression);
        }
    }
}
