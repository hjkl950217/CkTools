using AutoFixture;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A21_实体方法空间占用)]
    public class A21_实体方法空间占用 : IVerification
    {
        /*
         * 参考 C++虚函数表原理浅析：https://www.cnblogs.com/zhxmdefj/p/11594459.html
         *
         *
         */

        public void Start(string[]? args)
        {
            this.Test(10);
            this.Test(10 * 10);
            this.Test(10 * 100);
            this.Test(10 * 1000);

            Console.WriteLine("结论:实体方法不会影响内存占用");

            //this.Test(10 * 1_0000);
        }

        public void Test(int count)
        {
            System.Console.WriteLine("======================================================");
            System.Console.WriteLine($"执行测试，测试对象个数:{count}");
            System.GC.Collect();

            System.GC.Collect();
            this.Test("--无方法--", count, this.CrteateData);
            System.Console.WriteLine();
            System.GC.Collect();
            this.Test("--使用实体方法--", count, this.CrteateDataByEntity);
            System.Console.WriteLine();
            System.GC.Collect();
            this.Test("--使用扩展方法--", count, this.CrteateDataByExtension);

            System.Console.WriteLine("======================================================");
        }

        public void Test<T>(string? name, int count, Func<int, T[]> testFunc)
        {
            System.Console.WriteLine(name);
            long b1 = this.ShowMemoryUsage();
            T[] result = testFunc(count);
            long b2 = this.ShowMemoryUsage();
            this.ShowDifference(b1, b2);
            System.Console.WriteLine(name);
            System.GC.Collect();
        }

        #region 造数据

        private T[] CreateBase<T>(int count)
        {
            Fixture fixture = new Fixture();

            T[] result = new T[count];
            for(int i = 0 ; i < count ; i++)
            {
                result[i] = fixture.Create<T>();
            }
            return result;
        }

        public A21_TestObj[] CrteateData(int count)
        {
            return this.CreateBase<A21_TestObj>(count);
        }

        public A21_TestObj1[] CrteateDataByEntity(int count)
        {
            return this.CreateBase<A21_TestObj1>(count);
        }

        public A21_TestObj2[] CrteateDataByExtension(int count)
        {
            return this.CreateBase<A21_TestObj2>(count);
        }

        #endregion 造数据

        #region 显示内存占用

        private long ShowMemoryUsage()
        {
            long bytes = System.GC.GetTotalAllocatedBytes(true);

            System.Console.WriteLine($"已经占用的内存：" +
                $"\t{bytes} 字节 " +
                $"\t{bytes / 1024} KB" +
                $"\t{bytes / (1024 * 1024)} MB"
                );

            return bytes;
        }

        private void ShowDifference(long byte1, long byte2)
        {
            long byteEnd = byte2 - byte1;

            System.Console.WriteLine($"相差：\t\t" +
                $"\t{byteEnd} 字节 " +
                $"\t{byteEnd / 1024} KB" +
                $"\t{byteEnd / (1024 * 1024)} MB"
                );
        }

        #endregion 显示内存占用
    }
}