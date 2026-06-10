using Microsoft.AspNetCore.SignalR;

namespace 技术点验证.A47_SinglR示例.Controllers
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"客户端 {this.Context.ConnectionId} 连接成功。");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"客户端 {this.Context.ConnectionId} 断开连接。异常: {exception?.Message}");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageaaa(string user, string message)
        {
            Console.WriteLine($"收到来自客户端消息[{user}] {this.Context.ConnectionId} : {message}");

            //向所有客户端的订阅的“PublicClientChannel”事件通道发送消息
            //前端通过  connection.on("PublicClientChannel", function (user, message) {}订阅
            await this.Clients.All.SendAsync("PublicClientChannel", user, message);
        }
    }
}