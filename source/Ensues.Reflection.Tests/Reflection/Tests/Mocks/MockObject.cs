using System;

namespace Ensues.Reflection.Tests.Mocks {

    internal class MockObject {

        public bool BoolField = default(bool);
        public string StringField = default(string);

        public bool BoolProperty { get { throw new NotSupportedException(); } }
        public string StringProperty { get { throw new NotSupportedException(); } }

        public int IntMethod() { throw new NotSupportedException(); }
        public double DoubleMethod() { throw new NotSupportedException(); }

        public string StringMethod() { throw new NotSupportedException(); }
        public string String1ParamMethod(object param1) { throw new NotSupportedException(); }

        public void VoidMethod() { throw new NotSupportedException(); }
        public void Void1ParamMethod(int param) { throw new NotSupportedException(); }
    }
}
