using Microsoft.Extensions.Logging;

namespace 技术点验证
{
    public class TestLoggerProvider : ILoggerProvider
    {
        private readonly ILogClient logClient;
        private readonly Func<string, LogLevel, bool>? loggerCategoryLevelFilter;

        public TestLoggerProvider(
            ILogClient logClient,
            Func<string, LogLevel, bool>? categoryLevelFilter)
        {
            this.logClient = logClient;
            this.loggerCategoryLevelFilter = categoryLevelFilter;
        }

        public ILogger CreateLogger(string? categoryName)
        {
            return new TestLogger(categoryName, this.logClient, this.loggerCategoryLevelFilter);
        }

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposedValue)
            {
                if(disposing)
                {
                    // 释放托管状态(托管对象)。
                }

                //  释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                //  将大型字段设置为 null。

                this.disposedValue = true;
            }
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            this.Dispose(true);
            //  如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}