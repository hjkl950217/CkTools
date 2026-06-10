#nullable disable

namespace 技术点验证
{
    /// <summary>
    /// 学生课程实体(结构)<para></para>
    /// 不要直接使用此Entity,请使用<see cref="PreStudent"/>
    /// </summary>
    public class StuClassInfo_Entity
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// 课程名字
        /// </summary>
        public ClassName ClassName { get; set; }
    }
}