using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace 技术点验证
{
    [ApiController]
    [Produces("application/json")]
    [Route("A27/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public dynamic TestGet()
        {
            string userAgent = base.HttpContext.Request.Headers[HeaderNames.UserAgent];
            return new
            {
                UserAgent = userAgent
            };
        }
    }
}