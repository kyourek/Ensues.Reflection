Ensues.Reflection
=================

(Formerly [NetNames][1])

**Methods for resolving .NET member names to strings.**

For example, consider the following class:

    class MockObject {
        public bool BoolField = default(bool);
        public string StringProperty { get { throw new NotSupportedException(); } }
        public void VoidMethod() { throw new NotSupportedException(); }
        public string String1ParamMethod(object param1) { 
            throw new NotSupportedException(); 
        }
    }

With the namespace `Ensues.Objects` used, the following program:

    class Program {
        static void Main(string[] args) {

            var instance = new MockObject();

            Console.WriteLine(instance.GetMemberName(i => i.BoolField));
            Console.WriteLine(instance.GetMemberName(i => i.StringProperty));
            Console.WriteLine(instance.GetMemberName(i => i.VoidMethod()));
            Console.WriteLine(instance.GetMemberName(i => i.String1ParamMethod("")));

            Console.ReadKey();
        }
    }

produces the output:

    BoolField
    StringProperty
    VoidMethod
    String1ParamMethod

The `Ensues.Reflection` namespace also provides the `MemberResolver` class for getting member names without a type instance.

    var mr = new MemberResolver<MockObject>();
    Console.WriteLine(mr.GetMemberName(m => m.BoolField));
    Console.WriteLine(mr.GetMemberName(m => m.StringProperty));
    Console.WriteLine(mr.GetMemberName(m => m.VoidMethod()));
    Console.WriteLine(mr.GetMemberName(m => m.String1ParamMethod("")));

  [1]: http://code.google.com/p/net-names/
