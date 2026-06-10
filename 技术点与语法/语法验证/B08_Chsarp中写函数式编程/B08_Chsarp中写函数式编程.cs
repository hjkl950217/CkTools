namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B08_Chsarp中写函数式编程)]
    public class B08_Chsarp中写函数式编程 : IVerification
    {
        //参考资料： https://www.oschina.net/translate/functional-programming-in-csharp?lang=chs&p=1

        public void Start(string[]? args)
        {
            LinkAction.Start()
                .Add(this.FactorialDemo)
                .Add(this.CurryFunctionDemo)
                .Add(this.ClosureDemo) //这个放最后，执行时间长
                .BatchRun();
        }

        /// <summary>
        /// 闭包Demo
        /// </summary>
        /// <remarks>
        /// 如果一个委托在其函数体中引用了一些变量，那么这些变量将会像这个函数的“属性”一样，在这个函数被调用时作为它的一部分。
        ///
        /// 上面是我理解的闭包
        /// </remarks>
        public void ClosureDemo()
        {
            Console.WriteLine("演示--利用闭包特性完成缓存");

            Func<DateTime> now = () => DateTime.Now;

            Func<DateTime> nowCached = now.Cache(3);

            Console.WriteLine("\tCurrent time\tCached time");
            for(int i = 0 ; i < 9 ; i++)
            {
                Console.WriteLine("{0}.\t{1:T}\t{2:T}", i + 1, now(), nowCached());
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 阶乘
        /// </summary>
        /// <remarks>
        /// 阶乘：n! = n*(n-1)*(n-2)*(n-3)*...*3*2*1
        /// 阶乘最小是1，所以使用递归是比较好的。
        /// </remarks>
        public void FactorialDemo()
        {
            //
            /*
             * lambda不能引用自己(因为没有名字)，则需要使用委托变量来套一层
             *
             * 但利用内部方法可以引用自己
             */
            Console.WriteLine("演示--利用递归完成阶乘");

            int factorial(int num)
            {
                return num < 1 ? 1 : num * factorial(num - 1);
            }

            int number = 5;
            Console.WriteLine($"{number}的阶乘为: {{0}}", factorial(number));
        }

        /// <summary>
        /// 分部方法
        /// <para>使用默认值来降低调用时的参数个数的一种方法</para>
        /// </summary>
        public void PartialFunctionDemo()
        {
            Func<double, double, double> pow = Math.Pow;
            Func<double, double> exp = pow.Partial1(Math.E);//得到: e^x  这样的函数
            Func<double, double> step = pow.Partial1(2);//得到: 2^x  这样的函数

            Func<double, double> square = pow.Partial2(2);//得到 x^2 这样的函数
            Func<double, double> cube = pow.Partial2(3);//得到 x^3 这样的函数

            /*
             * 在三维坐标系中，要计算一个点到原点的距离，当有一个值固定的时候
             *
             * 原始方法： x,y,z=>开方( x * x + y * y + z * z ) //三坐标平方，再开方
             *
             * 利用分部:  x,y=>开方( x * x + y * y + 0 )  //在X轴和Y轴组成的平面计算。因为此平面Z坐标永远为0
             *
             */
        }

        #region 柯里化

        /// <summary>
        /// 柯里化
        /// </summary>
        /// <remarks>
        /// 柯里化 (Currying)是一种能将一个N参函数变为N个单参函数的方法
        /// https://www.oschina.net/translate/functional-programming-in-csharp?lang=chs&p=8
        /// </remarks>
        public void CurryFunctionDemo()
        {
            Func<double, double, double> pow = Math.Pow;
            Func<double, Func<double, double>> curriedPow = pow.Curry();
            double p1 = Math.Pow(Math.E, 2);
            double p2 = curriedPow(Math.E)(2);

            /*
             * 接上面的继续
             * 建一个二维距离函数，用来计算X轴上X=3平面中，点与点之间的距离
             * 利用柯里化能很好的生成出来。(上面的分部方法可以认为是柯里化里面的一小部分)
             *
             * 分部方式每一种都需要写一个方法，而柯里化只需要写一个就搞定
             *
             */

            // 柯里化
            Func<double, double, double, double> distance3D = Distance3D;
            Func<double, Func<double, Func<double, double>>> curriedDistance = distance3D.Curry();
            double d1 = Distance3D(3, 4, 12);
            double d2 = curriedDistance(3)(4)(12);

            // 生成平面X=3上的计算方法
            Func<double, Func<double, double>> distance2DAtX3 = curriedDistance(3);
            double d11 = Distance3D(3, 4, 12);
            double d22 = distance2DAtX3(4)(12);

            //生成平面X=3上的线Y=4
            Func<double, double> distanceAtX3Y5 = distance2DAtX3(4);
            double d111 = Distance3D(3, 4, 12);
            double d222 = distanceAtX3Y5(12);

            // 反柯里化
            Func<double, double, double, double> unCurriedDistance = curriedDistance.UnCurry();
        }

        /// <summary>
        /// 计算一个点到原点的距离-只是示例方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static double Distance3D(double x, double y, double z)
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        #endregion 柯里化
    }

    /// <summary>
    /// 仅仅只是放这次研究中，写的一些扩展方法，不要在意名字
    /// </summary>
    public static class B08_Chsarp中写函数式编程Fun
    {
        public static Func<T> Cache<T>(this Func<T> func, int cacheInterval)
        {
            T? cachedValue = func();//记录第一次调用的值
            DateTimeOffset timeCached = DateTimeOffset.Now;//记录缓存方法第一次调用时的时间

            T cachedFunc()
            {
                bool isExpired = (DateTimeOffset.Now - timeCached).Seconds >= cacheInterval;
                if(isExpired)
                {
                    timeCached = DateTimeOffset.Now;
                    cachedValue = func();//过期后，调用方法更新返回值
                }
                return cachedValue;
            }

            return cachedFunc;
        }

        /// <summary>
        /// 分部方法
        /// <para>使用默认值来降低调用时的参数个数的一种方法</para>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="func"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Func<T2, TR> Partial1<T1, T2, TR>(this Func<T1, T2, TR> func, T1 defaultValue)
        {
            return b => func(defaultValue, b);
        }

        public static Func<T1, TR> Partial2<T1, T2, TR>(this Func<T1, T2, TR> func, T2 defaultValue)
        {
            return a => func(a, defaultValue);
        }

        #region 柯里化

        public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(this Func<T1, T2, TResult> function)
        {
            /*
             * a=>b  或是 b =>a 都无所谓
             * 因为都是利用闭包传递引用，所以只要能对上号 最后的function
             *
             *
             */

            return b => a => function(b, a);
        }

        public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(
           this Func<T1, T2, T3, TResult> function)
        {
            /*
             * 才看这个代码可能懵逼，但是理解了闭包就好办了
             * a => [ b => c => function(a, b, c)]
             *
             * a是参数,在最里面的function中则直接使用了
             * 相当于(a,b,c)=>function(a, b, c)
             *
             */

            return a => b => c => function(a, b, c);
        }

        public static Func<T1, T2, T3, TResult> UnCurry<T1, T2, T3, TResult>(this Func<T1, Func<T2, Func<T3, TResult>>> function)
        {
            /*
             * curriedDistance(3)(4)(12) 是不是等价于 Distance3D(3, 4, 12)
             */

            return (a, b, c) => function(a)(b)(c);
        }

        #endregion 柯里化
    }
}