using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace 技术点验证.A47_SinglR示例.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    [Controller]
    public class SocketController : ControllerBase
    {
        private readonly ILogger _logger;

        public SocketController(ILogger<SocketController> logger)
        {
            this._logger = logger;
        }

        //[Route("test")]
        [HttpGet("test")]
        public async Task Get()
        {
            if(this.HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket websockt = await this.HttpContext.WebSockets.AcceptWebSocketAsync();
                await this.Handle(websockt);
            }
            else
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task Handle(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024 * 4]; // 4KB buffer size for receiving data.

            //接收一次发送
            WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while(!receiveResult.CloseStatus.HasValue)
            {
                string resultStr = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                await Console.Out.WriteLineAsync($"接收到了:{resultStr}");

                //准备要发送的信息
                string message = $"你好，接收到了{resultStr}";

                //将message编码后写入buffer中，准备发送
                byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                messageBuffer.CopyTo(buffer, 0);

                //发送
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, messageBuffer.Length),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage, //第一次发送的消息中包含，是一次性消息还是未结束
                    CancellationToken.None
                    );

                //接收下一次
                receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None); //关闭连接
        }
    }
}