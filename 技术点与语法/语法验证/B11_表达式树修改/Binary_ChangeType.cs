using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace 语法验证与学习
{
    public class Binary_ChangeType : ExpressionVisitor
    {
        //利用Expression的引用当Key来存放将要改变的类型
        private readonly ConcurrentDictionary<Expression, ExpressionType> ChangeTo = new ConcurrentDictionary<Expression, ExpressionType>();

        protected override Expression VisitBinary(BinaryExpression node)
        {
            return Expression.MakeBinary(
                this.ChangeTo[node],
                node.Left,
                node.Right,
                node.IsLiftedToNull,//返回值
                node.Method);
        }

        //Chage收到的表达式节点是高层级的，但是VisitBinary需要的是比较低层级的
        //如何将Chage收到的type准确的传递到VisitBinary中，这里考虑的是多线程情况下，

        public Expression Chage(Expression expression, ExpressionType expressionType)
        {
            if(expression is BinaryExpression exp)
            {
                _ = this.ChangeTo.AddOrUpdate(exp, expressionType, (k, v) => v = expressionType);
                return base.Visit(expression);
            }
            else
                return expression;
        }
    }
}