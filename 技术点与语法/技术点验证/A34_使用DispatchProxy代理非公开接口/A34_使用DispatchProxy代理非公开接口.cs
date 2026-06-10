using System.Reflection;
using CkTools;

namespace 技术点验证
{
    public interface IIds
    {
        ICollection<int> Ids { get; set; }
    }

    [VerifcationType(VerificationTypeEnum.A34_使用DispatchProxy代理非公开接口)]
    public partial class A34_使用DispatchProxy代理非公开接口 : IVerification
    {
        public void Start(string[]? args)
        {
            //参考资料：https://www.cnblogs.com/lwqlun/p/11575686.html

            LinkAction.Start()
                .Add(this.UseReflection)
                .Add(this.UseDispatchProxy)
                .BatchRun();
        }

        /// <summary>
        /// 使用反射
        /// </summary>
        public void UseReflection()
        {
            Console.WriteLine("使用反射");

            //查找非公开类型
            System.Type? helloType = Assembly.GetExecutingAssembly()
                .GetType(name: "技术点验证.A34HelloDefinition+Hello", throwOnError: true)
                ?? throw new TypeLoadException("未找到type信息");

            //提取方法
            MethodInfo methodInfo = helloType.GetMethod("ShowHello") ?? throw new TypeLoadException("未找到方法信息");

            //调用
            object? instanceObj = Activator.CreateInstance(helloType);
            Console.WriteLine(methodInfo.Invoke(instanceObj, null));
        }

        public void UseDispatchProxy()
        {
            IIds testInterfaceObj = DispatchProxy.Create<IIds, PublicHelloProxy>();
            _ = testInterfaceObj.Ids;
        }
    }

    public class AAA : IIds
    {
        public ICollection<int> Ids { get; set; }
    }
}