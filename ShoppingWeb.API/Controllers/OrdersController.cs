using Microsoft.AspNetCore.Mvc;
using ShoppingWeb.Shared.DTOs;
using ShoppingWeb.Shared.Interfaces;
using System.Collections.Generic;

namespace ShoppingWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult PlaceOrder(OrderDto orderDto)
        {
            _orderService.PlaceOrder(orderDto);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
