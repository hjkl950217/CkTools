#pragma warning disable CS8602 // 取消引用可能出现的空引用。

using System.Linq.Expressions;
using System.Text;

namespace 语法验证与学习
{
    public class BinaryTranslator : BaseTranslator
    {
        public new BinaryExpression Node { get; set; }

        public BinaryTranslator(
            BinaryExpression node,
            string? strPrefix = null,
            StringBuilder? content = null
            )
            : base(node, strPrefix, content)
        {
            this.Node = node;
        }

        public override void Translator()
        {
            base.WriteLine($"这个二进制表达式是 {this.Node.NodeType} 类型的表达式");
            base.WriteLine($"这个二进制表达式的代码: {this.Node.ToString()}");

            var left = base.GetTranslator(this.Node.Left);
            base.WriteLine($"这个左形参节点:");

            left.Translator();

            var right = base.GetTranslator(this.Node.Right);
            base.WriteLine($"这个右形参节点:");
            right.Translator();
        }
    }
}

#pragma warning restore CS8602 // 取消引用可能出现的空引用。