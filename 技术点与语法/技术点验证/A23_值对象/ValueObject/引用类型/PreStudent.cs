namespace 技术点验证
{
    /// <summary>
    /// 将要注册为学生的数据<para></para>
    /// PS:主要是命名空间下有下Student了，但作用不一样，不能复用，所以改了下名字
    /// </summary>
    public class PreStudent : ValueObjectBase<PreStudent_Entity>
    {
        public PreStudent(PreStudent_Entity data) : base(data)
        {
        }

        public override bool BizCheckValue()
        {
            List<StuClassInfo> t = this.Value.ClassInfos.Value;

            return this.Value.StudentID > 0
                && this.Value != null//模拟实体类的业务规则，这里只是不可空
                && !this.Value.ClassInfos.Value.Any(t => t.Value.ClassName == this.Value.Name);//模拟业务规则，这里是学员姓名不能与任何一个班级名相等
        }

        protected override IEnumerable<object> GetEqualityComponents(PreStudent_Entity value)
        {
            yield return this.Value.StudentID;
            yield return this.Value.Name;

            foreach(object item in this.Value.ClassInfos.Value)//针对数组、集合类型
            {
                yield return item;
            }

            //这里忽略了Msg属性，因为从设计上来说Msg信息与学员是否重复没有关系
        }
    }
}