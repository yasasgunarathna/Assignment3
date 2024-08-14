namespace ShoppingWeb.Shared.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; }
    }


}
