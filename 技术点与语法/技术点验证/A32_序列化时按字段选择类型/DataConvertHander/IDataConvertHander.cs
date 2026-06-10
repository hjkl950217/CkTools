namespace 技术点验证.DataConvertHander
{
    /// <summary>
    /// 数据转换接口定义
    /// </summary>
    /// <typeparam name="TType">针对什么类型处理</typeparam>
    /// <typeparam name="TDataSource">数据源类型</typeparam>
    /// <typeparam name="TOutPut">返回体类型</typeparam>
    public interface IDataConvertHander<TType, TDataSource, TOutPut>
    {
        /// <summary>
        /// 指示针对什么类型处理
        /// </summary>
        public TType HanderType { get; }

        /// <summary>
        /// 解析代码,(若传递参数为null或没数据，则返回null对象)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IEnumerable<TOutPut> Parse(TDataSource data);
    }
}