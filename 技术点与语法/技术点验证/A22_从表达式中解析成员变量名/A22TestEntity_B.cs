using System.Diagnostics.CodeAnalysis;

namespace 技术点验证
{
    public class A22TestEntity_B
    {
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; } = string.Empty;

        [NotNull]
        public A22TestEntity_C TestCObject { get; set; } = new A22TestEntity_C();
    }
}