using ShoppingWeb.Shared.Models;
using System.Collections.Generic;

namespace ShoppingWeb.Shared.Interfaces
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
    }
}
