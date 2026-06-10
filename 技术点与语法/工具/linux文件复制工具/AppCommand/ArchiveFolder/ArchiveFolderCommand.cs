using linux文件复制工具.AppCommand.RsyncToQb;
using Spectre.Console.Cli;
using 工具基础核心库.BaseTool.ConfigHelper;
using 工具基础核心库.BaseTool.LogHlper;

namespace linux文件复制工具.AppCommand.ArchiveFolder
{
    public partial class ArchiveFolderCommand : Command<ArchiveFolderSetting>
    {
        public override int Execute(CommandContext context, ArchiveFolderSetting settings)
        {
            //读取配置
            AppSettings appSettings = ConfigHelper.GetConfig<AppSettings>();

            //执行
            LogHelper.Log($"【ArchiveFolder】当前执行时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            new ArchiveFolderRunner(appSettings, settings)
                .Run();

            LogHelper.CloseLog();
            return 1;
        }
    }
}