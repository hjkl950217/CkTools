namespace 语法验证与学习
{
    public class Father
    { public int ID { get; set; } }

    public class Son : Father
    { public string? Name { get; set; } }

    [VerifcationType(VerificationTypeEnum.B03_协变和逆变)]
    public class B03_协变和逆变 : IVerification
    {
        public delegate void mydelege1(Father f);

        public delegate void mydelege2(Son s);

        private static void Fun1(Father s)
        {
            Console.WriteLine(s.ID);
        }

        //private static void fun2(Son s)
        //{
        //    Console.WriteLine(s.ID);
        //}

        public void Start(string[]? args)
        {
            /*
             * T->>IEnumerable<T> 这个过程可以看成是一个`投影`，或是说过程
             * 而协变/逆变是对这个`投影`的描述。这三个描述是类型转换中的。
             * 协变/逆变如果不能确定能不能成功，则描述为不变
             *
             * 协变：在派生类型可以当基类使用的情况下，由派生类转换成基类的过程就是协变
             *
             * 逆变：在框架中，经常能看到接收参数是Object类型的。
             * 但里面使用时会转换为具体类型
             * 比如：收到object类型的数据，会转换为string?使用。
             *
             * 上面这种就是逆变。
             *
             * 不变：上面的协变或是逆变，都能转换成功（在特定条件下）。不确定转换能不能成功就是不变。
             * 比如：string?->Objet 一能成功。
             *       Object->string? 一定能成功么？ 如果Object里装的是int类型数据？
             *
             *       对第二种就是不变的。因为不知道，不确定
             *
             */

            Father f = new Father() { ID = 5 };
            Son s = new Son() { ID = 500, Name = "Son" };
            f = s;//ok，儿子可以赋值给父亲

            /*
             * error,输入参数Father不能兼容（包含Son）,因为Son需要的一些东西对Father来说是没有的。
             * 派生类比基类更细节 具体一些
            */
            // mydelege1 md1 = fun2;
            mydelege2 md2 = Fun1;//ok，输入参数Son是兼容（包含）Father的，协变了。

            md2(s);//调用
        }
    }
}