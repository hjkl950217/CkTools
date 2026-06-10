namespace 工具基础核心库.BaseTool.LogHlper
{
    /// <summary>
    /// 记录日志范围
    /// </summary>
    [Flags]
    public enum LogExRange
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = LogExRange.ErrorMsg | LogExRange.ErrorSourceExMsg | LogExRange.ErrorStack,

        /// <summary>
        /// 错误信息
        /// </summary>
        ErrorMsg = 0x1,

        /// <summary>
        /// 原始错误信息
        /// </summary>
        ErrorSourceExMsg = 0x2,

        /// <summary>
        /// 错误堆栈
        /// </summary>
        ErrorStack = 0x3,
    }
}