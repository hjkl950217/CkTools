using linux文件复制工具.AppCommand;
using linux文件复制工具.AppCommand.ArchiveFolder;
using linux文件复制工具.ArchiveFile;
using Spectre.Console.Cli;
using 工具基础核心库.BaseTool.LogHlper;

namespace linux文件复制工具
{
    public class Program
    {
        public const string separatorMsg = "==============================";

        // 发布命令
        // dotnet publish -c Release -r linux-x64 --self-contained -p:PublishReadyToRun=true -p:PublishSingleFile=true

        public static int Main(string[] args)
        {
#if DEBUG
            Environment.SetEnvironmentVariable("IsDebug", "1");

            //调试代码
            //args = new[] { "pack", "--db-type=MySql", "-i=intput", "-o=output", "-h=192.168.1.241", "-u=eform", "-p=1qaz2WSX1qaz2WSX", "--port=30001", "" };
            //args = new[] { "pack", "--db-type=Dm", "-i=intput", "-o=output", "-h=192.168.1.241", "-u=eform", "-p=1qaz2WSX1qaz2WSX", "--port=30001", "" };
#endif

            LogHelper.CleanLog(); //清理日志
            LogHelper.Log(Program.separatorMsg); //输出分割符

            CommandApp app = new();

            //按命令执行
            ConfigureCommandApp(app);

            return app.Run(args);
        }

        private static void ConfigureCommandApp(CommandApp app)
        {
            app.Configure(config =>
            {
                #region 配置示例

                config.AddExample(["archiveFile"]);
                config.AddExample(["archiveFolder"]);
                // config.AddExample(new[] { "xxx","-x=1","--xx=2"});

                #endregion 配置示例

                #region 配置命令

                config.AddCommand<ArchiveFileCommand>("archiveFile")
                    .WithAlias("packing")
                    .WithDescription("将qb目录的文件进行归档");

                config.AddCommand<ArchiveFolderCommand>("archiveFolder")
                    //.WithAlias("")
                    .WithDescription("将115目录的文件复制到qb下载目录，替换掉qb文件");

                #endregion 配置命令

                config.SetHelpProvider(new CustomHelpProvider(config.Settings));
                config.PropagateExceptions();
                config.ValidateExamples();
            });
        }
    }
}