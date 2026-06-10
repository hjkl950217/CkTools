namespace 技术点验证
{
    public interface TestInterface
    {
        [Test(Name = "添加姓名-接口")]
        void AddName(string? name);

        void AddInt(int num);
    }
}