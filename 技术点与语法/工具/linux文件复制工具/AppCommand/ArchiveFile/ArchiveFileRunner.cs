using System.Diagnostics;
using linux文件复制工具.AppCommand.ArchiveFile;
using Newtonsoft.Json;
using 工具基础核心库.AppCommand;
using 工具基础核心库.BaseTool.Executer;
using 工具基础核心库.BaseTool.Extension;
using 工具基础核心库.BaseTool.LogHlper;

namespace linux文件复制工具.ArchiveFile
{
    public class ArchiveFileRunner : BaseRunner
    {
        private readonly AppSettings appSettings;
        private readonly ArchiveFileSetting archiveFileSetting;

        /// <summary>
        /// 定义最新复制文件的修改时间
        /// </summary>
        private DateTime lastModifiedTime;

        public ArchiveFileRunner(AppSettings appSettings, ArchiveFileSetting archiveFileSetting)
        {
            this.appSettings = appSettings;
            this.archiveFileSetting = archiveFileSetting;
            this.lastModifiedTime = DateTime.MinValue;

            base.actionExecuter
                .Pipe(this.复制文件);
        }

        protected override void InternalRun()
        {
            base.actionExecuter.Execute();
        }

        public void 复制文件()
        {
            #region 准备配置参数

            string sourceDir = this.appSettings.ArchiveFile.Source;
            string targetDir = this.appSettings.ArchiveFile.Target;
            long minFileSizeLimit = (long)this.appSettings.MinFileSizeLimit * 1024 * 1024;
            long maxFileSizeLimit = (long)this.appSettings.MaxFileSizeLimit * 1024 * 1024;
            DateTime timeLimit = this.appSettings.ArchiveFile.TimeLimit ?? DateTime.MinValue;
            string[] allowedExtensions = this.appSettings.AllowedExtensions ?? [];
            string[] excludeAddrs = this.appSettings.ArchiveFile.ExcludeAddrs ?? [];

            #endregion 准备配置参数

            #region 复制文件

            int count = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            string[] fileNameArray = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories);
            foreach(string sourceFilePath in fileNameArray)
            {
                FileInfo sourceFileInfo = new(sourceFilePath);
                // string sourceFileName = Path.GetFileName(sourceFilePath);

                string sourceFolderPath = Path.GetDirectoryName(sourceFilePath);
                DirectoryInfo sourceFolderInfo = new(sourceFolderPath);

                string targetFilePath = Path.Combine(targetDir, sourceFileInfo.Name);
                FileInfo targetFileInfo = new(targetFilePath);

                #region 条件判断

                // 判断是否为排除地址，如果不是则跳过
                if(excludeAddrs.Any(t => sourceFilePath.Contains(t)))
                {
                    string message = $"文件 [{sourceFileInfo.Name}] 在排除文件夹中，跳过复制";
                    LogHelper.Log(message, LogTypeEnum.Debug);
                    continue;
                }

                // 判断是否为允许的文件后缀，如果不是则跳过
                string extension = sourceFileInfo.Extension
                    .TrimStart('.')
                    .ToLower();
                if(allowedExtensions.Length > 0 && !allowedExtensions.Contains(extension))
                {
                    string message = $"文件 [{sourceFileInfo.Name}] 不是允许的文件后缀，跳过复制";
                    LogHelper.Log(message, LogTypeEnum.Debug);
                    continue;
                }

                // 判断目标地址下是否已经存在同名文件，如果存在则跳过该文件
                if(File.Exists(targetFilePath))
                {
                    if(targetFileInfo.Length == sourceFileInfo.Length)
                    {
                        string message = $"文件 [{sourceFileInfo.Name}] 已经存在于目标地址，跳过复制";
                        LogHelper.Log(message, LogTypeEnum.Debug);
                        continue;
                    }

                    //遇到同名但大小不一样的文件，需要重命名后计算
                    targetFilePath = Path.Combine(targetDir, sourceFileInfo.GetMD5Name());
                    if(File.Exists(targetFilePath))
                    {
                        string message = $"文件 {sourceFileInfo.GetMD5Name()} 已经存在于目标地址，跳过复制";
                        LogHelper.Log(message, LogTypeEnum.Debug);
                        continue;
                    }
                    else
                    {
                        targetFileInfo = new(targetFilePath);
                    }
                }

                // 判断文件大小是否小于限制，如果小于则跳过该文件
                long sourceFileSize = sourceFileInfo.Length;
                if(sourceFileSize < minFileSizeLimit)
                {
                    string message = $"文件 [{sourceFileInfo.Name}] 小于限制大小，跳过复制";
                    LogHelper.Log(message, LogTypeEnum.Debug);
                    continue;
                }

                // 判断文件大小是否大于限制，如果大于则跳过该文件
                if(sourceFileSize > maxFileSizeLimit)
                {
                    string message = $"文件 [{sourceFileInfo.Name}] 大于限制大小，跳过复制";
                    LogHelper.Log(message, LogTypeEnum.Debug);
                    continue;
                }

                // 判断文件修改时间是否比限制时间新，如果新则进行复制
                DateTime lastTime = sourceFileInfo.GetLastTime(rootAddress: sourceDir);
                if(lastTime <= timeLimit)
                {
                    string message = $"文件 [{sourceFileInfo.Name}] 修改时间早于限制时间，跳过复制";
                    LogHelper.Log(message, LogTypeEnum.Debug);
                    continue;
                }

                #endregion 条件判断

                //复制
                stopwatch.Restart();
                string copyMsg = $"正在复制文件 [{sourceFileInfo.Name}]";
                LogHelper.Log(copyMsg);
                File.Copy(sourceFilePath, targetFilePath);//复制
                File.SetLastWriteTime(targetFilePath, lastTime);//设置时间为最新（因为qb下载时间复制出来时不会更新）
                stopwatch.Stop();
                count++;

                #region 每个文件复制后的处理

                double copyTimeSec = stopwatch.ElapsedMilliseconds / 1000.0;
                double fileSizeMb = new FileInfo(sourceFilePath).Length / 1024.0 / 1024.0;
                double speed = fileSizeMb / copyTimeSec;

                string successMessage = $"成功复制文件 [{sourceFileInfo.Name}],速度: {speed:F2} MiB/s,用时 {copyTimeSec} 秒";
                LogHelper.Log(successMessage);

                // 更新最新复制文件的修改时间为当前文件的修改时间
                if(this.lastModifiedTime < lastTime)
                {
                    this.lastModifiedTime = lastTime;
                }

                #endregion 每个文件复制后的处理
            }

            #endregion 复制文件

            #region 复制任务完成后的处理

            // 如果成功复制过文件，则更新配置文件中的修改时间
            if(this.lastModifiedTime != DateTime.MinValue)
            {
                this.appSettings.ArchiveFile.TimeLimit = this.lastModifiedTime;
                string updatedJson = JsonConvert.SerializeObject(this.appSettings, Formatting.Indented);
                File.WriteAllText("config.json", updatedJson);

                string updateTimeMessage = $"复制完成，成功更新{count}个文件,成功更新配置文件，最新处理时间为 {this.lastModifiedTime:yyyy-MM-dd HH:mm:ss}";
                LogHelper.Log(updateTimeMessage);
            }
            else
            {
                string updateTimeMessage = $"复制结束，未更新文件";
                LogHelper.Log(updateTimeMessage);
            }

            #endregion 复制任务完成后的处理
        }
    }
}