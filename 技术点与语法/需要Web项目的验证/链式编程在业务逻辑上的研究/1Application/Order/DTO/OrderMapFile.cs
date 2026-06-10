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

namespace 链式编程在业务逻辑上的研究.Orders.DTO
{
    public static class Test
    {
        /// <summary>
        /// 配置Order与OrderOut的映射关系
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static IMappingExpression<Order, OrderOut> ForOrderOut(this IMappingExpression<Order, OrderOut> map)
        {
            return map.ForMember(
                     dest => dest.OrderTotalAmount
                     , opt => opt.MapFrom(src => src.ItemTotalAmount + src.TaxTotalAmount)
                 );
        }
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
            // 配置AutoMapper dest是目标表达式 opt是源表达式
            CreateMap<Order, OrderOut>()
                //// 这里放在一个方法里是为了更好的修改对应关系，让这个构造方法不是几合一
                //.ForMember(
                //    dest => dest.OrderTotalAmount
                //    , opt => opt.MapFrom( src => src.ItemTotalAmount + src.TaxTotalAmount )
                //)
                .ForOrderOut();
        }
    }
}