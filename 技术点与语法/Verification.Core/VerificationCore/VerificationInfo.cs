using System;
using System.Diagnostics.CodeAnalysis;

namespace Verification.Core
{
    public class VerificationInfo
    {
        public VerificationInfo(
            VerificationTypeEnum type,
           [NotNull] Func<IVerification> factory)
        {
            this.VerificationType = type;
            this.VerificationFactory = factory;

            this.verificationInstance = new Lazy<IVerification>(factory, isThreadSafe: false);
        }

        public VerificationTypeEnum VerificationType { get; private set; }
        private Lazy<IVerification> verificationInstance;
        public IVerification VerificationInstance { get => this.verificationInstance.Value; }
        public Func<IVerification> VerificationFactory { get; private set; }

        public static VerificationInfo Create(
            VerificationTypeEnum type,
            Func<IVerification> factory)
        {
            return new VerificationInfo(type, factory);
        }
    }
}