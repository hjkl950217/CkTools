namespace linux文件复制工具.ArchiveFile.Model
{
    public enum CopyActionEnum
    {
        /// <summary>
        /// 跳过排除地址
        /// </summary>
        SkipBlockAddr,

        /// <summary>
        /// 跳过排除的文件后缀
        /// </summary>
        SkipBlockFileExtension,

        /// <summary>
        ///跳过同名文件，同名但其他不同时重命名
        /// </summary>
        SkipSameFileAndRename,

        /// <summary>
        /// 跳过小于限制的文件
        /// </summary>
        SkipFileSizeLimit,

        /// <summary>
        /// 跳过晚于限制时间的文件
        /// </summary>
        SkipTimeLimit,

        /// <summary>
        /// 复制文件
        /// </summary>
        CopyFile,

        /// <summary>
        /// 设置最后写入时间
        /// </summary>
        SetLastWriteTime,

        /// <summary>
        /// 更新限制时间
        /// </summary>
        UpdateTimelimit,
    }
}