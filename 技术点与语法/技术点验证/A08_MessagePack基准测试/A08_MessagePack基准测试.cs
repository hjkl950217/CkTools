using BenchmarkDotNet.Running;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A08_MessagePack基准测试)]
    public class A08_MessagePack基准测试 : IVerification
    {
        /*
* 依赖的库
<PackageReference Include="BenchmarkDotNet" Version="0.11.1" />
<PackageReference Include="MessagePack" Version="1.7.3.4" />
<PackageReference Include="MessagePack.AspNetCoreMvcFormatter" Version="1.7.3.4" />
<PackageReference Include="MessagePack.ImmutableCollection" Version="1.7.3.4" />
<PackageReference Include="MessagePackAnalyzer" Version="1.6.0" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Abstractions" Version="2.1.3" />
<PackageReference Include="Microsoft.Extensions.Caching.Memory" Version="2.1.2" />
<PackageReference Include="Microsoft.Extensions.Caching.Redis" Version="2.1.2" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="2.1.1" />

            还可以使用Bogus来造数据
*/

        public void Start(string[]? args)
        {
            Console.WriteLine("测试开始");

            //Summary summary = BenchmarkRunner.Run<TestCase_Serialize>();

            //BenchmarkSwitcher.FromAssembly(typeof(A08_MessagePack基准测试).Assembly).RunAll();
            Type[] types = new Type[]
            {
                typeof(TestCase_Deserialize),
                typeof(TestCase_Serialize)
            };
            BenchmarkSwitcher.FromTypes(types).RunAll();

            //var tt = new TestCase_Serialize();
            //tt.SetUp();
            //tt.TestIntSerialize();
            //tt.Teststring?Serialize();
            //tt.TestNonSerialize();
            //tt.TestDefaultSerialize();
            //tt.TestJsonSerialize();

            //兼容性 测试
            //CompatibilityTest();

            Console.WriteLine("测试结束");
            Console.ReadLine();
        }

        private static void CompatibilityTest()
        {
#pragma warning disable IDE0059 // 不需要赋值
            MsgSerializer.InitializeMsgPackSerializer();//初始化序列化器

            TestEntity_Non? result;

            // 测试intKey To NonKey
            TestEntity_Int test = new TestEntity_Int()
            {
                TestEnum = TestEnum.Close
            };

            result = BaseTest(test);

            //测试匿名类型+枚举值
            dynamic test2 = new { TestEnum = 20 };
            result = BaseTest(test2);

            //测试枚举名-错误
            //dynamic test3 = new { TestEnum = "Close" };
            //result = BaseTest(test3);

            //测试0 1向bool转换--错误
            //dynamic test4 = new { Boolen = 0 };
            //result = BaseTest(test4);

            //测试true向bool转换
            //错误
            //dynamic test5 = new { Boolen = "True" };
            //result = BaseTest(test5);

            //错误
            //dynamic test55 = new { Boolen = "true" };
            //result = BaseTest(test55);

            dynamic test555 = new { Boolen = true };
            result = BaseTest(test555);

            //测试数值类型之间转换

            //错误
            dynamic test6 = new { Integr = 10.00 };
            result = BaseTest(test6);

            //错误
            //dynamic test66 = new { Double = 20.02M };
            //result = BaseTest(test66);

            //错误
            //dynamic test666 = new {  Decimal = 30 };
            //result = BaseTest(test666);

            dynamic test6666 = new { Decimal = 30M };
            result = BaseTest(test6666);

#pragma warning restore IDE0059 // 不需要赋值
        }

        public static TestEntity_Non BaseTest<T>(T obj)
        {
            byte[] byteT = MsgSerializer.Serialize(obj);
            TestEntity_Non result = MsgSerializer.Deserialize<TestEntity_Non>(byteT);
            return result;
        }
    }
}