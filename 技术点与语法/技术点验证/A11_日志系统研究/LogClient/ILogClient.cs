namespace 技术点验证
{
    public interface ILogClient
    {
        /// <summary>
        /// 纪录日志
        /// </summary>
        /// <param name="message"></param>
        void WriteLog(string? message);
    }
}