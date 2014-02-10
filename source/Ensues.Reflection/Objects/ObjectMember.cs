using System;
using System.Linq.Expressions;

using Ensues.Reflection;
namespace Ensues.Objects {
    
    /// <summary>
    /// A static class that provides extension methods for getting
    /// the names of an object's members.
    /// </summary>
    public static class ObjectMember {

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type whose member name should be returned.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The result of the delegate used by the expression of the <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="obj">
        /// The object whose member is referenced in the <paramref name="selector"/>.
        /// </param>
        /// <param name="selector">
        /// The expression that calls the class member
        /// whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>        
        public static string GetMemberName<T, TResult>(this T obj, Expression<Func<T, TResult>> selector) {
            return MemberName.Of(selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type whose member name should be returned.
        /// </typeparam>
        /// <param name="obj">
        /// The object whose member is referenced in the <paramref name="selector"/>.
        /// </param>
        /// <param name="selector">
        /// The expression that calls the class member
        /// whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>   
        public static string GetMemberName<T>(this T obj, Expression<Func<T, object>> selector) {
            return MemberName.Of(selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <param name="obj">
        /// The object whose member is referenced in the <paramref name="selector"/>.
        /// </param>
        /// <param name="selector">
        /// The expression that calls the class member
        /// whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>        
        public static string GetMemberName<T>(this T obj, Expression<Action<T>> selector) {
            return MemberName.Of(selector);
        }
    }
}
