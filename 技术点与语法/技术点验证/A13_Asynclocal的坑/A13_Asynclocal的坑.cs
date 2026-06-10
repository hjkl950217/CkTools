namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A13_Asynclocal的坑)]
    public class A13_Asynclocal的坑 : IVerification
    {
        public static AsyncLocal<int> v = new AsyncLocal<int>();

        public void Start(string[]? args)
        {
            Task task = Task.Run(async () =>
            {
                v.Value = 123;
                A13_Asynclocal的坑.Show("await之前的值");

                Intercept intercept = new Intercept();
                await Intercept.Invoke();

                A13_Asynclocal的坑.Show("await之后的值");
            });
            task.Wait();
        }

        public static void Show(string? preStr)
        {
            Console.WriteLine("当前线程ID:" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(preStr + A13_Asynclocal的坑.v.Value);
            Console.WriteLine("----------------");
            Console.WriteLine();
        }
    }

    public class Intercept
    {
        public static Task Invoke()
        {
            return Task.Run(() =>
            {
                A13_Asynclocal的坑.v.Value = 888;
                A13_Asynclocal的坑.Show("await之中的值");
            });
        }
    }
}