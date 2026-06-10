//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading;

//namespace 技术点验证
//{
//    /*
//     * ConfigurationBuilder需要添加包："Microsoft.Extensions.Configuration"
//     *
//     * AddJsonFile需要添加包："Microsoft.Extensions.Configuration.Json"
//     *
//     * DI-ServiceCollection需要添加包： "Microsoft.Extensions.DependencyInjection"
//     *
//     * AddOptions需要添加包： "Microsoft.Extensions.Options.ConfigurationExtensions"
//     *
//    <PackageReference Include="Microsoft.Extensions.Configuration" Version="2.0.1" />
//    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="2.0.1" />
//    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="2.0.0" />
//    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="2.0.1" />
//     */

//    public class NairHelper
//    {
//        /*
//         * 用户：
//         * 使用字典访问，只管读写数据，不考虑其他
//         *
//         *
//         * 编码：
//         * 1.制作成伪滑动过期
//         * 2.密码统一成MKPL
//         * 3.需要与小A确认下过期时间这个设定，如果需要指定，则不能使用字典的方式
//         * 4.不考虑跨服务，跨服务使用Redis做
//         * 5.帮助类不存数据，都是从nair上取，只是伪装成字典类型
//         *
//         *
//         * 3.1如果不能定死，考虑使用对象的方式。然后值在用户查询时转换成他要的格式
//         *
//         *
//         * 需求确认：
//         * 把每次方法里的databasename   pwd封装掉
//         *
//         * 如果过期时间也不需要指定，指用索引器的方式获取
//         * 需要指定时间时调用方法
//         *
//         * 考虑是不是去扩展他的库，贡献过去
//         *
//         */
//    }

//    internal class Program2
//    {
//        private static void Main(string?[] args)
//        {
//            //创建配置
//            IConfiguration config = new ConfigurationBuilder()
//                .AddInMemoryCollection()//将配置文件的数据加载到内存中
//                .SetBasePath(Directory.GetCurrentDirectory())//设置配置文件所在目录
//                                                             //.AddJsonFile( "nairConfig.json" , optional: true , reloadOnChange: true )//指定加载的配置文件
//                .AddJsonFile("appsettingsTTT.json", optional: true, reloadOnChange: true)
//                .Build(); //编译成对象

//            /*
//             *
//             * var tt = config.GetSection( "NairSDKSettings" );//读取不到
//             * var t2 = config.GetSection( "NairSDKSettings:DefaultCluster" );//有值
//             * var t3 = config[ "NairSDKSettings" ];//读取不到
//             *
//             * 默认会按层级读取，所以上面有层级关系的可以读取
//             * 要按对象读取请按：（以下是nair库里的）
//             *  config.GetSection("NairSDKSettings").Bind(Settings);
//             *
//             *  把数据绑定到Settings这个字段上，修改文件后，Settings也会更新
//             */

//            //创建DI
//            var di = new ServiceCollection()
//               .AddOptions() //注入IOptions<T>，这样就可以在DI容器中获取IOptions<T>了
//                             //.Configure<NairOption>( config )//注入配置数据
//               .AddSingleton<IConfiguration>(config)
//               // .AddSingleton<INairFactory >(new NairFactory( config ) )//添加Nair的IOC关系
//               .AddSingleton<INairFactory, NairFactory>()
//               .BuildServiceProvider();//编译

//            //显示获取的配置
//            //var nairOption = di.GetService<IOptions<NairOption>>();
//            //  Console.WriteLine( nairOption.Value.NairSDKSettings.DefaultCluster );
//            // Console.WriteLine("配置获取成功");

//            //获取Nair客户端
//            var client = di.GetService<INairFactory>().GetNairClient();

//            //写入
//            //存在则执行修改，否则新增
//            string? testA = "AAAA";
//            var testB = new { Name = "BBBB" };
//            client.Put("TestDB", "TestA", testA, 30);
//            client.Put("TestDB", "TestB", testB, 30);
//            client.Put("TestDB", "TestC", 100, 30);

//            Console.WriteLine("写入完成");

//            Thread.Sleep(10 * 1000);

//            //读取
//            // Dictionary<string? , T> Get<T>(string? databasename , List<string?> keys , string? pwd = null);

//            string? strA = client.Get("TestDB", "TestA");//找不到是""
//            var objB = client.Get<object>("TestDB", "TestB");//找不到是null
//            int objc = client.Get<int>("TestDB", "TestC");//找不到是抛异常

//            //目前用集合找  一个都找不到 如果Key存在会抛出异常
//            // List<string?> keyList = new List<string?>() { "TestA" };
//            // Dictionary<string? , string?> objD = client.Get<string?>("TestDB" , keyList);

//            Console.WriteLine(strA);
//            Console.WriteLine("对象：" + objB?.Tostring?());
//            Console.WriteLine("读取完成");

//            //删除
//            client.Remove("TestDB", "TestA");
//            client.Remove("TestDB", "TestB");

//            Console.WriteLine();
//            Console.WriteLine();
//            Console.WriteLine("程序执行完毕");
//            Console.ReadLine();
//        }
//    }
//}