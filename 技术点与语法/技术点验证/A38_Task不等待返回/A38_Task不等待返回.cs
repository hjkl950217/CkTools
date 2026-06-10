namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A38_Task不等待返回)]
    public class A38_Task不等待返回 : IVerification
    {
        public void Start(string[]? args)
        {
            System.Console.WriteLine("testAAAAA");
            TaskTest taskTest = new TaskTest();
            taskTest.Test1();
            taskTest.Test2();
            taskTest.Test3();

            System.Console.WriteLine("testBBBB");
        }
    }

    public class TaskTest
    {
        public void Test1()
        {
            Task.Run(() =>
           {
               Task.Delay(1000);
               System.Console.WriteLine("Test1");
               return "Test1";
           });
        }

        public string Test2()
        {
            Task<string> task = Task.Run(() =>
            {
                Task.Delay(2000);
                System.Console.WriteLine("Test2");
                return "Test2";
            });

            Task.WaitAll(task);

            return task.Result;
        }

        public void Test3()
        {
            Task.Factory.StartNew(() =>
            {
                Task.Delay(3000);
                System.Console.WriteLine("Test3");
            });
        }
    }
}