using System.Collections.Concurrent;

namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B18_ConcurrentDictionary原子操作)]
    public class B18_ConcurrentDictionary原子操作 : IVerification
    {
        /*
         * ConcurrentDictionary的GetOrAdd和AddOrUpdate不是原子操作！
         * 资料：https://docs.microsoft.com/en-us/dotnet/standard/collections/thread-safe/how-to-add-and-remove-items
         * 原因：
         * 为了防止传递的委托执行时间太长，导致所有引用并行字段的线程卡死，委托的执行是放在锁外面的。
         * 然后并行字典读取是无锁状态。
         * 如果构建方法执行开销大一点，会导致读取读取到的数据不一致
         *
         * 缓解办法1：使用Lazy
         * 原理：Lazy的构建开销比委托执行的开销低，以少换多。lazy容器依然可能被构建多次，但是在调用value属性前都没有执行委托
         * ，这样总体开销降低
         * 存在缺陷：GetOrAdd方法中，会先塞一个null值进去，仍然可能被读取到null。 AddOrUpdate没有这个问题
         *
         * 缓解办法2：先TryGetValue，获取不到时再调用GetOrAdd
         * 参考：https://stackoverflow.com/questions/9900814/how-can-i-tell-concurrentdictionary-getoradd-to-not-add-a-value
         * 原理：未知，猜测是利用TryGetValue是线程安全的特点
         * 存在缺陷：回答者说委托仍然会被调用多次，但能确保返回结果一直是一个。原因也还没搞懂
         *
         * 缓解办法3：对GetOrAdd的调用直接加lock
         * 原理：强制让GetOrAdd操作变成单线程操作，但开销会大一些。除非其它地方不使用GetOrAdd方法
         * 存在缺陷：开销大，而且违背使用并行字典的目的。特殊情况下可以使用，比如修改为Dictionary没有时间，但又要快速解决bug
         *
         */

        public void Start(string[]? args)
        {
            LinkAction.Start()
                .Add(this.Normal)
                .Add(this.UseLazy)
                .BatchRun();
        }

        #region 普通

        private static int runCount = 0;

        private static readonly ConcurrentDictionary<string, string> cache
            = new ConcurrentDictionary<string, string>();

        public void Normal()
        {
            Task task1 = Task.Run(() => ShowValue("第一个值"));
            Task task2 = Task.Run(() => ShowValue("第二个值"));
            Task.WaitAll(task1, task2);

            ShowValue("第三个值");

            Console.WriteLine($"总共运行: {runCount}");
        }

        public static void ShowValue(string value)
        {
            string valueFound = cache.GetOrAdd(
                key: "key",
                valueFactory: _ =>
                {
                    Interlocked.Increment(ref runCount);
                    Thread.Sleep(10);
                    return value;
                });

            Console.WriteLine(valueFound);
        }

        #endregion 普通

        #region 使用Lazy

        private static int runCount2 = 0;

        private static readonly ConcurrentDictionary<string, Lazy<string>> cache2
            = new ConcurrentDictionary<string, Lazy<string>>();

        public void UseLazy()
        {
            Task task1 = Task.Run(() => ShowValue2("第一个值"));
            Task task2 = Task.Run(() => ShowValue2("第二个值"));
            Task.WaitAll(task1, task2);

            ShowValue2("第三个值");

            Console.WriteLine($"总共运行: {runCount2}");
        }

        public static void ShowValue2(string value)
        {
            Lazy<string> valueFound = cache2.GetOrAdd(
                key: "key",
                valueFactory: _ => new Lazy<string>(
                    () =>
                    {
                        Interlocked.Increment(ref runCount2);
                        Thread.Sleep(100);
                        return value;
                    })
                );
            Console.WriteLine(valueFound.Value);
        }

        #endregion 使用Lazy
    }
}