using System.Linq.Expressions;
using System.Text;

namespace 语法验证与学习
{
    public class ConstantTranslator : BaseTranslator
    {
        private new readonly ConstantExpression Node;

        public ConstantTranslator(ConstantExpression node, string? strPrefix = null, StringBuilder? content = null) : base(node, strPrefix, content)
        {
            this.Node = node;
        }

        public override void Translator()
        {
            base.WriteLine($"这一个{this.Node.NodeType}的常量表达式");
            base.WriteLine($"常量值是: {this.Node.Type}类型");
            base.WriteLine($"常量值是: {this.Node.Value}");
        }
    }
}