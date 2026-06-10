using BenchmarkDotNet.Attributes;

namespace 技术点验证.A29_快速分片
{
    [VerifcationType(VerificationTypeEnum.A29_快速分片)]
    public class A29_快速分片 : IVerification
    {
        public void Start(string[]? args)
        {
            //Console.WriteLine("测试开始");
            ////需要在发布模式下测试

            //Type[] types = new Type[]
            //{
            //    typeof(SliceTest)
            //};
            //BenchmarkSwitcher.FromTypes(types).RunAll();
        }
    }

    [SimpleJob]
    [ProcessCount(2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
    public class SliceTest
    {
        #region 准备区

        [Params(10 * 1000, 30 * 1000)]
        public int DataCount;

        public List<string> TestList;

        private List<string> GetTestDataList()
        {
            //todo: 完善切片测试
            return new List<string>();
        }

        [GlobalSetup]
        public void SetUp()
        {
            this.TestList = this.GetTestDataList();
        }

        #endregion 准备区

        #region 测试方法区

        [Benchmark(Baseline = true)]
        public void SliceByToLookup_Test()
        {
        }

        public IEnumerable<IEnumerable<T>> SliceByToLookup<T>(IEnumerable<T> source, int segmentSize = 10)
        {
            T[] queryTemps = source.ToArray();

            List<List<T>> sliceQuerys = Enumerable.Range(1, queryTemps.Length)
                 .ToLookup(k => k / segmentSize, v => queryTemps[v - 1])
                 .Select(g => g.ToList())
                 .ToList();

            return sliceQuerys;
        }

        [Benchmark]
        public void SliceByGroupBy_Test()
        {
        }

        public IEnumerable<IEnumerable<T>> SliceByGroupBy<T>(IEnumerable<T> source, int segmentSize = 10)
        {
            T[] queryTemps = source.ToArray();

            List<List<T>> sliceQuerys = Enumerable.Range(1, queryTemps.Length)
                 .GroupBy(k => k / segmentSize, v => queryTemps[v - 1])
                 .Select(g => g.ToList())
                 .ToList();

            return sliceQuerys;
        }

        [Benchmark]
        public void SliceBySkipAndTake_Test()
        {
        }

        public IEnumerable<IEnumerable<T>> SliceBySkipAndTake<T>(IEnumerable<T> source, int segmentSize = 10)
        {
            int totalCount = (int)Math.Ceiling(source.Count() * 1.0 / segmentSize);
            return Enumerable.Range(0, totalCount)
                .Select(i => source.Skip(segmentSize * i).Take(segmentSize));
        }

        #endregion 测试方法区
    }
}