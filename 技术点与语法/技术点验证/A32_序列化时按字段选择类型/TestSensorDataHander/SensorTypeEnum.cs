namespace 技术点验证.TestSensorDataHander
{
    /// <summary>
    /// 传感器类型
    /// </summary>
    public enum SensorTypeEnum
    {
        /// <summary>
        /// 无，占位
        /// </summary>
        None = 0,

        /// <summary>
        /// 位移传感器(提供位移和加速度)
        /// </summary>
        Displacement = 10,

        /// <summary>
        /// 应力传感器(提供应力)
        /// </summary>
        Stresses = 20,

        /// <summary>
        /// 结构应力传感器
        /// </summary>
        StructureStresses = 30,

        /// <summary>
        /// 土压力传感器
        /// </summary>
        SoilPressure = 40,

        /// <summary>
        /// 孔隙水压力传感器
        /// </summary>
        CrackWaterPressure = 50,

        /// <summary>
        /// 有毒气体
        /// </summary>
        PoisonousGas = 60,

        /// <summary>
        /// 精准小气象
        /// </summary>
        AccurateSmallWeather = 70,

        /// <summary>
        /// 门状态传感器
        /// </summary>
        DoorState = 80,

        /// <summary>
        /// 消防水压力
        /// </summary>
        FireFightWaterPressure = 90,

        /// <summary>
        /// 消防液位高度传感器
        /// </summary>
        FireFightLiquidHeight = 100,

        /// <summary>
        /// 锥形桶姿态传感器
        /// </summary>
        ConicalBarrelPosture = 110,
    }
}