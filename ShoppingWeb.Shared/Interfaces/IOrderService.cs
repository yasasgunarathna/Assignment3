using ShoppingWeb.Shared.DTOs;

namespace ShoppingWeb.Shared.Interfaces
{
    public interface IOrderService
    {
        void PlaceOrder(OrderDto orderDto);
        IEnumerable<OrderDto> GetOrders();
        OrderDto GetOrderById(int id);
    }
}
