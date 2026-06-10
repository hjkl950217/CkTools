using AspectCore.DynamicProxy;

namespace 技术点验证
{
    public class TestAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 测试用的Name
        /// </summary>
        public string? Name { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine($"触发了-{this.Name}");
                await next(context);
            }
            catch
            {
                throw;
            }
        }

        //public TestAttribute(string? name)
        //{
        //    this.Name = name;
        //}
    }
}