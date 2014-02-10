using System;
using System.Linq.Expressions;

namespace Ensues.Reflection {

    /// <summary>
    /// The exception that is thrown when a member name cannot be resolved
    /// from an instance of <see cref="T:LambdaExpression"/>.
    /// </summary>
    public class InvalidTargetMemberException : Exception {

        /// <summary>
        /// Gets the instance of <see cref="T:LambdaExpression"/> for which
        /// the member name resolution failed.
        /// </summary>
        public LambdaExpression Expression { get { return _Expression; } }
        private readonly LambdaExpression _Expression;

        /// <summary>
        /// Creates a new instance of the error.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="T:LambdaExpression"/> for which the member
        /// name resolution failed.
        /// </param>
        public InvalidTargetMemberException(LambdaExpression expression) {
            _Expression = expression;
        }

        /// <summary>
        /// Gets a description of this error.
        /// </summary>
        public override string Message {
            get {
                return "A member name cannot be resolved from the given expression.";
            }
        }
    }
}
