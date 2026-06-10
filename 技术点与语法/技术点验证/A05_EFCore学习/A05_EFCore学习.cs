using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A05_EFCore学习)]
    public class A05_EFCore学习 : IVerification
    {
        /*
         * 需要
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.1.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="2.1.4" />
    <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="2.1.1" />
    <PackageReference Include="newtonsoft.json" Version="11.0.2" />
         *
        */

        public void Start(string[]? args)
        {
            /*
             * 教程地址：
             * https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/?view=aspnetcore-2.1
             *
             * 只能参考，他的教程更像是说明文档
             *
             */

            Console.WriteLine("Hello World!");

            #region 模拟注册

            //配置
            KeyValuePair<string?, string?>[] testConfig = new KeyValuePair<string?, string?>[]
                {
               new KeyValuePair<string?, string?>("DefaultConnection",@"Application Name=Lynn;data source=wcmis218\sqlexpress;database=Lynn_Test;user id=sa;password=yhdckGTX007.;Timeout=30;connection lifetime=300; min pool size=2; max pool size=50")
                };

            IServiceCollection services = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(testConfig)
                .Build();

            //模拟注册服务的方法
            services.AddDbContext<SchoolContext>(options =>
                options
                .UseLoggerFactory(A05_EFCore学习.loggerFactory)
                .UseSqlServer(configuration.GetValue<string?>("DefaultConnection"))
            );

            //模拟注册日志工厂
            services.AddSingleton<ILoggerFactory>(A05_EFCore学习.loggerFactory);

            //模拟业务代码注册
            services.AddSingleton<TestCode>();

            ServiceProvider di = services.BuildServiceProvider();
            SchoolContext context = di.GetRequiredService<SchoolContext>();

            DbInitializer.Initalze(context);//这行是初始化数据的

            #endregion 模拟注册

            //模拟业务代码开始
            TestCode testCode = di.GetRequiredService<TestCode>();
            testCode.Run();
        }

        //public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[]
        //{
        //    //new ConsoleLoggerProvider(
        //    //    opt=>
        //    //    {
        //    //         (opt.LogToStandardErrorThreshold==LogLevel.Information)
        //    //        ||(level==LogLevel.Warning)
        //    //        ||(level==LogLevel.Error);
        //    //    }
        //    //    )

        //});

        public static readonly ILoggerFactory loggerFactory = new LoggerFactory();
    }
}