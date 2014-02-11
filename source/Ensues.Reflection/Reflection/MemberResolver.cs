using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ensues.Reflection {

    /// <summary>
    /// A class that provides methods for resolving member names 
    /// from instances and derivatives of <see cref="T:LambdaExpression"/>.
    /// </summary>
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

        /// <summary>
        /// Gets the name of the member used in the <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="T:LambdaExpression"/> that calls the class member
        /// whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="expression"/>.
        /// </returns>   
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

    /// <summary>
    /// A class that provides methods for resolving member names 
    /// from instances and derivatives of <see cref="T:LambdaExpression"/>.
    /// </summary>
    public class MemberResolver<T> : MemberResolver {

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TMember">
        /// The <see cref="T:Type"/> of the member.
        /// </typeparam>
        /// <param name="selector">
        /// An expression that uses the member whose name is returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>
        /// <exception cref="T:InvalidTargetMemberException">
        /// Thrown if a member name cannot be resolved from the <paramref name="selector"/>.
        /// </exception>
        public string GetMemberName<TMember>(Expression<Func<T, TMember>> selector) {
            return GetMemberName((LambdaExpression)selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TMember">
        /// The <see cref="T:Type"/> of the member.
        /// </typeparam>
        /// <param name="selector">
        /// An expression that uses the member whose name is returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>
        /// <exception cref="T:InvalidTargetMemberException">
        /// Thrown if a member name cannot be resolved from the <paramref name="selector"/>.
        /// </exception>
        public string GetMemberName<TMember>(Expression<Action<T>> selector) {
            return GetMemberName((LambdaExpression)selector);
        }

        /// <summary>
        /// Gets the property used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The <see cref="T:Type"/> of the property.
        /// </typeparam>
        /// <param name="selector">
        /// An expression that uses the property.
        /// </param>
        /// <returns>
        /// The <see cref="T:PropertyInfo"/> of the property used in the <paramref name="selector"/>.
        /// </returns>
        /// <exception cref="T:InvalidTargetMemberException">
        /// Thrown if a member name cannot be resolved from the <paramref name="selector"/>.
        /// </exception>
        public PropertyInfo GetProperty<TProperty>(Expression<Func<T, TProperty>> selector) {
            return typeof(T).GetProperty(GetMemberName(selector));
        }

        /// <summary>
        /// Gets the field used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TField">
        /// The <see cref="T:Type"/> of the field.
        /// </typeparam>
        /// <param name="selector">
        /// An expression that uses the field.
        /// </param>
        /// <returns>
        /// The <see cref="T:FieldInfo"/> of the field used in the <paramref name="selector"/>.
        /// </returns>
        /// <exception cref="T:InvalidTargetMemberException">
        /// Thrown if a member name cannot be resolved from the <paramref name="selector"/>.
        /// </exception>
        public FieldInfo GetField<TField>(Expression<Func<T, TField>> selector) {
            return typeof(T).GetField(GetMemberName(selector));
        }

        /// <summary>
        /// Gets the method used in the <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">
        /// An expression that uses the method.
        /// </param>
        /// <returns>
        /// The <see cref="T:MethodInfo"/> of the method used in the <paramref name="selector"/>.
        /// </returns>
        /// <exception cref="T:InvalidTargetMemberException">
        /// Thrown if a member name cannot be resolved from the <paramref name="selector"/>.
        /// </exception>
        public MethodInfo GetMethod(Expression<Action<T>> selector) {
            return typeof(T).GetMethod(GetMemberName(selector));
        }
    }
}
