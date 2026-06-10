namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B15_在CSharp中使用FSharp)]
    public class B14_用函数式检查SellerID : IVerification
    {
        public void Start(string[]? args)
        {
            this.CheckSellerIDFormat("BH12", true);
        }

        #region FP

        /// <summary>
        /// 只判断<paramref name="value"/>是否符合sellerID规则<para></para>
        /// 不处理大小写 空格 长度等问题
        /// </summary>
        /// <returns></returns>
        private bool CheckSellerIDFormat(string? value, bool enableStrictMode)
        {
            /*
             * sellerID生成规则：SP  s7edidb01.mktpls.UP_EDI_GenerateSellerID_V2
             *
             * 4位，每一位可能是大写字母或数字
             *
             */

            if(enableStrictMode)
            {
                return checkSellerIDFormatByStrictMode(value);
            }
            else
            {
                return checkSellerIDFormatByNormalMode(value);
            }
        }

        #region 如果有FP库就不用写的函数

        public static Func<string?, bool> isNull = c => c == null;
        public static Func<string?, bool> isNotNull = c => c != null;
        public static Func<string?, bool> isNotNullOrEmpty = c => !string.IsNullOrEmpty(c);
        public static Func<char, bool> isUpper = char.IsUpper;

        public static Func<char, bool> isInt = c => 47 < c && c < 58;//0~9 asc值：48-57
        public static Func<char, bool> isUpperOrInt = c => isUpper(c) || isInt(c);

        public static Func<string?, int> strLength = str => (str ?? string.Empty).Length;
        public static Func<string?, int> strTrimLength = str => str == null ? 0 : str.Trim().Length;

        public static Func<Func<string?, int>, Func<string?, int, bool>> equalOperator = getLength => (str, num) => getLength(str) == num;

        public static Func<Func<string?, int, bool>, int, Func<string?, bool>> checkLength = (@operator, length) => str => @operator(str, length);

        public static Func<Func<string?, bool>[], Func<string?, bool>> pipe = GetPipeByType<string?>();

        #endregion 如果有FP库就不用写的函数

#pragma warning disable CS8602 // 取消引用可能出现的空引用。

        public static Func<string?, bool> checkSellerIDFormat = str => isNotNull(str)
          && isUpper(str[0])
          && isUpperOrInt(str[1])
          && isUpperOrInt(str[2])
          && isUpperOrInt(str[3]);

#pragma warning restore CS8602 // 取消引用可能出现的空引用。

        //严格模式
        public static Func<string?, bool> checkSellerIDFormatByStrictMode = pipe(Collect(
            isNotNullOrEmpty,
            checkLength(equalOperator(strLength), 4),
            checkSellerIDFormat
            ));

        //宽容模式
        public static Func<string?, bool> checkSellerIDFormatByNormalMode = pipe(Collect(
            isNotNull,
            checkLength(equalOperator(strTrimLength), 4)
            ));

        /// <summary>
        /// 获得一个管道。单独一个方法是为了用泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Func<Func<T, bool>[], Func<T, bool>> GetPipeByType<T>()
        {
            return methods => inputValue =>
            {
                for(int i = 0 ; i < methods.Length ; i++)
                {
                    if(!methods[i](inputValue))
                        return false;//有一个返回false则中断，然后直接返回 表示检查不过
                }
                return true;
            };
        }

        public static T[] Collect<T>(params T[] source)
        {
            return source;
        }

        #endregion FP
    }
}