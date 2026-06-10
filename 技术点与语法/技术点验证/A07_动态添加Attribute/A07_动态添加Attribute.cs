using System.ComponentModel;
using System.Reflection;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace 技术点验证
{
    // 用到了AspectCore.Extensions.DependencyInjection

    [VerifcationType(VerificationTypeEnum.A04_ConcurrentDictionary的研究)]
    public class A07_动态添加Attribute : IVerification
    {
#pragma warning disable CS8602 // 取消引用可能出现的空引用。

        public void Start(string[]? args)
        {
            ServiceProvider di = new ServiceCollection()
             .AddSingleton<TestInterface, TestClass>()
             .ConfigureDynamicProxy()
             .BuildDynamicProxyProvider();//编译

            TestInterface testInterface = di.GetService<TestInterface>();

            TypeDescriptor.AddAttributes(typeof(TestInterface), new TestAttribute());
            AttributeCollection collection = TypeDescriptor.GetAttributes(typeof(TestInterface));
            TestAttribute attr = ((TestAttribute)collection[typeof(TestAttribute)]);

            Type temp = testInterface.GetType();

            Assembly[] allAssembly = AppDomain.CurrentDomain.GetAssemblies();
            Assembly tragetAssembly = allAssembly

                .FirstOrDefault(t => t.FullName.Contains("动态添加Attribute2"));

            IEnumerable<Type> results = from type in tragetAssembly.GetTypes()
                                        where type.IsInterface == true
                                        select type;

            List<List<TestAttribute>> resultAttr = (from type in tragetAssembly.GetTypes()
                                                    where type.FullName.Contains("动态添加Attribute2")
                                                    select type.GetCustomAttributes<TestAttribute>().ToList()
                             )
                             .ToList();

            testInterface.AddName("");

            Console.WriteLine("运行完成");
            Console.ReadLine();
        }

#pragma warning restore CS8602 // 取消引用可能出现的空引用。
    }
}