using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Shared.Interfaces;
using ShoppingWeb.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingWeb.Services.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingDbContext _context;

        public OrderRepository(ShoppingDbContext context)
        {
            _context = context;
        }

        public void PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }

    }
}
