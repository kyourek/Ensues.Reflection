using System;
using System.Linq.Expressions;

namespace Ensues.Reflection {

    /// <summary>
    /// A class that provides methods for resolving member names 
    /// from instances and derivatives of <see cref="T:LambdaExpression"/>.
    /// </summary>
    public class MemberName {

        internal MemberNameResolver NameResolver {
            get { return _NameResolver ?? (_NameResolver = new MemberNameResolver()); }
            set { _NameResolver = value; }
        }
        private MemberNameResolver _NameResolver;

        /// <summary>
        /// Gets the instance used in the static methods of this class.
        /// </summary>
        public static MemberName Instance {
            get { return _Instance ?? (_Instance = new MemberName()); }
        }
        private static MemberName _Instance;

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
        public string Get(LambdaExpression expression) {
            return NameResolver.Resolve(expression);
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
        public static string Of(LambdaExpression expression) {
            return Instance.Get(expression);
        }
    }

    /// <summary>
    /// A class that provides methods for resolving member names 
    /// from instances and derivatives of <see cref="T:LambdaExpression"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type whose member names are to be returned.
    /// </typeparam>
    public class MemberName<T> : MemberName {

        /// <summary>
        /// Gets the instance used in the static methods of this class.
        /// </summary>
        public static new MemberName<T> Instance {
            get { return _Instance ?? (_Instance = new MemberName<T>()); }
        }
        private static MemberName<T> _Instance;

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TResult">
        /// The type returned by the delegate used in expression of the <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="selector">
        /// The expression that calls the type member whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>  
        public string Get<TResult>(Expression<Func<T, TResult>> selector) {
            return NameResolver.Resolve(selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="TResult">
        /// The type returned by the delegate used in expression of the <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="selector">
        /// The expression that calls the type member whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>  
        public static string Of<TResult>(Expression<Func<T, TResult>> selector) {
            return Instance.Get(selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">
        /// The expression that calls the class member whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>   
        public string Get(Expression<Func<T, object>> selector) {
            return NameResolver.Resolve(selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">
        /// The expression that calls the class member whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>   
        public static string Of(Expression<Func<T, object>> selector) {
            return Instance.Get(selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">
        /// The expression that calls the class member whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>        
        public string Get(Expression<Action<T>> selector) {
            return NameResolver.Resolve(selector);
        }

        /// <summary>
        /// Gets the name of the member used in the <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">
        /// The expression that calls the class member whose name should be returned.
        /// </param>
        /// <returns>
        /// The name of the member used in the <paramref name="selector"/>.
        /// </returns>        
        public static string Of(Expression<Action<T>> selector) {
            return Instance.Get(selector);
        }
    }
}
