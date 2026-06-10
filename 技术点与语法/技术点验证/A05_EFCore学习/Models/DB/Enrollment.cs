namespace 技术点验证
{
    /// <summary>
    /// 修读纪录
    /// </summary>
    public class Enrollment
    {
        /// <summary>
        /// 修读ID-主键
        /// </summary>
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        /// <summary>
        /// 年纪
        /// </summary>
        public Grade? Grade { get; set; }

        /// <summary>
        /// 关联的Course-单个
        /// </summary>
        public Course? Courses { get; set; }

        /// <summary>
        /// 关联的Student-单个
        /// </summary>
        public Student? Students { get; set; }
    }

    public enum Grade
    {
        A,
        B,
        C,
        D
    }
}