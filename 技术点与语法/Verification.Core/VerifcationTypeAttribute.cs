using System;

namespace Verification.Core
{
    /// <summary>
    /// 标记验证类的验证类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class VerifcationTypeAttribute : Attribute
    {
        public VerifcationTypeAttribute(VerificationTypeEnum verificationTypeEnum)
        {
            this.VerificationTypeEnum = verificationTypeEnum;
        }

        public VerificationTypeEnum VerificationTypeEnum { get; }
    }
}