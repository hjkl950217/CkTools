using System.Diagnostics;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A23_值对象)]
    public class A23_值对象 : IVerification
    {
        public void Start(string[]? args)
        {
            //Show只是执行方法  try方法报错时会输出错误信息
            //这里这样写是为了方便复制+快速切换

            string getNewstring(params char[] c)
            {
                return new string(c);
            }

            #region 测试值类型

            this.Show("--测试值类型--");

            this.Show(() => _ = new IdCard("012345678912345678"));
            this.Show(() => _ = new IdCard("01234567891234567X"));
            this.TryShow(() => _ = new IdCard("01234567891234567x"));
            this.TryShow(() => _ = new IdCard("01234567891234567-"));

            this.Show(() => _ = new Age(25));
            this.TryShow(() => _ = new Age(18));
            this.TryShow(() => _ = new Age(-1));

            #endregion 测试值类型

            #region 测试枚举

            this.Show("--测试枚举--");

            this.Show(() => _ = MemberType.Create(500));
            this.TryShow(() => _ = MemberType.Create(-500));

            #endregion 测试枚举

            #region 测试快速创建嵌套的值对象

            this.Show("--测试快速创建嵌套的值对象--");
            /*
             * 未引入值对象的转换:   int-> 枚举
             * 引入值对象的转换:   int -> int值对象 ->通过2个值对象内在数值的关系创建->枚举值对象
             *
             */

            this.TryShow(() => _ = MemberType.Create(new IdCardNo(255)));

            #endregion 测试快速创建嵌套的值对象

            #region 测试==重写

            Stopwatch sp = new Stopwatch();
            long[] msArray = new long[100];
            for(int i = 0 ; i < msArray.Length ; i++)
            {
                sp.Start();
                this.Show(() => _ = Value.Create("0") == Value.Create(getNewstring('c')));
                sp.Stop();

                msArray[i] = sp.ElapsedMilliseconds;

                //this.Show($"运行第{i}次,用时 {sp.ElapsedMilliseconds} ms");
            }
            this.Show($"测试值对象==,运行{msArray.Length}次, 最大值: {msArray.Max()} ms,最小值: {msArray.Min()} ms,平均: {msArray.Average()} ms");

            #endregion 测试==重写

            #region 测试引用值对象的Equals

            //这是为了快速造数据的
            StuClassInfo_Entity getClassInfo(int id, string name)
            {
                return new StuClassInfo_Entity()
                {
                    ClassID = id,
                    ClassName = new ClassName(name)
                };
            }

            StuClassInfo stuClassInfo1 = new StuClassInfo(getClassInfo(1, "1a"));
            StuClassInfo stuClassInfo2 = new StuClassInfo(getClassInfo(1, "1a"));

            Console.WriteLine("测试引用值对象的Equals");
            this.Show(() => Console.WriteLine(stuClassInfo1 == stuClassInfo2));
            this.Show(() => Console.WriteLine(stuClassInfo1.Equals(stuClassInfo2)));

            #endregion 测试引用值对象的Equals

            #region 测试引用值对象的GetHashCode

            void testHashCode(int id1, string str1, int id2, string str2, bool expected)
            {
                StuClassInfo obj1 = new StuClassInfo(new StuClassInfo_Entity() { ClassID = id1, ClassName = new ClassName(str1) });
                StuClassInfo obj2 = new StuClassInfo(new StuClassInfo_Entity() { ClassID = id2, ClassName = new ClassName(str2) });

                bool result = obj1.GetHashCode() == obj2.GetHashCode();

                string? isOk = expected == result ? "" : "*";
                Console.WriteLine($"\t {obj1.Value.ClassID}\t {obj1.Value.ClassName.Value}\t {obj2.Value.ClassID}\t {obj2.Value.ClassName.Value}\t {expected}\t {result} {isOk}");
            }

            Console.WriteLine("测试引用值对象的GetHashCode");
            Console.WriteLine($"\t id1 \t cont1 \t id2 \t cont2 \t 预期 \t 实际");

            testHashCode(1, "1", 1, "1", true);
            testHashCode(2, "1", 200, "1", true);
            testHashCode(3, "1", 3, "12", false);
            testHashCode(4, "1", 4, "12", false);

            #endregion 测试引用值对象的GetHashCode
        }

        #region 用于输出到控制台的辅助方法

        public void Show(Action action)
        {
            action();
        }

        public void Show(string? str)
        {
            Console.WriteLine(str);
        }

        public void TryShow(Action action)
        {
            try
            {
                action();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void TryShow(string? str)
        {
            Console.WriteLine();
        }

        #endregion 用于输出到控制台的辅助方法

        /* 我的值对象设计理念：
         * 值对象中的数据不可为null
         * 值对象中的数据是业务正确的
         * 使用起来尽量简单,接近原生类型
         *
         * 这里值对象与DDD中值对象的区别：
         *
         * 这里的值对象是技术性的，而DDD中值对象是概念上的
         * 这里的值对象是可变的，而DDD中值对象是不可变的
         * 这里的值对象用来保证符合业务规则，而DDD中值对象仅仅只是充当“数值”的作用，你也可以理解为某个状态的“快照”
         *
         *
         *
         * 第1版：简易值对象。仅仅只是构造方法中加业务规则的单个类
         * 第2版：增加值类型值对象的父类。重写哈希、相等、==  !=符号
         * 第3版：增加引用类型值对象的父类。子类在参与相等、哈希计算时只需要重写一个方法即可
         * 第4版：优化父类。优化：子类继承需要完成的方法、引用类型值对象计算相等和哈希的方式
         * 第5版：优化使用细节,基础接口调整。增加默认的值对象类、增加Value这个静态类
         *
         *
         *
         */
    }
}