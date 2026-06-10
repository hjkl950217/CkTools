namespace 技术点验证
{
    public class IdCardNo : ValueBase<int>
    {
        public IdCardNo(int data) : base(data)
        {
        }

        public override bool BizCheckValue() => base.Value > -1;
    }
}