using System.Text;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A17_马尔可夫链)]
    public class A17_马尔科夫链 : IVerification
    {
        /*
         * 用马尔可夫链来生成不同的路径
         *
         * 1.先从样本中找到每个节点到下个节点的概率(或是找出所有可能的路径)
         * 2.然后重组成新的、 不同的链路
         *
         * 原文：https://zhuanlan.zhihu.com/p/61028042
         */

        public void Start(string[]? args)
        {
            Console.WriteLine("原始数据");
            foreach(string item in this.data)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("======================");
            Console.WriteLine();

            this.SplitData(this.data);

            Console.WriteLine("分析概率后的数据");
            Dictionary<string, MarkovChainNode> markovData = this.AnalyzeData(this.CreateList, this.SplitData, this.data);
            foreach(KeyValuePair<string, MarkovChainNode> item in markovData.OrderBy(t => t.Key))
            {
                Console.WriteLine($"key:{item.Key}\t到下一节点的数量：{item.Value.NextNodeNum}\t概率：{item.Value.NextNodeProbabilityString}");
            }
            Console.WriteLine("======================");
            Console.WriteLine();

            //todo:A17_马尔科夫链
            /*
             * 遍历这里还有点问题，遍历的算法不对
             *
             * 扩展：每个路径长度问题，如何按指定的长度来处理？
             *
             */
            Console.WriteLine("所有可能的路径");
            List<MarkovChainNode[]> result = this.FindAllPath(markovData);
            foreach(MarkovChainNode[] item in result)
            {
                Console.WriteLine(this.CombinPath(item));
            }
        }

        #region 字段区

        /// <summary>
        /// 原始数据
        /// </summary>
        private readonly string[] data = new string[]
        {
            "->",
            "1A -> 2F -> 3A -> 4F",
            "1E -> 2C -> 3A -> 4C",
            "1A -> 2A -> 3F -> 4C"
        };

        /// <summary>
        /// 分割符
        /// </summary>
        private readonly string separator = "->";

        private static Func<MarkovChainNode> NodeFactory(string key)
        {
            return () =>
            {
                return key switch
                {
                    MarkovChainNode.StartNodeName => MarkovChainNode.CreateStartNode(),
                    MarkovChainNode.EndNodeName => MarkovChainNode.EndNode,
                    _ => new MarkovChainNode() { NodeName = key },
                };
            };
        }

        #endregion 字段区

        #region 数据分析、处理部分

        /// <summary>
        /// 创建初始集合，包含开始节点
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, MarkovChainNode> CreateList()
        {
            Dictionary<string, MarkovChainNode> result = new Dictionary<string, MarkovChainNode>();
            return result;
        }

        /// <summary>
        /// 分割数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// 一条string?[]代表处理好的样本
        /// </returns>
        private List<string[]> SplitData(string[] data)
        {
            List<string[]> result = new List<string[]>();

            foreach(string item in data)
            {
                if(item == null || item.Length == 0 || item.Trim() == this.separator)
                    continue;

                string[] tempArry = ($"{MarkovChainNode.StartNodeName}->{item.ToUpper()}->{MarkovChainNode.EndNodeName}")
                    .Split(this.separator, StringSplitOptions.RemoveEmptyEntries)
                    .Where(t => !string.IsNullOrEmpty(t))//找出不为null的字段
                    .Select(t => t.Trim())
                    .ToArray();

                result.Add(tempArry);
            }

            return result;
        }

        /// <summary>
        /// 分析数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, MarkovChainNode> AnalyzeData(
            Func<Dictionary<string, MarkovChainNode>> initialFactory,
            Func<string[], List<string[]>> splitFunc,
            string[] sourceData)
        {
            Dictionary<string, MarkovChainNode> result = initialFactory();
            List<string[]> splitedData = splitFunc(sourceData);

            #region 遍历并处理

            foreach(string[] item in splitedData)//遍历每条数据
            {
                for(int i = 0 ; i < item.Length ; i++)//遍历每条数据中的节点
                {
                    //从结果区中拿到节点
                    MarkovChainNode tempNode = result.GetOrAdd(item[i], NodeFactory(item[i]));

                    //添加节点 关联下一个节点
                    bool isEnd = i + 1 == item.Length;
                    if(!isEnd)
                    {
                        //如果不是最后一个节点，关联下一个节点
                        MarkovChainNode nextNode = result.GetOrAdd(item[i + 1], NodeFactory(item[i + 1]));
                        tempNode.NextNodeList.Add(nextNode);
                    }

                    //最后一个节点是默认的end节点，所以只需要处理end节点之前的就好
                }
            }

            #endregion 遍历并处理

            return result;
        }

        #endregion 数据分析、处理部分

        #region 遍历部分

        /// <summary>
        /// 将路径的节点数组转换为<see cref="string?"/>，以方便输出
        /// </summary>
        /// <param name="markovChainNodes"></param>
        /// <returns></returns>
        public string? CombinPath(MarkovChainNode[] markovChainNodes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(markovChainNodes[0].NodeName);
            foreach(MarkovChainNode? item in markovChainNodes.Skip(1))
            {
                stringBuilder.AppendFormat(" -> {0}", item.NodeName);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data">包含节点关系的数据集合</param>
        /// <param name="resultSet">递归过程中传递结果集，返回时也是返回此结果集</param>
        /// <param name="startKey">每次递归要查找的key</param>
        /// <param name="tempList">临时集合，在递归中代表当前的路径</param>
        /// <returns></returns>
        private List<MarkovChainNode[]> FindAllPath(
            Dictionary<string, MarkovChainNode> data,
            List<MarkovChainNode[]>? resultSet = null,
            string startKey = MarkovChainNode.StartNodeName,
            List<MarkovChainNode>? tempList = null)
        {
            resultSet ??= new List<MarkovChainNode[]>();
            tempList ??= new List<MarkovChainNode>();

            switch(startKey)
            {
                case MarkovChainNode.EndNodeName:
                    //如果要查找的key为end节点名，代表当前路径已经找完，需要把当前路径拷贝到结果集中
                    MarkovChainNode[] tempResult = tempList.Copy(tempList.Count + 1);//这里加1是要算上end节点本身
                    tempResult[tempList.Count] = MarkovChainNode.EndNode;//给最后一个节点赋值上结束节点
                    resultSet.Add(tempResult);
                    tempList.Clear();
                    return resultSet;

                default:
                    MarkovChainNode tempNode = data[startKey];
                    tempList.Add(tempNode);//添加到临时集合中
                    foreach(MarkovChainNode item in tempNode.NextNodeList)//遍历子节点集合
                    {
                        this.FindAllPath(data, resultSet, item.NodeName, tempList);
                    }
                    return resultSet;
            }
        }

        #endregion 遍历部分
    }
}