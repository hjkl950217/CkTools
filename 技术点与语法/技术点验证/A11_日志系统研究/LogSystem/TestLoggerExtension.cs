using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace 技术点验证
{
    public static class TestLoggerExtension
    {
        public static ILoggingBuilder AddTestLogger(
            this ILoggingBuilder loggingBuilder,
            Func<string, LogLevel, bool>? globalCategoryLevelFilter = null,
            Func<string, LogLevel, bool>? testLoggerCategoryLevelFilter = null)
        {
            if(globalCategoryLevelFilter != null)
            {
                //这种是全局过滤的,第一个参数是categoryName
                loggingBuilder.AddFilter(globalCategoryLevelFilter);
            }

            loggingBuilder.Services.AddTestLogClientForConsole();
            loggingBuilder.Services.AddSingleton<ILoggerProvider>(t =>
                new TestLoggerProvider(
                    t.GetService<ILogClient>(),
                    testLoggerCategoryLevelFilter)
            );

            return loggingBuilder;

            /*
             * ILoggerFactory 的方式 也是第一次new一个Provider
             *
             * https://github.com/aspnet/Extensions/blob/f162f1006bf8954f0102af8ff98c04077cf21b04/src/Logging/Logging.Console/src/ConsoleLoggerFactoryExtensions.cs
             */
        }

        public static ILoggingBuilder AddTestLogger(
          this ILoggingBuilder loggingBuilder,
          Func<string, LogLevel, bool>? globalFilter = null)
        {
            return loggingBuilder.AddTestLogger(
                globalCategoryLevelFilter: globalFilter,
                testLoggerCategoryLevelFilter: null);
        }
    }
}