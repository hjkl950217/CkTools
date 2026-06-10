using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace 技术点验证
{
    public static class LogClientExtension
    {
        /*
         * 日志提供者应该决定到底用什么方式纪录日志
         *
         * 这里仅仅只是提供注册，同时调用时只会注册第一个
         *
         */

        /// <summary>
        /// 按IOC中注册ILogClient Txt文件类型
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTestLogClientForTxt(this IServiceCollection services)
        {
            services.TryAddSingleton<ILogClient, TxtLogClient>();
            return services;
        }

        /// <summary>
        /// 按IOC中注册ILogClient 控制台类型
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTestLogClientForConsole(this IServiceCollection services)
        {
            services.TryAddSingleton<ILogClient, ConsoleLogClient>();
            return services;
        }
    }
}