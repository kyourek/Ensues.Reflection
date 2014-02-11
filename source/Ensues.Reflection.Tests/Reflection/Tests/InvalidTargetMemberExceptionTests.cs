using System;
using System.Linq.Expressions;

using NUnit.Framework;

using Ensues.Reflection.Tests.Mocks;
namespace Ensues.Reflection.Tests {

    [TestFixture]
    public class InvalidTargetMemberExceptionTests {

        [Test]
        public void Constructor_SetsExpression() {
            Expression<Func<MockObject, string>> expression = o => o.StringField + "something else";
            var exception = new InvalidTargetMemberException(expression);
            Assert.AreSame(expression, exception.Expression);
        }
    }
}
