using CkTools.Attribute;
using 技术点验证.DataConvertHander;

namespace 技术点验证.TestSensorDataHander
{
    public abstract class TestSensorDataHander : ResponseConvertHanderBase<SensorTypeEnum, SensorDataTypeEnum, Dictionary<string, string>, SensorDataInfo>, ITestSensorDataHander
    {
        protected override Func<Dictionary<string, string>, SensorTypeEnum, SensorDataInfo> SetOutPutInitializer()
        {
            return (dataSource, type) => new SensorDataInfo()
            {
                SensorType = type,
                SensorID = dataSource.GetOrDefault("sensorid", ""),
            };
        }

        protected override Func<Dictionary<string, string>, SensorDataTypeEnum, SensorDataInfo, SensorDataInfo> SetOutPutSetter()
        {
            return (dataSource, dataType, oldObj) =>
            {
                oldObj.SensorDataType = dataType;
                oldObj.Value = dataSource
                    .GetOrDefault(dataType.GetDBValue(), "")
                    .ToDecimalOrDefault();

                return oldObj;
            };
        }
    }
}