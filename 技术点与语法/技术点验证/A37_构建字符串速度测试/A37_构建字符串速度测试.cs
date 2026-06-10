using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace 技术点验证.A37_快速填0测试
{
    [VerifcationType(VerificationTypeEnum.A37_构建字符串速度测试)]
    public class A37_构建字符串速度测试 : IVerification
    {
        public void Start(string[]? args)
        {
            //结论见测试代码、测试个数处

            Type[] types = new Type[]
            {
                typeof(BuildString)
            };
            BenchmarkSwitcher.FromTypes(types).RunAll();
        }
    }

    [SimpleJob]
    [ProcessCount(2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
    public class BuildString
    {
        //[Params(10, 100, 1_0000)] // 10个时 stringAdd多百分之30内存开销，但CPU开销低一丁丁。10个以后就都不行了
        [Params(1, 3, 5, 7, 10)]// 7个差不多是拐点，7个字符串以下 string直接加更好一点
        public int DataCount;

        public Random random;

        [GlobalSetup]
        public void SetUp()
        {
            this.random = new Random();
        }

        [Benchmark(Baseline = true)]
        public string UseStringBuilder()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for(int i = 0 ; i < this.DataCount ; i++)
            {
                stringBuilder.Append(this.GetRanmodString());
            }

            return stringBuilder.ToString();
        }

        [Benchmark]
        public string UseStringAdd()
        {
            string result = string.Empty;
            for(int i = 0 ; i < this.DataCount ; i++)
            {
                result += this.GetRanmodString();
            }
            return result;
        }

        private string GetRanmodString()
        {
            return Convert.ToString((byte)this.random.Next(0, 256), 2);
        }
    }
}