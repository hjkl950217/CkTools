namespace 技术点验证
{
    public interface IMyApplicationBuilder
    {
        MockRequestDelegate Build();

        MyApplicationBuilder Use(Func<MockRequestDelegate, MockRequestDelegate> inlineDelegate);
    }
}