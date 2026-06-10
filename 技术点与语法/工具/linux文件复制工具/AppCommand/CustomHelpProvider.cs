using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Help;
using Spectre.Console.Rendering;

namespace linux文件复制工具.AppCommand
{
    internal class CustomHelpProvider : HelpProvider
    {
        public CustomHelpProvider(ICommandAppSettings settings)
            : base(settings)
        {
        }

        public override IEnumerable<IRenderable> GetHeader(ICommandModel model, ICommandInfo command)
        {
            return new[]
            {
                new Text("--------------------------------------"), Text.NewLine,
                new Text("-----      长空数据迁移工具      -----"), Text.NewLine,
                new Text("--------------------------------------"), Text.NewLine,
                Text.NewLine,
            };
        }
    }
}