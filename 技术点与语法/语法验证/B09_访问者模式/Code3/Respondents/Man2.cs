namespace 语法验证与学习
{
    public class Man2 : Person2
    {
        public override void GetConclusion(IVisitor visitor)
        {
            visitor.GetManConclusion();
        }
    }
}