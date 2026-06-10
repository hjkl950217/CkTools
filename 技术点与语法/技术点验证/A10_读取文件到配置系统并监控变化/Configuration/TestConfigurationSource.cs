using Microsoft.Extensions.Configuration;

namespace 技术点验证
{
    public class TestConfigurationSource : IConfigurationSource
    {
        public TestConfigurationSource(string? fileName, bool reloadOnChange = true)
        {
            this.ConfigFileName = fileName;
            this.ReloadOnChange = reloadOnChange;
        }

        /// <summary>
        /// 是否是可选配置
        /// </summary>
        public bool Optional { get => false; }

        /// <summary>
        /// 配置文件的全名
        /// </summary>
        public string? ConfigAddress { get; set; }

        /// <summary>
        /// 配置发生改变时，是否更改程序中的配置
        /// </summary>
        public bool ReloadOnChange { get; set; }

        /// <summary>
        /// 配置文件名
        /// </summary>
        public string? ConfigFileName { get; set; }

        /// <summary>
        /// 构建配置提供器
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            string? debugAddress = Directory.GetCurrentDirectory();
            this.ConfigAddress = Path.Combine(debugAddress, $"{{{nameof(A10_读取文件到配置系统并监控变化)}}}");

            return new TestConfigurationProvider(this);
        }
    }
}