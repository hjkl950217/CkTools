// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SellerRole.cs" company="Newegg" Author="lw47">
//   Copyright (c) 2018 Newegg.inc. All rights reserved.
// </copyright>
// <summary>
//   SellerRole created at  2018-02-24 13:56:13
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

namespace 链式编程在业务逻辑上的研究.Role
{
    /// <summary>
    /// 模拟权限,用位运算判断的，所以要注意值对应的二进制位
    /// </summary>
    public enum RoleEnum
    {
        /// <summary>
        /// 开发者
        /// </summary>
        Developer = 4,

        /// <summary>
        /// 内部卖家
        /// </summary>
        InternalSeller = 2,

        /// <summary>
        /// 正常卖家-1
        /// </summary>
        NormalSeller = 1,

        /// <summary>
        /// 老板-111
        /// </summary>
        /// <remarks>
        /// 包括前三个权限
        /// </remarks>
        Boss = 7,

        /// <summary>
        /// 开发主管-101
        /// </summary>
        /// <remarks>
        /// 包括开发权限与正常卖家权限
        /// </remarks>
        DevBoss = 5,
    }

    public static class RoleEnumExt
    {
        public static RoleEnum? IsBoss(this RoleEnum? target)
        {
            return RoleEnum.Boss.CheckRole(target);
        }

        public static RoleEnum? IsDeveloper(this RoleEnum? target)
        {
            return RoleEnum.Developer.CheckRole(target);
        }

        public static RoleEnum? IsInternal(this RoleEnum? target)
        {
            return RoleEnum.InternalSeller.CheckRole(target);
        }

        public static RoleEnum? IsNormal(this RoleEnum? target)
        {
            return RoleEnum.NormalSeller.CheckRole(target);
        }

        public static RoleEnum? IsDevBoss(this RoleEnum? target)
        {
            return RoleEnum.DevBoss.CheckRole(target);
        }

        /// <summary>
        /// 权限是否验证成功
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsCheckSuccess(this RoleEnum? target)
        {
            return (target == null) ? false : true;
        }

        /// <summary>
        /// 检查有没有目标权限-让外部自己调用的
        /// </summary>
        /// <remarks>
        /// 验证sources是不是有target权限
        /// </remarks>
        /// <param name="sources">要被验证的权限</param>
        /// <param name="target">用来判断的权限</param>
        /// <returns></returns>
        public static RoleEnum? CheckRole(this RoleEnum? sources, RoleEnum target)
        {
            //判断空
            if(sources == null)
                return null;

            //做位与运算来判断权限
            bool isSeccess = (sources.Value | target) == sources.Value;

            return (isSeccess == true) ? sources : null;
        }

        /// <summary>
        /// 检查有没有权限-给上面的IS方法用的
        /// </summary>
        /// <remarks>
        /// 验证target是不是有sources权限
        /// </remarks>
        /// <param name="sources">用来判断的权限</param>
        /// <param name="target">要被验证的权限</param>
        /// <returns></returns>
        private static RoleEnum? CheckRole(this RoleEnum sources, RoleEnum? target)
        {
            //判断空
            if(target == null)
                return null;

            //做位与运算来判断权限
            bool isSeccess = (sources | target) == target;

            return (isSeccess == true) ? target : null;
        }
    }
}