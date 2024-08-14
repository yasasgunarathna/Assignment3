using AutoMapper;
using ShoppingWeb.Shared.Interfaces;
using ShoppingWeb.Shared.DTOs;
using ShoppingWeb.Shared.Models;
using System.Collections.Generic;

namespace ShoppingWeb.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public void PlaceOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            // Ensure each OrderItem is associated with the correct Order
            foreach (var item in order.OrderItems)
            {
                item.Order = order;
            }

            _orderRepository.PlaceOrder(order);
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            var orders = _orderRepository.GetOrders();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            return _mapper.Map<OrderDto>(order);
        }
    }
}
