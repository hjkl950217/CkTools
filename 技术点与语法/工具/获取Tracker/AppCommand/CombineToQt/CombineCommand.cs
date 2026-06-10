using Spectre.Console.Cli;
using 工具基础核心库.BaseTool.ConfigHelper;
using 工具基础核心库.BaseTool.LogHlper;
using 获取Tracker.AppCommand.CombineToQt;

namespace 获取Tracker.AppCommand.Combine
{
    public partial class CombineToQtCommand : Command<CombineToQtSetting>
    {
        public override int Execute(CommandContext context, CombineToQtSetting settings)
        {
            //读取配置
            AppSettings appSettings = ConfigHelper.GetConfig<AppSettings>();

            //执行
            LogHelper.Log($"【Combine】当前执行时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            new CombineToQtRunner(appSettings, settings)
                .Run();

            LogHelper.CloseLog();
            return 1;
        }
    }
}