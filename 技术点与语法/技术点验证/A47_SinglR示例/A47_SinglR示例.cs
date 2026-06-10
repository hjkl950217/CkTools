using Microsoft.Extensions.FileProviders;
using 技术点验证.A47_SinglR示例.Controllers;

namespace 技术点验证.A47_SinglR示例
{
    [VerifcationType(VerificationTypeEnum.A47_SinglR示例)]
    public class A47_SinglR示例 : IVerification
    {
        public static readonly string currentDirectory = Path.Combine(Environment.CurrentDirectory, nameof(A47_SinglR示例), "wwwroot");

        public void Start(string[]? args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://+:5000");

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddSignalR();

            WebApplication app = builder.Build();
            //app.UseHttpsRedirection();

            app.UseDefaultFiles(new DefaultFilesOptions()
            {
                FileProvider = new PhysicalFileProvider(A47_SinglR示例.currentDirectory),
                DefaultFileNames = new List<string>() { "index.html" }
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(A47_SinglR示例.currentDirectory),
                RequestPath = ""
            });
            app.UseWebSockets(new WebSocketOptions
            {
                //sdf
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            });
            app.MapControllers();
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}