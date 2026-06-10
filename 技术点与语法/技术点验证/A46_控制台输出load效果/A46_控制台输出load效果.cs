namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A46_控制台输出load效果)]
    public class A46_控制台输出load效果 : IVerification
    {
        public void Start(string[]? args)
        {
            ConsoleSpinner csp = new ConsoleSpinner();

            Console.CursorVisible = false;
            while(true)
            {
                csp.Turn();
            }
        }
    }

    public class ConsoleSpinner
    {
        private int counter;

        public void Turn()
        {
            this.counter++;
            switch(this.counter % 4)
            {
                case 0:
                    Console.Write("/");
                    this.counter = 0;
                    break;

                case 1:
                    Console.Write("-");
                    break;

                case 2:
                    Console.Write("\\");
                    break;

                case 3:
                    Console.Write("|");
                    break;
            }
            Thread.Sleep(100);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}