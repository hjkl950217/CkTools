namespace 工具基础核心库.BaseTool.Extension
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetString(
            this DateTime dateTime,
            string format = "yyyy-MM-dd HH:mm:ss")
        {
            return dateTime.ToString(format);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetString(
            this DateTime? dateTime,
            string format = "yyyy-MM-dd HH:mm:ss")
        {
            return (dateTime ?? DateTime.MinValue).ToString(format);
        }
    }

    public static class StringExtension
    {
        /// <summary>
        /// 判断文件大小是否在范围内
        /// </summary>
        /// <param name="str"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsFileSizeRange(this string str, long min, long max)
        {
            return new FileInfo(str).IsRange(min, max);
        }
    }
}