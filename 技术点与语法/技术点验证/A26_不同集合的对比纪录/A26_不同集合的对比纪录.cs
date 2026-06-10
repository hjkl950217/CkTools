using System.Collections.ObjectModel;
using CkTools.Abstraction.ConstAndEnum;
using CkTools.Helper;

namespace 技术点验证.A26_不同集合的对比纪录
{
    [VerifcationType(VerificationTypeEnum.A26_不同集合的对比纪录)]
    public class A26_不同集合的对比纪录 : IVerification
    {
        public void Start(string[]? args)
        {
            Console.WriteLine("运行起来只有验证代码,结论和分析在代码中");

            this.HashSet_TestHashHowTime();

            #region 集合

            //List
            /*
             * 使用最多的集合，在添加、删除、查找、易用性上取得了一个折中点。
             * 有特殊性能要求时，请结合实际情况使用其它类型
             *
             *
             */

            //HashSet
            /*
             * 哈希表
             *
             * 问题：何时哈希？频率如何？ 每次哈希？开始时哈希？使用时仅哈希一次？
             * 验证结果： 在调用添加方法时计算哈希。 修改数据时，时间和空间性增长
             * 与List的修改对比：https://stackoverflow.com/questions/150750/hashset-vs-list-performance
             */

            #endregion 集合

            #region 字典

            //HybridDictionary
            /*
             * 混合字典。在数据量小的时候使用 ListDictionary，在数据量大时使用 Hashtable
             *
             *
             */

            //ListDictionary
            //Hashtable
            //ConcurrentDictionary
            /*
             * Dictionary的多线程版本，可以认为是线程安全的。要求特别高的情况下未验证
             * 与Dictionary的对比：在多线程情况下，添加KV时线程安全，读取性能差不多。
             *
             *
             */

            //ReadOnlyDictionary
            /*
             * 只读的Dictionary，仅针对Key.如果对Value的修改无效
             *
             */

            #endregion 字典
        }

        public void HashSet_TestHashHowTime()
        {
            HashSet<TestString> hashSet = new HashSet<TestString>();
            Console.WriteLine("创建HashSet完成");

            hashSet.Add(TestHelper.Mock<TestString>());
            Console.WriteLine("添加HashSet完成");
            hashSet.Add(TestHelper.Mock<TestString>());
            Console.WriteLine("添加HashSet完成");
            hashSet.Add(TestHelper.Mock<TestString>());
            Console.WriteLine("添加HashSet完成");
        }

        public void ReadOnlyDictionary_TestValueChanage()
        {
            Dictionary<int, int> rodSource = new Dictionary<int, int>()
            {
                {1,1 },{2,2}
            };

            ReadOnlyDictionary<int, int> rod1 = new ReadOnlyDictionary<int, int>(rodSource);
        }
    }

    public class TestString
    {
        public string Value { get; set; }

        public override int GetHashCode()
        {
            Console.WriteLine("GetHashCode-Time: " + DateTime.Now.ToString(DateTimeFormatConst.Standard));
            return this.Value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return this.Value.Equals(obj);
        }
    }
}