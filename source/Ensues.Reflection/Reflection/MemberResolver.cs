using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ensues.Reflection {

    public class MemberResolver {

        private string GetMemberName(MemberExpression expression) {
            if (null == expression) throw new ArgumentNullException("expression");

            var member = expression.Member;
            if (member == null) throw new ArgumentException("The member of the member expression is null.");

            return member.Name;
        }

        private string GetMemberName(MethodCallExpression expression) {
            if (null == expression) throw new ArgumentNullException("expression");

            var method = expression.Method;
            if (method == null) throw new ArgumentException("The method of the method expression is null.");

            return method.Name;
        }

        public virtual string GetMemberName(LambdaExpression expression) {
            if (null == expression) throw new ArgumentNullException("expression");

            var body = expression.Body;
            if (body != null) {

                var try1 = body as UnaryExpression;
                if (try1 != null) {

                    var try11 = try1.Operand as MemberExpression;
                    if (try11 != null) {
                        return GetMemberName(try11);
                    }

                    var try12 = try1.Operand as MethodCallExpression;
                    if (try12 != null) {
                        return GetMemberName(try12);
                    }
                }

                var try2 = body as MemberExpression;
                if (try2 != null) {
                    return GetMemberName(try2);
                }

                var try3 = body as MethodCallExpression;
                if (try3 != null) {
                    return GetMemberName(try3);
                }
            }

            throw new InvalidTargetMemberException(expression);
        }
    }

    public class MemberResolver<T> : MemberResolver {

        public string GetMemberName<TMember>(Expression<Func<T, TMember>> selector) {
            return GetMemberName((LambdaExpression)selector);
        }

        public string GetMemberName<TMember>(Expression<Action<T>> selector) {
            return GetMemberName((LambdaExpression)selector);
        }

        public PropertyInfo GetProperty<TProperty>(Expression<Func<T, TProperty>> selector) {
            return typeof(T).GetProperty(GetMemberName(selector));
        }

        public PropertyInfo GetProperty<TProperty>(Expression<Func<T, TProperty>> selector, BindingFlags bindingAttr) {
            return typeof(T).GetProperty(GetMemberName(selector), bindingAttr);
        }

        public FieldInfo GetField<TField>(Expression<Func<T, TField>> selector) {
            return typeof(T).GetField(GetMemberName(selector));
        }

        public FieldInfo GetField<TField>(Expression<Func<T, TField>> selector, BindingFlags bindingAttr) {
            return typeof(T).GetField(GetMemberName(selector), bindingAttr);
        }

        public MethodInfo GetMethod(Expression<Action<T>> selector) {
            return typeof(T).GetMethod(GetMemberName(selector));
        }

        public MethodInfo GetMethod(Expression<Action<T>> selector, BindingFlags bindingAttr) {
            return typeof(T).GetMethod(GetMemberName(selector), bindingAttr);
        }
    }
}
