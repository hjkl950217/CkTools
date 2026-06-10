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
using System.Threading.Tasks;
using AutoMapper;
using 链式编程在业务逻辑上的研究.Infrastructure;
using 链式编程在业务逻辑上的研究.Orders.DTO;
using 链式编程在业务逻辑上的研究.Role;

namespace 链式编程在业务逻辑上的研究.Orders
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

        /// <summary>
        /// 权限检查
        /// </summary>
        private readonly ICheckRole CheckRole;

        public OrderServices(IMapper mapper, ICheckRole checkRole)
        {
            this.Mapper = mapper;
            this.CheckRole = checkRole;
        }

        /// <summary>
        /// 查找所有订单
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderOut>> GetAllOrder()
        {
            Task<List<OrderOut>> task = Task.Run(() =>
             {
                 return this.Mapper.Map<List<OrderOut>>(Data.OrderData);
             });

            return await task;
        }

        /// <summary>
        /// 按卖家权限查询所有订单
        /// </summary>
        /// <param name="sellerID"></param>
        /// <returns></returns>
        public async Task<List<OrderOut>> GetAllOrder(string sellerID)
        {
            Task<List<OrderOut>> task = Task.Run(() =>
           {
               //模拟有内部用户权限的才可以查所有的
               bool isRole = this.CheckRole.InternalOrderCheck(sellerID?.ToUpper());
               if(isRole == false)
                   return null;

               return this.Mapper.Map<List<OrderOut>>(Data.OrderData);
           });

            return await task;
        }

        /// <summary>
        /// 按订单号查找对象
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public async Task<OrderOut> GetOrder(int orderNumber)
        {
            Task<OrderOut> task = Task.Run(() =>
           {
               Order result = Data
               .OrderData.Find(t => t.OrderNumber == orderNumber);

               return this.Mapper.Map<OrderOut>(result);
           });

            return await task;
        }
    }
}