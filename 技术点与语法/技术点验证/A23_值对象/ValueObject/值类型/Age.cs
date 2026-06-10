namespace 技术点验证
{
    public class Age : ValueBase<int>
    {
        public Age(int data) : base(data)
        {
        }

        public override bool BizCheckValue()
        {
            return base.Value > 17;
        }

        public override string ErrorMsgForCheckValue(int value)
        {
            return "年龄必须大于18岁";
        }
    }
}