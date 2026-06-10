using System.Linq.Expressions;
using System.Text;

namespace 语法验证与学习
{
    /// <summary>
    /// 表达式翻译帮助类
    /// </summary>
    public static class ExpressionTranslatorHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="strPrefix"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static BaseTranslator? GetTranslator(Expression rootNode, string? strPrefix = null, StringBuilder? content = null)
        {
            strPrefix ??= string.Empty;
            content ??= new StringBuilder();
            switch(rootNode.NodeType)
            {
                case ExpressionType.Add:
                    return new BinaryTranslator((BinaryExpression)rootNode, strPrefix, content);

                case ExpressionType.Constant:
                    return new ConstantTranslator((ConstantExpression)rootNode, strPrefix, content);

                default:
                    content.AppendLine($"遇到了不支持的节点类型:{rootNode.NodeType}");
                    return null;
            }
        }
    }
}