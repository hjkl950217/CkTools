using System.Runtime.CompilerServices;

namespace 语法验证.B23_获取调用者的信息
{
    [VerifcationType(VerificationTypeEnum.B23_获取调用者的信息)]
    public class B23_获取调用者的信息 : IVerification
    {
        public void Start(string[]? args)
        {
            this.DumpCallerInfo();
            this.DumpArgumentInfo(args is null);
            this.DumpArgumentInfo2(() => true);
        }

        /// <summary>
        /// 显示调用者的信息
        /// </summary>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <param name="callerMemberName"></param>
        private void DumpCallerInfo(
            [CallerFilePath] string? callerFilePath = null,
            [CallerLineNumber] int? callerLineNumber = null,
            [CallerMemberName] string? callerMemberName = null
        )
        {
            Console.WriteLine("调用信息:");
            Console.WriteLine($@"调用文件路径: {callerFilePath}");
            Console.WriteLine($@"调用代码行: {callerLineNumber}");
            Console.WriteLine($@"调用方法名: {callerMemberName}");
        }

        /// <summary>
        /// 显示参数信息
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <param name="msgParamName"></param>
        /// <exception cref="ArgumentException"></exception>
        private void DumpArgumentInfo(
            bool condition,
            [CallerArgumentExpression(nameof(condition))] string? paramName = null)
        {
            Console.WriteLine($"演示-获取到参数:{paramName}");
        }

        /// <summary>
        /// 显示参数信息
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <param name="msgParamName"></param>
        /// <exception cref="ArgumentException"></exception>
        private void DumpArgumentInfo2(
            object condition,
            [CallerArgumentExpression(nameof(condition))] string? paramName = null)
        {
            Console.WriteLine($"演示-获取到参数2:{paramName}");
        }
    }
}