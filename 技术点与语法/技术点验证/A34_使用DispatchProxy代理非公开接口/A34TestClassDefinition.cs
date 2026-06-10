using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace 技术点验证
{
    /// <summary>
    /// 定义一个公开的接口用于对外调用
    /// </summary>
    public interface IPublicHello
    {
        string ShowHello();
    }

    public interface IOther
    {
    }

    public class PublicHello : IPublicHello
    {
        private readonly IOther other;

        public PublicHello(IOther other)
        {
            this.other = other;
        }

        public string ShowHello()
        {
            throw new System.NotImplementedException();
        }
    }

    public class PublicHelloProxy : DispatchProxy
    {
        public PublicHelloProxy()
        {
            //如果有IOC，就从IOC中或
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DemoCode : IStartupFilter
    {
        public static IPublicHello GlobalPublicHello;

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                next(builder);
                DemoCode.GlobalPublicHello = builder.ApplicationServices.GetService<IPublicHello>();
            };
        }
    }

    /// <summary>
    /// 模拟最极端的接口定义和实现
    /// </summary>
    public class A34HelloDefinition
    {
        private interface IHello
        {
            string ShowHello();
        }

        private class Hello : IHello
        {
            public string ShowHello() => "this is Hello Class's hello";
        }
    }
}