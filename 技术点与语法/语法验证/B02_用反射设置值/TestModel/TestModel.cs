namespace 语法验证与学习
{
    public static class TestModel
    {
        public static A A { get; set; } = new A();
        public static string? XXX { get; set; } = "default";
    }

    //大类
    public class A
    {
        public B BBB { get; set; } = new B();
    }

    //小类
    public class B
    {
        public string? Data { get; set; } = "原始数据";
    }
}