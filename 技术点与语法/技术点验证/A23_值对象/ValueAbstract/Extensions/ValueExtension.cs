namespace 技术点验证
{
    public static class ValueExtension
    {
        /// <summary>
        /// 创建一个<see cref="ValueObject{TValue}"/>对象
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ValueObject<TValue> ToValue<TValue>(this TValue value)
            where TValue : class
            => new ValueObject<TValue>(value);
    }
}