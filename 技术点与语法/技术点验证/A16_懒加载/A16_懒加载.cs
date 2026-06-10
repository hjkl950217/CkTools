namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A16_懒加载)]
    public class A16_懒加载 : IVerification
    {
        public void Start(string[]? args)
        {
            int result = LazyDemo.InfiniteEvenNumbersSet().Skip(10).First();

            int result2 = LazyDemo.InfiniteEvenNumbersSet2();

            Func<int[]> getNumbers = () =>
            {
                Console.WriteLine("加载数据方法执行一次");
                return new[] { 1, 2, 3, 4, 5, 6, 7 };
            };
            Lazy<int[]> lazyNumbers = new Lazy<int[]>(getNumbers, true);
            int k = lazyNumbers.Value.Sum();
            int k2 = lazyNumbers.Value.Sum();

            Console.WriteLine("观察控制台输出,每次执行委托都会输出信息.");
        }
    }

    public static class LazyDemo
    {
        public static int ExecutCount = 0;

        public static IEnumerable<int> InfiniteEvenNumbersSet()
        {
            for(int i = 0 ; ; i++)
            {
                LazyDemo.ExecutCount++;
                if(i % 2 == 0)
                    yield return i;
            }
        }

        public static int InfiniteEvenNumbersSet2()
        {
            for(int i = 0 ; ; i++)
                if(i % 2 == 0)
                    return i;
        }
    }
}