using CkTools.Attribute;

namespace 技术点验证.TestSensorDataHander
{
    /// <summary>
    /// 传感器数据类型
    /// </summary>
    /// <remarks>
    /// 定义中<see cref="EnumDescriptionAttribute.DBValue"/>为返回接口的字段名 <para></para>
    /// 定义中<see cref="EnumDescriptionAttribute.Description"/>为单位
    /// </remarks>
    public enum SensorDataTypeEnum
    {
        #region 位移传感器

        /// <summary>
        /// 位移
        /// </summary>
        [EnumDescription(ShowValue: "位移", DbValue: "displacement")]
        Displacement = 10,

        /// <summary>
        /// 加速度
        /// </summary>
        [EnumDescription(ShowValue: "加速度", DbValue: "accSpeed")]
        Acceleration = 20,

        #endregion 位移传感器

        #region 应力传感器

        /// <summary>
        /// 应力
        /// </summary>
        [EnumDescription(ShowValue: "应力", DbValue: "stress")]
        Stresses = 30,

        #endregion 应力传感器

        #region 结构应力传感器

        /// <summary>
        /// 结构应力
        /// </summary>
        [EnumDescription(ShowValue: "结构应力")]
        StructureStresses = 40,

        #endregion 结构应力传感器

        #region 土压力传感器

        /// <summary>
        /// 土压力
        /// </summary>
        [EnumDescription(ShowValue: "土压力")]
        SoilPressure = 50,

        #endregion 土压力传感器

        #region 孔隙水压力传感器

        /// <summary>
        /// 孔隙水压力
        /// </summary>
        [EnumDescription(ShowValue: "孔隙水压力")]
        CrackWaterPressure = 60,

        #endregion 孔隙水压力传感器

        #region 有毒气体传感器模块

        /// <summary>
        /// 甲烷(瓦斯)
		///<para>单位：百万分比浓度（ppm）</para>
        ///<para>PPM:PPM表示一百万份单位质量的溶液中所含溶质的质量</para>
        /// </summary>
        [EnumDescription(ShowValue: "甲烷", DbValue: "CH4", Description: "ppm")]
        CH4 = 70,

        /// <summary>
        /// 氨气
		///<para>单位：百万分比浓度（ppm）</para>
        ///<para>PPM:PPM表示一百万份单位质量的溶液中所含溶质的质量</para>
        /// </summary>
        [EnumDescription(ShowValue: "氨气", DbValue: "NH3", Description: "ppm")]
        NH3 = 80,

        /// <summary>
        /// 一氧化碳
		///<para>单位：百万分比浓度（ppm）</para>
        ///<para>PPM:PPM表示一百万份单位质量的溶液中所含溶质的质量</para>
        /// </summary>
        [EnumDescription(ShowValue: "一氧化碳", DbValue: "CO", Description: "ppm")]
        CO = 90,

        /// <summary>
        ///PM2.5
		///<para>单位：微克每立方米（ug/m3）</para>
        /// </summary>
        [EnumDescription(ShowValue: "PM2.5", DbValue: "PM25", Description: "ug/m3")]
        PM25 = 100,

        /// <summary>
        ///PM10
		///<para>单位：微克每立方米（ug/m3）</para>
        /// </summary>
        [EnumDescription(ShowValue: "PM10", DbValue: "PM10", Description: "ug/m3")]
        PM10 = 101,

        /// <summary>
        /// 二氧化硫
		///<para>单位：百万分比浓度（ppm）</para>
        ///<para>PPM:PPM表示一百万份单位质量的溶液中所含溶质的质量</para>
        /// </summary>
        [EnumDescription(ShowValue: "二氧化硫", DbValue: "SO2", Description: "ppm")]
        SO2 = 110,

        /// <summary>
        /// 氢硫酸
		///<para>单位：百万分比浓度（ppm）</para>
        ///<para>PPM:PPM表示一百万份单位质量的溶液中所含溶质的质量</para>
        /// </summary>
        [EnumDescription(ShowValue: "氢硫酸", DbValue: "H2S", Description: "ppm")]
        H2S = 120,

        /// <summary>
        /// 二氧化氮
		///<para>单位：百万分比浓度（ppm）</para>
        ///<para>PPM:PPM表示一百万份单位质量的溶液中所含溶质的质量</para>
        /// </summary>
        [EnumDescription(ShowValue: "二氧化氮", DbValue: "NO2", Description: "ppm")]
        NO2 = 130,

        /// <summary>
        /// 二氧化碳
		///<para>单位：百万分比浓度（ppm）</para>
        ///<para>PPM:PPM表示一百万份单位质量的溶液中所含溶质的质量</para>
        /// </summary>
        [EnumDescription(ShowValue: "二氧化碳", DbValue: "CO2", Description: "ppm")]
        CO2 = 140,

        /// <summary>
        /// 温度
		///<para>单位：摄氏度（℃）</para>
        /// </summary>
        [EnumDescription(ShowValue: "温度", DbValue: "temperature", Description: "℃")]
        Temperature = 150,

        /// <summary>
        /// 湿度
		///<para>单位：相对湿度（%RH）</para>
        /// </summary>
        [EnumDescription(ShowValue: "湿度", DbValue: "humidity", Description: "%RH")]
        Humidity = 160,

        /// <summary>
        /// 能见度
		///<para>单位：米（M）</para>
        /// </summary>
        [EnumDescription(ShowValue: "湿度", DbValue: "visibility", Description: "M")]
        Visibility = 170,

        #endregion 有毒气体传感器模块

        #region 精准小气象数据传感器模块

        /// <summary>
        ///风速
		///<para>单位：米每秒（m/s）</para>
        /// </summary>
        [EnumDescription(ShowValue: "风速", DbValue: "windSpeed", Description: "m/s")]
        WindSpeed = 180,

        /// <summary>
        /// 风向角度值
        /// <para>转换为汉字时可使用<see cref="DirectionAngleType8Enum"/>相关的方法</para>
        /// </summary>
        [EnumDescription(ShowValue: "风向角度值", DbValue: "windDirection")]
        WindDirection = 190,

        /// <summary>
        /// 降雨量
		///<para>单位：毫米（mm）</para>
        /// </summary>
        [EnumDescription(ShowValue: "降雨量", DbValue: "precipitation", Description: "mm")]
        Precipitation = 200,

        //Temperature = 210,
        //Humidity = 220,

        /// <summary>
        ///气压
		///<para>单位：百帕（hpa）</para>
        /// </summary>
        [EnumDescription(ShowValue: "气压", DbValue: "airPressure", Description: "hPa")]
        AirPressure = 230,

        // Visibility = 240,

        //2020-07-13 最开始有，后面拿掉。如果其它传感器有这个类型直接迁移过去，小气象没有了
        ///// <summary>
        ///// 结冰
        /////<para>单位：毫米（mm）</para>
        ///// </summary>
        //[EnumDescription(ShowValue: "结冰", DbValue: "freeze", Description: "mm")]
        //Freeze = 250,

        //PM25 = 260,
        //PM10 = 270,
        //CO=271,
        //NO2=272,
        //NH3=273

        #endregion 精准小气象数据传感器模块

        #region 门状态传感器模块

        /// <summary>
        /// 门状态
        ///<para>0：开</para>
        ///<para>1：关</para>
        /// </summary>
        [EnumDescription(ShowValue: "门状态", DbValue: "status")]
        DoorState = 280,

        #endregion 门状态传感器模块

        #region 消防液位高度传感器

        /// <summary>
        /// 消防液位高度
		///<para>单位：米（M）</para>
        /// </summary>
        [EnumDescription(ShowValue: "消防液位高度", DbValue: "liquidLevel", Description: "M")]
        FireFightLiquidHeight = 290,

        // Temperature = 290,
        /// <summary>
        /// 电量
		///<para>百分比（％）</para>
        /// </summary>
        [EnumDescription(ShowValue: "电量", DbValue: "electricQuantity", Description: "%")]
        ElectricQuantity = 300,

        /// <summary>
        /// 信号强度
		///<para>单位：分贝毫瓦（dbm）</para>
        /// </summary>
        [EnumDescription(ShowValue: "信号强度", DbValue: "signalStrength", Description: "dbm")]
        SignalStrength = 310,

        #endregion 消防液位高度传感器

        #region 孔隙水压力传感器

        /// <summary>
        /// 消防水压力
		///<para>单位：兆帕（MPa）</para>
        /// </summary>
        [EnumDescription(ShowValue: "孔隙水压力", DbValue: "pressure", Description: "MPa")]
        FireFightWaterPressure = 320,

        #endregion 孔隙水压力传感器

        #region 锥形桶姿态传感器

        /// <summary>
        /// 经度
        /// </summary>
        [EnumDescription(ShowValue: "经度", DbValue: "longitude")]
        Longitude = 330,

        /// <summary>
        /// 纬度
        /// </summary>
        [EnumDescription(ShowValue: "纬度", DbValue: "latitude")]
        Latitude = 340,

        /// <summary>
        /// 海拔高度
		///<para>单位：米（M）</para>
        /// </summary>
        [EnumDescription(ShowValue: "海拔高度", DbValue: "height", Description: "M")]
        AltitudeHeight = 350,

        /// <summary>
        /// 锥形桶姿态
        /// <para>0： 正常</para>
        /// <para>1： 不正常</para>
        /// </summary>
        [EnumDescription(ShowValue: "锥形桶姿态", DbValue: "posture", Description: "")]
        ConicalBarrelPosture = 360,

        #endregion 锥形桶姿态传感器
    }
}