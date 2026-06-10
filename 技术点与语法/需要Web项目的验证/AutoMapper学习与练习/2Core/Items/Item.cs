// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class.cs" company="Newegg" Author="lw47">
//   Copyright (c) 2018 Newegg.inc. All rights reserved.
// </copyright>
// <summary>
//   Class created at  2018-02-24 11:17:03
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

using System.ComponentModel.DataAnnotations;

namespace AutoMapper学习与练习.Item
{
    /// <summary>
    /// 商品，对应数据库中的Item表
    /// </summary>
    public class Item
    {
        /// <summary>
        /// 商品号
        /// </summary>
        [Key]
        public int ItemNumber { get; set; }

        /// <summary>
        /// 商品价格金额
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 商品税的总金额
        /// </summary>
        public decimal TaxTotalAmount { get; set; }

        /// <summary>
        /// 商品名字
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// UPC代码
        /// </summary>
        public string UPCCode { get; set; }

        /// <summary>
        /// 货币代码,3位国家代码,如CAN
        /// </summary>
        public string CurrencyCode { get; set; }
    }
}