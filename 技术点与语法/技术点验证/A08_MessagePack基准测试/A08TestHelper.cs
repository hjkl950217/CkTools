using System.Drawing;
using System.Text;

namespace 技术点验证
{
    public static class A08TestHelper
    {
        public static Random random = new Random(DateTime.UtcNow.Second);

        public static List<TestEntity_Int> GetIntTestData(int totaol)
        {
            List<TestEntity_Int> testData = new List<TestEntity_Int>();
            Random random = new Random(totaol);

            byte[] tempByte = new byte[random.Next(100)];
            random.NextBytes(tempByte);
            string? testStr = UTF8Encoding.UTF8.GetString(tempByte);
            TestEnum[] testEnums = new TestEnum[3] { TestEnum.Open, TestEnum.Close, TestEnum.All };

            for(int i = 0 ; i < totaol ; i++)
            {
                TestEntity_Int temp = new TestEntity_Int()
                {
                    Integr = 10,
                    Double = 20.22,
                    Decimal = 300.333M,
                    Boolen = (totaol % 2) == 0,
                    Name = testStr,
                    TestEnum = testEnums[random.Next(0, 3)],
                    TestColor = Color.FromArgb(random.Next(0, 256)),
                    IntList = new List<int>() { 50, 100 },
                    DoubleList = new List<double> { 50.50, 100.001 },
                    StringArry = new string[] { testStr },
                    DicList = new Dictionary<int, string?>() { { 20, testStr } },
                    DynamicObj = new List<object>() { new object() }
                };

                testData.Add(temp);
            }

            return testData;
        }

        public static List<TestEntity_String?> GetIntTestData2(int totaol)
        {
            List<TestEntity_String?> testData = new List<TestEntity_String?>();
            Random random = new Random(totaol);

            byte[] tempByte = new byte[random.Next(100)];
            random.NextBytes(tempByte);
            string? testStr = UTF8Encoding.UTF8.GetString(tempByte);
            TestEnum[] testEnums = new TestEnum[3] { TestEnum.Open, TestEnum.Close, TestEnum.All };

            for(int i = 0 ; i < totaol ; i++)
            {
                TestEntity_String? temp = new TestEntity_String()
                {
                    Integr = 10,
                    Double = 20.22,
                    Decimal = 300.333M,
                    Boolen = (totaol % 2) == 0,
                    Name = testStr,
                    TestEnum = testEnums[random.Next(0, 3)],
                    TestColor = Color.FromArgb(random.Next(0, 256)),
                    IntList = new List<int>() { 50, 100 },
                    DoubleList = new List<double> { 50.50, 100.001 },
                    StringArry = new string[] { testStr },
                    DicList = new Dictionary<int, string?>() { { 20, testStr } },
                    DynamicObj = new List<object>() { new object() }
                };

                testData.Add(temp);
            }

            return testData;
        }

        public static List<TestEntity_Non> GetIntTestData3(int totaol)
        {
            List<TestEntity_Non> testData = new List<TestEntity_Non>();
            Random random = new Random(totaol);

            byte[] tempByte = new byte[random.Next(100)];
            random.NextBytes(tempByte);
            string? testStr = UTF8Encoding.UTF8.GetString(tempByte);
            TestEnum[] testEnums = new TestEnum[3] { TestEnum.Open, TestEnum.Close, TestEnum.All };

            for(int i = 0 ; i < totaol ; i++)
            {
                TestEntity_Non temp = new TestEntity_Non()
                {
                    Integr = 10,
                    Double = 20.22,
                    Decimal = 300.333M,
                    Boolen = (totaol % 2) == 0,
                    Name = testStr,
                    TestEnum = testEnums[random.Next(0, 3)],
                    TestColor = Color.FromArgb(random.Next(0, 256)),
                    IntList = new List<int>() { 50, 100 },
                    DoubleList = new List<double> { 50.50, 100.001 },
                    StringArry = new string[] { testStr },
                    DicList = new Dictionary<int, string?>() { { 20, testStr } },
                    DynamicObj = new List<object>() { new object() }
                };

                testData.Add(temp);
            }

            return testData;
        }

        /// <summary>
        /// 获取随机的字符串
        /// </summary>
        /// <param name="byteSize"></param>
        /// <returns></returns>
        public static string? GetRandomString(int byteSize = 100)
        {
            byte[] tempByte = new byte[random.Next(byteSize)];
            random.NextBytes(tempByte);//用随机数器随机填充
            string? testStr = UTF8Encoding.UTF8.GetString(tempByte);

            return testStr;
        }

        public static T GetRandomEnum<T>(int maxValue)
            where T : struct
        {
            return Enum.Parse<T>(A08TestHelper.random.Next(maxValue).ToString(), true);
        }

        public static T GetRandomEnum<T>(int minValue, int maxValue)
            where T : struct
        {
            return Enum.Parse<T>(A08TestHelper.random.Next(minValue, maxValue).ToString(), true);
        }

        /// <summary>
        /// 返回一个随机的小写字符
        /// </summary>
        /// <returns></returns>
        public static char GetRandomLowerChar()
        {
            //97  122
            return Convert.ToChar(A08TestHelper.random.Next(97, 123));
        }

        /// <summary>
        /// 返回一个随机的大写字符
        /// </summary>
        /// <returns></returns>
        public static char GetRandomUpChar()
        {
            //65  90
            return Convert.ToChar(A08TestHelper.random.Next(65, 91));
        }

        /// <summary>
        /// 返回一个随机的数字字符
        /// </summary>
        /// <returns></returns>
        public static char GetRandomNum()
        {
            //0-9
            return Convert.ToChar(A08TestHelper.random.Next(0, 10));
        }

        /// <summary>
        /// 获取一个随机的字符，可能是大写或小写的
        /// </summary>
        /// <returns></returns>
        public static char GetRandomChar()
        {
            bool isUp = DateTime.Now.Ticks % 2 == 0;
            if(isUp == true)
            {
                return A08TestHelper.GetRandomUpChar();
            }
            else
            {
                return A08TestHelper.GetRandomLowerChar();
            }
        }
    }
}