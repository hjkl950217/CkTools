using System.Reflection;
using System.Runtime.InteropServices;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A02_线程ID验证)]
    public class A02_线程ID验证 : IVerification
    {
        public void Start(string[]? args)
        {
            Console.WriteLine("验证开始");

            Task.Run(async () => { await GetA(); });
            Thread.Sleep(1 * 1000);

            Task.Run(() => { GetB(); });
            Thread.Sleep(1 * 1000);

            ShowTheadInfo("Main");

            Console.WriteLine("主代码执行完毕");
        }

        public static async Task GetA()
        {
            ShowTheadInfo("GetA");
            await Task.Run(() => { ShowTheadInfo("GetAAA"); return "A"; });
        }

        public static Task GetB()
        {
            ShowTheadInfo("GetB");
            return Task.Run(() => { return "B"; });
        }

        #region 获取线程信息

        /// <summary>
        /// 获取操作系统线程ID
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        public static int GetNativeThreadId(Thread thread)
        {
            FieldInfo? f = typeof(Thread).GetField("DONT_USE_InternalThread",
                BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);

#pragma warning disable CS8605 // 取消装箱可能为 null 的值。
            IntPtr pInternalThread = (IntPtr)f?.GetValue(thread);
#pragma warning restore CS8605 // 取消装箱可能为 null 的值。
            //var nativeId = Marshal.ReadInt32( pInternalThread , ( IntPtr.Size == 8 ) ? 548 : 348 );
            int nativeId = Marshal.ReadInt32(pInternalThread, (IntPtr.Size == 8) ? 0x022C : 0x0160);
            return nativeId;
        }

        /// <summary>
        /// 获取托管线程ID
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        public static int GetManagedThreadId(Thread thread)
        {
            return thread.ManagedThreadId;
        }

        /// <summary>
        /// 获取托管线程哈希值
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        public static int GetThreadHashCode(Thread thread)
        {
            return thread.GetHashCode();
        }

        /// <summary>
        /// 显示线程信息
        /// </summary>
        public static void ShowTheadInfo(string? methodName)
        {
            Thread nowThead = Thread.CurrentThread;

            //int sysID = GetNativeThreadId( nowThead );
            int managedID = GetManagedThreadId(nowThead);
            int managedHashCode = GetThreadHashCode(nowThead);

            Console.WriteLine($"方法名:{methodName}");
            //Console.WriteLine( $"系统线程ID{sysID}" );
            Console.WriteLine($"托管线程ID:{managedID}");
            Console.WriteLine($"托管线程哈希代码:{managedHashCode}");
            Console.WriteLine("--------------------");
        }

        #endregion 获取线程信息
    }
}