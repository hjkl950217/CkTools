namespace 工具基础核心库.BaseTool.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// 用于标记Flags的枚举是否包含某个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <param name="enumValue2"></param>
        /// <returns></returns>
        public static bool IncludeFlags<T>(this T enumValue, params T[] enumValueArray)
            where T : Enum
        {
            if(enumValueArray == null || enumValueArray.Length == 0)
                throw new ArgumentException($"必须提供参数 {nameof(enumValueArray)} 的值,不能为null或0长度");

            ulong value = Convert.ToUInt64(enumValue);//转换为uint64以支持所有类型的枚举
            return enumValueArray
                .Select(flag => Convert.ToUInt64(flag))
                .All(flagValue => (value & flagValue) == flagValue);
        }
    }
}