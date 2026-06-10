// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderMapFile.cs" company="Newegg" Author="lw47">
//   Copyright (c) 2018 Newegg.inc. All rights reserved.
// </copyright>
// <summary>
//   OrderMapFile created at  2018-02-24 09:24:54
// </summary>
//<Description>
//
//</Description>
// --------------------------------------------------------------------------------------------------------------------
/**
 *                             _ooOoo_
 *                            o8888888o
 *                            88" . "88
 *                            (\ -_- \)
 *                            O\  =  \O
 *                         ____/`---'\____
 *                       .'  \\\\    \\\  `.
 *                      /  \\|||  :  |||\\  \
 *                     /  _||||| -:- |||||-  \
 *                     |   | \\\  -  /// |   |
 *                     | \_|  ''\---/''  |   |
 *                     \  .-\__  `-`  ___/-. /
 *                   ___`. .'  /--.--\  `. . __
 *                ."" '<  `.___\_<|>_/___.'  >'"".
 *               | | :  `- \`.;`\ _ /`;.`/ - ` : | |
 *               \  \ `-.   \_ __\ /__ _/   .-` /  /
 *          ======`-.____`-.___\_____/___.-`____.-'======
 *                             `=---='
 *          ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
 *                     佛祖保佑        永无BUG
 *            佛曰:
 *                   写字楼里写字间，写字间里程序员；
 *                   程序人员写程序，又拿程序换酒钱。
 *                   酒醒只在网上坐，酒醉还来网下眠；
 *                   酒醉酒醒日复日，网上网下年复年。
 *                   但愿老死电脑间，不愿鞠躬老板前；
 *                   奔驰宝马贵者趣，公交自行程序员。
 *                   别人笑我忒疯癫，我笑自己命太贱；
 *                   不见满街漂亮妹，哪个归得程序员？
 */

using AutoMapper;

namespace AutoMapper学习与练习.Orders.DTO
{
    /// <summary>
    /// 配置映射关系
    /// </summary>
    /// <remarks>
    /// 每个方法只配置直接关系，不配置间接关系（示例：Order与OrderTotalInfoOut就是间接关系）
    /// </remarks>
    public static class Test
    {
        #region 直接关系映射

        /// <summary>
        /// 配置Order与OrderBaseOut的映射关系
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static IMappingExpression<Order, OrderBaseOut> ForOrderOut(this IMappingExpression<Order, OrderBaseOut> map)
        {
            var result = map
                .ForMember(
                    dest => dest.Status
                    , opt => opt.Condition(src => src.Status > 0)
                    //如果状态大于0则忽略这个属性映射
                    //如果要针对这个属性有默认值，应该写在out对象的构造方法里
                )
                .ForMember(
                     dest => dest.OrderTotalAmount
                     , opt => opt.MapFrom(src => src.ItemTotalAmount + src.TaxTotalAmount)
                 );

            result.ReverseMap();

            return result;
        }

        /// <summary>
        /// 配置Order与OrderAmountOut的映射关系
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static IMappingExpression<Order, OrderAmountOut> ForOrderAmountOut(this IMappingExpression<Order, OrderAmountOut> map)
        {
            //1. 懒
            //2.

            var result = map
                //.ForMember( dest => dest.OrderTotalAmount , opt => opt.Ignore( ) )//忽略部分属性的映射
                .ForMember(
                     dest => dest.OrderTotalAmount
                     , opt => opt.MapFrom(src => src.ItemTotalAmount + src.TaxTotalAmount + src.OrderShipAmount)
                 );

            result.ReverseMap();

            return result;
        }

        /// <summary>
        /// 配置Order与OrderPeoleOut的映射关系
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static IMappingExpression<Order, OrderPeopleOut> ForOrderPeoleOut(this IMappingExpression<Order, OrderPeopleOut> map)
        {
            var result = map;

            result.ReverseMap();

            return result;
        }

        #endregion 直接关系映射

        #region 复杂关系映射

        #region Order-->OrderTotalInfoOut

        /// <summary>
        /// 配置Order与OrderTotalInfoOut的映射关系
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static void ForTotalInfoOut(this OrderMapFile map)
        {
            map.CreateMap<Order, OrderTotalInfoOut>()
                .ForMember(
                    dest => dest.AmountInfo
                    , opt => opt.ReuseMap()
                ).ForMember(
                    dest => dest.BaseInfo
                    , opt => opt.ReuseMap()
                ).ForMember(
                    dest => dest.PeopleInfo
                    , opt => opt.ReuseMap()
                );
        }

        /// <summary>
        /// 复用映射逻辑 TSource-->TMember
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <typeparam name="TMember">中间类型，目标类型中需要转换的属性的类型</typeparam>
        /// <param name="opt"></param>
        public static void ReuseMap<TSource, TDestination, TMember>(this IMemberConfigurationExpression<TSource, TDestination, TMember> opt)
        {
            opt.MapFrom((a, b, c, context) => context.Mapper.Map<TMember>(a));
        }

        #endregion Order-->OrderTotalInfoOut

        #endregion 复杂关系映射
    }

    /// <summary>
    /// 订单对象映射配置
    /// </summary>
    public class OrderMapFile : Profile
    {
        public OrderMapFile()
        : this("OrderMapFile")
        {
        }

        protected OrderMapFile(string profileName)
        : base(profileName)
        {
            /*
             * 具体配置放在其他方法里是为了更好的修改对应关系，让这个构造方法不是几合一
             */

            // 配置AutoMapper dest是目标表达式 opt是源表达式
            base.CreateMap<Order, OrderBaseOut>()
                //.ForMember(
                //    dest => dest.OrderTotalAmount
                //    , opt => opt.MapFrom( src => src.ItemTotalAmount + src.TaxTotalAmount )
                //)
                // .ReverseMap( );//允许翻转
                .ForOrderOut();

            base.CreateMap<Order, OrderAmountOut>().ForOrderAmountOut();
            base.CreateMap<Order, OrderPeopleOut>().ForOrderPeoleOut();

            this.ForTotalInfoOut();
        }
    }
}