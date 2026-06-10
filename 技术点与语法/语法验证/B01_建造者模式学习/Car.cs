namespace 语法验证与学习
{
    /// <summary>
    /// 汽车类
    /// </summary>
    public sealed class Car
    {
        // 汽车部件集合
        //  private readonly IList<string?> parts = new List<string?>();
        private readonly IDictionary<string, string?> parts = new Dictionary<string, string?>();

        /// <summary>
        /// 把单个部件添加到汽车部件集合中
        /// </summary>
        /// <param name="partName">partName部件的名字</param>
        /// <param name="part">部件的具体东西，比如部件名是发送机，这里就是发动机的具体型号</param>
        public void Add(string partName, string? partValue)
        {
            this.parts.Add(partName, partValue);
        }

        /// <summary>
        /// 显示建造过程
        /// </summary>
        public void Show()
        {
            foreach(KeyValuePair<string, string?> item in this.parts)
            {
                Console.WriteLine($"组件{item.Key}-{item.Value}已装好");
            }
        }
    }
}