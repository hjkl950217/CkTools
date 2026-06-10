using Microsoft.Extensions.DependencyInjection;

namespace 技术点验证
{
    public static class ServiceExtension
    {
        /// <summary>
        /// 添加要映射的配置类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTestConfig<T>(this IServiceCollection services)
        {
            //把T加到IOC中
            //监听并修改数据的部分由配置那边做,这里只是配置上

            return services;
        }
    }
}