using System;
using System.Linq.Expressions;

namespace Ensues.Reflection {

    internal class MemberNameResolver {

        private string Resolve(MemberExpression expression) {
            if (null == expression) throw new ArgumentNullException("expression");

            var member = expression.Member;
            if (member == null) throw new ArgumentException("The member of the member expression is null.");

            return member.Name;
        }

        private string Resolve(MethodCallExpression expression) {
            if (null == expression) throw new ArgumentNullException("expression");

            var method = expression.Method;
            if (method == null) throw new ArgumentException("The method of the method expression is null.");

            return method.Name;
        }

        public virtual string Resolve(LambdaExpression expression) {
            if (null == expression) throw new ArgumentNullException("expression");

            var body = expression.Body;
            if (body != null) {

                var try1 = body as UnaryExpression;
                if (try1 != null) {

                    var try11 = try1.Operand as MemberExpression;
                    if (try11 != null) {
                        return Resolve(try11);
                    }

                    var try12 = try1.Operand as MethodCallExpression;
                    if (try12 != null) {
                        return Resolve(try12);
                    }
                }

                var try2 = body as MemberExpression;
                if (try2 != null) {
                    return Resolve(try2);
                }

                var try3 = body as MethodCallExpression;
                if (try3 != null) {
                    return Resolve(try3);
                }
            }

            throw new InvalidTargetMemberException(expression);
        }
    }
}
