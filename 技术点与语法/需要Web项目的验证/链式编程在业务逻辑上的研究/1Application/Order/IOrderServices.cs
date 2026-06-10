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
using System.Threading.Tasks;
using 链式编程在业务逻辑上的研究.Orders.DTO;

namespace 链式编程在业务逻辑上的研究.Orders
{
    /// <summary>
    /// The class of IOrderServices.
    /// </summary>
    public interface IOrderServices
    {
        /// <summary>
        /// 查询所有订单
        /// </summary>
        /// <returns></returns>
        Task<List<OrderOut>> GetAllOrder();

        /// <summary>
        /// 按卖家权限查询所有订单
        /// </summary>
        /// <param name="sellerID"></param>
        /// <returns></returns>
        Task<List<OrderOut>> GetAllOrder(string sellerID);

        /// <summary>
        /// 按订单号查询订单
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        Task<OrderOut> GetOrder(int orderNumber);
    }
}