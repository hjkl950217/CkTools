using Microsoft.Extensions.Hosting;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A27_浏览器嗅探)]
    public class A27_浏览器嗅探 : IVerification
    {
        //文章地址：https://mp.weixin.qq.com/s/K436A6aSW2SQEfRyHzs8GA

        public void Start(string[]? args)
        {
            WebProgram.CreateHost().Run();
        }
    }
}