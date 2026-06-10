namespace 技术点验证
{
    public static class A21_TestObj2Extension
    {
        public static string? Method1(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.StringArry.ToJsonExt()}{a21_TestObj2.DicList.ToJsonExt()}";
        }

        public static string? Method2(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.DoubleList.ToJsonExt()}";
        }

        public static string? Method3(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.TestColor.ToJsonExt()}";
        }

        public static string? Method4(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.Integr.ToJsonExt()}";
        }

        public static string? Method5(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.Double.ToJsonExt()}";
        }

        public static string? Method6(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.Name.ToJsonExt()}";
        }

        public static string? Method7(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.TestEnum.ToJsonExt()}";
        }

        public static string? Method8(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.TestEnum.ToJsonExt()}{a21_TestObj2.Integr.ToJsonExt()}";
        }

        public static string? Method9(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.Double.ToJsonExt()}{a21_TestObj2.Integr.ToJsonExt()}";
        }

        public static string? Method10(this A21_TestObj2 a21_TestObj2)
        {
            return $"{a21_TestObj2.StringArry.ToJsonExt()}{a21_TestObj2.Integr.ToJsonExt()}";
        }
    }
}