namespace 技术点验证
{
    /// <summary>
    /// 值对象接口-用于值类型是class<para></para>
    /// 用于约束值对象的行为规范
    /// </summary>
    /// <remarks>
    /// 微软关于值对象的资料:https://docs.microsoft.com/zh-cn/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    public interface IValueObject<TValue> : IValue<TValue>
        where TValue : class
    {
    }
}