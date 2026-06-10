namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A12_扫描泛型接口_判断类型)]
    public class A12_扫描泛型接口_判断类型 : IVerification
    {
        public void Start(string[]? args)
        {
            #region 1.获取泛型结构

            //Type genericClass = typeof(Generic<>);//获取基本泛型结构
            //Type genericClass2 = typeof(Generic2<,>);//实例获取双参数基本泛型结构

            ////1.1 用参数结构制作实参泛型结构
            //string? typeName = "System.string?";
            //Type typeArgument = Type.GetType(typeName);//泛型形参结构
            //Type constructedClass = genericClass.MakeGenericType(typeArgument);
            //object created = Activator.CreateInstance(constructedClass);

            #endregion 1.获取泛型结构

            #region 2.确定结构是否为泛型

            // IsGenericType 结构是否为泛型
            // IsGenericTypeDefinition 结构是滞为泛型定义

            //Type strType = typeof(string?);
            //Type genericType = typeof(Generic<>);
            //Type genericType2 = genericType.MakeGenericType(strType);

            //Console.WriteLine($"strType-IsGenericType\t{strType.IsGenericType}");//False
            //Console.WriteLine($"strType-IsGenericTypeDefinition\t{strType.IsGenericTypeDefinition}");//False

            //Console.WriteLine($"genericType-IsGenericType\t{genericType.IsGenericType}");//True
            //Console.WriteLine($"genericType-IsGenericTypeDefinition\t{genericType.IsGenericTypeDefinition}");//True

            //Console.WriteLine($"genericType2-IsGenericType\t{genericType2.IsGenericType}");//True
            //Console.WriteLine($"genericType2-IsGenericTypeDefinition\t{genericType2.IsGenericTypeDefinition}");//False

            #endregion 2.确定结构是否为泛型

            #region 3.获取泛型参数数组

            //{type}.GetGenericArguments()

            //Type strType = typeof(string?);
            //Type genericType = typeof(Generic<>);
            //Type genericType2 = genericType.MakeGenericType(strType);

            //Type[] typeArry = new Type[] { strType, genericType, genericType2 };

            ////遍历上面3个
            //foreach (var itemA in typeArry)
            //{
            //    //遍历每个结构中的参数
            //    //Generic<string?> 会包括2个  它自己和string?的
            //    Console.WriteLine($"TypeName:  {itemA.FullName}");
            //    foreach (Type item in itemA.GetGenericArguments())
            //    {
            //        Console.WriteLine(item.FullName);
            //    }
            //    Console.WriteLine();
            //    Console.WriteLine("-----------");
            //}

            #endregion 3.获取泛型参数数组

            #region 4.扫描程序集中所有类型

            //Func<Type, bool> func = i =>
            //{
            //    return i.IsClass //类
            //           && i.IsPublic //公共的
            //                         //&& i.IsGenericType == false //泛型类型
            //                         // && i.IsGenericTypeDefinition == false//泛型具体实现
            //                         // && i.IsAbstract == false //抽象

            //           ;
            //};

            //var types = AppDomain.CurrentDomain.GetAssemblies()
            // .SelectMany(i =>
            // {
            //     try
            //     {
            //         return i.GetExportedTypes();
            //     }
            //     catch
            //     {
            //         return new Type[0];
            //     }
            // })
            // .Where(func)
            //.Where(t=>t.FullName.Contains("Generic2"))//这只是为了演示，需要过滤一部分
            //.Select(type => (TypeName: type.FullName, type))
            //.ToList()
            //;

            //foreach (var (TypeName, type) in types)
            //{
            //    Console.WriteLine($"TypeName:{TypeName} IsEnum:{type.IsEnum}");

            //}

            #endregion 4.扫描程序集中所有类型
        }
    }

    public interface IGeneric<T>
    {
        void ShowInfo();
    }

    public class Generic<T> : IGeneric<T>
    {
        public void ShowInfo()
        {
            Console.WriteLine("T={0}", typeof(T));
        }
    }

    public class Generic3 : IGeneric<string?>
    {
        public void ShowInfo()
        {
            Console.WriteLine("T={0}", typeof(string));
        }
    }

    public class Generic2<T, U>
    {
        public Generic2()
        {
            Console.WriteLine("T={0},U={1}", typeof(T), typeof(U));
        }
    }
}