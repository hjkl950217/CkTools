namespace 语法验证与学习
{
    public class CauseFailureVistor : IVisitor
    {
        public void GetManConclusion()
        {
            Console.WriteLine("男人失败时,闷头喝酒");
        }

        public void GetWomanConclusion()
        {
            Console.WriteLine("女人失败时,眼泪汪汪");
        }
    }
}