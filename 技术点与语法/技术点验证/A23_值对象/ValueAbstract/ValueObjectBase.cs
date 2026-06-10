namespace 技术点验证
{
    /// <summary>
    /// 值对象基类-针对引用类型<para></para>
    /// 可保证数据不能为null，数据正确(需重写<see cref="BizCheckValue"/>方法以完成业务规则检测)<para></para>
    /// 使用时可以当普通数据使用，如: int a=new Age(50)
    /// </summary>
    /// <remarks>
    /// 微软关于值对象的资料:https://docs.microsoft.com/zh-cn/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
    /// 其它人关于class为引用类型的处理  https://enterprisecraftsmanship.com/posts/value-object-better-implementation <para></para>
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    public abstract class ValueObjectBase<TValue> : ValueBase<TValue>, IValueObject<TValue>, IValue<TValue>
        where TValue : class
    {
        protected ValueObjectBase(TValue data) : base(data)
        {
        }

        #region 子类可重写

        /// <summary>
        /// 子类可重写，指示如果使用<paramref name="value"/>与<see cref="Value"/>进行内容比较<para></para>
        /// 引用、类型比较由基类完成<para></para>
        /// <see cref="ValueObjectBase{TValue}"/>基类已经完成了成员的比较，子类只需要实现<see cref="GetEqualityComponents(TValue)"/>即可，有特殊需求时可修改成员所属类型的<see cref="System.Object.Equals(object)"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool EqualsCode(TValue value)
        {
            return Enumerable.SequenceEqual(
                first: this.GetEqualityComponents(this.Value),
                second: this.GetEqualityComponents(value));
        }

        /// <summary>
        /// 子类可重写，指示如何计算<see cref="Value"/>的哈希值<para></para>
        /// 基类默认行为：调用<see cref="GetEqualityComponents(TValue)"/>方法获取要参与哈希计算的成员，然后调用成员所属的<see cref="System.Object.GetHashCode"/>方法成员的哈希值，最后利用System.Linq.Enumerable.Aggregate累加器函数计算哈希值。<para></para>
        /// 计算逻辑:
        /// <code>
        /// //first种子为1 next为每个成员 <para></para>
        /// (first * 256) ^ (next?.GetHashCode() ?? 0)
        /// </code>
        /// </summary>
        /// <returns></returns>
        protected override int GetHashCodeCore()
        {
            return this.GetEqualityComponents(this.Value)
                  .Aggregate(1, this.HashCodeAggregate);
        }

        /// <summary>
        /// 计算哈希值的累加方法
        /// </summary>
        /// <param name="first"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        protected virtual int HashCodeAggregate(int first, object next)
        {
            //这里256这个数字是随便写的，是为了避免计算哈希值的逻辑有重复的
            return (first * 256) ^ (next?.GetHashCode() ?? 0);
        }

        #endregion 子类可重写

        #region 由子类重写

        /// <summary>
        /// 由子类重写，获取参与相等比较的成员
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetEqualityComponents(TValue value);

        #endregion 由子类重写
    }
}