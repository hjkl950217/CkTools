namespace 语法验证与学习
{
    public class Code3
    {
        public void Run()
        {
            VistorManager manager = new VistorManager();
            manager.Attach(new Man2());
            manager.Attach(new Woman2());

            //成功时
            CauseSuccessVistor causeSuccessVistor = new CauseSuccessVistor();
            manager.Display(causeSuccessVistor);

            //失败时
            CauseFailureVistor causeFailureVistor = new CauseFailureVistor();
            manager.Display(causeFailureVistor);

            //恋爱时
            InLoveVistor inLoveVistor = new InLoveVistor();
            manager.Display(inLoveVistor);
        }
    }
}