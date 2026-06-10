using System.Text;

namespace 语法验证与学习
{
    //参考资料：https://stackoverflow.com/questions/4628243/is-the-operator-thread-safe
    //线程太少，无法真正观察到++操作符 有锁与无锁的版本

    [VerifcationType(VerificationTypeEnum.B10_I加加的原子性)]
    public class B10_I加加的原子性 : IVerification
    {
        public const int ThreadCount = 10;//线程数
        public static int num = 1;

        public class LockOject
        { public int Num { get; set; } = 1; }

        public static StringBuilder stringBuilder = new StringBuilder();

        public void Start(string[]? args)
        {
            NoLockDemo();
            //LockDemo();
        }

        public static void NoLockDemo()
        {
            Console.WriteLine("无锁版");

            DemoBase(
                () => stringBuilder.Append($"{num++}\r\n"),
                () => num);
        }

        public static void LockDemo()
        {
            Console.WriteLine("有锁版");
            LockOject lockObj = new LockOject();

            void addNum()
            {
                lock(lockObj)
                {
                    stringBuilder.Append($"{lockObj.Num++}\r\n");
                }
            }

            DemoBase(addNum, () => lockObj.Num);
        }

        /// <summary>
        /// demo的基础方法
        /// </summary>
        /// <param name="threadStart">执行方法</param>
        /// <param name="getEndValue">获取显示值的方法</param>
        public static void DemoBase(ThreadStart threadStart, Func<int> getEndValue)
        {
            Thread[] threads = new Thread[ThreadCount];
            for(int i = 0; i < threads.Length; i++)
            {
                Thread thread = new Thread(threadStart)
                {
                    IsBackground = true
                };
                threads[i] = thread;
            }

            for(int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            Thread.Sleep(200);
            Console.WriteLine(stringBuilder.ToString());
            Console.WriteLine($"运行{threads.Length}个线程改变I的值，执行完毕。");
            Console.WriteLine($"I的值\t{getEndValue()}");
        }
    }
}