using BenchmarkDotNet.Attributes;

namespace 技术点验证.A28_字符串做加法
{
    [VerifcationType(VerificationTypeEnum.A28_字符串做加法)]
    public class A28_字符串做加法 : IVerification
    {
        //todo:需要迁移面试题中的代码过来
        public void Start(string[]? args)
        {
            string a = AddOnne.StringAddition("12321425443443", "433222");

            // string a = AddOnne.StringAddition("19", "17");

            string result = "12321425876665";
            //Console.WriteLine("测试开始");
            ////需要在发布模式下测试

            //Type[] types = new Type[]
            //{
            //    typeof(AddTest)
            //};
            //BenchmarkSwitcher.FromTypes(types).RunAll();
        }
    }

    [SimpleJob]
    [ProcessCount(2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
    public class AddTest
    {
        [Params(10 * 1000, 30 * 1000)]
        public int DataCount;

        public List<(string, string)> TestList;

        private List<(string, string)> GetTestDataList()
        {
            List<(string, string)> result = new List<(string, string)>();
            for(int i = 0 ; i < this.DataCount ; i++)
            {
                int lenth = A08TestHelper.random.Next(20);
                char[] tempChars = new char[lenth];
                char[] tempChars2 = new char[lenth];

                //随机一个字符数组
                for(int j = 0 ; j < lenth ; j++)
                {
                    tempChars[j] = A08TestHelper.GetRandomNum();
                    tempChars2[j] = A08TestHelper.GetRandomNum();
                }

                //把随机的字符数组添加到返回结果中
                result.Add((new string(tempChars), new string(tempChars2)));
            }

            return result;
        }

        public string MethodA(string? xStr, string? yStr)
        {
            //if (source == null)
            //{
            //    throw new ArgumentNullException(nameof(source));
            //}
            //else if (source.Length == 0)
            //{
            //    return source;
            //}

            //char[] chars = source.ToCharArray();

            //bool isExtra = symbols.Any(t => chars[chars.Length - 1] == t);
            //if (isExtra == true)
            //{
            //    return new string(chars.Take(chars.Length - 1).ToArray());
            //}
            //else
            //{
            //    return source;
            //}

            return AddOnne.StringAddition(xStr, yStr);
        }

        [Benchmark(Baseline = true)]
        public void MethodA_Master()
        {
            foreach((string, string) item in this.TestList)
            {
                this.MethodA(item.Item1, item.Item2);
            }
        }

        public string MethodB(string? xStr, string? yStr)
        {
            //if (source == null)
            //{
            //    throw new ArgumentNullException(nameof(source));
            //}
            //else if (source.Length == 0)
            //{
            //    return source;
            //}

            //var chars = source.AsSpan();
            //bool isExtra = false;
            //foreach (var item in symbols)
            //{
            //    isExtra = chars[source.Length - 1] == item;
            //    if (isExtra == true) break;
            //}
            //if (isExtra == true)
            //{
            //    return chars.Slice(0, source.Length - 1).ToString();
            //}
            //else
            //{
            //    return source;
            //}
            return "";
        }

        [Benchmark]
        public void MethodB_Master()
        {
            foreach((string, string) item in this.TestList)
            {
                this.MethodB(item.Item1, item.Item2);
            }
        }

        [GlobalSetup]
        public void SetUp()
        {
            this.TestList = this.GetTestDataList();
        }
    }
}