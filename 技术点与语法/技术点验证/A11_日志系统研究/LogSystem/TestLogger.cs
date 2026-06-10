using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace 技术点验证
{
    public class TestLogger : ILogger
    {
        private readonly string categoryName;
        private readonly ILogClient logClient;
        private readonly Func<string, LogLevel, bool> categoryLevelFilter;

        public TestLogger(string? categoryName,
            ILogClient logClient,
            Func<string, LogLevel, bool>? categoryLevelFilter)
        {
            this.categoryName = categoryName ??= string.Empty;
            this.logClient = logClient;
            this.categoryLevelFilter = categoryLevelFilter ??= (cate, logLevel) => true;
        }

        public IDisposable? BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return this.categoryLevelFilter(this.categoryName, logLevel);
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            [NotNull] Func<TState, Exception, string?> formatter)
        {
            if(state == null || exception.IsNullOrEmpty())
                return;

            Task.Run(() =>
            {
                string msg = $"logLevel:{logLevel} eventId:{eventId}{Environment.NewLine}msg:{formatter(state, exception)}{Environment.NewLine}";

                this.logClient.WriteLog(msg);
            });
        }
    }
}