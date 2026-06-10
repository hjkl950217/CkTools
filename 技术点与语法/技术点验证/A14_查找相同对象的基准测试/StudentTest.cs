using System.Net;

namespace 技术点验证._A14_查找相同对象的基准测试_
{
    /// <summary>
    /// A14使用的实体类
    /// </summary>
    public class StudentTest
    {
        public string? Name { get; set; }

        public string? Address { get; set; }

        public HttpStatusCode Code { get; set; }

        public int PhoneNumber { get; set; }

        public string? Tag { get; set; }
    }
}