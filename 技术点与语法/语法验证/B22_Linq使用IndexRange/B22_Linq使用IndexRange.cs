namespace 语法验证.B22_Linq使用IndexRange
{
    [VerifcationType(VerificationTypeEnum.B22_Linq使用IndexRange)]
    public class B22_Linq使用IndexRange : IVerification
    {
        public void Start(string[]? args)
        {
            IEnumerable<int>? sourceArray = Enumerable.Range(1, 10);
            int[]? takeArray = sourceArray.Take(1..2).ToArray();
            bool two = takeArray[0] == 2;//true
        }
    }
}