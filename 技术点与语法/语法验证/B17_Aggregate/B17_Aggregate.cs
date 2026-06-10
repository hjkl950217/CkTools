using System.Linq.Expressions;

namespace 语法验证与学习.B17_Aggregate
{
    [VerifcationType(VerificationTypeEnum.B17_Aggregate)]
    public class B17_Aggregate : IVerification
    {
        public void Start(string[]? args)
        {
            int[] sourceInts = new int[] { 1, 2, 3, 4 };
            int seed = 10;
            Expression<Func<int, int, int>> aggregateFunc = (a, b) => a * b;
            Expression<Func<int, int>> resultSelector = a => a + 10000;

            int result = System.Linq.Enumerable.Aggregate(
                  source: sourceInts,
                  seed: seed,
                  func: aggregateFunc.Compile(),
                  resultSelector: resultSelector.Compile()
                  );

            Console.WriteLine("源数组：" + sourceInts.ToJsonExt());
            Console.WriteLine("起始种子：" + seed.ToJsonExt());
            Console.WriteLine("累加函数：" + aggregateFunc.ToString());
            Console.WriteLine("结果映射函数：" + resultSelector.ToString());
            Console.WriteLine("结果：" + result.ToJsonExt());
        }
    }
}