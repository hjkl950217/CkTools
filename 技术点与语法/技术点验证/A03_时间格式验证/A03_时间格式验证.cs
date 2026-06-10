namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A03_时间格式验证)]
    public class A03_时间格式验证 : IVerification
    {
        public void Start(string[]? args)
        {
            Console.WriteLine($"-{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")}-");
            Console.WriteLine($"-{DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")}-");

            /*参考资料-datetime tostring?中自定义的格式资料
             * https://docs.microsoft.com/zh-cn/dotnet/standard/base-types/custom-date-and-time-format-string?s
             */
            Console.WriteLine("Task Start\r\n");
            List<object> outputList = new List<object>();

            string? dt1 = DateTime.Now.ToString("o");
            string? dt2 = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");

            //unix时间戳
            long dt3 = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            // Console.WriteLine($"other unix{(DateTime.UtcNow.ToUniversalTime().Ticks - 621355968000000000) / 10000000}");

            //输出
            outputList.Add(dt1);
            outputList.Add(dt2);
            outputList.Add(dt3);

            int index = 1;
            foreach(object? item in outputList)
            {
                Console.WriteLine($"第{index++}个");
                Console.WriteLine(item);
                Console.WriteLine("-----");
            }

            Console.WriteLine("----------------------");

            //转换回去
            Console.WriteLine("转换回去：");
            outputList.Clear();
            index = 1;
            DateTime dt10 = Convert.ToDateTime(dt1);
            DateTime dt20 = Convert.ToDateTime(dt2);
            DateTime dt30 = DateTimeOffset.FromUnixTimeMilliseconds(dt3).DateTime.AddHours(8);

            outputList.Add(dt10);
            outputList.Add(dt20);
            outputList.Add(dt30);
            foreach(object? item in outputList)
            {
                Console.WriteLine($"第{index++}个");
                Console.WriteLine(item);
                Console.WriteLine("-----");
            }
        }
    }
}