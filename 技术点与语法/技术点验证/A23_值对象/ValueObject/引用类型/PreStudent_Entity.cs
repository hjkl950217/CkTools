#nullable disable

namespace 技术点验证
{
    /// <summary>
    /// 不要直接使用此Entity,请使用<see cref="PreStudent"/>
    /// </summary>
    public class PreStudent_Entity
    {
        /// <summary>
        /// Pre学生ID
        /// </summary>
        public int StudentID { get; set; }

        /// <summary>
        /// 班级信息-模拟值类型值对象
        /// </summary>
        public ValueObject<string> Name { get; set; }

        /// <summary>
        /// 班级信息-模拟引用类型值对象
        /// </summary>
        /// <remarks>
        /// 模拟嵌套的情况
        /// </remarks>
        public ValueObject<List<StuClassInfo>> ClassInfos { get; set; }

        /// <summary>
        /// 消息-模拟传统属性
        /// </summary>
        public Msg_Entity Msg { get; set; }
    }
}