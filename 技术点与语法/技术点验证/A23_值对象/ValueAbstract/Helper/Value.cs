namespace 技术点验证
{
    /// <summary>
    /// 这个类仅仅只是提供一组静态方法，让使用值对象更加方便而已
    /// </summary>
    public static class Value
    {
        /// <summary>
        /// 创建一个<see cref="ValueObject{TValue}"/>对象
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ValueObject<TValue> Create<TValue>(TValue value)
            where TValue : class
            => new ValueObject<TValue>(value);
    }
}