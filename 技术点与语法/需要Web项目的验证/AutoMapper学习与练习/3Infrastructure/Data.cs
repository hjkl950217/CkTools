// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OderTable.cs" company="Newegg" Author="lw47">
//   Copyright (c) 2018 Newegg.inc. All rights reserved.
// </copyright>
// <summary>
//   OderTable created at  2018-02-24 10:12:15
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
using System.Collections.Generic;
using AutoMapper学习与练习.Orders;
using AutoMapper学习与练习.Role;
using AutoMapper学习与练习.Sellers;

namespace AutoMapper学习与练习.Infrastructure
{
    /// <summary>
    /// 模拟订单数据表
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// 模拟订单数据表
        /// </summary>
        public static List<Order> OrderData = new List<Order>()
        {
            new Order ()
            {
                OrderNumber =1000,
                ItemTotalAmount =100,
                TaxTotalAmount =10,
                OrderShipAmount=1,
                Status=OrderStatusEnum.Invoiced,
                SellerID="BB",
                CustomerID="Z1",
                InDate=Convert.ToDateTime("2017-01-01")
            } ,
            new Order ()
            {
                OrderNumber =2000,
                ItemTotalAmount =200,
                TaxTotalAmount =20,
                OrderShipAmount=2,
                Status=OrderStatusEnum.Void,
                SellerID="CC",
                InDate=DateTime.Now
            }
        };

        /// <summary>
        /// 模拟卖家数据表
        /// </summary>
        public static List<Seller> SellerData = new List<Seller>()
        {
            new Seller (){SellerID="AA",Role=RoleEnum.Developer},
            new Seller (){SellerID="BB",Role=RoleEnum.InternalSeller},
            new Seller (){SellerID="CC",Role=RoleEnum.NormalSeller},
            new Seller (){SellerID="DD",Role=RoleEnum.Boss},
            new Seller (){SellerID="EE",Role=RoleEnum.DevBoss},
            new Seller (){SellerID="Z1"},
            new Seller (){}
        };
    }
}