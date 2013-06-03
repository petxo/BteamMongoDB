using System;
using System.Linq.Expressions;

namespace BteamMongoDB.Repository
{
    internal class ReflectionHelper
    {
        /// <summary>
        /// Finds the property.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns></returns>
        public static string FindProperty(LambdaExpression lambdaExpression)
        {
            Expression expressionToCheck = lambdaExpression;

            var done = false;

            while (!done)
            {
                switch (expressionToCheck.NodeType)
                {
                    case ExpressionType.Convert:
                        expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
                        break;

                    case ExpressionType.Lambda:
                        expressionToCheck = ((LambdaExpression)expressionToCheck).Body;
                        break;

                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression)expressionToCheck;

                        if (memberExpression.Expression.NodeType != ExpressionType.Parameter &&
                            memberExpression.Expression.NodeType != ExpressionType.Convert)
                        {
                            throw new ArgumentException(
                                string.Format("Expression '{0}' must resolve to top-level member.", lambdaExpression),
                                "lambdaExpression");
                        }

                        return memberExpression.Member.Name;

                    default:
                        done = true;
                        break;
                }
            }

            return null;
        }
    }
}