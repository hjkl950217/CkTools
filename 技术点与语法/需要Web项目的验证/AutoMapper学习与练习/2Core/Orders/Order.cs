// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Oder.cs" company="Newegg" Author="lw47">
//   Copyright (c) 2018 Newegg.inc. All rights reserved.
// </copyright>
// <summary>
//   Oder created at  2018-02-24 10:01:36
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

using System;
using System.ComponentModel.DataAnnotations;

namespace AutoMapper学习与练习.Orders
{
    /// <summary>
    /// 订单，对应数据库中的Order表
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Key]
        public int OrderNumber { get; set; }

        /// <summary>
        /// 订单中的商品总金额
        /// </summary>
        public decimal ItemTotalAmount { get; set; }

        /// <summary>
        /// 订单中的税总金额
        /// </summary>
        public decimal TaxTotalAmount { get; set; }

        /// <summary>
        /// 订单总运费
        /// </summary>
        public decimal OrderShipAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatusEnum Status { get; set; }

        /// <summary>
        /// 卖家ID
        /// </summary>
        public string SellerID { get; set; }

        /// <summary>
        /// 买家ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 订单生成时间
        /// </summary>
        public DateTime InDate { get; set; }

        //.退运费
    }
}