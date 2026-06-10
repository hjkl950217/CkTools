using System.Drawing;

namespace 技术点验证
{
    public class A21_TestObj
    {
        public int Integr { get; set; }
        public double Double { get; set; }
        public decimal Decimal { get; set; }
        public bool Boolen { get; set; }
        public string? Name { get; set; }
        public TestEnum TestEnum { get; set; }
        public Color? TestColor { get; set; }
        public List<int>? IntList { get; set; }
        public List<double>? DoubleList { get; set; }
        public string?[]? StringArry { get; set; }
        public IDictionary<int, string?>? DicList { get; set; }
        public dynamic? DynamicObj { get; set; }
    }
}