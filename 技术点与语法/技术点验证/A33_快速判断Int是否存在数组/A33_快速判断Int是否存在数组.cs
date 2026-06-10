namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A33_快速判断Int是否存在数组)]
    public class A33_快速判断Int是否存在数组 : IVerification
    {
        //原文：https://mp.weixin.qq.com/s?__biz=MzIzMjQ2MTkyOA==&mid=2247484895&idx=1&sn=bf06f90fcb9aaacacdd2f9d8f5d5ca30&chksm=e895c6d8dfe24fce2d2346389acdc4c2994004ccea5df41207531da4cf6c411e06aa24b315b8&mpshare=1&scene=1&srcid=0824BKqU2JySr6RWOAqvJ0Jg&sharer_sharetime=1598272645948&sharer_shareid=cc609ea2d56165426b8c4ccd3a28d316&key=df4daa9b126aa670a4a8e16b57bceede74995f20944926b4ba33e432c7b91ec2ea22b8c29f943f512d13443ec3681b33eba75db20e25308f25631114d47c534d52ce5f4683d5a9e3993595d64497e2813ec609fda7fafab95907ba345cee28131d9003bc5cd4cfdc389a13083db8a39e11f7dc15b6d6b70442ed27fc337aa837&ascene=1&uin=MTkxNzAzOTk1&devicetype=Windows+10+x64&version=62090529&lang=zh_CN&exportkey=Ad874l8W40zSUERdbcR5%2B4s%3D&pass_ticket=cbigLbzP2Hifc8%2FbDSjPmHj9ruCrjbkV7Rn%2FoRT16dg%3D

        //作者：会点代码的大叔

        public void Start(string[]? args)
        {
            BitMap bm = new BitMap(100);

            bm.Add(1);
            bm.Add(12);
            bm.Add(12);
            bm.Add(14);
            bm.Add(51);
            bm.Add(71);
            bm.Add(100);

            Console.WriteLine("12:" + (bm.Contains(12) ? "存在" : "不存在"));
            Console.WriteLine("13:" + (bm.Contains(13) ? "存在" : "不存在"));
            Console.WriteLine("51:" + (bm.Contains(51) ? "存在" : "不存在"));
            Console.WriteLine("66:" + (bm.Contains(66) ? "存在" : "不存在"));
            Console.WriteLine("100:" + (bm.Contains(100) ? "存在" : "不存在"));
        }
    }

    public class BitMap
    {
        //数据
        private List<byte> bits;

        //最大值
        private int max_value;

        //容量
        private uint capacity;

        public BitMap(int max_value)
        {
            this.max_value = max_value;
            //1bit存储8个数据，存储最大值为 max_value 的数组需要 max_value/8+1 个 byte，除以8就是右移3位
            this.capacity = (uint)((max_value >> 3) + 1);
            this.bits = new List<byte>();

            //扩充数据集
            for(int i = 0 ; i < this.capacity ; i++)
            {
                this.bits.Add(0);
            }
        }

        public BitMap Add(int num)
        {
            if(num > this.max_value)
            {
                this.Expansion(num);
            }

            //数据保存在整个数组的哪个下标中
            Index index = num / 8;
            //数据在这个下标元素的哪个位置
            int position = num % 8;

            this.bits[index] |= (byte)(1 << position);

            return this;
        }

        public bool Contains(int num)
        {
            if(num > this.max_value)
            {
                return false;
            }
            //数据保存在整个数组的哪个下标中
            int index = num / 8;
            //数据在这个下标元素的哪个位置
            int position = num % 8;
            return (this.bits[index] & 1 << position) != 0;
        }

        private void Expansion(int newMaxnum)
        {
            uint newCapacity = (uint)((newMaxnum >> 3) + 1);//需要的新容量
            uint differenceCapacity = newCapacity - this.capacity;//需要扩充的容量

            for(int i = 0 ; i < differenceCapacity ; i++)
            {
                this.bits.Add(0);
            }

            this.max_value = newMaxnum;
            this.capacity = newCapacity;
        }
    }
}