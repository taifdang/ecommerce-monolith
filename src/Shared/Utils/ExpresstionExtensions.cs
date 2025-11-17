using System.Linq.Expressions;

namespace Shared.Utils;

//ref: https://coding.abel.nu/2013/01/merging-expression-trees-to-reuse-in-linq-queries/
//ref: https://www.codeproject.com/articles/Combining-expressions-to-dynamically-append-criter#comments-section
//ref: https://blog.elmah.io/expression-trees-in-c-building-dynamic-linq-queries-at-runtime/
public static class ExpresstionExtensions
{
    public static Expression<Func<T, bool>> And<T>(
       Expression<Func<T, bool>> left,
       Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T),"x");

        var leftBody = ReplaceParameter(left.Body, left.Parameters[0], param);
        var rightBody = ReplaceParameter(right.Body, right.Parameters[0], param);

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(leftBody, rightBody), param);
    }

    private static Expression ReplaceParameter(Expression expression, ParameterExpression toReplace, ParameterExpression replaceWith)
    {
        return new ParameterReplacer(toReplace, replaceWith).Visit(expression);
    }
    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _from;
        private readonly ParameterExpression _to;
        public ParameterReplacer(ParameterExpression from, ParameterExpression to)
        {
            _from = from;
            _to = to;
        }
        protected override Expression VisitParameter(ParameterExpression node)
            => node == _from ? _to : base.VisitParameter(node);
    }
}
