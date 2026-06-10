#pragma warning disable CS8602 // 取消引用可能出现的空引用。

using System;
using System.Linq;
using System.Reflection;
using Verification.Core;

namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B02_用反射设置值)]
    public class B02_用反射设置值 : IVerification
    {
        public void Start(string[]? args)
        {
            Console.WriteLine(TestModel.A.BBB.Data);
            Console.WriteLine("开始测试");

            string? inPut = "";
            while (inPut != "2")
            {
                string? tempIn = Console.ReadLine();

                Ext.GetDataOnWatch<A, B>(tempIn);

                Console.WriteLine("触发一次后的结果");
                Console.WriteLine(TestModel.A.BBB.Data);
            }

            Console.WriteLine("验证结束");
            Console.ReadLine();
        }
    }

    public static class Ext
    {
        public static void GetDataOnWatch<TSystemName, TConfigName>(
             string? data
             , string? systemName = null
             , string? configName = null

             )
             where TSystemName : class
             where TConfigName : class
        {
            if (systemName == null) systemName = typeof(TSystemName).Name;
            if (configName == null) configName = typeof(TConfigName).Name;

            //取A的信息
            var sysObj = typeof(TestModel)
                .GetProperties()
                .Where(t => t.PropertyType == typeof(TSystemName))
                .FirstOrDefault();

            var tttt = sysObj.GetValue(null);

            //取B的信息
            PropertyInfo? confObj = sysObj?.PropertyType
                .GetProperties()
                .Where(t => t.PropertyType == typeof(TConfigName))
                .FirstOrDefault();

            //从上面A中取数据
            //var tttt2 = confObj.GetValue( tttt ) as B;
            // tttt2.Data = "ABC";

            //从其他A的实例中取数据
            // var tttt2 = confObj.GetValue( new A( ) { BBB=new B { Data="新数据"} } ) as B;

            //这个就会失败,传递的必须是confObj.ReflectedType里面的类型
            //var tttt2 = confObj.GetValue(   new B { Data = "新数据" }  ) ;

            /*原因：
            PropertyInfo
            返回指定对象的属性值
            */

            //取具体数据

            var battributes = confObj.PropertyType

                .GetProperties()
                .Where(t => t.PropertyType == typeof(string))
                .First();

            //测试赋值
            //B bbb = new B( ) ;
            //battributes.GetValue( bbb );
            //battributes.SetValue( bbb , "testtet" );

            //测试把A下面的B修改了
            var tttt2 = confObj.GetValue(tttt) as B;
            B newB = new B() { Data = $"{  tttt2.Data }++{data}" };
            confObj.SetValue(tttt, newB);//参数1是上层数据，参数2是要修改的数据
        }
    }
}

#pragma warning restore CS8602 // 取消引用可能出现的空引用。