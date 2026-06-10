/// <summary>
/// 现在人们的生活水平都提高了，有钱了，我今天就以汽车组装为例子
/// 每台汽车的组装过程都是一致的，所以我们使用同样的构建过程可以创建不同的表示(即可以组装成不同型号的汽车，不能像例子这样，一会别克，一会奥迪的)
/// 组装汽车、电脑、手机、电视等等负责对象的这些场景都可以应用建造者模式来设计
/// </summary>
namespace 语法验证与学习
{
    [VerifcationType(VerificationTypeEnum.B01_建造者模式学习)]
    internal class B01_建造者模式学习 : IVerification
    {
        public void Start(string[]? args)
        {
            IBuilder buickCarBuilder = new BuickBuilder();
            IBuilder aoDiCarBuilder = new AoDiBuilder();

            //别克
            Car buickCar = buickCarBuilder
                .UseCarDoor()
                .UseCarEngine()
                .UseCarWheel("雪地专用车轮") //车不可能没有轮子，如果没有调用这个方法，能不能装一个默认的？
                .BuildCar();
            //buickCar.Show();

            Console.WriteLine();

            //奥迪
            Car aoDiCar = aoDiCarBuilder
                .UseCarDoor()
                .UseCarEngine()
                .UseCarWheel()
                .BuildCar();
            // aoDiCar.Show();
        }
    }

    /// <summary>
    /// [调度器]
    /// 这个类才是组装的，Construct方法里面的实现就是创建复杂对象固定算法的实现，该算法是固定的，或者说是相对稳定的
    /// 这个人当然就是老板了，也就是建造者模式中的指挥者
    ///// </summary>
    //public static class Scheduler
    //{
    //    // 组装汽车
    //    public static Car Build(this IBuilder builder)
    //    {
    //        return builder.BuildCar();
    //    }
    //}
}