namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A41_生成WIFI二维码)]
    public class A41_生成WIFI二维码 : IVerification
    {
        public void Start(string[]? args)
        {
            Console.WriteLine("纯记录而已");
            Console.WriteLine("二维码资料:https://github.com/zxing/zxing/wiki/Barcode-Contents#wi-fi-network-config-android-ios-11");
            Console.WriteLine("示例: WIFI:S:Macrowing_2114;T:WPA3-Personal;P:gmp-cd-2114; ");
            Console.WriteLine("有时手机识别到但连接失败,可能是最后多了一个;的原因");
        }
    }
}