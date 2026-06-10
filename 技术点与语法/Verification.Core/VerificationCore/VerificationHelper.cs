using System;
using System.Linq;
using System.Reflection;

namespace Verification.Core.VerificationCore
{
    public static class VerificationHelper
    {
        public static void StartVerification(VerificationTypeEnum verificationTypeEnum, string[]? args)
        {
            VerificationHelper.GetVerification(verificationTypeEnum)
                .CheckNull()
                .StartVerification(verificationTypeEnum, args);
        }

        private static VerificationInfo GetVerification(VerificationTypeEnum verificationTypeEnum)
        {
            #region 查找

            //判断是否为class
            Func<Type, bool> isClass = i =>
            {
                return i.IsClass //类
                       && i.IsPublic //公共的
                       ;
            };

            //判断是否为验证类
            Func<Type, bool> isVerification = i => i.GetInterfaces().Contains(typeof(IVerification));

            //判断是否为我们要用的验证类
            Func<Type, bool> isEnumInterface = i =>
            {
                VerifcationTypeAttribute? attr = i.GetCustomAttribute<VerifcationTypeAttribute>();

                if(attr.VerificationTypeEnum == verificationTypeEnum)//判断标签中验证类型
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };

            //组合验证的方法
            Func<Type, bool> typeJudge = i =>
                   isClass(i)
                   && isVerification(i)
                   && isEnumInterface(i);

            Type getType()
            {
                Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(i => { try { return i.GetExportedTypes(); } catch { return Array.Empty<Type>(); } })
                    .Where(typeJudge)
                    .ToArray();

                switch(types.Length)
                {
#pragma warning disable CS8603 // 可能的 null 引用返回。
                    case int a when a < 1:
                        Console.WriteLine($"{verificationTypeEnum}没有找到实现");
                        return null;

                    case int b when b > 1:
                        Console.WriteLine($"找到多个{verificationTypeEnum}的实现，请检查");
                        return null;
#pragma warning restore CS8603 // 可能的 null 引用返回。
                    default:
                        return types[0];
                }
            }

            Type type = getType();
            if(type == null)
                throw new NotSupportedException($"{verificationTypeEnum} 发生异常");

            #endregion 查找

            return VerificationInfo.Create(verificationTypeEnum, () => (IVerification)Activator.CreateInstance(type));
        }
    }
}