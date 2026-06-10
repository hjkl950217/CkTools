namespace 技术点验证
{
    public class TestClass : TestInterface
    {
        public void AddInt(int num)
        {
            throw new NotImplementedException();
        }

        //[Test(Name = "添加姓名-实现")]
        public void AddName(string? name)
        {
            Console.WriteLine("完成AddName的执行");
        }
    }
}