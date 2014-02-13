using System;

using NUnit.Framework;

using Ensues.Reflection.Tests.Mocks;
namespace Ensues.Objects.Tests {
    
    [TestFixture]
    public class ObjectReflectionTests {

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

        [Test]
        public void GetField_ReturnsTypeFieldInfo() {
            var obj = new MockObject();
            var typ = typeof(MockObject);
            Assert.AreEqual(typ.GetField("BoolField"), obj.GetField(o => o.BoolField));
            Assert.AreEqual(typ.GetField("BoolProperty"), obj.GetField(o => o.BoolProperty));
        }

        [Test]
        public void GetProperty_ReturnsTypePropertyInfo() {
            var obj = new MockObject();
            var typ = typeof(MockObject);
            Assert.AreEqual(typ.GetProperty("StringProperty"), obj.GetProperty(o => o.StringProperty));
            Assert.AreEqual(typ.GetProperty("StringField"), obj.GetProperty(o => o.StringField));
        }

        [Test]
        public void GetMethod_ReturnsTypeMethodInfo() {
            var obj = new MockObject();
            var typ = typeof(MockObject);
            Assert.AreEqual(typ.GetMethod("VoidMethod"), obj.GetMethod(o => o.VoidMethod()));
            Assert.AreEqual(typ.GetMethod("Void1ParamMethod"), obj.GetMethod(o => o.Void1ParamMethod(12)));
            Assert.AreEqual(typ.GetMethod("String1ParamMethod"), obj.GetMethod(o => o.String1ParamMethod("")));
        }
    }
}
