namespace 语法验证与学习
{
    public class CauseSuccessVistor : IVisitor
    {
        public void GetManConclusion()
        {
            Console.WriteLine("男人成功时,背后多半有一个伟大的女人。");
        }

        public void GetWomanConclusion()
        {
            Console.WriteLine("女人成功时,背后大多有一个不成功的男人。");
        }
    }
}