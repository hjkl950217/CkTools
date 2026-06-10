using Spectre.Console.Cli;
using 工具基础核心库.BaseTool.ConfigHelper;
using 工具基础核心库.BaseTool.LogHlper;

namespace 获取Tracker.AppCommand.CombineOnly
{
    public partial class CombineOnlyCommand : Command<CombineOnlySetting>
    {
        public override int Execute(CommandContext context, CombineOnlySetting settings)
        {
            //读取配置
            AppSettings appSettings = ConfigHelper.GetConfig<AppSettings>();

            //执行
            // LogHelper.Log($"【CombineOnly】当前执行时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            new CombineOnlyRunner(appSettings, settings)
                .Run();

            LogHelper.CloseLog();
            return 1;
        }
    }
}