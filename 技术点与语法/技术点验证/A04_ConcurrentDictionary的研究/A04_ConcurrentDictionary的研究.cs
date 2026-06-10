using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A04_ConcurrentDictionary的研究)]
    public class A04_ConcurrentDictionary的研究 : IVerification
    {
        /*
         * BlockingCollection<T> 为实现IProducerConsumerCollection <T>的任何类型提供边界和阻止功能。有关更多信息，请参见BlockingCollection概述。
         *IProducerConsumerCollection<T>  类型必须实现才能在中使用的接口BlockingCollection。
         *
         *
         * ConcurrentDictionary <TKey，TValue>   键值对字典的线程安全实现。
         * ConcurrentQueue<T>  FIFO（先进先出）队列的线程安全实现。
         * ConcurrentStack<T>  LIFO（后进先出）堆栈的线程安全实现。
         *
         */

        // Create a new concurrent dictionary.
        private static readonly ConcurrentDictionary<string, CityInfo> cities = new ConcurrentDictionary<string, CityInfo>();

        public void Start(string[]? args)
        {
            CityInfo[] data =
            {
                new CityInfo()
                {
                    Name = "Boston",
                    Latitude = 42.358769M,
                    Longitude = -71.057806M,
                    RecentHighTemperatures = new int[] {56, 51, 52, 58, 65, 56,53}
                },
                new CityInfo(){ Name = "Miami", Latitude = 25.780833M, Longitude = -80.195556M, RecentHighTemperatures = new int[] {86,87,88,87,85,85,86}},
                new CityInfo(){ Name = "Los Angeles", Latitude = 34.05M, Longitude = -118.25M, RecentHighTemperatures =   new int[] {67,68,69,73,79,78,78}},
                new CityInfo(){ Name = "Seattle", Latitude = 47.609722M, Longitude =  -122.333056M, RecentHighTemperatures =   new int[] {49,50,53,47,52,52,51}},
                new CityInfo(){ Name = "Toronto", Latitude = 43.716589M, Longitude = -79.340686M, RecentHighTemperatures =   new int[] {53,57, 51,52,56,55,50}},
                new CityInfo(){ Name = "Mexico City", Latitude = 19.432736M, Longitude = -99.133253M, RecentHighTemperatures =   new int[] {72,68,73,77,76,74,73}},
                new CityInfo(){ Name = "Rio de Janiero", Latitude = -22.908333M, Longitude = -43.196389M, RecentHighTemperatures =   new int[] {72,68,73,82,84,78,84}},
                new CityInfo(){ Name = "Quito", Latitude = -0.25M, Longitude = -78.583333M, RecentHighTemperatures =   new int[] {71,69,70,66,65,64,61}}
            };

            // Add some key/value pairs from multiple threads.
            Task[] tasks = new Task[2];

            tasks[0] = Task.Run(() =>
            {
                for(int i = 0 ; i < 2 ; i++)
                {
                    if(cities.TryAdd(data[i].Name, data[i]))
                        Console.WriteLine("Added {0} on thread {1}", data[i],
                            Thread.CurrentThread.ManagedThreadId);
                    else
                        Console.WriteLine("Could not add {0}", data[i]);
                }
            });

            tasks[1] = Task.Run(() =>
            {
                for(int i = 2 ; i < data.Length ; i++)
                {
                    if(cities.TryAdd(data[i].Name, data[i]))
                        Console.WriteLine("Added {0} on thread {1}", data[i],
                            Thread.CurrentThread.ManagedThreadId);
                    else
                        Console.WriteLine("Could not add {0}", data[i]);
                }
            });

            // 迄今为止的输出结果。
            Task.WaitAll(tasks);

            // 从应用程序主线程枚举集合。
            // 请注意，ConcurrentDictionary是一个并发集合
            // 不支持线程安全枚举。
            foreach(KeyValuePair<string, CityInfo> city in cities)
            {
                Console.WriteLine("{0} has been added.", city.Key);
            }

            //添加或更新
            AddOrUpdateWithoutRetrieving();

            //读取或添加
            RetrieveValueOrAdd();

            //读取后更新或添加
            RetrieveAndUpdateOrAdd();

            //尝试更新数据
            TryUpdate();

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        //此方法显示如何将键值对添加到字典中
        //在密钥可能已经存在的情况下。
        private static void AddOrUpdateWithoutRetrieving()
        {
            // 一段时间之后。 我们收到来自某些来源的新数据。
            //这是模拟新收到的数据
            CityInfo ci = new CityInfo()
            {
                Name = "Toronto",
                Latitude = 43.716589M,
                Longitude = -79.340686M,
                RecentHighTemperatures = new int[] { 54, 59, 67, 82, 87, 55, -14 }
            };

            //尝试添加数据。
            //如果它不存在，则添加对象ci。
            //如果它已经存在，则根据委托中的自定义逻辑更新存在数据
            cities.AddOrUpdate(
                ci.Name, //字典的key
                ci,//不存在时 新加的对象
                (key, existingVal) =>//存在时的更新数据逻辑
                {
                    //如果这个委托被调用，那么该键已经存在。
                    //在这里，我们确保这个城市真的是我们已有的城市。
                    //(可以使用同名的多个城市作为读者的练习。)
                    //PS：这个委托也就代表存在时如何更新数据，取消或放开注释来观察效果
                    //if(ci != existingVal)
                    //    throw new ArgumentException("不允许重复的城市名称: {0}.", ci.Name);

                    // 唯一可更新的字段是温度数组和上次查询日期。
                    existingVal.LastQueryDate = DateTime.Now;
                    existingVal.RecentHighTemperatures = ci.RecentHighTemperatures;
                    return existingVal;
                });

            // 验证字典是否包含新的或更新的数据。
            UseData(ci, true);
        }

        // 此方法显示如何使用数据并确保它已被添加到字典中。
        private static void RetrieveValueOrAdd()
        {
            string? searchKey = "Caracas";
            CityInfo? retrievedValue = null;

            try
            {
                retrievedValue = cities.GetOrAdd(searchKey, GetDataForCity(searchKey));
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            //使用数据
            //PS：只是为了显示效果而已，其实可以把取得的数据返回了
            if(retrievedValue != null)
            {
                UseData(retrievedValue, true);
            }
        }

        // 此方法显示如何从字典中获取数据并更新，或者添加新数据
        // 当键/值对已存在时，如何更新数据
        // 当键/值对不存在时如何新添一个数据
        private static void RetrieveAndUpdateOrAdd()
        {
            string? searchKey = "Buenos Aires";

            //获取数据成功
            if(cities.TryGetValue(searchKey, out CityInfo retrievedValue))
            {
                // 使用数据
                UseData(retrievedValue);

                // 制作数据的副本。
                //lastQueryDate会自动赋值为当前时间
                CityInfo newValue = new CityInfo(retrievedValue.Name,
                                                retrievedValue.Longitude,
                                                retrievedValue.Latitude,
                                                retrievedValue.RecentHighTemperatures);

                // 用新值替换旧值。
                //PS：这里应该理解为（key,要更新的新值,用来对比的对象）
                if(!cities.TryUpdate(searchKey, retrievedValue, newValue))
                {
                    //数据未更新。 记录错误，抛出异常等
                    Console.WriteLine("不能更新 {0}", retrievedValue.Name);
                }
            }
            else//获取数据失败时添加
            {
                // 添加新的键和值。
                // 这里我们调用一个方法来获取新数据。
                // 另一种选择是在这里添加一个默认值，稍后在其他线程上更新实际数据。
                CityInfo newValue = GetDataForCity(searchKey);
                if(cities.TryAdd(searchKey, newValue))
                {
                    // 使用数据
                    UseData(newValue);
                }
                else
                    Console.WriteLine("无法为{0}添加数据 ", searchKey);
            }
        }

        //假设此方法知道如何为任何指定的城市查找长/纬度/温度信息。
        private static CityInfo GetDataForCity(string name)
        {
            //真正的实施留给读者作为练习。
            //PS:真实的情况可能是从其他数据源中查询数据
            if(string.CompareOrdinal(name, "Caracas") == 0)
                return new CityInfo()
                {
                    Name = "Caracas",
                    Longitude = 10.5M,
                    Latitude = -66.916667M,
                    RecentHighTemperatures = new int[] { 91, 89, 91, 91, 87, 90, 91 }
                };
            else if(string.CompareOrdinal(name, "Buenos Aires") == 0)
                return new CityInfo()
                {
                    Name = "Buenos Aires",
                    Longitude = -34.61M,
                    Latitude = -58.369997M,
                    RecentHighTemperatures = new int[] { 80, 86, 89, 91, 84, 86, 88 }
                };
            else
                throw new ArgumentException("Cannot find any data for {0}", name);
        }

        //演示更新数据操作
        private static void TryUpdate()
        {
            cities.TryGetValue("Toronto", out CityInfo? retrievedValue);

            retrievedValue ??= new CityInfo();

            // 制作数据的副本。
            //lastQueryDate会自动赋值为当前时间
            CityInfo newValue = new CityInfo(retrievedValue.Name,
                                            retrievedValue.Longitude,
                                            retrievedValue.Latitude,
                                            retrievedValue.RecentHighTemperatures);
            newValue.LastQueryDate = Convert.ToDateTime("2010-11-11");

            // 用新值替换旧值。
            //PS：这里应该理解为（key,要更新的新值,用来对比的对象）
            cities.TryUpdate("Toronto", retrievedValue, newValue);
        }

        //模拟使用数据
        private static void UseData(CityInfo showData, bool isDictory = false)
        {
            Console.WriteLine("--Use Data Start--");
            Console.Write("最近{0}的高温: ", showData.Name);

            int[] temps;
            if(isDictory == false)
            {
                temps = showData.RecentHighTemperatures;
            }
            else
            {
                temps = cities[showData.Name].RecentHighTemperatures;
            }

            foreach(int temp in temps)
                Console.Write("{0}, ", temp);
            Console.WriteLine();
            Console.WriteLine("--Use Data End--");
        }
    }

    public class CityInfo : IEqualityComparer<CityInfo>//相等比较器的接口
    {
        public string Name { get; set; }

        /// <summary>
        /// 最后查询时间
        /// </summary>
        public DateTime LastQueryDate { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// 最近高温
        /// </summary>
        public int[] RecentHighTemperatures { get; set; }

        public CityInfo(string name, decimal longitude, decimal latitude, int[] temps)
        {
            this.Name = name;
            this.LastQueryDate = DateTime.Now;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.RecentHighTemperatures = temps;
        }

        public CityInfo()
        {
            this.Name = string.Empty;
            this.RecentHighTemperatures = Array.Empty<int>();
        }

        public CityInfo(string key)
        {
            this.Name = key;
            // MaxValue means "not initialized"
            this.Longitude = Decimal.MaxValue;
            this.Latitude = Decimal.MaxValue;
            this.LastQueryDate = DateTime.Now;
            this.RecentHighTemperatures = new int[] { 0 };
        }

        public bool Equals([AllowNull] CityInfo x, [AllowNull] CityInfo y)
        {
            return x?.Name == y?.Name
                   && x?.Longitude == y?.Longitude
                   && x?.Latitude == y?.Latitude;
        }

        public int GetHashCode(CityInfo obj)
        {
            CityInfo ci = (CityInfo)obj;//（没懂）强转一下，以处理子类
            return ci.Name.GetHashCode();//以name做为判断相同的依据
        }
    }
}