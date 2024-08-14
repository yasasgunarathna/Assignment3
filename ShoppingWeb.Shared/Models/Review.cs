namespace ShoppingWeb.Shared.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }  // Rating out of 5

        // Foreign Key to Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Foreign Key to Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

}
