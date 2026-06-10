using System.Diagnostics;

namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B07_递归)]
    public class B07_递归 : IVerification
    {
        /*
         * 递推：从小到大,如:1+2+3
         * 递归：从大小到,如:(3-0)+(3-1)+(3-2)
         * 动态规划：一种找最优路线的思路。比如从昆明到北京有很多种路线，最优一定会经过成都，然后再找成都到北京的最优路线，依次找下去，最后组合成一套完整的出行方案。
         *
         * 动态规划个人理解是递归的一种，是分段去确定最优解
         *
         */

        public void Start(string[]? args)
        {
            // 1. 递推与递归
            //都是以阶乘为例子
            int a = this.Iterative(5);
            int b = this.Recursive(5);
            Console.WriteLine($"递推与递归\t递推{a}\t递归{b}");

            #region 2. 结算货币找零 找最少张数

            int money = 10;
            //int[] coins = new int[] { 1, 2, 5, 10, 20, 50, 100 };
            int[] coins = new int[] { 1, 2, 5 };
            Console.WriteLine();
            Console.WriteLine("结算货币找零有多少种方式:");
            Console.WriteLine("方式\t\t耗时\t计算次数");

            Stopwatch sp = new Stopwatch();

            sp.Start();
            int result = this.CountChange_Recursive(money, coins);
            sp.Stop();
            Console.WriteLine($"递归\t\t{result}种\t用时{sp.ElapsedTicks}毫微秒\t计算{this.RecursiveNum}次");

            sp.Restart();
            int result_A = this.CountChange_DynamicPlan(money, coins);
            sp.Stop();
            Console.WriteLine($"动态规划\t{result_A}种\t用时{sp.ElapsedTicks}毫微秒\t计算{this.DynamicPlanNum}次");

            Console.WriteLine();
            Console.WriteLine("100毫微秒为CPU一个晶振周期，为十亿分之一秒。 别名：ns、纳秒");

            #endregion 2. 结算货币找零 找最少张数
        }

        #region 递推与递归

        //下面2个函数是阶乘的不同写法

        /// <summary>
        /// 递推（也有地方叫迭代）
        /// </summary>
        public int Iterative(int n)
        {
            int acc = 1;//递推需要使用一个累积器保存中间结果
            for(int i = 1 ; i <= n ; i++)
            {
                acc *= i;
            }
            return acc;
        }

        /// <summary>
        /// 递归
        /// </summary>
        public int Recursive(int n)
        {
            if(n == 0)
                return 1;
            return n * this.Recursive(n - 1);//把递归调用写在函数的最末尾，叫尾递归
        }

        #endregion 递推与递归

        #region 结算货币找零有多少种方式

        //递归版本
        private int RecursiveNum = 0;

        /// <summary>
        /// 用递归找： 金额找零时最少需要多少张
        /// 复杂为度为 coins.Length^money
        /// </summary>
        /// <param name="money"></param>
        /// <param name="coins"></param>
        /// <returns></returns>
        public int CountChange_Recursive(int money, int[] coins)
        {
            this.RecursiveNum++;

            int min_ret = money;

            if(money == 1)
            {
                return 1;
            }
            if(coins.Length == 0 || money <= 0)
            {
                return 0;
            }

            foreach(int c in coins)
            {
                if(money >= c)
                {
                    /*
                     * 下面这二行代码是程序的出口。
                     * 跳出的时候一定是money和c只相差一块。 比如 3块钱，金额为2。11块钱 金额为10
                     *
                     * 每次进入，都是一次新的循环。min_ret也只是当前进入时，找最少的张数
                     *
                     *
                     */

                    int min_count = this.CountChange_Recursive(money - c, coins) + 1;
                    if(min_count < min_ret)
                    {
                        min_ret = min_count;
                    }
                }
            }

            return min_ret;

            ////第一个：先找出一个，然后把剩余的 在所有方式中查找
            ////第二个：所有方式中，跳过第一个，然后找所有的

            //return this.CountChange_Greedy(money - coins.First(), coins)
            //    + this.CountChange_Greedy(money, coins.Skip(1).ToArray());
        }

        //动态规划版本
        private int DynamicPlanNum = 0;

        /// <summary>
        /// 用动态规划找： 金额找零时最少需要多少张
        /// 复杂为度为：C^amount C是一个常数，代表机器当前运行状况等
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="coins"></param>
        /// <param name="sheets"></param>
        /// <returns></returns>
        public int CountChange_DynamicPlan(int amount, int[] coins, int[]? sheets = null)
        {
            if(sheets == null)
                sheets = new int[amount + 1];//这里都是整数，所以这样计算。其实是每一块钱都需要存下

            //遍历金额，找出每种金额对应的找零方式
            for(int money = 1 ; money < sheets.Length ; money++)//第一层：金钱
            {
                int minWays = money;//临时变量-已经金钱为money时，最少的方式数
                foreach(int item in coins)//第二层：金钱面额
                {
                    this.DynamicPlanNum++;
                    if(money >= item)//当前的金钱必须要大于等于面额。 比如找补34块，面额不可能是50
                    {
                        /*
                         * 这里的+1是因为已经找补了一张面额为item的钱
                         *
                         * ways[25]=ways[20]种+[5元 1种]
                         *
                         */
                        int temp_Money = sheets[money - item] + 1;

                        //如果找到的值比当前最少的方式还少，则修正
                        if(temp_Money < minWays)
                            minWays = temp_Money;
                    }
                }

                sheets[money] = minWays;//把每种金额的最少找零方式，存起来
            }

            return sheets[amount];
        }

        #endregion 结算货币找零有多少种方式
    }
}