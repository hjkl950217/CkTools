using 技术点验证.DataConvertHander;

namespace 技术点验证.TestSensorDataHander
{
    public interface ITestSensorDataHander : IDataConvertHander<SensorTypeEnum, Dictionary<string, string>, SensorDataInfo>
    {
        ///// <summary>
        ///// 指示针对什么类型处理
        ///// </summary>
        //public SensorTypeEnum HanderSensorType { get; }

        ///// <summary>
        ///// 解析代码,(若传递参数为null或没数据，则返回null对象)
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //SensorDataInfo[] Parse(Dictionary<string, string> data);
    }
}