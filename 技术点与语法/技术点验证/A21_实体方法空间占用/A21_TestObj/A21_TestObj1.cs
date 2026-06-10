using System.Drawing;

namespace 技术点验证
{
    public class A21_TestObj1
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

        public string? Method1()
        {
            return $"{this.StringArry.ToJsonExt()}{this.DicList.ToJsonExt()}";
        }

        public string? Method2()
        {
            return $"{this.DoubleList.ToJsonExt()}";
        }

        public string? Method3()
        {
            return $"{this.TestColor.GetValueOrDefault().ToString()}";
        }

        public string? Method4()
        {
            return $"{this.Integr.ToString()}";
        }

        public string? Method5()
        {
            return $"{this.Double.ToString()}";
        }

        public string? Method6()
        {
            return $"{this.Name.ToString()}";
        }

        public string? Method7()
        {
            return $"{this.TestEnum.ToString()}";
        }

        public string? Method8()
        {
            return $"{this.TestEnum.ToString()}{this.Integr.ToString()}";
        }

        public string? Method9()
        {
            return $"{this.Double.ToString()}{this.Integr.ToString()}";
        }

        public string? Method10()
        {
            return $"{this.StringArry?.ToJsonExt()}{this.Integr.ToString()}";
        }
    }
}