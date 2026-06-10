namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B09_访问者模式)]
    public class B09_访问者模式 : IVerification
    {
        //原始代码摘抄至：大话设计模式之访问者模式

        public void Start(string[]? args)
        {
            new Code1().Run();

            Console.WriteLine();
            new Code2().Run();

            Console.WriteLine();
            new Code3().Run();
        }
    }
}