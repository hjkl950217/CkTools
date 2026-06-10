namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B06_模式匹配_In_70)]
    public class B06_模式匹配_In_70 : IVerification
    {
        public void Start(string[]? args)
        {
            Console.WriteLine("if中使用is进行类型匹配");
            Circle circle = new Circle(3);
            double result1 = this.CalcuateAreaByIF(circle);
            Console.WriteLine($"Circle的面积是{result1}");
            Console.WriteLine();

            Console.WriteLine("switch中使用表达式进行匹配");
            Rectangle rectangle = new Rectangle(3, 2);
            double result2 = this.CalcuateAreaBySwitch(rectangle);
            Console.WriteLine($"Rectangle的面积是{result2}");
            Console.WriteLine();

            Console.WriteLine("switch中使用var进行匹配");
            string? testStr = string.Empty;
            object? result3 = this.CreateShapeBySwitchVar(testStr);
            Console.WriteLine($"创建出来的图形是:-- {result3?.GetType().Name}--");
            Console.WriteLine();
        }

        public double CalcuateAreaByIF(object shape)
        {
            /*
             * is判断成功后，会为后面的变量赋值
             *
             * 作用域就是IF下面的所有分支(如果这些if下面还有其它的代码)
             *
             */
            double result = 0;
            if(shape is Square s)
            {
                if(s.Side > 0)
                {
                    //如果上面的结论是错的，这里s.side应该是没有值的
                    Console.WriteLine("正方形的边长大于0");
                }

                result = s.Side * s.Side;
            }
            else if(shape is Circle c)
            {
                result = c.Radius * c.Radius * Math.PI;
            }
            else if(shape is Rectangle r)//这是一个结构体类型
            {
                result = r.Length * r.Height;
            }
            else
            {
                Console.WriteLine($"计算面积时遇到不支持的类型:{shape?.GetType().Name}");
                result = 0;
            }

            return result;
        }

        public double CalcuateAreaBySwitch(object shape)
        {
            /*
             * 以前switch只能匹配常量(数值类型或sting)
             * 现在可以使用类型匹配
             *
             *
             * 关于case子句:排布的顺序很重要，会从上向下匹配,匹配到则出去(类似交换机上面的ACL)
             * 如下面的例子，前面2个判断0的，影响范围就很小，最在最上面。
             *
             * case null：只会匹配引用类型，可以适用Rectangle来测试
             *
             * default关键字无论你写在什么位置，永远最后一个执行
             */

            switch(shape)
            {
                case Square s when s.Side == 0:
                case Circle c when c.Radius == 0:
                    return 0;

                case Square s:
                    return s.Side * s.Side;

                case Circle c:
                    return c.Radius * c.Radius * Math.PI;

                //case Rectangle r:
                //    return r.Length * r.Height;

                case null:
                    Console.WriteLine($"计算面积时遇到空对象:{shape?.GetType().Name}");
                    return 0;

                default:
                    Console.WriteLine($"计算面积时遇到不支持的类型:{shape?.GetType().Name}");
                    return 0;
            }
        }

        public object? CreateShapeBySwitchVar(string? shapeDescription)
        {
            /*
             * var关键字可以在写代码时就确定类型
             *
             * 例子中最后有一个判断字符串为空白长度的
             * 代表参数为空字符时，default永远不会被执行
             *
             * 编译器不会报错，但非常建议保留，以防止以后修改时忘记和增强表义完整性
             *
             */

            return shapeDescription switch
            {
                "circle" => new Circle(2),
                "square" => new Square(4),
                "large-circle" => new Circle(12),
                var o when(o?.Trim().Length ?? 0) == 0 => null,// 匹配到空白字符
                _ => "invalid shape description",//永远不会被执行
            };
        }
    }
}