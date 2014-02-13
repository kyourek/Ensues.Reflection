using System;
using System.Linq.Expressions;
using System.Reflection;

using Ensues.Reflection;
namespace Ensues.Objects {

    /// <summary>
    /// A static class that provides extension methods for getting
    /// member information from an object instance.
    /// </summary>
    public static class ObjectReflection {

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="T:Type"/> of the object <paramref name="obj"/>.
        /// </typeparam>
        /// <typeparam name="TMember">
        /// The <see cref="T:Type"/> of the member used in the expression of the <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="obj">
        /// The object whose member is referenced in the <paramref name="selector"/>.
        /// </param>
        /// <param name="selector">
        /// The expression that calls the class member whose name is returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>        
        public static string GetMemberName<T, TMember>(this T obj, Expression<Func<T, TMember>> selector) {
            return MemberResolver<T>.Default.GetMemberName(selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="T:Type"/> of the object <paramref name="obj"/>.
        /// </typeparam>
        /// <param name="obj">
        /// The object whose member is referenced in the <paramref name="selector"/>.
        /// </param>
        /// <param name="selector">
        /// The expression that calls the class member whose name is returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>        
        public static string GetMemberName<T>(this T obj, Expression<Action<T>> selector) {
            return MemberResolver<T>.Default.GetMemberName(selector);
        }

        /// <summary>
        /// Gets the <see cref="T:FieldInfo"/> for the field specified by the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="T:Type"/> of the object <paramref name="obj"/>.
        /// </typeparam>
        /// <typeparam name="TField">
        /// The <see cref="T:Type"/> of the field specified by the <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="obj">
        /// The object whose <see cref="T:FieldInfo"/> is returned.
        /// </param>
        /// <param name="selector">
        /// The expression that calls the field whose <see cref="T:FieldInfo"/> is returned.
        /// </param>
        /// <returns>
        /// The <see cref="T:FieldInfo"/> for the specified <paramref name="selector"/>.
        /// </returns>
        /// <exception cref="T:InvalidTargetMemberException">
        /// Thrown if the name of the member specified by the <paramref name="selector"/> cannot
        /// be resolved.
        /// </exception>
        public static FieldInfo GetField<T, TField>(this T obj, Expression<Func<T, TField>> selector) {
            return MemberResolver<T>.Default.GetField(selector);
        }

        /// <summary>
        /// Gets the <see cref="T:PropertyInfo"/> for the property specified by the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="T:Type"/> of the object <paramref name="obj"/>.
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// The <see cref="T:Type"/> of the property specified by the <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="obj">
        /// The object whose <see cref="T:PropertyInfo"/> is returned.
        /// </param>
        /// <param name="selector">
        /// The expression that calls the property whose <see cref="T:PropertyInfo"/> is returned.
        /// </param>
        /// <returns>
        /// The <see cref="T:PropertyInfo"/> for the specified <paramref name="selector"/>.
        /// </returns>
        /// <exception cref="T:InvalidTargetMemberException">
        /// Thrown if the name of the member specified by the <paramref name="selector"/> cannot
        /// be resolved.
        /// </exception>
        public static PropertyInfo GetProperty<T, TProperty>(this T obj, Expression<Func<T, TProperty>> selector) {
            return MemberResolver<T>.Default.GetProperty(selector);
        }

        /// <summary>
        /// Gets the <see cref="T:MethodInfo"/> for the method specified by the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="T:Type"/> of the object <paramref name="obj"/>.
        /// </typeparam>
        /// <param name="obj">
        /// The object whose <see cref="T:MethodInfo"/> is returned.
        /// </param>
        /// <param name="selector">
        /// The expression that calls the method whose <see cref="T:MethodInfo"/> is returned.
        /// </param>
        /// <returns>
        /// The <see cref="T:MethodInfo"/> for the specified <paramref name="selector"/>.
        /// </returns>
        /// <exception cref="T:InvalidTargetMemberException">
        /// Thrown if the name of the member specified by the <paramref name="selector"/> cannot
        /// be resolved.
        /// </exception>
        public static MethodInfo GetMethod<T>(this T obj, Expression<Action<T>> selector) {
            return MemberResolver<T>.Default.GetMethod(selector);
        }
    }
}
