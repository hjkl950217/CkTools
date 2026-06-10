// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="Newegg" Author="lw47">
//   Copyright (c) 2018 Newegg.inc. All rights reserved.
// </copyright>
// <summary>
//   Class1 created at  2018-02-24 09:10:07
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

using System.Collections.Generic;
using AutoMapper;
using AutoMapper学习与练习.Infrastructure;
using AutoMapper学习与练习.Orders.DTO;

namespace AutoMapper学习与练习.Orders
{
    /// <summary>
    /// 订单服务
    /// </summary>
    public class OrderServices : IOrderServices
    {
        /// <summary>
        /// 映射对象
        /// </summary>
        private readonly IMapper Mapper;

        public OrderServices(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        /// <summary>
        /// 查询所有订单基础信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderBaseOut> GetAllOrderBase()
        {
            return this.Mapper.Map<IEnumerable<OrderBaseOut>>(Data.OrderData);
        }

        /// <summary>
        /// 查询所有订单信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderTotalInfoOut> GetAllOrderInfo()
        {
            return this.Mapper.Map<IEnumerable<OrderTotalInfoOut>>(Data.OrderData);
        }

        /// <summary>
        /// 按订单号查询订单信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public OrderBaseOut GetOrderBase(int orderNumber)
        {
            return this.GetOrder<OrderBaseOut>(orderNumber);
        }

        /// <summary>
        /// 按订单号查询订单金额相关信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public OrderAmountOut GetAmountByOrder(int orderNumber)
        {
            return this.GetOrder<OrderAmountOut>(orderNumber);
        }

        /// <summary>
        /// 按订单号查询订单人员相关信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public OrderPeopleOut GetPeopleByOrder(int orderNumber)
        {
            return this.GetOrder<OrderPeopleOut>(orderNumber);
        }

        /// <summary>
        /// 按订单号查询订单所有相关信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public OrderTotalInfoOut GetOrder(int orderNumber)
        {
            return this.GetOrder<OrderTotalInfoOut>(orderNumber);
        }

        private T GetOrder<T>(int orderNumber)
        {
            Order result = Data.OrderData.Find(t => t.OrderNumber == orderNumber);
            return this.Mapper.Map<T>(result);
        }
    }
}