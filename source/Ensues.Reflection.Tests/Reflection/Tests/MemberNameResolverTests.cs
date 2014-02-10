using System;
using System.Linq.Expressions;

using NUnit.Framework;

using Ensues.Reflection.Tests.Mocks;
namespace Ensues.Reflection.Tests {
    
    [TestFixture]
    public class MemberNameResolverTests {

        [Test]
        public void Resolve_ResolvesFieldName() {
            Expression<Func<MockObject, object>> boolExpression = m => m.BoolField;
            Expression<Func<MockObject, object>> stringExpression = m => m.StringField;

            Assert.AreEqual("BoolField", new MemberNameResolver().Resolve(boolExpression));
            Assert.AreEqual("StringField", new MemberNameResolver().Resolve(stringExpression));
        }

        [Test]
        public void Resolve_ResolvesPropertyName() {
            Expression<Func<MockObject, object>> boolExpression = m => m.BoolProperty;
            Expression<Func<MockObject, object>> stringExpression = m => m.StringProperty;

            Assert.AreEqual("BoolProperty", new MemberNameResolver().Resolve(boolExpression));
            Assert.AreEqual("StringProperty", new MemberNameResolver().Resolve(stringExpression));
        }

        [Test]
        public void Resolve_ResolvesMethodName() {
            Expression<Func<MockObject, object>> intExpression = m => m.IntMethod();
            Expression<Func<MockObject, object>> doubleExpression = m => m.DoubleMethod();
            Expression<Func<MockObject, object>> stringExpression = m => m.String1ParamMethod(null);

            Assert.AreEqual("IntMethod", new MemberNameResolver().Resolve(intExpression));
            Assert.AreEqual("DoubleMethod", new MemberNameResolver().Resolve(doubleExpression));
            Assert.AreEqual("String1ParamMethod", new MemberNameResolver().Resolve(stringExpression));
        }

        [Test]
        public void Resolve_ResolvesVoidMethodName() {
            Expression<Action<MockObject>> voidExpression = m => m.VoidMethod();
            Expression<Action<MockObject>> void1ParamExpression = m => m.Void1ParamMethod(default(int));

            Assert.AreEqual("VoidMethod", new MemberNameResolver().Resolve(voidExpression));
            Assert.AreEqual("Void1ParamMethod", new MemberNameResolver().Resolve(void1ParamExpression));
        }

        [Test]
        public void Resolve_ThrowsInvalidTargetMemberExceptionOnInvalidMember_1() {
            Expression<Func<MockObject, object>> expression = m => 1 + 2;

            try {
                new MemberNameResolver().Resolve(expression);
            }
            catch (Exception ex) {
                Assert.AreEqual(typeof(InvalidTargetMemberException), ex.GetType());
                Assert.AreSame(expression, ((InvalidTargetMemberException)ex).Expression);
            }
        }

        [Test]
        public void Resolve_ThrowsInvalidTargetMemberExceptionOnInvalidMember_2() {
            Expression<Func<MockObject, object>> expression = m => m.String1ParamMethod(123) + m.BoolProperty;

            try {
                new MemberNameResolver().Resolve(expression);
            }
            catch (Exception ex) {
                Assert.AreEqual(typeof(InvalidTargetMemberException), ex.GetType());
                Assert.AreSame(expression, ((InvalidTargetMemberException)ex).Expression);
            }
        }
    }
}
