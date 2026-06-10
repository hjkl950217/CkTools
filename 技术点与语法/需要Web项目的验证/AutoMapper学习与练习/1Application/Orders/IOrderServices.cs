// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderServices.cs" company="Newegg" Author="lw47">
//   Copyright (c) 2018 Newegg.inc. All rights reserved.
// </copyright>
// <summary>
//   IOrderServices created at  2018-02-24 09:17:57
// </summary>
//<Discription>
//
//</Discription>
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
using AutoMapper学习与练习.Orders.DTO;

namespace AutoMapper学习与练习.Orders
{
    /// <summary>
    ///
    /// </summary>
    public interface IOrderServices
    {
        /// <summary>
        /// 查询所有订单
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderBaseOut> GetAllOrderBase();

        /// <summary>
        /// 查询所有订单
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderTotalInfoOut> GetAllOrderInfo();

        /// <summary>
        /// 按订单号查询订单
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        OrderBaseOut GetOrderBase(int orderNumber);

        /// <summary>
        /// 按订单号查询订单中的金额相关信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        OrderAmountOut GetAmountByOrder(int orderNumber);

        /// <summary>
        /// 按订单号查询订单中的相关人员信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        OrderPeopleOut GetPeopleByOrder(int orderNumber);

        /// <summary>
        /// 查询一个订单中所有信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        OrderTotalInfoOut GetOrder(int orderNumber);
    }
}