using Microsoft.ML.Data;

namespace 技术点验证
{
    /// <summary>
    /// 鸢尾花数据实体
    /// </summary>
    /// <remarks>
    /// 用作训练的时候的数据结构，并且用作预测操作的输入实体
    /// </remarks>
    public class IrisData
    {
        /// <summary>
        /// 萼片长度
        /// </summary>
        [LoadColumn(0)]
        public float SepalLength;

        /// <summary>
        /// 萼片宽度
        /// </summary>
        [LoadColumn(1)]
        public float SepalWidth;

        /// <summary>
        /// 花瓣长度
        /// </summary>
        [LoadColumn(2)]
        public float PetalLength;

        /// <summary>
        /// 花瓣宽度
        /// </summary>
        [LoadColumn(3)]
        public float PetalWidth;

        /// <summary>
        /// 标签,或是说类型
        /// </summary>
        /// <remarks>
        /// 数据中最后一列为标签，用于在算法的训练和测试阶段标识每个数据行。
        /// </remarks>
        [LoadColumn(4)]
        [ColumnName(nameof(Label))] //这行不是必须
        public string? Label;
    }
}