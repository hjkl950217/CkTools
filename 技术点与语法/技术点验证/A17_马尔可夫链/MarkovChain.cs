namespace 技术点验证
{
    /// <summary>
    /// 马尔可夫链节点实体，一个对象代表一个关系
    /// <para>A->B 为一组, A->C 为另一组</para>
    /// </summary>
    public class MarkovChainNode
    {
        public const string StartNodeName = "Start";
        public const string EndNodeName = "End";

        /// <summary>
        /// 公用的结束节点
        /// </summary>
        public static readonly MarkovChainNode EndNode = new MarkovChainNode()
        {
            NodeName = EndNodeName,
            NextNodeList = new List<MarkovChainNode>()
        };

        /// <summary>
        /// 创建一个开始节点
        /// </summary>
        /// <param name="nodeName">节点名。默认值为<see cref="MarkovChainNode.StartNodeName"/></param>
        /// <remarks>
        /// 为什么结束节点是静态字段而开始节点需要使用方法呢？
        /// <para>答：因为结束节点可以公用，到结束节点就没了。但是不同的数据，他们的开始节点关联的下一个节点是不同的，所以无法公用。</para>
        /// </remarks>
        /// <returns>一个新的开始节点，引用地址与其它的开始节点不同。</returns>
        public static MarkovChainNode CreateStartNode(string nodeName = MarkovChainNode.StartNodeName)
        {
            return new MarkovChainNode()
            {
                NodeName = nodeName
            };
        }

        /// <summary>
        /// 当前节点的名字
        /// </summary>
        public string NodeName { get; set; } = string.Empty;

        /// <summary>
        /// 后续节点
        /// </summary>
        public List<MarkovChainNode> NextNodeList { get; private set; } = new List<MarkovChainNode>();

        /// <summary>
        /// 到下一个节点，出现的次数
        /// </summary>
        public int NextNodeNum { get => this.NextNodeList.Count; }

        /// <summary>
        /// 到下一个节点的概率
        /// </summary>
        public double NextNodeProbability { get => this.NextNodeNum == 0 ? 0 : 1 / (double)this.NextNodeNum; }

        /// <summary>
        /// 到下一个节点的概率(字符串版本)
        /// </summary>
        public string? NextNodeProbabilityString { get => $"{(this.NextNodeProbability * 100).ToString("0.00")}%"; }
    }
}