namespace 技术点验证
{
    /// <summary>
    /// 值对象接口-用于值类型非calss<para></para>
    /// 用于约束值对象的行为规范
    /// </summary>
    /// <remarks>
    /// 微软关于值对象的资料:https://docs.microsoft.com/zh-cn/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    public interface IValue<TValue> : IValueCheck<TValue>
    {
        /// <summary>
        /// 代表当前值对象对应的值
        /// </summary>
        TValue Value { get; protected set; }

        void SetValue(TValue value);
    }
}