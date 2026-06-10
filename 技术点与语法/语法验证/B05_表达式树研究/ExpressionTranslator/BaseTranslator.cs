using System.Linq.Expressions;
using System.Text;

namespace 语法验证与学习
{
    public abstract class BaseTranslator
    {
        /// <summary>
        /// 代表内容
        /// </summary>
        public StringBuilder Content { get; set; }

        /// <summary>
        /// 换行符
        /// </summary>
        protected readonly string? LinaBreakSymbol;

        /// <summary>
        /// 纪录翻译的文字时加的前缀
        /// </summary>
        protected readonly string? StrPrefix;

        /// <summary>
        /// 当前翻译器需要翻译的节点
        /// </summary>
        public Expression Node { get; set; }

        public ExpressionType NodeType => this.Node.NodeType;

        protected BaseTranslator(
            Expression node,
            string? strPrefix = null,
            StringBuilder? content = null)
        {
            this.Content = content ?? new StringBuilder();
            this.LinaBreakSymbol = "\r\n";
            this.StrPrefix = strPrefix;
            this.Node = node;
        }

        /// <summary>
        /// 替代Console.WriteLine 因为{strPrefix}每次都要写
        /// </summary>
        /// <param name="value"></param>
        /// <param name="strPrefix"></param>
        /// <returns></returns>
        protected virtual StringBuilder WriteLine(string? value)
        {
            this.Content.AppendLine($"{this.StrPrefix}{value}{this.LinaBreakSymbol}");
            return this.Content;
        }

        //protected virtual StringBuilder Write(string? value, string? strPrefix = null)
        //{
        //    this.content.Append($"{strPrefix}{value}");
        //    return this.content;
        //}

        /// <summary>
        ///
        /// </summary>
        public abstract void Translator();

        /// <summary>
        /// 创建翻译者
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="strPrefix">翻译时的前缀,用来显示成树形结构。子类调用时要传递</param>
        /// <param name="content">翻译时的前缀,用来显示成树形结构。子类调用时要传递</param>
        /// <returns></returns>
        public BaseTranslator? GetTranslator(Expression rootNode, string? strPrefix = null, StringBuilder? content = null)
        {
            //每次新创建节点时会调用这个方法，这样处理就又缩进了一次
            strPrefix = $"{strPrefix}\t";

            //处理这些变量的引用，因为是递归调用此方法，尽量避免额外的内存分配
            //这样处理后，子类在调用时就可以一直把最开始的这些变量引用传递进去
            content ??= this.Content;

            //分类型解析
            return ExpressionTranslatorHelper.GetTranslator(rootNode, strPrefix, content);
        }
    }
}