namespace System.Collections
{
    public static class EnumerableExtensions
    {
        #region check null or empty

        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            return !source.IsNotNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty(this IEnumerable source)
        {
            return (source?.GetEnumerator()?.MoveNext()).GetValueOrDefault();
        }

        #endregion check null or empty
    }
}