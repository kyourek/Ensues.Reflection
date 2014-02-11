using System;
using System.Linq.Expressions;
using System.Reflection;

using NUnit.Framework;

using Ensues.Reflection.Tests.Mocks;
namespace Ensues.Reflection.Tests {

    public class MemberResolverTests {

        [Test]
        public void GetMemberName_ResolvesFieldName() {
            Expression<Func<MockObject, object>> boolExpression = m => m.BoolField;
            Expression<Func<MockObject, object>> stringExpression = m => m.StringField;

            Assert.AreEqual("BoolField", new MemberResolver().GetMemberName(boolExpression));
            Assert.AreEqual("StringField", new MemberResolver().GetMemberName(stringExpression));
        }

        [Test]
        public void GetMemberName_ResolvesPropertyName() {
            Expression<Func<MockObject, object>> boolExpression = m => m.BoolProperty;
            Expression<Func<MockObject, object>> stringExpression = m => m.StringProperty;

            Assert.AreEqual("BoolProperty", new MemberResolver().GetMemberName(boolExpression));
            Assert.AreEqual("StringProperty", new MemberResolver().GetMemberName(stringExpression));
        }

        [Test]
        public void GetMemberName_ResolvesMethodName() {
            Expression<Func<MockObject, object>> intExpression = m => m.IntMethod();
            Expression<Func<MockObject, object>> doubleExpression = m => m.DoubleMethod();
            Expression<Func<MockObject, object>> stringExpression = m => m.String1ParamMethod(null);

            Assert.AreEqual("IntMethod", new MemberResolver().GetMemberName(intExpression));
            Assert.AreEqual("DoubleMethod", new MemberResolver().GetMemberName(doubleExpression));
            Assert.AreEqual("String1ParamMethod", new MemberResolver().GetMemberName(stringExpression));
        }

        [Test]
        public void GetMemberName_ResolvesVoidMethodName() {
            Expression<Action<MockObject>> voidExpression = m => m.VoidMethod();
            Expression<Action<MockObject>> void1ParamExpression = m => m.Void1ParamMethod(default(int));

            Assert.AreEqual("VoidMethod", new MemberResolver().GetMemberName(voidExpression));
            Assert.AreEqual("Void1ParamMethod", new MemberResolver().GetMemberName(void1ParamExpression));
        }

        [Test]
        public void GetMemberName_ThrowsInvalidTargetMemberExceptionOnInvalidMember_1() {
            Expression<Func<MockObject, object>> expression = m => 1 + 2;

            try {
                new MemberResolver().GetMemberName(expression);
            }
            catch (Exception ex) {
                Assert.AreEqual(typeof(InvalidTargetMemberException), ex.GetType());
                Assert.AreSame(expression, ((InvalidTargetMemberException)ex).Expression);
            }
        }

        [Test]
        public void GetMemberName_ThrowsInvalidTargetMemberExceptionOnInvalidMember_2() {
            Expression<Func<MockObject, object>> expression = m => m.String1ParamMethod(123) + m.BoolProperty;

            try {
                new MemberResolver().GetMemberName(expression);
            }
            catch (Exception ex) {
                Assert.AreEqual(typeof(InvalidTargetMemberException), ex.GetType());
                Assert.AreSame(expression, ((InvalidTargetMemberException)ex).Expression);
            }
        }

        [Test]
        public void GetProperty_ReturnsTypePropertyInfo() {
            var expected = typeof(MockObject).GetProperty("BoolProperty");
            var actual = new MemberResolver<MockObject>().GetProperty(o => o.BoolProperty);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetProperty_UsesBindingFlags() {
            var mr = new MemberResolver<MockObject>();
            var expected = typeof(MockObject).GetProperty("StringProperty", BindingFlags.Public | BindingFlags.Instance);
            var actual = mr.GetProperty(o => o.StringProperty, BindingFlags.Public | BindingFlags.Instance);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);

            var doesNotExist = mr.GetProperty(o => o.StringProperty, BindingFlags.Public | BindingFlags.Static);
            Assert.IsNull(doesNotExist);
        }

        [Test]
        public void GetField_ReturnsTypeFieldInfo() {            
            var expected = typeof(MockObject).GetField("BoolField");
            var actual = new MemberResolver<MockObject>().GetField(o => o.BoolField);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetField_UsesBindingFlags() {
            var mr = new MemberResolver<MockObject>();
            var expected = typeof(MockObject).GetField("StringField", BindingFlags.Public | BindingFlags.Instance);
            var actual = mr.GetField(o => o.StringField, BindingFlags.Public | BindingFlags.Instance);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);

            var doesNotExist = mr.GetField(o => o.StringField, BindingFlags.Public | BindingFlags.Static);
            Assert.IsNull(doesNotExist);
        }

        [Test]
        public void GetMethod_ReturnsTypeMethodInfo() {
            var expected = typeof(MockObject).GetMethod("StringMethod");
            var actual = new MemberResolver<MockObject>().GetMethod(o => o.StringMethod());
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMethod_UsesBindingFlags() {
            var mr = new MemberResolver<MockObject>();
            var expected = typeof(MockObject).GetMethod("Void1ParamMethod", BindingFlags.Public | BindingFlags.Instance);
            var actual = mr.GetMethod(o => o.Void1ParamMethod(0), BindingFlags.Public | BindingFlags.Instance);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);

            var doesNotExist = mr.GetMethod(o => o.Void1ParamMethod(0), BindingFlags.Public | BindingFlags.Static);
            Assert.IsNull(doesNotExist);
        }
    }
}
