using AutoMapper学习与练习.Orders;
using AutoMapper学习与练习.Orders.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapper学习与练习
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 添加Mapper映射关系
        /// </summary>
        /// <param name="services"></param>
        public static void AddMapper(IServiceCollection services)
        {
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg =>
           {
               //配置替换字符，一定要在创建map之前
               //cfg.ReplaceMemberName( "Ä" , "A" );//把源类型属性名中的Ä替换成A

               cfg.AddProfile(new OrderMapFile());

               // cfg.AddExpressionMapping();

               // 映射具有public或internal的get的属性
               cfg.ShouldMapProperty = p =>
                  p.GetMethod != null
                  && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly);
           });

            config.AssertConfigurationIsValid();
            AutoMapper.IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddIoc(IServiceCollection services)
        {
            services.AddScoped<IOrderServices, OrderServices>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddMapper(services);

            AddIoc(services);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}