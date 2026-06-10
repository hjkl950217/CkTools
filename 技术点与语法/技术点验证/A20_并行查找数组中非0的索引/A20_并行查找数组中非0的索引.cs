using System.Numerics;
using System.Runtime.InteropServices;
using AutoFixture;

namespace 技术点验证
{
    /*
     * 使用了 Autofixture 来造数据
     *
     *
     * 从文章 https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-core-3-0/
     * 中看来的并行代码，还没有读懂
     *
     */

    [VerifcationType(VerificationTypeEnum.A20_并行查找数组中非0的索引)]
    public class A20_并行查找数组中非0的索引 : IVerification
    {
        private readonly byte[] buffer1;

        private readonly byte[] buffer2;

        private readonly byte[] buffer3;

        private readonly Fixture fixture;

        public A20_并行查找数组中非0的索引()
        {
            int countNum = 17;

            this.fixture = new Fixture();

            this.buffer1 = this.CreateTestData<byte>(countNum);

            this.buffer2 = new byte[countNum];
            this.buffer3 = new byte[countNum];

            this.buffer1.AsSpan().CopyTo(this.buffer2);//快速复制一份
            this.buffer1.AsSpan().CopyTo(this.buffer3);
        }

        public void Start(string[]? args)
        {
            int index = this.ParallelMethod();
            Console.WriteLine($"查找的索引为:{index}");
        }

        /// <summary>
        /// 默认方式-遍历
        /// </summary>
        public int DefaultMethod()
        {
            byte[] buffer = this.buffer1;
            for(int i = 0 ; i < buffer.Length ; i++)
            {
                if(buffer[i] != 0)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 并行方式
        /// </summary>
        /// <remarks>
        ///
        /// “向量化”是一种并行化方法，它在单个内核上执行多个操作，作为单个指令的一部分一些优化的编译器可以执行自动矢量化，借此编译器分析循环以确定其是否可以生成功能上等效的代码，这些代码将利用此类指令来更快地运行。.NET JIT编译器当前不执行自动矢量化，但是可以手动对循环进行矢量化，并且在.NET Core 3.0中，用于执行此操作的选项已得到显着改进。就像矢量化看起来像一个简单的示例一样，假设有一个字节数组，想在其中搜索第一个非零字节，然后返回该字节的位置。
        ///
        /// 在64位进程中考虑将字节数组重新解释为long数组，这  Span<T> 很好地支持了这一点。然后，我们一次有效地比较8个字节，而不是一次比较1个字节，这是以增加的代码复杂度为代价的：一旦找到一个非零长，我们就需要查看它包含的每个字节以确定其位置。第一个非零值（尽管也有方法可以改善这一点）。同样，数组的长度可能不会平均除以8，因此我们需要能够处理溢出。
        ///
        ///
        /// </remarks>
        /// <returns></returns>
        public int ParallelMethod()
        {
            byte[] buffer = this.buffer2;
            int remainingStart = 0;

            if(IntPtr.Size == sizeof(long))//平台上的面指针或句柄类型要与long的长度一致 sizeof返回的是类型占用的字节数
            {
                Span<long> longBuffer = MemoryMarshal.Cast<byte, long>(buffer);
                remainingStart = longBuffer.Length * sizeof(long);//转换成longs后，剩余部分的起始索引

                for(int i = 0 ; i < longBuffer.Length ; i++)
                {
                    if(longBuffer[i] != 0)
                    {
                        remainingStart = i * sizeof(long);
                        break;
                    }
                }
            }

            //确定位置
            for(int i = remainingStart ; i < buffer.Length ; i++)
            {
                if(buffer[i] != 0)
                    return i;
            }

            return -1;
        }

        public int VectorMethod()
        {
            byte[] buffer = this.buffer3;
            int remainingStart = 0;

            if(Vector.IsHardwareAccelerated)
            {
                while(remainingStart <= buffer.Length - Vector<byte>.Count)
                {
                    Vector<byte> vector = new Vector<byte>(buffer, remainingStart);
                    if(!Vector.EqualsAll(vector, default))
                    {
                        break;
                    }
                    remainingStart += Vector<byte>.Count;
                }
            }

            for(int i = remainingStart ; i < buffer.Length ; i++)
            {
                if(buffer[i] != 0)
                    return i;
            }

            return -1;
        }

        private T[] CreateTestData<T>(int? count = null)
        {
            return this.fixture.CreateMany<T>(count ?? 20).ToArray();
        }
    }
}