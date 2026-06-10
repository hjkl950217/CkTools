using System.Text;
using BenchmarkDotNet.Attributes;

namespace 技术点验证
{
    [SimpleJob]
    [ProcessCount(5)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, MemoryDiagnoser]
    public class TestCase_Deserialize
    {
#pragma warning disable CS8618 // 不可为 null 的字段未初始化。请考虑声明为可以为 null。
        private byte[] testIntData;
        private byte[] testStringData;
        private byte[] testNonData;
        private byte[] testDefaultData;
        private byte[] testJsonData;
#pragma warning restore CS8618 // 不可为 null 的字段未初始化。请考虑声明为可以为 null。

        [Params(1 * 100, 1000, 5000, 10000)]
        public int TestTotal;

        [GlobalSetup]
        public void SetUp()
        {
            MsgSerializer.InitializeMsgPackSerializer();
            List<TestEntity_Int> testIntDataT = A08TestHelper.GetIntTestData(this.TestTotal);
            List<TestEntity_String?> testStringData = A08TestHelper.GetIntTestData2(this.TestTotal);
            List<TestEntity_Non> testNonDataT = A08TestHelper.GetIntTestData3(this.TestTotal);
            List<TestEntity_Non> testDefaultDataT = A08TestHelper.GetIntTestData3(this.TestTotal);
            List<TestEntity_Non> testJsonDataT = A08TestHelper.GetIntTestData3(this.TestTotal);

            this.testIntData = MsgSerializer.Serialize(testIntDataT);
            this.testStringData = MsgSerializer.Serialize(testStringData);
            this.testNonData = MsgSerializer.Serialize(testNonDataT);
            this.testDefaultData = MsgSerializer.SerializeDefault(testDefaultDataT);
            this.testJsonData = Encoding.UTF8.GetBytes(MsgSerializer.SerializeJson(testJsonDataT));
        }

        [Benchmark]
        public List<TestEntity_Int> TestIntDeserialize()
        {
            return MsgSerializer.Deserialize<List<TestEntity_Int>>(this.testIntData);
        }

        [Benchmark]
        public List<TestEntity_String> TestStringDeserialize()

        {
            return MsgSerializer.Deserialize<List<TestEntity_String>>(this.testStringData);
        }

        [Benchmark]
        public List<TestEntity_Non> TestNonDeserialize()
        {
            return MsgSerializer.Deserialize<List<TestEntity_Non>>(this.testNonData);
        }

        [Benchmark]
        public List<TestEntity_Non> TestDefaultDeserialize()
        {
            return MsgSerializer.DeserializeDefault<List<TestEntity_Non>>(this.testDefaultData);
        }

        [Benchmark]
        public List<TestEntity_Non> TestJsonDeserialize()
        {
            return MsgSerializer
                .DeserializeJson<List<TestEntity_Non>>(Encoding.UTF8.GetString(this.testJsonData));
        }
    }
}