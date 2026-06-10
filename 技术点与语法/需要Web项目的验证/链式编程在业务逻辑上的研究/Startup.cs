using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using 链式编程在业务逻辑上的研究.Orders;
using 链式编程在业务逻辑上的研究.Orders.DTO;
using 链式编程在业务逻辑上的研究.Role;

namespace 链式编程在业务逻辑上的研究
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
               cfg.AddProfile(new OrderMapFile());
           });

            AutoMapper.IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }

        public static void AddIoc(IServiceCollection services)
        {
            services.AddScoped<IOrderServices, OrderServices>();

            services.AddSingleton<ICheckRole, CheckRole>();
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