namespace 技术点验证
{
    public class ClassName : ValueBase<string>
    {
        public ClassName(string data) : base(data)
        {
        }

        public override bool BizCheckValue()
        {
            //ClassName不允许为null 或 empty ,长度小于10

            return !string.IsNullOrEmpty(this.Value)
                && this.Value.Length < 10;
        }
    }
}