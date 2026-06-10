namespace 技术点验证
{
    /// <summary>
    /// 值对象-针对引用类型<para></para>
    /// 针对所有引用类型:不能为null
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class ValueObject<TValue> : ValueObjectBase<TValue>
        where TValue : class
    {
        public ValueObject(TValue data) : base(data)
        {
        }

        public override bool BizCheckValue()
        {
            return base.Value != null;
        }

        protected override IEnumerable<object> GetEqualityComponents(TValue value)
        {
            yield return base.Value;
        }
    }
}