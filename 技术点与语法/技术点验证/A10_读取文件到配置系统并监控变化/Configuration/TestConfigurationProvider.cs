using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace 技术点验证
{
    public class TestConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// 配置源-代表具体配置
        /// </summary>
        public TestConfigurationSource ConfigurationSource { get; set; }

        public IFileProvider FileProvider { get; set; }

        public TestConfigurationProvider(TestConfigurationSource configurationSource)
        {
            this.ConfigurationSource = configurationSource;

            //设置一个目录映射
            this.FileProvider = new PhysicalFileProvider(this.ConfigurationSource.ConfigAddress);

            //注册改变时的事件
            if(configurationSource.ReloadOnChange == true)//配置时说不监控，则不注册了
            {
                ChangeToken.OnChange(
                    () => this.FileProvider.Watch(this.ConfigurationSource.ConfigFileName),
                    () => this.Load());

                /*
                 *  ChangeToken只是传递数据被改变这样的消息
                 *
                 * 第一个参数：配置监控，也就是生产者
                 * 第二个参数：配置接收，也就是订阅者.如果订阅中是调用一个委托对象，则会触发一系列的操作
                 *
                 * 消息产生后，程序怎么处理它是不管的，所以需要自己再读取一次
                 *
                 */
            }
        }

        public override void Load()
        {
            //获取最新的配置数据
            Stream fileStream = this.FileProvider.GetFileInfo(this.ConfigurationSource.ConfigFileName)
               .CreateReadStream();

            IDictionary<string, string?> newDate = this.ReadData(fileStream);

            //更新配置
            foreach(KeyValuePair<string, string?> item in newDate)
            {
                if(base.Data.ContainsKey(item.Key))//存在则更新
                {
                    // var oldValue = base.Data[item.Key];
                    // Console.WriteLine($"老Key:{item.Key} 老值为:{oldValue}");

                    base.Data[item.Key] = item.Value;

                    // Console.WriteLine($"新Key:{item.Key} 新值为:{item.Value}");
                    // Console.WriteLine("-----------------");
                }
                else//不存在则新加
                {
                    base.Data.Add(item.Key, item.Value);
                }
            }

            base.OnReload();//发出信号，表示TestConfigurationProvider的数据已经被改变
            Console.WriteLine($"模拟的单例数据:{A10_读取文件到配置系统并监控变化.testConfig?.TestName}");
        }

        /// <summary>
        /// 从流中读取数据
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private IDictionary<string, string?> ReadData(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string? result = reader.ReadToEnd();

            reader.Close();
            stream.Close();

            return JsonConvert.DeserializeObject<Dictionary<string, string?>>(result);
        }

        public override void Set(string? key, string? value)
        {
            //调用父类方法，走配置系统的流程更新配置
            base.Set(key, value);

            //调用其它方法，更新配置文件
        }

        public override bool TryGet(string key, out string? value)
        {
            /*
             *
             * 在测试中其实不需要，
             * 因为数据源是本地文件。文件改变的时候直接会调回调方法更新数据
             *
             * 但数据源在远端的时候，就需要先判断是不是在内存中，然后再判断是不是在远端了
             *
             */

            return base.TryGet(key, out value);
        }

        public static void Bind<T>(IConfiguration configuration, string? sectionKey, T data)
            where T : TestConfig
        {
            IConfigurationSection section = configuration.GetSection(sectionKey);
            //【1】获取新数据
            TestConfig? newConfig = JsonConvert.DeserializeObject<TestConfig>(section.Value);

            //【2】赋值新数据
            /*
             * 这里不能new,要保证同步数据时引用不会变
             *
             */
            data.TestName = newConfig.TestName;

            //【3】绑定下次的回调
            //IConfigurationSection section = configuration.GetSection(sectionKey);

            section.GetReloadToken()
                .RegisterChangeCallback(
                    o => Bind(configuration, sectionKey, data),
                    null);
        }
    }
}