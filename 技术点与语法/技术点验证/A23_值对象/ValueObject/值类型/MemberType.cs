namespace 技术点验证
{
    /*
     * 这个的Crete方法说明：
     *
     * 有一些类不容易简单的通过构造方法创建。
     * 比如枚举值对象，我们知道int和枚举在某种程度上可以看成一样的东西，但在泛型中你不能将int当枚举值使用。
     *
     * 这种情况下引入第二种创建模式--自写Create方法
     * 值对象的构造方法主要是调用你的BizCheckValue方法完成检查，以保证数据从构造方法进来后一定是符合业务规则的。
     *
     * 这里需要完全自写，可以让BizCheckValue永远为true，然后自己在Create方法中完成业务创建。
     * 自己写了创建方法后，就可以完全控制逻辑了
     *
     *
     *
     * 既然这样为什么还要使用值对象呢？
     *
     * 这样写，依旧与我们值对象的理念相符(能使用，就一定符合业务规则)
     * 对外:创建即可用(从构造方法改为调用创建方法)，并且可以提供重载
     * 对内：复用了部分代码
     *
     * 如果不这样做，数据就不能保证是业务正确的
     */

    /// <summary>
    /// 枚举值对象+演示复杂业务规则值对象的创建
    /// </summary>
    public class MemberType : ValueBase<MemberTypeEnum>
    {
        private MemberType(MemberTypeEnum data) : base(data)
        {
        }

        public static MemberType Create(int idCardNo)
        {
            /*
             * 演示需要从 一种数据 转换成另一种数据时如何使用 ValueObject
             *
             * 假定 id  在 1 ~1000 为老师
             *          大于1000 为学生
             *
             */

            return idCardNo switch
            {
                int id when id < 0 => throw new System.ArgumentException(errorMsg),
                int id when id < 1001 => new MemberType(MemberTypeEnum.Teacher),
                _ => new MemberType(MemberTypeEnum.Student),
            };

            //模式匹配版本
            //switch (idCard)
            //{
            //    case int id when id < 0:
            //        throw new System.ArgumentException(errorMsg);

            //    case int id when id < 1000:
            //        return new MemberTypeType(MemberTypeEnum.Teacher);

            //    default:
            //        return new MemberTypeType(MemberTypeEnum.Student);
            //}
        }

        public static MemberType Create(IdCardNo idCardNo) => Create((int)idCardNo);

        public override bool BizCheckValue() => true;

        public const string errorMsg = "idCard must be greater than 0";

        public override string ErrorMsgForCheckValue(MemberTypeEnum memberTypeEnum) => errorMsg;
    }
}