namespace 工具基础核心库.BaseTool.Extension
{
    public static class IEnumerableExtension
    {
        public static string JoinString(this IEnumerable<string> list, string separator = "")
        {
            return string.Join(separator, list);
        }

        public static string JoinString<T>(this IEnumerable<T> list, Func<T, string> selector, string separator = "")
        {
            return string.Join(separator, list.Select(selector));
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach(T item in list)
            {
                action(item);
            }
            return list;
        }
    }
}