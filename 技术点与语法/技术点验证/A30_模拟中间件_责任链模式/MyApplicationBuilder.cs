namespace 技术点验证
{
    public class MyApplicationBuilder : IMyApplicationBuilder
    {
        private readonly IList<Func<MockRequestDelegate, MockRequestDelegate>> inlineDelegates = new List<Func<MockRequestDelegate, MockRequestDelegate>>();

        public MyApplicationBuilder Use(Func<MockRequestDelegate, MockRequestDelegate> inlineDelegate)
        {
            this.inlineDelegates.Add(inlineDelegate);
            return this;
        }

        public MockRequestDelegate Build()
        {
            MockRequestDelegate result = context => Task.CompletedTask;

            foreach(Func<MockRequestDelegate, MockRequestDelegate>? item in this.inlineDelegates.Reverse())
            {
                result = item(result);
            }
            return result;
        }
    }
}