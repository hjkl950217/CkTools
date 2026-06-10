namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A19_熵的计算)]
    public class A19_熵的计算 : IVerification
    {
        /*
         * 知乎资料：https://zhuanlan.zhihu.com/p/35379531
         *
         * 博客资料1：https://www.cnblogs.com/pinard/p/6050306.html
         * 博客资料2：https://www.cnblogs.com/pinard/p/6093948.html
         */

        //计算 熵、条件熵、相对熵、交叉熵
        public void Start(string[]? args)
        {
            /*
             * 这里的计算会用到A18中的鸢尾花类型,把类型转换为数字进行计算
             * 1=Iris Setosa(山鸢尾)
             * 2=Iris Versicolour(杂色鸢尾)
             * 3=Iris Virginica(维吉尼亚鸢尾)
             *
             */

            this.CalculationInformationEntropy();
        }

        /// <summary>
        /// 计算信息熵
        /// </summary>
        public void CalculationInformationEntropy()
        {
            double iris_probaility = 1.0 / 3.0;//计算均等概率

            //计算熵的函数 newBase为对数中的底数
            Func<double, double> func_h(double newBase)
            {
                return a => -(3 *  //公式中是相加，这里乘3是因为总数为3个，并且概率是均等的。
                (iris_probaility * Math.Log(a, newBase))
                );
            }

            Console.WriteLine($"鸢尾花的信息熵为(以为2底):{func_h(2)(iris_probaility)}");
            Console.WriteLine($"鸢尾花的信息熵为(以为E底):{func_h(Math.E)(iris_probaility)}");
        }

        /// <summary>
        /// 计算条件熵
        /// </summary>
        public void CalculationConditionalEntropy()
        {
        }

        /// <summary>
        /// 计算相对熵
        /// </summary>
        public void CalculationRelativeEntropy()
        {
        }

        /// <summary>
        /// 计算交叉熵
        /// </summary>
        public void CalculationCrossEntropy()
        {
        }
    }
}