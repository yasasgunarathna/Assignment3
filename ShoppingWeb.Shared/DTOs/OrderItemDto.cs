namespace ShoppingWeb.Shared.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }  // ID of the product
        public int Quantity { get; set; }   // Quantity of the product ordered
        public decimal Price { get; set; }  // Price of the product at the time of the order
    }
}
