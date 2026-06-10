#pragma warning disable CA2235 // Mark all non-serializable fields

using System;
using System.Collections.Generic;
using System.Drawing;
using MessagePack;

namespace 技术点验证
{
    //public abstract class TestEntity
    //{
    //    public TestEntity() { }

    //    //需要测试下3个数值类型之间的转换

    //    public abstract int Integr { get; set; }
    //    public abstract double Double { get; set; }
    //    public abstract decimal Decimal { get; set; }

    //    //需要测试下0 1和 true字符串向bool转换
    //    public abstract bool Boolen { get; set; }

    //    public abstract string? Name { get; set; }
    //    public abstract TestEnum TestEnum { get; set; }
    //    public abstract Color TestColor { get; set; }
    //    public abstract List<int> IntList { get; set; }

    //    //需要测试下IntList向DoubleList转换
    //    public abstract List<double> DoubleList { get; set; }

    //    public abstract string?[] string?Arry { get; set; }
    //    public abstract IDictionary<int, string?> DicList { get; set; }
    //    public abstract dynamic DynamicObj { get; set; }
    //}

    /// <summary>
    /// 测试枚举。需要测试 数字 字符串向枚举的转换
    /// </summary>
    public enum TestEnum
    {
        Open = 10,
        Close = 20,
        All = 30
    }

    [Serializable]
    public class TestEntity_Int
    {
        [Key(0)]
        public int Integr { get; set; }

        [Key(1)]
        public double Double { get; set; }

        [Key(2)]
        public decimal Decimal { get; set; }

        [Key(3)]
        public bool Boolen { get; set; }

        [Key(4)]
        public string? Name { get; set; }

        [Key(5)]
        public TestEnum TestEnum { get; set; }

        [Key(6)]
        public Color TestColor { get; set; }

        [Key(7)]
        public List<int>? IntList { get; set; }

        [Key(8)]
        public List<double>? DoubleList { get; set; }

        [Key(9)]
        public string?[]? StringArry { get; set; }

        [Key(10)]
        public IDictionary<int, string?>? DicList { get; set; }

        [Key(11)]
        public dynamic? DynamicObj { get; set; }
    }

    [Serializable]
    public class TestEntity_String
    {
        [Key(nameof(Integr))]
        public int Integr { get; set; }

        [Key(nameof(Double))]
        public double Double { get; set; }

        [Key(nameof(Decimal))]
        public decimal Decimal { get; set; }

        [Key(nameof(Boolen))]
        public bool Boolen { get; set; }

        [Key(nameof(Name))]
        public string? Name { get; set; }

        [Key(nameof(TestEnum))]
        public TestEnum TestEnum { get; set; }

        [Key(nameof(TestColor))]
        public Color TestColor { get; set; }

        [Key(nameof(IntList))]
        public List<int>? IntList { get; set; }

        [Key(nameof(DoubleList))]
        public List<double>? DoubleList { get; set; }

        [Key(nameof(StringArry))]
        public string?[]? StringArry { get; set; }

        [Key(nameof(DicList))]
        public IDictionary<int, string?>? DicList { get; set; }

        [Key(nameof(DynamicObj))]
        public dynamic? DynamicObj { get; set; }
    }

    [Serializable]
    public class TestEntity_Non
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

#pragma warning restore CA2235 // Mark all non-serializable fields