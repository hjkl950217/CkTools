using 工具基础核心库.AppCommand;
using 工具基础核心库.BaseTool.Executer;

namespace 获取Tracker.AppCommand.CombineOnly
{
    public class CombineOnlyRunner
    {
        private readonly AppSettings appSettings;
        private readonly CombineOnlySetting combineSetting;

        private string[] trackerSourceUrlList = Array.Empty<string>();
        protected readonly ActionExecuter actionExecuter;

        public CombineOnlyRunner(AppSettings appSettings, CombineOnlySetting combineSetting)
        {
            this.actionExecuter = ActionExecuter.Init();
            this.appSettings = appSettings;
            this.combineSetting = combineSetting;

            this.获取所有Tracker源地址();
            this.输出();
        }

        public void Run()
        {
            this.actionExecuter.Execute();
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
                        allLines.AddRange(tempLine);
                    }
                }
                catch(Exception)
                {
                    //这个模式只能在最后抛异常，部分源获取识别时忽略错误，继续获取其他源
                }
            });

            this.trackerSourceUrlList = [.. allLines.Distinct()];
        }

        private void 输出()
        {
            OutputTypeEnum outputTypeEnum = this.combineSetting.OutputType ?? OutputTypeEnum.Console;

            //输出到控制台
            if(outputTypeEnum == OutputTypeEnum.Console)
            {
                foreach(string tracker in this.trackerSourceUrlList)
                {
                    Console.WriteLine(tracker);
                }
            }

            //输出到文本文件
            if(outputTypeEnum == OutputTypeEnum.Txt)
            {
                string outputAddress = this.combineSetting.OutputAddress ?? "allTracker.txt";
                File.WriteAllLines(outputAddress, this.trackerSourceUrlList, BaseRunner.UTF8);
            }
        }
    }
}