namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A30_模拟中间件_责任链模式)]
    public class A30_模拟中间件_责任链模式 : IVerification
    {
        public void Start(string[]? args)
        {
            MyApplicationBuilder application = new MyApplicationBuilder();

            application.Use(async (context, next) =>
            {
                Console.WriteLine("进入第1个中间件");
                await next();
                Console.WriteLine("离开第1个中间件");
            });

            application.Use(async (context, next) =>
            {
                Console.WriteLine("进入第2个中间件");
                await next();
                Console.WriteLine("离开第2个中间件");
            });

            application.Run(context => { Console.WriteLine("进入末尾中间件"); return Task.CompletedTask; });
            //application.Run(_ => Console.WriteLine("进入末尾中间件"));

            MockRequestDelegate mockPipe = application.Build();

            mockPipe.Invoke(new MockHttpContext());
        }
    }
}