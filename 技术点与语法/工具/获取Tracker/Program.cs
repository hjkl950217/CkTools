using Spectre.Console;
using Spectre.Console.Cli;
using 工具基础核心库.BaseTool.LogHlper;
using 获取Tracker.AppCommand;
using 获取Tracker.AppCommand.Combine;
using 获取Tracker.AppCommand.CombineOnly;

namespace 获取Tracker
{
    public class Program
    {
        public const string separatorMsg = "==============================";

        // 发布命令
        // dotnet publish -c Release -r linux-x64 --self-contained -p:PublishReadyToRun=true -p:PublishSingleFile=true
        // dotnet publish -c Release -r win-x64 --self-contained -p:PublishReadyToRun=true -p:PublishSingleFile=true
        public static int Main(string[] args)
        {
#if DEBUG
            Environment.SetEnvironmentVariable("IsDebug", "1");

            //调试代码
            //args = new[] { "-h" };
            //args = new[] { "combineToQt" };
            //args = new[] { "pack", "--db-type=Dm", "-i=intput", "-o=output", "-h=192.168.1.241", "-u=eform", "-p=1qaz2WSX1qaz2WSX", "--port=30001", "" };
#endif

            LogHelper.CleanLog(); //清理日志
            //LogHelper.Log(Program.separatorMsg); //输出分割符

            CommandApp app = new();

            //配置命令
            ConfigureCommandApp(app);

            //执行
            try
            {
                return app.Run(args);
            }
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                return -99;
            }
        }

        private static void ConfigureCommandApp(CommandApp app)
        {
            app.Configure(config =>
            {
                #region 配置示例

                //config.AddExample(new[] { "combineToQt" });
                // config.AddExample(new[] { "xxx","-x=1","--xx=2"});

                #endregion 配置示例

                #region 配置命令

                config.AddCommand<CombineToQtCommand>("combineToQt")
                    .WithExample(["combineToQt"])
                    .WithAlias("ctqt")
                    .WithDescription("\t|别名:ctqt|\t合并后替换qt配置");

                config.AddCommand<CombineOnlyCommand>("combineOnly")
                    .WithExample(["combineOnly"])
                    .WithExample(["combineOnly", "-t=console"])
                    .WithExample(["combineOnly", "-t=txt", "-o=.\\output\\tracker.txt"])
                    .WithAlias("co")
                    .WithDescription("\t|别名:co|\t合并后输出");

                #endregion 配置命令

                #region 设置异常处理器

                #endregion 设置异常处理器

                config.SetHelpProvider(new CustomHelpProvider(config.Settings));
                //config.PropagateExceptions();//输出原始异常
                config.ValidateExamples();
            });
        }
    }
}