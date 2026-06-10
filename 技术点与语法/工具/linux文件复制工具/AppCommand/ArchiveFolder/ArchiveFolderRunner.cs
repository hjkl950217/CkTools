using 工具基础核心库.AppCommand;
using 工具基础核心库.BaseTool.Executer;
using 工具基础核心库.BaseTool.Extension;
using 工具基础核心库.BaseTool.LogHlper;

namespace linux文件复制工具.AppCommand.RsyncToQb
{
    public class ArchiveFolderRunner : BaseRunner
    {
        /// <summary>
        /// 路径分隔符
        /// </summary>
        private static char[] addressSeparator = ['\\', '/'];

        private readonly AppSettings appSettings;
        private readonly ArchiveFolderSetting archiveFolderSetting;

        private string[] folder_115Down = Array.Empty<string>();
        private string[] folder_qb = Array.Empty<string>();

        public ArchiveFolderRunner(AppSettings appSettings, ArchiveFolderSetting archiveFolderSetting)
        {
            this.appSettings = appSettings;
            this.archiveFolderSetting = archiveFolderSetting;

            base.actionExecuter
               .Pipe(this.整体复制文件夹);
        }

        protected override void InternalRun()
        {
            base.actionExecuter.Execute();
        }

        private void 整体复制文件夹()
        {
            #region 准备配置参数

            string sourceDir = this.appSettings.ArchiveFolder.SourceAddr;
            string targetDir = this.appSettings.ArchiveFolder.TargetAddr;

            #endregion 准备配置参数

            #region 复制文件

            //【1】获取原文件夹下的所有文件
            string[] sourceFileNameArray = Directory.GetFiles(this.appSettings.ArchiveFolder.SourceAddr, "*.*", SearchOption.AllDirectories);

            //【2】根据文件名分组，路径层级相同的在一组（统计路径分割符的数量）
            IOrderedEnumerable<IGrouping<int, string>> sortFileNameArray = sourceFileNameArray
                .GroupBy(t => t.Count(c => Path.DirectorySeparatorChar == c))
                .OrderByDescending(t => t.Key);

            //【3】按顺序执行，每一组内可以并行执行复制
            sortFileNameArray.AsParallel()
                .Select(fileNameArray =>
                {
                    foreach(string item in fileNameArray)
                    {
                        this.复制单个文件(item);
                    }

                    return string.Empty;
                })
                .ToArray();

            #endregion 复制文件
        }

        private void 复制单个文件(string sourceFileName)
        {
            #region 准备配置参数

            long minFileSizeLimit = (long)this.appSettings.MinFileSizeLimit * 1024 * 1024;
            long maxFileSizeLimit = (long)this.appSettings.MaxFileSizeLimit * 1024 * 1024;
            string[] allowedExtensions = this.appSettings.AllowedExtensions ?? [];

            #endregion 准备配置参数

            FileInfo sourceFileInfo = new(sourceFileName);
            FileInfo targetFileInfo = new(sourceFileName.Replace(
                this.appSettings.ArchiveFolder.SourceAddr,
                this.appSettings.ArchiveFolder.TargetAddr));

            #region 条件判断

            // 判断文件夹不存在，则创建
            if(!targetFileInfo.Directory.Exists)
            {
                string message = $"创建文件夹[{targetFileInfo.DirectoryName}]";
                LogHelper.Log(message, LogTypeEnum.Debug);

                Directory.CreateDirectory(targetFileInfo.DirectoryName);
            }

            // 判断文件后缀名
            string extension = sourceFileInfo.Extension
                .TrimStart('.')
                .ToLower();
            if(allowedExtensions.Length > 0 && !allowedExtensions.Contains(extension))
            {
                string message = $"文件 [{sourceFileInfo.Name}] 不是允许的文件后缀，跳过复制";
                LogHelper.Log(message, LogTypeEnum.Debug);
                return;
            }

            // 判断文件大小是否小于限制，如果小于则跳过该文件
            long sourceFileSize = sourceFileInfo.Length;
            if(sourceFileSize < minFileSizeLimit)
            {
                string message = $"文件 [{sourceFileInfo.Name}] 小于限制大小，跳过复制";
                LogHelper.Log(message, LogTypeEnum.Debug);
                return;
            }

            // 判断文件大小是否大于限制，如果大于则跳过该文件
            if(sourceFileSize > maxFileSizeLimit)
            {
                string message = $"文件 [{sourceFileInfo.Name}] 大于限制大小，跳过复制";
                LogHelper.Log(message, LogTypeEnum.Debug);
                return;
            }

            // 判断文件时间是否较晚，如果源文件比目标文件旧则跳过该文件
            if(sourceFileInfo.GetLastTime() < targetFileInfo.GetLastTime())
            {
                string message = $"文件 [{sourceFileInfo.Name}] 比文件晚，跳过复制";
                LogHelper.Log(message, LogTypeEnum.Debug);
                return;
            }

            #endregion 条件判断

            //复制文件
            File.Copy(sourceFileName, targetFileInfo.FullName, true);
            File.SetLastWriteTime(targetFileInfo.FullName, sourceFileInfo.LastWriteTime);//设置时间为源文件时间

            string successMessage = $"成功复制文件 [{sourceFileInfo.Name}]";
            LogHelper.Log(successMessage);

            //删除文件
            FileInfo[] deleteFileNameArray = targetFileInfo.Directory.GetFiles("*.!qb", SearchOption.AllDirectories);
            foreach(FileInfo item in deleteFileNameArray)
            {
                item.TryDelete();
            }
        }
    }
}

/*
    整体复制文件思路，从A复制到B：
    1. 扫描A文件夹下所有的文件（包括子文件夹中的文件）
    2. 根据文件名分组，路径层级相同的在一组（统计路径分割符的数量）
    3. 将组名排序，数字小的排在前面
    4. 按顺序执行，每一组内可以并行执行复制
    5. 复制前获取文件名中的路径，判断文件夹是否存在，不存在则创建一个
    6. 然后再复制文件
 */