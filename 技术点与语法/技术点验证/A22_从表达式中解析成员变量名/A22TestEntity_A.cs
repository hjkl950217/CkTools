namespace 技术点验证
{
    public class A22TestEntity_A
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public A22TestEntity_B TestBObject { get; set; } = new A22TestEntity_B();
    }
}