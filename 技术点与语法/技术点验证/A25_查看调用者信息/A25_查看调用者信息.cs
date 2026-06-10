using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace 技术点验证.A25_查看调用者信息
{
    [VerifcationType(VerificationTypeEnum.A25_查看调用者信息)]
    public class A25_查看调用者信息 : IVerification
    {
        public void Start(string[]? args)
        {
            System.Diagnostics.Trace.Listeners.Add(new TextWriterTraceListener(System.Console.Out));

            TraceMessage();
            TraceMessage();
            TraceMessage();
        }

        public static void TraceMessage(string? message = null,
            [CallerMemberName] string? memberName = "",
            [CallerFilePath] string? sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            Trace.Indent();

            Trace.WriteLine("message: " + message);
            Trace.WriteLine("member name: " + memberName);
            Trace.WriteLine("source file path: " + sourceFilePath);
            Trace.WriteLine("source line number: " + sourceLineNumber);
            Trace.Unindent();
        }
    }
}