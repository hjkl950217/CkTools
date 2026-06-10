using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using 工具基础核心库.BaseTool.Extension;

namespace 工具基础核心库.BaseTool.LogHlper
{
    public static class LogHelper
    {
        /// <summary>
        /// 日志配置
        /// </summary>
        public class LogConfig
        {
            /// <summary>
            /// 日志文件-前缀
            /// </summary>
            public string LogFilePrefix { get; set; } = "log_";

            /// <summary>
            /// 日志文件-后缀
            /// </summary>
            public string LogFileSuffix { get; set; } = ".txt";

            /// <summary>
            /// 日志文件夹
            /// </summary>
            public string LogFolder { get; set; } = "logs";

            /// <summary>
            /// 日志文件名（格式化之后，能直接使用）
            /// </summary>
            public Func<string> LogFileName
            {
                get => () => $"{this.LogFilePrefix}{DateTime.Now:yyyy_MM_dd}{this.LogFileSuffix}";
            }

            /// <summary>
            /// 是否为调试模式，环境变量IsDebug=1时为调试模式
            /// </summary>
            public string IsDebug { get; set; } = Environment.GetEnvironmentVariable("IsDebug");

            /// <summary>
            /// 用于控制load效果的取消
            /// </summary>
            public CancellationTokenSource LoadCts { get; set; } = new CancellationTokenSource();
        }

        public static FileStream logFileStream = null;
        public static StreamWriter logFileWriter = null;
        public static LogConfig logConfig = new();
        public static object LockLogObj = new();

        /// <summary>
        /// 日志格式化函数
        /// </summary>
        private static readonly Func<DateTime, LogTypeEnum, string, string> logFormat = (logDate, logType, message) =>
        {
            return $"[{DateTime.Now:HH:mm:ss}\t{logType}\t] {message}";
        };

        #region 创建日志写入器

        private static void PreLogWriter(DateTime logDate)
        {
            // 每天创建一个新的日志文件，如果当前日期与日志日期不同，则关闭当前日志文件并创建一个新的日志文件

            if(logFileWriter == null)
            {
                CreateWriter(DateTime.Now);
                return;
            }
            else if(logDate.Date != DateTime.Today)
            {
                logFileWriter.Close();
                logFileWriter.Dispose();
                logFileWriter = null;

                CreateWriter(logDate);
                return;
            }
        }

        private static void CreateWriter(DateTime? logDate)
        {
            // 创建日志文件夹
            string logFolder = LogHelper.logConfig.LogFolder;
            if(!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            // 计算当前日期的字符串表示
            string logFilePath = Path.Combine(logFolder, LogHelper.logConfig.LogFileName());

            // 如果当天的日志文件不存在，则创建一个新的日志文件
            if(!File.Exists(logFilePath))
            {
                logFileStream = new FileStream(logFilePath, FileMode.CreateNew, FileAccess.Write);
                logFileWriter = new StreamWriter(logFileStream);
            }
            else
            {
                logFileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write);
                logFileWriter = new StreamWriter(logFileStream);
            }
        }

        #endregion 创建日志写入器

        /// <summary>
        /// 关闭日志流写入器
        /// </summary>
        public static void CloseLog()
        {
            //关闭日志流
            logFileWriter?.Close();
            logFileStream?.Close();
        }

        /// <summary>
        /// 清理日志文件
        /// </summary>
        /// <param name="days"></param>
        public static void CleanLog(int days = 30)
        {
            // 获取日志文件夹路径
            string logFolder = LogHelper.logConfig.LogFolder;
            if(!Directory.Exists(logFolder))
            {
                return; // 日志文件夹不存在，无需清理
            }

            // 定义文件名匹配的正则表达式
            string pattern = @$"^{Regex.Escape(LogHelper.logConfig.LogFilePrefix)}(\d{{4}}_\d{{2}}_\d{{2}}){Regex.Escape(LogHelper.logConfig.LogFileSuffix)}$";
            Regex regex = new(pattern);

            DateTime cutoffDate = DateTime.Today.AddDays(-days);

            // 筛选出需要删除的文件
            List<string> logFiles = Directory.GetFiles(logFolder)
                .Where(file =>
                {
                    string fileName = Path.GetFileName(file);
                    Match match = regex.Match(fileName);
                    if(!match.Success)
                    {
                        return false;
                    }

                    string dateStr = match.Groups[1].Value;
                    return DateTime.TryParseExact(dateStr, "yyyy_MM_dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fileDate) && fileDate < cutoffDate;
                })
                .ToList();

            // 删除符合条件的文件
            foreach(string file in logFiles)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"删除日志文件: {file}");
                }
                catch(IOException ex)
                {
                    Console.WriteLine($"删除日志文件 {file} 时出错: {ex.Message}");
                }
            }
        }

        #region Log

        public static void Log(
            string message,
            LogTypeEnum logType = LogTypeEnum.Info)
        {
            DateTime currDateTime = DateTime.Now;
            LogHelper.PreLogWriter(currDateTime);

            if(logType == LogTypeEnum.Debug && LogHelper.logConfig.IsDebug != "1")
            {
                return;
            }

            string logMessage = logFormat(currDateTime, logType, message);

            // 异步写入日志
            _ = Task.Run(() =>
            {
                try
                {
                    //文件日志
                    lock(LockLogObj)
                    {
                        logFileWriter?.WriteLineAsync(logMessage)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult();
                    }

                    //控制台日志
                    Console.WriteLine(logMessage);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"日志写入失败: {ex.Message}");
                }

                return Task.CompletedTask;
            });
        }

        public static void LogError(
            string message,
            Exception ex,
            LogExRange logExRange = LogExRange.All)
        {
            if(ex == null)
            {
                LogHelper.Log(message, LogTypeEnum.Error);
                return;
            }

            #region 按枚举附加错误信息

            StringBuilder stringBuilder = new();

            if(logExRange.IncludeFlags(LogExRange.ErrorMsg))
            {
                stringBuilder.Append(message).AppendLine();
            }

            if(logExRange.IncludeFlags(LogExRange.ErrorSourceExMsg))
            {
                stringBuilder.Append($"异常信息:{ex.Message}").AppendLine();
            }

            if(logExRange.IncludeFlags(LogExRange.ErrorStack))
            {
                stringBuilder.Append($"堆栈:{ex.StackTrace}");
            }

            #endregion 按枚举附加错误信息

            LogHelper.Log(stringBuilder.ToString(), LogTypeEnum.Error);
        }

        public static void LogError(
            string message,
            Exception? ex = null)
        {
            LogHelper.LogError(message, ex, LogExRange.All);
        }

        #endregion Log

        #region 加载效果

        public static void StartLoad(
            string message,
            LogTypeEnum logType = LogTypeEnum.Info)
        {
            CancellationToken ct = LogHelper.logConfig.LoadCts.Token;

            if(logType == LogTypeEnum.Debug && LogHelper.logConfig.IsDebug != "1")
            {
                return;
            }

            string logMessage = logFormat(DateTime.Now, logType, message);

            Console.CursorVisible = false;//隐藏光标
            Console.Write(logMessage);//写入消息
            _ = new ConsoleSpinner().TurnAsync(ct);//显示加载效果

            return;
        }

        public static void StopLoad(
            string endMsg,
            LogTypeEnum logType = LogTypeEnum.Info)
        {
            if(Console.CursorLeft != 0)
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);//将光标移回到上一次的位置
            }

            string logMessage = logFormat(DateTime.Now, logType, endMsg);

            Console.WriteLine();
            Console.WriteLine(logMessage);
            Console.CursorVisible = true;//显示光标
            LogHelper.logConfig.LoadCts.Cancel();
        }

        #endregion 加载效果
    }

    public class ConsoleSpinner
    {
        private int counter;

        /// <summary>
        /// 显示加载效果
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task TurnAsync(CancellationToken ct)
        {
            while(!ct.IsCancellationRequested)
            {
                this.InternalTurn();
                await Task.Delay(100);
            }
        }

        private void InternalTurn()
        {
            this.counter++;
            switch(this.counter % 4)
            {
                case 0:
                    Console.Write("/");
                    this.counter = 0;
                    break;

                case 1:
                    Console.Write("-");
                    break;

                case 2:
                    Console.Write("\\");
                    break;

                case 3:
                    Console.Write("|");
                    break;
            }

            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);//将光标移回到上一次的位置
        }
    }
}