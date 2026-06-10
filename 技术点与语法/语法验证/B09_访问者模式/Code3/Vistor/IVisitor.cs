namespace 语法验证与学习
{
    public interface IVisitor
    {
        /// <summary>
        /// 获取男人的反应
        /// </summary>
        void GetManConclusion();

        /// <summary>
        /// 获取女人的反应
        /// </summary>
        void GetWomanConclusion();
    }
}