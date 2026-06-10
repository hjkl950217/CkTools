using System.ComponentModel;
using Spectre.Console.Cli;

namespace 获取Tracker.AppCommand.CombineOnly
{
    public enum OutputTypeEnum
    {
        /// <summary>
        /// 控制台
        /// </summary>
        Console,

        /// <summary>
        /// 文件
        /// </summary>
        Txt,
    }

    public class CombineOnlySetting : CommandSettings
    {
        /// <summary>
        /// 输出类型
        /// </summary>
        [CommandOption("-t|--type")]
        [Description($"输出类型，默认{nameof(OutputTypeEnum.Console)}")]
        public OutputTypeEnum? OutputType { get; set; }

        /// <summary>
        /// 输出类型
        /// </summary>
        [CommandOption($"-o|--outputAddress")]
        [Description("输出地址，输出到文件时使用,不传时默认输出到程序目录")]
        public string? OutputAddress { get; set; }
    }
}