using CkTools;

namespace 技术点验证
{
    /// <summary>
    /// 值对象检查接口
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IValueCheck<TValue>
    {
        /// <summary>
        /// 指示如何完成业务检查
        /// </summary>
        /// <returns></returns>
        (bool checkResult, BizError errorObj) Check(TValue value);
    }
}