using BenchmarkDotNet.Attributes;

namespace 技术点验证.A39_结构体类型的ToString
{
    [VerifcationType(VerificationTypeEnum.A39_结构体类型的ToString)]
    public class A39_结构体类型的ToString : IVerification
    {
        public void Start(string[]? args)
        {
        }
    }

    [SimpleJob]
    [ProcessCount(2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
    public class StructToStringTest
    {
        [Params(10 * 1000, 30 * 1000)]
        public int DataCount;

        public List<int> TestDataList_int = new();
        public List<double> TestDataList_double = new();
        public List<float> TestDataList_float = new();
        public List<long> TestDataList_long = new();
        public List<decimal> TestDataList_decimal = new();
        public List<char> TestDataList_char = new();
        public List<byte> TestDataList_byte = new();

        [GlobalSetup]
        public void SetUp()
        {
            Random random = new Random();
            for(int i = 0 ; i < this.DataCount ; i++)
            {
                this.TestDataList_int.Add(random.Next());
                this.TestDataList_double.Add(random.NextDouble());
                this.TestDataList_float.Add((float)random.NextDouble());
                this.TestDataList_long.Add(random.Next());
                this.TestDataList_decimal.Add((decimal)random.NextDouble());
                this.TestDataList_char.Add((char)random.Next(128));
                this.TestDataList_byte.Add((byte)random.Next(256));
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            this.TestDataList_int.Clear();
            this.TestDataList_double.Clear();
            this.TestDataList_float.Clear();
            this.TestDataList_long.Clear();
            this.TestDataList_decimal.Clear();
            this.TestDataList_char.Clear();
            this.TestDataList_byte.Clear();
        }

        #region 测试代码-请每组分开跑

        [Benchmark(Baseline = true)]
        public void TryToString_int<T>(T source)
        {
            foreach(int item in this.TestDataList_int)
            {
                this.TryToString(item);
            }
        }

        [Benchmark]
        public void TryToString2_int<T>(T source)
        {
            foreach(int item in this.TestDataList_int)
            {
                this.TryToString2(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void TryToString_double()
        {
            foreach(int item in this.TestDataList_double)
            {
                this.TryToString(item);
            }
        }

        [Benchmark]
        public void TryToString2_double()
        {
            foreach(int item in this.TestDataList_double)
            {
                this.TryToString2(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void TryToString_float()
        {
            foreach(int item in this.TestDataList_float)
            {
                this.TryToString(item);
            }
        }

        [Benchmark]
        public void TryToString2_float()
        {
            foreach(int item in this.TestDataList_float)
            {
                this.TryToString2(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void TryToString_long()
        {
            foreach(int item in this.TestDataList_long)
            {
                this.TryToString(item);
            }
        }

        [Benchmark]
        public void TryToString2_long()
        {
            foreach(int item in this.TestDataList_long)
            {
                this.TryToString2(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void TryToString_decimal()
        {
            foreach(int item in this.TestDataList_decimal)
            {
                this.TryToString(item);
            }
        }

        [Benchmark]
        public void TryToString2_decimal()
        {
            foreach(int item in this.TestDataList_decimal)
            {
                this.TryToString2(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void TryToString_char()
        {
            foreach(int item in this.TestDataList_char)
            {
                this.TryToString(item);
            }
        }

        [Benchmark]
        public void TryToString2_char()
        {
            foreach(int item in this.TestDataList_char)
            {
                this.TryToString2(item);
            }
        }

        [Benchmark(Baseline = true)]
        public void TryToString_byte()
        {
            foreach(int item in this.TestDataList_byte)
            {
                this.TryToString(item);
            }
        }

        [Benchmark]
        public void TryToString2_byte()
        {
            foreach(int item in this.TestDataList_byte)
            {
                this.TryToString2(item);
            }
        }

        #endregion 测试代码-请每组分开跑

        public string TryToString<T>(T source)
        {
#pragma warning disable CS8603 // 可能的 null 引用返回。
#pragma warning disable CS8602 // 解引用可能出现空引用。
            return default(T).Equals(source)
                ? string.Empty
                : source.ToString();
#pragma warning restore CS8602 // 解引用可能出现空引用。
#pragma warning restore CS8603 // 可能的 null 引用返回。
        }

        public string TryToString2<T>(T source)
        where T : struct
        {
#pragma warning disable CS8603 // 可能的 null 引用返回。
            return source switch
            {
                int i => i switch { default(int) => string.Empty, _ => i.ToString() },
                double d => d switch { default(double) => string.Empty, _ => d.ToString() },
                float f => f switch { default(float) => string.Empty, _ => f.ToString() },
                long l => l switch { default(long) => string.Empty, _ => l.ToString() },
                decimal de => de switch { default(decimal) => string.Empty, _ => de.ToString() },
                _ => source.ToString()
            };
#pragma warning restore CS8603 // 可能的 null 引用返回。
        }
    }
}