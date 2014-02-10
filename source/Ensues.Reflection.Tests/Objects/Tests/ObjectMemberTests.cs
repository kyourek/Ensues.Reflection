using System;
using System.Linq.Expressions;

using NUnit.Framework;

using Ensues.Reflection.Tests.Mocks;
namespace Ensues.Objects.Tests {
    
    [TestFixture]
    public class ObjectMemberTests {

        [Test]
        public void GetName_ReturnsMemberName() {
            var obj = new MockObject();
            Assert.AreEqual("BoolField", obj.GetMemberName(o => o.BoolField));
            Assert.AreEqual("StringField", obj.GetMemberName(o => o.StringField));
            Assert.AreEqual("BoolProperty", obj.GetMemberName(o => o.BoolProperty));
            Assert.AreEqual("StringProperty", obj.GetMemberName(o => o.StringProperty));
            Assert.AreEqual("VoidMethod", obj.GetMemberName(o => o.VoidMethod()));
            Assert.AreEqual("String1ParamMethod", obj.GetMemberName(o => o.String1ParamMethod(default(object))));
        }
    }
}
