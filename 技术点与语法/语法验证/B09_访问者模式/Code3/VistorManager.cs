namespace 语法验证与学习
{
    /// <summary>
    /// 访问者管理器
    /// </summary>
    /// <remarks>
    /// Vistor可以认为是不同的状态，使用的时候并不是使用这些状态。
    ///
    /// 而是把状态传递给不同的受访者来进行互动。具体的状态->管理器->触发不同受访者的反应
    ///
    /// </remarks>
    internal class VistorManager
    {
        private readonly List<Person2> persons = new List<Person2>();

        /// <summary>
        /// 附加上受访者
        /// </summary>
        /// <param name="visitor"></param>
        public void Attach(params Person2[] person2s)
        {
            this.persons.AddRange(person2s);
        }

        /// <summary>
        /// 移除访问者
        /// </summary>
        /// <param name="person2s"></param>
        public void Detach(params Person2[] person2s)
        {
            var detachObjs = (from a in this.persons
                              from b in person2s
                              where a.GetType() == b.GetType()
                              select a).ToList();

            detachObjs.ForEach(t => this.persons.Remove(t));
        }

        /// <summary>
        /// 显示反应
        /// </summary>
        /// <param name="visitor"></param>
        public void Display(IVisitor visitor)
        {
            foreach(var item in this.persons)
            {
                item.GetConclusion(visitor);
            }
        }

        //static IEnumerable<int> InfiniteEvenNumbersSet()
        //{
        //    for (int i = 0 ; ; i++)
        //        if (i % 2 == 0) yield return i;
        //}

        //static <int>InfiniteEvenNumbersSet()
        //{
        //    for (int i = 0 ; ; i++)
        //        if (i % 2 == 0)  return i;
        //}
    }
}