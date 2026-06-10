using System.Reflection;

namespace 技术点验证.A24_使用高阶函数
{
    [VerifcationType(VerificationTypeEnum.A24_使用高阶函数)]
    public class A24_使用高阶函数 : IVerification
    {
        public void Start(string[]? args)
        {
            Console.WriteLine("具体用法要查看代码");

            /*
            * 高阶函数的解析：
            var t1 = ShowInfo_FromClassType(typeof(TestAttributeInfo));
            var t2 = t1(nameof(TestAttributeInfo.TestGenericParameterMethod));
            var t3 = t2(typeof(GenericParameterAttribute));
            t3();
            */

            ShowInfo_FromClassType(typeof(TestAttributeInfo))
                (nameof(TestAttributeInfo.TestGenericParameterMethod))
                (typeof(GenericParameterAttribute))
                ();
        }

        public static Func<Type, Func<string, Func<Type, Action>>> ShowInfo_FromClassType =
            classType =>
            {
                Console.WriteLine($"类型:{classType.Name}");

                return methodName =>
                {
                    MethodInfo? methodInfo = classType.GetMethod(methodName);
                    if(methodInfo != null)
                    {
                        Console.WriteLine($"方法名签名:{methodInfo.Name}");
                        return attrType => () =>
                        {
                            Console.WriteLine($"类型:{attrType.Name}");
                        };
                    }
                    else
                    {
                        return attrType => () => { };
                    }
                };
            };
    }

    public class TestAttributeInfo
    {
        public void TestGenericParameterMethod<[GenericParameter] T>(T value)
        {
        }
    }

    /*
* 高阶函数的解析：
var t1 = ShowInfo_FromClassType(typeof(TestAttributeInfo));
var t2 = t1(nameof(TestAttributeInfo.TestGenericParameterMethod));
var t3 = t2(typeof(GenericParameterAttribute));
t3();
*/

    //ShowInfo_FromClassType(typeof(TestAttributeInfo))
    //    (nameof(TestAttributeInfo.TestGenericParameterMethod))
    //    (typeof(GenericParameterAttribute))
    //    ();

    [AttributeUsage(AttributeTargets.GenericParameter, Inherited = false, AllowMultiple = false)]
    public sealed class GenericParameterAttribute : Attribute
    {
    }
}