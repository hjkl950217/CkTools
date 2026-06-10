namespace 技术点验证
{
    /// <summary>
    /// 学生课程
    /// </summary>
    public class StuClassInfo : ValueObjectBase<StuClassInfo_Entity>
    {
        public StuClassInfo(StuClassInfo_Entity data) : base(data)
        {
        }

        public override bool BizCheckValue()
        {
            return this.Value.ClassID > 0;//因为calssName用了默认值对象，代表ClassName只要不为null就可以，所以这里没有再加判断
        }

        protected override IEnumerable<object> GetEqualityComponents(StuClassInfo_Entity value)
        {
            yield return this.Value.ClassID;

            /*
             * 如果想改变属性在比较和计算哈希时的行为，可以像下面这样写
             *
             * 比如这里就是忽略了首尾空格.并且因为ClassName是值对象，不可能为null， 这里可以放心的调用Trim方法
             */
            yield return this.Value.ClassName.Value.Trim();
        }
    }
}