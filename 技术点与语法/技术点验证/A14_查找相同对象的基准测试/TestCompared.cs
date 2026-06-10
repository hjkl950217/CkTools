using System.Net;
using BenchmarkDotNet.Attributes;

namespace 技术点验证._A14_查找相同对象的基准测试_
{
    [SimpleJob]
    [ProcessCount(2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
    public class TestCompared
    {
#pragma warning disable S1450
        private List<StudentTest>? testData;
#pragma warning restore S1450

#pragma warning disable S1104 // Fields should not have public accessibility

        // [Params(200, 1000, 5000, 10000)]
        [Params(5 * 10000, 10 * 10000)]
        public int DataCount;

#pragma warning restore S1104 // Fields should not have public accessibility

        [GlobalSetup]
        public void SetUp()
        {
            this.testData = this.GetTestDataList();
        }

        private List<StudentTest> GetTestDataList()
        {
            List<StudentTest> studentTests = new List<StudentTest>();
            for(int i = 0 ; i < this.DataCount ; i++)
            {
                StudentTest tempStu = new StudentTest()
                {
                    Address = A08TestHelper.GetRandomString(),
                    Code = A08TestHelper.GetRandomEnum<HttpStatusCode>(500, 512),
                    Name = A08TestHelper.GetRandomString(),
                    PhoneNumber = A08TestHelper.random.Next(this.DataCount),
                    Tag = this.DataCount % 2 == 0 ? "01" : "02"
                };
                studentTests.Add(tempStu);
            }

            return studentTests;
        }

        [Benchmark(Baseline = true)]
        public void MethodA_Base()
        {
            string? duplicates = this.testData
                .GroupBy(i => i.Tag)
                .Select(i => i.Count() > 1 ? i.Key : null)
                .FirstOrDefault(i => i != null);
        }

        [Benchmark]
        public void MethodB()
        {
            string? duplicates = this.testData
                .ToLookup(i => i.Tag)//相比MethodA的差异点
                .Select(i => i.Count() > 1 ? i.Key : null)
                .FirstOrDefault(i => i != null);
        }

        [Benchmark]
        public void MethodC()
        {
            //经测试 这个写法性能最好
            string? duplicates = this.testData
                .ToLookup(i => i.Tag)
                .Where(i => i.Key != null && i.Key.Length != 0)
                .FirstOrDefault(t => t.Count() > 1)
                ?.Key;
        }
    }
}