using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace 技术点验证.A43_AspNetCore迷你
{
    [VerifcationType(VerificationTypeEnum.A43_AspNetCore迷你)]
    public class A43_AspNetCore迷你 : IVerification
    {
        /*
         *200行代码，7个对象——让你了解ASP.NET Core框架的本质
         *需要配合文章来看这个代码
         *原文  https://www.cnblogs.com/artech/p/inside-asp-net-core-framework.html
         *爱迪生.周的：https://github.com/XiLife-OSPC/AspNetCore.Mini
         *
         *
         * WebHost
         *  Server
         *  Application(HttpHandle)
         *      多个Middleware
         *
         * 整体
         * WebHost                      代表一个web应用程序
         * Server                       代表一个服务器程序，用来处理： tcp连接与http处理之间的适配
         * Application(HttpHandle)      代表一个处理管道，处理由Server生成的HttpContext并将返回数据写入HttpContext.Response中
         * 处理模型：Pipeline = Server + 多个Middleware   进一步简化模型=> Pipeline = Server + HttpHandle
         *
         *
         * 7个对象
         * HttpContext          服务器与中间件、中间件与中间件之间共享数据的上下文对象
         * RequestDelegate      Http中间节点的处理代码,Action<HttpContext>的异步版本 Func<HttpContext，Task>,因为这个太重要了所以实明成一个委托
         * Middleware           Http处理管道节点,将后续的处理代码 RequestDelegate 和现在要新增的处理代码封装，返回一个 RequestDelegate 用来代表自己+后续的处理代码。这里更多是起包装作用
         * ApplicationBuilder   负责一系列 Middleware 的构建过程
         * Server               传输层的TCP和应用层Http处理之间的适配, tcp请求 => 转成HttpContext 调用Http处理管道 => 管道执行处理  => 将结果转成tcp数据并返回
         * WebHost              代表一个web应用, 就是 Server+HttpHandle
         * WebHostBuilder       负责WebHost的构建过程
         */

        public void Start(string[]? args)
        {
            var webHost = new WebHostBuilder()
                .UseHttpListener()
                .Configure(app => app
                    .Use(FooMiddleware)
                    .Use(BarMiddleware)
                    .Use(BazMiddleware)
                )
                .Build();

            Console.WriteLine("从浏览器访问 获取响应：http://localhost:5000");

            webHost.StartAsync().Wait();
        }

        public static RequestDelegate FooMiddleware(RequestDelegate next)
            => async context =>
            {
                await context.Response.WriteAsync("Foo=>");
                await next(context);
            };

        public static RequestDelegate BarMiddleware(RequestDelegate next)
            => async context =>
            {
                await context.Response.WriteAsync("Bar=>");
                await next(context);
            };

        public static RequestDelegate BazMiddleware(RequestDelegate next)
            => context => context.Response.WriteAsync("Baz");
    }

    public class HttpContext
    {
        public HttpRequest Request { get; }
        public HttpResponse Response { get; }

        public HttpContext(IFeatureCollection features)
        {
            this.Request = new HttpRequest(features);
            this.Response = new HttpResponse(features);
        }
    }

    public class HttpRequest
    {
        private readonly IHttpRequestFeature _feature;

        public Uri Url => this._feature.Url;
        public NameValueCollection Headers => this._feature.Headers;
        public Stream Body => this._feature.Body;

        public HttpRequest(IFeatureCollection features) => this._feature = features.Get<IHttpRequestFeature>();
    }

    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;

        public NameValueCollection Headers => this._feature.Headers;
        public Stream Body => this._feature.Body;
        public int StatusCode { get => this._feature.StatusCode; set => this._feature.StatusCode = value; }

        public HttpResponse(IFeatureCollection features) => this._feature = features.Get<IHttpResponseFeature>();
    }

    public delegate Task RequestDelegate(HttpContext httpContext);

    public interface IApplicationBuilder
    {
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);

        RequestDelegate Build();
    }

    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            this._middlewares.Add(middleware);
            return this;
        }

        public RequestDelegate Build()
        {
            this._middlewares.Reverse();
            return httpContext =>
            {
                RequestDelegate next = _ => { _.Response.StatusCode = 404; return Task.CompletedTask; };
                foreach(var middleware in this._middlewares)
                {
                    next = middleware(next);
                }
                return next(httpContext);
            };
        }
    }

    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }

    public interface IFeatureCollection : IDictionary<Type, object>
    { }

    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection
    { }

    public static partial class Extensions
    {
        public static T Get<T>(this IFeatureCollection features)
            => features.TryGetValue(typeof(T), out var value)
                ? (T)value
                : default(T);

        public static IFeatureCollection Set<T>(this IFeatureCollection features, T feature)
        {
            features[typeof(T)] = feature;
            return features;
        }
    }

    public interface IHttpRequestFeature
    {
        Uri Url { get; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
    }

    public interface IHttpResponseFeature
    {
        int StatusCode { get; set; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
    }

    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private readonly HttpListenerContext _httpListenerContext;

        public HttpListenerFeature(HttpListenerContext context) => this._httpListenerContext = context;

        //请求
        Uri IHttpRequestFeature.Url => this._httpListenerContext.Request.Url;

        NameValueCollection IHttpRequestFeature.Headers => this._httpListenerContext.Request.Headers;
        Stream IHttpRequestFeature.Body => this._httpListenerContext.Request.InputStream;

        //响应
        int IHttpResponseFeature.StatusCode { get => this._httpListenerContext.Response.StatusCode; set => this._httpListenerContext.Response.StatusCode = value; }

        NameValueCollection IHttpResponseFeature.Headers => this._httpListenerContext.Response.Headers;
        Stream IHttpResponseFeature.Body => this._httpListenerContext.Response.OutputStream;
    }

    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;

        public HttpListenerServer(params string[] launchUrls)
        {
            this._httpListener = new HttpListener();
            this._urls = launchUrls.Any() ? launchUrls : new string[] { "http://localhost:5000/" };
        }

        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(this._urls, url => this._httpListener.Prefixes.Add(url));

            this._httpListener.Start();

            while(true)
            {
                var listenerContext = await this._httpListener.GetContextAsync();
                var feature = new HttpListenerFeature(listenerContext);
                var features = new FeatureCollection()
                    .Set<IHttpRequestFeature>(feature)
                    .Set<IHttpResponseFeature>(feature);
                var httpContext = new HttpContext(features);
                await handler(httpContext);
                listenerContext.Response.Close();
            }
        }
    }

    public interface IWebHost
    {
        Task StartAsync();
    }

    public class WebHost : IWebHost
    {
        private readonly IServer _server;
        private readonly RequestDelegate _handler;

        public WebHost(IServer server, RequestDelegate handler)
        {
            this._server = server;
            this._handler = handler;
        }

        public Task StartAsync() => this._server.StartAsync(this._handler);
    }

    public interface IWebHostBuilder
    {
        IWebHostBuilder UseServer(IServer server);

        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);

        IWebHost Build();
    }

    public class WebHostBuilder : IWebHostBuilder
    {
        private IServer _server;
        private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();

        public IWebHostBuilder UseServer(IServer server)
        {
            this._server = server;
            return this;
        }

        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            this._configures.Add(configure);
            return this;
        }

        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach(var configure in this._configures)
            {
                configure(builder);
            }
            return new WebHost(this._server, builder.Build());
        }
    }

    public static partial class Extensions
    {
        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
            => builder.UseServer(new HttpListenerServer(urls));

        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}