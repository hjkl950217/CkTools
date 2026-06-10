namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A45_零宽字符加解密)]
    public class A45_零宽字符加解密 : IVerification
    {
        /*
         *
         * 资料： https://zhuanlan.zhihu.com/p/87919817
         * 原始代码实现（懒得勤快）： https://github.com/ldqk/Masuit.Tools/blob/master/Masuit.Tools.Abstractions/Security/ZeroWidthCodec.cs
         *
         */

        public void Start(string[]? args)
        {
            var aaa = "123".InjectZeroWidthString("张三");
            Console.WriteLine(aaa);

            var bbb = aaa.DecodeZeroWidthString();
            Console.WriteLine(bbb);
        }
    }
}