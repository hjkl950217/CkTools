namespace Verification.Core
{
    /// <summary>
    /// 验证想法的接口
    /// </summary>
    /// <remarks>
    /// 这里不能用泛型的,因为不同的枚举是值,不是类型
    /// </remarks>
    public interface IVerification
    {
        /// <summary>
        /// 开始验证
        /// </summary>
        /// <param name="args">启动时，传递给main方法的args</param>
        void Start(string[]? args);
    }

    //public interface IVerification<TVerTask> : IVerification
    //    where TVerTask : class, IVerification<TVerTask>
    //{
    //    /// <summary>
    //    /// 开始验证
    //    /// </summary>
    //    /// <param name="args">启动时，传递给main方法的args</param>
    //    new void Start(string[]? args);
    //}
}