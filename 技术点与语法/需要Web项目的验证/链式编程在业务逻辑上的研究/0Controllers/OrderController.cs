using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 链式编程在业务逻辑上的研究.Orders.DTO;
using 链式编程在业务逻辑上的研究.Role;

namespace 链式编程在业务逻辑上的研究.Orders
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderServices OrderServices;

        public OrderController(IOrderServices orderServices)
        {
            this.OrderServices = orderServices;
        }

        // GET api/Order
        [HttpGet("{orderNumber=0:int}")]
        public async Task<IEnumerable<OrderOut>> GetOrder(
            [FromRoute] int orderNumber,
            [FromQuery] string sellerID)
        {
            return await this.OrderServices.GetAllOrder(sellerID);
        }

        // GET api/Order/100
        [HttpGet("{orderNumber:int}")]
        public static dynamic GetOrder2([FromRoute] int orderNumber)
        {
            // return this.OrderServices.GetOrder( orderNumber );

#pragma warning disable CS0219 // 变量已被赋值，但从未使用过它的值
#pragma warning disable IDE0059 // 不需要赋值
            RoleEnum? boss = RoleEnum.Boss;
            RoleEnum? devBoss = RoleEnum.DevBoss;
            RoleEnum? roleNull = null;
#pragma warning restore IDE0059 // 不需要赋值
#pragma warning restore CS0219 // 变量已被赋值，但从未使用过它的值

            return new
            {
                isInterA = boss.IsInternal().IsDevBoss().IsNormal().IsCheckSuccess(),
                isInterB = boss
                    .CheckRole(RoleEnum.DevBoss)
                    .CheckRole(RoleEnum.InternalSeller)
                    .IsCheckSuccess(),
                //isInterB = devBoss.IsNormal( ).IsDevBoss().IsCheckSuccess( ),
                //isNull=roleNull.IsInternal().IsCheckSuccess()
            };
        }
    }
}