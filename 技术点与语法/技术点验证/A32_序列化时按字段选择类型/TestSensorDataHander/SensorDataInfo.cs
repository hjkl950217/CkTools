namespace 技术点验证.TestSensorDataHander
{
    public class SensorDataInfo
    {
        /// <summary>
        /// 传感器本身的ID
        /// </summary>
        public string? SensorID { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public SensorTypeEnum SensorType { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public SensorDataTypeEnum SensorDataType { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTimeOffset AddTime { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        public decimal Value { get; set; }
    }
}