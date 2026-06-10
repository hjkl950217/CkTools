namespace 语法验证与学习
{
    public class InLoveVistor : IVisitor
    {
        public void GetManConclusion()
        {
            Console.WriteLine("男人恋爱时,凡事不懂也要装懂");
        }

        public void GetWomanConclusion()
        {
            Console.WriteLine("女人恋爱时,遇事懂也装作不懂");
        }
    }
}