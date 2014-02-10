using System;
using System.Linq.Expressions;

using NUnit.Framework;

using Ensues.Reflection.Tests.Mocks;
namespace Ensues.Reflection.Tests {

    [TestFixture]
    public class MemberNameTests {

        [Test]
        public void NameResolver_IsInstanceOfMemberNameResolver() {
            Assert.AreEqual(typeof(MemberNameResolver), new MemberName().NameResolver.GetType());
        }

        [Test]
        public void Instance_IsInstanceOfMemberName() {
            Assert.AreEqual(typeof(MemberName), MemberName.Instance.GetType());
        }

        [Test]
        public void Instance_NameResolver_IsInstanceOfMemberNameResolver() {
            Assert.AreEqual(typeof(MemberNameResolver), MemberName.Instance.NameResolver.GetType());
        }

        [Test]
        public void Get_ReturnsNameResolverResult() {

            Expression<Func<object, string>> expression = o => o.ToString();

            var entered = 0;
            var expected = Guid.NewGuid().ToString();
            var memberName = new MemberName {
                NameResolver = new MockNameResolver {
                    MockResolve = e => {
                        entered++;
                        Assert.AreSame(expression, e);
                        return expected;
                    }
                }
            };

            var actual = memberName.Get(expression);

            Assert.AreEqual(1, entered);
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Of_ReturnsMemberName() {
            Expression<Func<MockObject, object>> e1 = o => o.StringField;
            Expression<Func<MockObject, object>> e2 = o => o.BoolProperty;
            Expression<Action<MockObject>> e3 = o => o.Void1ParamMethod(default(int));

            Assert.AreEqual("StringField", MemberName.Of(e1));
            Assert.AreEqual("BoolProperty", MemberName.Of(e2));
            Assert.AreEqual("Void1ParamMethod", MemberName.Of(e3));
        }

        [Test]
        public void Generic_Of_ReturnsMemberName() {
            Assert.AreEqual("StringField", MemberName<MockObject>.Of(o => o.StringField));
            Assert.AreEqual("BoolProperty", MemberName<MockObject>.Of(o => o.BoolProperty));
            Assert.AreEqual("Void1ParamMethod", MemberName<MockObject>.Of(o => o.Void1ParamMethod(default(int))));
        }
    }
}
