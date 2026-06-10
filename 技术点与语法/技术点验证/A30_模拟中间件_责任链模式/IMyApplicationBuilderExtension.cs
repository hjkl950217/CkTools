using AspectCore.DynamicProxy.Parameters;

namespace 技术点验证
{
#pragma warning disable RCS1021 // Simplify lambda expression.

    public static class IMyApplicationBuilderExtension
    {
        /// <summary>
        ///  将内联中间件的委托添加到应用程序的请求管道。
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="inlineDelegate">用于内联中间件的委托。传入<see cref="MockHttpContext"/>与下一个组件,内联后返回<see cref="Task"/></param>
        /// <returns></returns>
        public static MyApplicationBuilder Use(
            this IMyApplicationBuilder applicationBuilder,
            [NotNull] Func<MockHttpContext, Func<Task>, Task> inlineDelegate)
        {
            return applicationBuilder.Use(next => { return context => inlineDelegate(context, () => next(context)); });
        }

        /// <summary>
        /// 将终端中间件委托添加到应用程序的请求管道。
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="inlineDelegate">用于内联终端中间件的委托</param>
        /// <returns></returns>
        public static MyApplicationBuilder Run(
            this IMyApplicationBuilder applicationBuilder,
            Func<MockHttpContext, Task> inlineDelegate)
        {
            return applicationBuilder.Use(_ => { return context => inlineDelegate(context); });
        }

        /// <summary>
        /// 将终端中间件委托添加到应用程序的请求管道。
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="inlineDelegate">用于内联终端中间件的委托</param>
        /// <returns></returns>
        public static MyApplicationBuilder Run(
            this IMyApplicationBuilder applicationBuilder,
            [NotNull] Action<MockHttpContext> inlineDelegate)
        {
            return applicationBuilder.Run(context =>
            {
                inlineDelegate(context);
                return Task.CompletedTask;
            });
        }
    }

#pragma warning restore RCS1021 // Simplify lambda expression.
}