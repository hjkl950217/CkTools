using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper学习与练习.Orders.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AutoMapper学习与练习.Orders
{
    public class OrderController : Controller
    {
        private readonly IOrderServices OrderServices;
        private readonly IMapper Mapper;

        public OrderController(IOrderServices orderServices, IMapper mapper)
        {
            this.OrderServices = orderServices;
            this.Mapper = mapper;
        }

        /// <summary>
        /// 查询所有订单基础信息
        /// </summary>
        /// <returns></returns>
        // GET api/OrderBase
        [Route("api/[controller]Base")]
        [HttpGet]
        public IEnumerable<OrderBaseOut> GetOrderBase()
        {
            return this.OrderServices.GetAllOrderBase();
        }

        /// <summary>
        /// 查询所有订单的所有信息
        /// </summary>
        /// <returns></returns>
        // GET api/Order
        [Route("api/[controller]")]
        [HttpGet]
        public IEnumerable<OrderTotalInfoOut> GetOrder()
        {
            return this.OrderServices.GetAllOrderInfo();
        }

        // GET api/Order/1000
        [Route("api/[controller]/{orderNumber:int}")]
        [HttpGet]
        public OrderBaseOut GetOrder([FromRoute] OrderIn orderIn)
        {
            return this.OrderServices.GetOrderBase(orderIn.OrderNumber);
        }

        // GET api/OrderAmount/1000?
        [Route("api/[controller]Amount/{orderNumber:int}")]
        [HttpGet]
        public OrderAmountOut GetOrderAmount([FromRoute] OrderIn orderIn)
        {
            return this.OrderServices.GetAmountByOrder(orderIn.OrderNumber);
        }

        // GET api/OrderPeole/1000?
        [Route("api/[controller]People/{orderNumber:int}")]
        [HttpGet]
        public OrderPeopleOut GetPeople([FromRoute] OrderIn orderIn)
        {
            return this.OrderServices.GetPeopleByOrder(orderIn.OrderNumber);
        }

        // GET autoMapper
        [Route("autoMapper")]
        [HttpGet]
        public dynamic GetMapperData()
        {
            Order order = new Order()
            {
                OrderNumber = 1000,
                ItemTotalAmount = 100,
                TaxTotalAmount = 10,
                OrderShipAmount = 1,
                Status = OrderStatusEnum.Invoiced,
                SellerID = "BB",
                CustomerID = "Z1",
                InDate = Convert.ToDateTime("2017-01-01")
            };

            OrderBaseOut orderBase = new OrderBaseOut()
            {
                InDate = DateTime.Now,
                OrderNumber = 10,
                OrderTotalAmount = 100
            };

            // var temp = this.Mapper.Map<OrderAmountOut>( order );

            //创建新对象
            return this.Mapper.Map<OrderTotalInfoOut>(order);
            // return this.Mapper.Map<Order>( orderBase );

            //合并对象,
            // return this.Mapper.Map( order,orderBase);
        }
    }
}