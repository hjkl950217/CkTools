using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace 内存缓存学习.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IMemoryCache memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        private readonly string key = "AA";

        [HttpGet("Get")]
        public dynamic Get()
        {
            string result = this.memoryCache.Get<string>(this.key);

            return new
            {
                Time = DateTimeOffset.Now,
                result = result
            };
        }

        [HttpGet("Add")]
        public dynamic Add()
        {
            //DateTimeOffset expire = this.GetExpire(10);
            //this.memoryCache.Set(this.key, "ABCD", expire);

            MemoryCacheEntryOptions option = this.GetOption(5);
            this.memoryCache.Set(this.key, "ABCD", option);

            return "OK";
        }

        public DateTimeOffset GetExpire(int expireNum)
        {
            return DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() + expireNum);
        }

        public MemoryCacheEntryOptions GetOption(int expireNum)
        {//SlidingExpiration
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            options.SlidingExpiration = TimeSpan.FromSeconds(expireNum);//滑动过期时间
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expireNum + 3);//绝对过期时间

            return options;
        }
    }
}