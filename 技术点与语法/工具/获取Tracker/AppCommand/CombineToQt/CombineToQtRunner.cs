using System.Text;
using 工具基础核心库.AppCommand;
using 工具基础核心库.BaseTool.Executer;
using 工具基础核心库.BaseTool.Extension;
using 工具基础核心库.BaseTool.LogHlper;
using 获取Tracker.AppCommand.Combine;

namespace 获取Tracker.AppCommand.CombineToQt
{
    public class CombineToQtRunner : BaseRunner
    {
        private readonly AppSettings appSettings;

        private string[] trackerSourceUrlList = Array.Empty<string>();

        public CombineToQtRunner(AppSettings appSettings, CombineToQtSetting combineSetting)
        {
            this.appSettings = appSettings;

            this.actionExecuter
               .MayEndPipe(
                    this.检测QT文件是否存在,
                    () => LogHelper.LogError("qt配置文件不存在，退出任务"))
               .Pipe(this.获取所有Tracker源地址)
               .MayEndPipe(
                    this.检测是否获取到Tracker地址,
                    () => LogHelper.LogError("未获取到Tracker地址，退出任务"))
               .Pipe(this.设置QT配置文件)
               ;
        }

        protected override void InternalRun()
        {
            this.actionExecuter.Execute();
        }

        private bool 检测QT文件是否存在()
        {
            return File.Exists(this.appSettings.QtConfigFilePath);
        }

        private void 获取所有Tracker源地址()
        {
            string[] trackerSourceUrlList = this.appSettings.TrackerSourceUrlList;

            // 用于存储所有行的列表
            List<string> allLines = new();

            // 使用HttpClient发送请求
            using HttpClient client = new();
            client.Timeout = TimeSpan.FromSeconds(15);

            Parallel.ForEach(trackerSourceUrlList, url =>
            {
                try
                {
                    // 发送GET请求并获取响应内容
                    string responseText = client.GetStringAsync(url)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                    // 将响应文本拆分成行
                    string[] lines = responseText.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
                    string[] tempLine = lines
                        .Select(t => t.Replace("\"", ""))
                        .ToArray();

                    // 使用锁确保线程安全地将行添加到allLines列表中
                    lock(allLines)
                    {
                        LogHelper.Log($"获取到[\t{tempLine.Length}]条，源：[{url}]");
                        allLines.AddRange(tempLine);
                    }
                }
                catch(Exception ex)
                {
                    LogExRange logExRange = LogExRange.ErrorMsg | LogExRange.ErrorSourceExMsg;
                    LogHelper.LogError($"获取源时遇到异常[{url}]，跳过。", ex, logExRange);
                }
            });

            this.trackerSourceUrlList = [.. allLines.Distinct()];
            LogHelper.Log($"合并Tracker完成，共计 {this.trackerSourceUrlList.Length} 条");
        }

        private bool 检测是否获取到Tracker地址()
        {
            return this.trackerSourceUrlList.Length > 0;
        }

        private void 设置QT配置文件()
        {
            string qtConfigFilePath = this.appSettings.QtConfigFilePath;

            string[] qtConfigLineArray = File.ReadAllLines(qtConfigFilePath, BaseRunner.UTF8);
            int trackerIndex = Array.FindIndex(qtConfigLineArray, x => x.Contains(this.appSettings.KeyName_AppendTracker));

            //准备新内容
            StringBuilder newContentBuilder = new();
            this.trackerSourceUrlList
               .ForEach(url => newContentBuilder.Append(this.appSettings.TrackerFormatTemplet.Replace("{url}", url)));
            string newLineContent = @$"{this.appSettings.KeyName_AppendTracker}={newContentBuilder}";

            //设置回去并回写文件
            qtConfigLineArray[trackerIndex] = newLineContent;
            //string newFileContent = string.Join(Environment.NewLine, qtConfigLineArray);
            File.WriteAllLines(qtConfigFilePath, qtConfigLineArray, BaseRunner.UTF8);
        }
    }
}