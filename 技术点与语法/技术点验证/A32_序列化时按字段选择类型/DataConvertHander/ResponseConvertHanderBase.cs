using CkTools.Helper;

namespace 技术点验证.DataConvertHander
{
    /// <summary>
    /// 响应体数据转换基类(针对响应体结构变化的情况，需要根据某一个字段的值反序列化成不同的子类)
    /// </summary>
    /// <typeparam name="TType">响应体中的标记类型,一般使用枚举去对应</typeparam>
    /// <typeparam name="TField">响应体中的数据字段的标记类型，一般使用数据类型枚举去对应</typeparam>
    /// <typeparam name="TDataSource">数据源类型</typeparam>
    /// <typeparam name="TOutPut">返回体类型</typeparam>
    public abstract class ResponseConvertHanderBase<TType, TField, TDataSource, TOutPut> : IDataConvertHander<TType, TDataSource, TOutPut>
        where TDataSource : class
    {
        public TType HanderType { get; }
        private readonly TField[] fieldTypes;

        /// <summary>
        /// 返回结果初始化器
        /// </summary>
        private readonly Func<TDataSource, TType, TOutPut> outPutInitializer;

        /// <summary>
        /// 数据设置器
        /// </summary>
        private readonly Func<TDataSource, TField, TOutPut, TOutPut> outPutSetter;

        protected ResponseConvertHanderBase()
        {
            this.HanderType = this.SetHanderType();
            this.fieldTypes = this.SetFieldTypes().ToArray();
            this.outPutInitializer = this.SetOutPutInitializer();
            this.outPutSetter = this.SetOutPutSetter();

            this.fieldTypes.CheckNullWithException(nameof(this.fieldTypes));
            this.outPutInitializer.CheckNullWithException(nameof(this.outPutInitializer));
            this.outPutSetter.CheckNullWithException(nameof(this.outPutSetter));
        }

        public IEnumerable<TOutPut> Parse(TDataSource dataSource)
        {
            if(dataSource.IsNullOrEmpty())
                return Array.Empty<TOutPut>();

            Func<TOutPut>? initFunc = this.outPutInitializer.Currying()(dataSource)(this.HanderType);

            TOutPut[]? result = ArrayHelper.GetArray<TOutPut>(
               count: this.fieldTypes.Length,
               createFactory: initFunc);

            for(int i = 0 ; i < this.fieldTypes.Length ; i++)
            {
                result[i] = this.outPutSetter(dataSource, this.fieldTypes[i], result[i]);
            }

            return result;
        }

        /// <summary>
        /// [由子类重写]指示针对什么类型处理
        /// </summary>
        /// <returns></returns>
        public abstract TType SetHanderType();

        /// <summary>
        /// [由子类重写]指示针对什么数据类型处理
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TField> SetFieldTypes();

        /// <summary>
        /// [由子类重写]指示如何初始化返回结果
        /// </summary>
        protected abstract Func<TDataSource, TType, TOutPut> SetOutPutInitializer();

        /// <summary>
        /// [重写]由子类重写,指示如何设置返回结果的数据
        /// </summary>
        protected abstract Func<TDataSource, TField, TOutPut, TOutPut> SetOutPutSetter();
    }
}