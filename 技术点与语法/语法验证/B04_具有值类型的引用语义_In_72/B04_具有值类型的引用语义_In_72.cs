using System.Drawing;

namespace 语法验证与学习
{
    /*

本项目为纯语法示例

需要查看时，选对应的文件，解除注释直接跑就行

说明看对应代码中的注释。

文件后面的数字代表最低的C#版本，如72就对应C#7.2

     *
     */

    [VerifcationType(VerificationTypeEnum.B04_具有值类型的引用语义_In_72)]
    public class B04_具有值类型的引用语义_In_72 : IVerification
    {
        public void Start(string[]? args)
        {
            this.ValueReference(new Point(10, 200));
            Console.WriteLine();
            this.ObjectReference(new TestStu() { ID = "10" });
        }

        public Point ValueReference(in Point point)
        {
            //这里对任何struct值的修改，写完代码就会报错
            Console.WriteLine($"原始Point X:{point.X}\t原始Point Y:{point.Y}");
            Console.WriteLine($"原始code:{point.GetHashCode()}");

            //输出不同方式获得的数据的哈西值
            Point testPoint = point;
            Console.WriteLine($"赋值后code:{testPoint.GetHashCode()}");
            //Console.WriteLine(testPoint.Equals());

            Point newPoint = new Point() { X = point.X, Y = point.Y };//这里看起来new了 其实哈西值后上面一样的
            Console.WriteLine($"new 1后code:{newPoint.GetHashCode()}");

            Point newPoint2 = new Point() { X = point.X, Y = point.Y + 100 };
            Console.WriteLine($"new 2后code:{newPoint2.GetHashCode()}");

            //经过模拟处理后的值（只能读取，因为修改不了值）
            Console.WriteLine($"处理后Point X:{point.X}\t处理后Point Y:{point.Y}");
            return point;
        }

        public TestStu ObjectReference(in TestStu point)
        {
            Console.WriteLine($"原始TestStu ID:{point.ID}\t原始TestStu Name:{point.Name}");

            point.ID = "AAAA";
            point.Name = "new Name";

            //这里修改不了point的引用。如果直接 point = new TestStu();写完就会报错

            Console.WriteLine($"处理后TestStu ID:{point.ID}\t处理后TestStu Name:{point.Name}");

            return point;
        }
    }
}