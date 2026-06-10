using System;

namespace Verification.Core
{
    public static class VerificationInfoExtension
    {
        public static VerificationInfo CheckNull(this VerificationInfo? verification)
        {
            if(verification == null)
            {
                throw new NotImplementedException($"{verification?.VerificationType} 发生异常");
            }

            return verification;
        }

        /// <summary>
        /// 开始验证
        /// </summary>
        /// <param name="typeEnum">要验证的类型</param>
        /// <param name="allVerification">验证库中所有注册的验证接口实例.A系列和B系列是分离的</param>
        /// <param name="args">从命令行中获取的参数</param>
        public static VerificationInfo StartVerification(
            this VerificationInfo verification,
            VerificationTypeEnum verificationTypeEnum,
            string[]? args)
        {
            Console.WriteLine("开始验证");
            Console.WriteLine($"验证:\t-{verificationTypeEnum.ToString()}-");
            Console.WriteLine("===============================================");
            Console.WriteLine();

            verification.VerificationInstance.Start(args);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("验证结束");
            Console.ReadLine();

            return verification;
        }
    }
}