namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A01_获取当前路径的方法)]
    public class A01_获取当前路径的方法 : IVerification
    {
        public void Start(string[]? args)
        {
            //按情况复制下面的7行代码到你的项目中，测试后再确定使用那一个
            /* 但下面的方法暂时不能解决这种问题：
             *
             * 如果用donet cli 扩展工具在命令行中运行，工具会把项目的dll用反射加载的话。
             * 下面的方法都获取的是工具的地址
             *
             *
             */

            this.Show("获取模块的完整路径", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            this.Show("获取和设置当前目录(该进程从中启动的目录)的完全限定目录", System.Environment.CurrentDirectory);
            this.Show("获取应用程序的当前工作目录", System.IO.Directory.GetCurrentDirectory());
            this.Show("获取程序的基目录", System.AppDomain.CurrentDomain.BaseDirectory);
            this.Show("获取和设置包括该应用程序的目录的名称", System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            //this.Show("获取启动了应用程序的可执行文件的路径", System.Windows.Forms.Application.StartupPath);
            //this.Show("获取启动了应用程序的可执行文件的路径及文件名", System.Windows.Forms.Application.ExecutablePath);

            Console.ReadLine();
        }

        public void Show(string? methodName, string? value)
        {
            Console.WriteLine(methodName);
            Console.WriteLine(value);
            Console.WriteLine();
        }
    }
}