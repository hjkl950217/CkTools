using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Help;
using Spectre.Console.Rendering;

namespace 获取Tracker.AppCommand
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
                new Text("-----      Tracker合并工具      -----"), Text.NewLine,
                new Text("--------------------------------------"), Text.NewLine,
                Text.NewLine,
            };
        }
    }
}