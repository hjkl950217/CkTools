using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace 技术点验证
{
    /*
     * 需要
     * Microsoft.Extensions.Configuration
     * Microsoft.Extensions.DependencyInjection
     *
     */

    [VerifcationType(VerificationTypeEnum.A10_读取文件到配置系统并监控变化)]
    public class A10_读取文件到配置系统并监控变化 : IVerification
    {
        public VerificationTypeEnum VerificationType =>
            VerificationTypeEnum.A10_读取文件到配置系统并监控变化;

        private const string? testFile = "testA10.json";

        public static TestConfig? testConfig = null;//模拟业务代码中的单例引用

        public void Start(string[]? args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                //IConfiguration在构建webhost的时候就确定了

                .AddTestConfiguration(testFile, true)
                .Build();

            IServiceCollection services = new ServiceCollection();

            services.AddTransient(t =>
            {
                IConfiguration? config = t.GetService<IConfiguration>();
                TestConfig? result = JsonConvert.DeserializeObject<TestConfig>(config["ConfigB"]);

                TestConfigurationProvider.Bind(config, "ConfigB", result);

                return result;
            });
            services.AddSingleton(configuration);

            //后面要模拟从IOC中获取对象并能监控的场景

            IServiceProvider di = services.BuildServiceProvider();

            TestConfig? configEntity = di.GetService<TestConfig>();
            testConfig = configEntity;
            Console.WriteLine($"第一次从DI获取到值:{configEntity.TestName}");
        }
    }
}