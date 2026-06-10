using BenchmarkDotNet.Running;
using 技术点验证._A14_查找相同对象的基准测试_;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A14_查找相同对象的基准测试)]
    public class A14_查找相同对象的基准测试 : IVerification
    {
        /*
* 依赖的库
<PackageReference Include="BenchmarkDotNet" Version="0.11.1" />
*/

        public void Start(string[]? args)
        {
            Console.WriteLine("测试开始");
            //需要在发布模式下测试

            Type[] types = new Type[]
            {
                typeof(TestCompared)
            };
            BenchmarkSwitcher.FromTypes(types).RunAll();
        }
    }
}