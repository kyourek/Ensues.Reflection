Ensues.Reflection
=================

(Formerly [NetNames][1])

**Static and extension methods for resolving .NET member names to strings.**

For example, consider the following class:

    class Members {
        public int MyField = default(int);
        public bool MyProperty {
            get { throw new NotSupportedException(); }
        }
        public object MyFunc() {
            throw new NotSupportedException();
        }
        public void MyAction(string param) {
            throw new NotSupportedException();
        }
    }

With the namespace `Ensues.Reflection.Objects` used, the following program:

    class Program {
        static void Main(string[] args) {

            var instance = new Members();

            Console.WriteLine(instance.GetMemberName(i => i.MyField));
            Console.WriteLine(instance.GetMemberName(i => i.MyProperty));
            Console.WriteLine(instance.GetMemberName(i => i.MyFunc()));
            Console.WriteLine(instance.GetMemberName(i => i.MyAction("")));

            Console.ReadKey();
        }
    }

produces the output:

    MyField
    MyProperty
    MyFunc
    MyAction

The `Ensues.Reflection` namespace also provides static methods for getting member names. These can be used without a type instance.

    Console.WriteLine(MemberName<Members>.Of(m => m.MyField));
    Console.WriteLine(MemberName<Members>.Of(m => m.MyProperty));
    Console.WriteLine(MemberName<Members>.Of(m => m.MyFunc()));
    Console.WriteLine(MemberName<Members>.Of(m => m.MyAction("")));

  [1]: http://code.google.com/p/net-names/
