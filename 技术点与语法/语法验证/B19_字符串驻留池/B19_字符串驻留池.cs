using System.Runtime.InteropServices;

namespace 语法验证.B19_字符串驻留池
{
    [VerifcationType(VerificationTypeEnum.B19_字符串驻留池)]
    public class B19_字符串驻留池 : IVerification
    {
        //原文：https://www.cnblogs.com/huangxincheng/p/12799736.html

        public void Start(string[]? args)
        {
            LinkAction
              .Start()
              .Add(this.Test1)
              .Add(this.Test2)
              .BatchRun();
        }

        public void Test1()
        {
            string a = "123";
            string b = "1" + "2" + "3";
            Console.WriteLine("请输入123,然后按下回车");
            string c = Console.ReadLine();
            string d = new string(new char[] { '1', '2', '3' });

            Console.WriteLine("=============");
            Console.WriteLine(a.GetHashCode());
            Console.WriteLine(b.GetHashCode());
            Console.WriteLine(c.GetHashCode());
            Console.WriteLine(d.GetHashCode());
            Console.WriteLine("引用相等判断 a=b:" + string.ReferenceEquals(a, b));
            Console.WriteLine("引用相等判断 b=c:" + string.ReferenceEquals(b, c));
            Console.WriteLine("引用相等判断 a=c:" + string.ReferenceEquals(a, c));

            Console.WriteLine("内存地址：" + GetMemory(a));
            Console.WriteLine("内存地址：" + GetMemory(b));
            Console.WriteLine("内存地址：" + GetMemory(c));
            Console.WriteLine("内存地址：" + GetMemory(d));
        }

        public void Test2()
        {
            string a = "123";
            string b = "1" + "2" + "3";
            Console.WriteLine("请输入123,然后按下回车");
            string c = string.Intern(Console.ReadLine());
            string d = string.Intern(new string(new char[] { '1', '2', '3' }));

            Console.WriteLine("=============");
            Console.WriteLine(a.GetHashCode());
            Console.WriteLine(b.GetHashCode());
            Console.WriteLine(c.GetHashCode());
            Console.WriteLine(d.GetHashCode());
            Console.WriteLine("引用相等判断 a=b:" + string.ReferenceEquals(a, b));
            Console.WriteLine("引用相等判断 b=c:" + string.ReferenceEquals(b, c));
            Console.WriteLine("引用相等判断 a=c:" + string.ReferenceEquals(a, c));

            Console.WriteLine("内存地址：" + GetMemory(a));
            Console.WriteLine("内存地址：" + GetMemory(b));
            Console.WriteLine("内存地址：" + GetMemory(c));
            Console.WriteLine("内存地址：" + GetMemory(d));
        }

        /// <summary>
        /// 获取引用类型的内存地址方法
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetMemory(object o)
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.Pinned);//为指定对象分配指定类型的句柄。
            IntPtr addr = h.AddrOfPinnedObject();//句柄中对象数据的地址
            return addr.ToString("X");//转换成16进制
        }
    }
}