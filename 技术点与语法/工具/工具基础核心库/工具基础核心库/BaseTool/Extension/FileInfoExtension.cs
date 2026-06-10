using System.Security.Cryptography;

namespace 工具基础核心库.BaseTool.Extension
{
    public static class FileInfoExtension
    {
        private static string GetMd5Hash(this string input)
        {
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        /// <summary>
        /// 获取 文件名，不带后缀
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string GetFileName(this FileInfo fileInfo)
        {
            int lastEnd = fileInfo.Name.LastIndexOf('.');
            return $"{fileInfo.Name[0..lastEnd]}";
        }

        /// <summary>
        /// 获取随机后的文件名，不带后缀
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string GetRandomFileName(this FileInfo fileInfo)
        {
            string seed = fileInfo.FullName.GetMd5Hash()[^4..];
            return $"{fileInfo.GetFileName()}_{seed}";
        }

        /// <summary>
        /// 获取MD5后的文件名，带后缀
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <remarks>用带路径的文件名计算MD5并附加到文件名中</remarks>
        /// <returns></returns>
        public static string GetMD5Name(this FileInfo fileInfo)
        {
            return $"{fileInfo.GetRandomFileName()}{fileInfo.Extension}";
        }

        /// <summary>
        /// 判断文件大小是否在指定范围内
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsRange(this FileInfo fileInfo, long min, long max)
        {
            return fileInfo.Length >= min && fileInfo.Length <= max;
        }

        /// <summary>
        /// 获取文件最后修改时间（如果文件夹的时间比文件新，会返回文件夹的时间）<para></para>
        /// 有些文件系统，修改文件后不会修改文件夹的时间，所以文件夹的时间可能比文件新
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="isIgnoreFolderTime">忽略文件夹时间，true时忽略</param>
        /// <returns></returns>
        public static DateTime GetLastTime(this FileInfo fileInfo, bool isIgnoreFolderTime = false)
        {
            DateTime folderTime = isIgnoreFolderTime
                ? DateTime.MinValue
                : fileInfo.Directory.LastWriteTime;

            DateTime lastTime = new DateTime[]
            {
                folderTime,
                fileInfo.CreationTime,
                fileInfo.LastWriteTime
            }
            .OrderByDescending(t => t)
            .First();

            return lastTime;
        }

        /// <summary>
        /// 获取文件最后修改时间,根目录时不会计算文件夹的时间 <para></para>
        /// 如果文件夹的时间比文件新，会返回文件夹的时）<para></para>
        /// 有些文件系统，修改文件后不会修改文件夹的时间，所以文件夹的时间可能比文件新
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="rootAddress">根目录地址，需要是full地址</param>
        /// <returns></returns>
        public static DateTime GetLastTime(this FileInfo fileInfo, string rootAddress)
        {
            if(fileInfo.Directory.FullName == rootAddress)
            {
                return FileInfoExtension.GetLastTime(fileInfo, true);
            }
            else
            {
                return FileInfoExtension.GetLastTime(fileInfo, false);
            }
        }

        /// <summary>
        /// 获取文件最后修改时间（如果文件夹的时间比文件新，会返回文件夹的时间）<para></para>
        /// 有些文件系统，修改文件后不会修改文件夹的时间，所以文件夹的时间可能比文件新
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static DateTime GetLastTime(this FileInfo fileInfo)
        {
            return FileInfoExtension.GetLastTime(fileInfo, false);
        }

        public static bool TryDelete(this FileInfo fileInfo)
        {
            try
            {
                // 尝试以独占模式打开文件
                using(FileStream fs = File.Open(fileInfo.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                };
                // 文件可以打开，说明没有被其他进程使用，可以安全删除
                fileInfo.Delete();
                return true;
            }
            catch(IOException)
            {
                // 文件正在被其他进程使用，无法删除
                return false;
            }
        }
    }
}