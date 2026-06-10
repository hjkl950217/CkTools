namespace 语法验证与学习
{
    public abstract class Person2
    {
        /// <summary>
        /// 获取反应,也就是受访者的接收方法
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>
        /// 这里用到一种分配技巧：
        /// Visitor类有2种方法，那到底怎么选择呢？子类中只会调用一种方法，new不同的子类代表选择不同的方法。
        /// 这种方法替代了if xxx do xxx的形式。
        ///
        /// 缺点也很明显：外部执行时看到的是父类，和Visitor，他可能不知道这2个方法在何时被选择了。所以访问者模式只适合数据结构及稳定的情况下。
        /// </remarks>
        public abstract void GetConclusion(IVisitor visitor);
    }
}