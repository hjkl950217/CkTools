namespace 语法验证.B19_字符串驻留池
{
    [VerifcationType(VerificationTypeEnum.B20_框架数据结构测试)]
    public class B20_框架数据结构测试 : IVerification
    {
        //原文：https://www.cnblogs.com/huangxincheng/p/12799736.html

        public void Start(string[]? args)
        {
            LinkAction
              .Start()
              .Add(this.BitArray)
              .Add(this.BitVector32)
              .BatchRun();
        }

        private void BitArray()
        {
            //待处理
            //集合 BitArray 类管理一个紧凑型的位值数组,它使用布尔值来表示
        }

        private void BitVector32()
        {
            // BitVector32 提供了一个简单结构,该结构以32位内存存储布尔和小数值 对于内部使用的布尔值和小整数,BitVector32比BitArray更有效。
            // BitArray 可以按需要无限地扩大，但它有内存和性能方面的系统开销，这是类实例所要求的。 相比之下，BitVector32 只使用 32 位。
        }
    }
}