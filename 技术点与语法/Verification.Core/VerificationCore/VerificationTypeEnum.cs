namespace Verification.Core
{
    /// <summary>
    /// 验证的类型
    /// <para>A：单文件,纯技术点的验证</para>
    /// <para>B：语法、设计思想层面的验证。会有多个文件</para>
    /// </summary>
    public enum VerificationTypeEnum
    {
        #region 技术点

        A01_获取当前路径的方法,
        A02_线程ID验证,
        A03_时间格式验证,
        A04_ConcurrentDictionary的研究,
        A05_EFCore学习,

        /// <summary>
        /// 只用来查看配置与IOC的使用即可
        /// </summary>
        A06_配置与IOC接口,

        A07_动态添加Attribute,
        A08_MessagePack基准测试,
        A09_读取文件并监控变化,
        A10_读取文件到配置系统并监控变化,
        A11_日志系统研究,
        A12_扫描泛型接口_判断类型,
        A13_Asynclocal的坑,
        A14_查找相同对象的基准测试,
        A15_分割字符串的基准测试,
        A16_懒加载,

        /// <summary>
        /// 用马尔可夫链推测出所有可能的场景
        /// </summary>
        A17_马尔可夫链,

        A18_预测鸢尾花的类型_多类分类,
        A19_熵的计算,
        A20_并行查找数组中非0的索引,
        A21_实体方法空间占用,
        A22_从表达式中解析成员变量名,
        A23_值对象,
        A24_使用高阶函数,
        A25_查看调用者信息,
        A26_不同集合的对比纪录,
        A27_浏览器嗅探,
        A28_字符串做加法,
        A29_快速分片,
        A30_模拟中间件_责任链模式,
        A31_快速转换枚举为字典,
        A32_序列化时按字段选择类型,
        A33_快速判断Int是否存在数组,
        A34_使用DispatchProxy代理非公开接口,
        A35_ConfigureAwait配置True或是False,
        A36_Task对比yield的协程,
        A37_构建字符串速度测试,
        A38_Task不等待返回,
        A39_结构体类型的ToString,
        A40_任意长度2进制操作,
        A41_生成WIFI二维码,
        A42_连接Kafka,
        A43_AspNetCore迷你,
        A44_Barrier同步基元的使用示例,
        A45_零宽字符加解密,
        A46_控制台输出load效果,
        A47_SinglR示例,

        #endregion 技术点

        #region 语法类

        B01_建造者模式学习,
        B02_用反射设置值,
        B03_协变和逆变,

        /// <summary>
        /// 在C# 7.2版本才有的
        /// </summary>
        B04_具有值类型的引用语义_In_72,

        B05_表达式树研究,
        B06_模式匹配_In_70,
        B07_递归,
        B08_Chsarp中写函数式编程,
        B09_访问者模式,
        B10_I加加的原子性,
        B11_表达式树修改,
        B12_Switch_In_80,
        B14_用函数式检查SellerID,
        B15_在CSharp中使用FSharp,
        B16_特性_在泛型和委托上标记_40,
        B17_Aggregate,
        B18_ConcurrentDictionary原子操作,
        B19_字符串驻留池,
        B20_框架数据结构测试,
        B21_Task的Wait方法是否任务级暂停,
        B22_Linq使用IndexRange,
        B23_获取调用者的信息,

        #endregion 语法类

        ZZZZ_Non
    }
}