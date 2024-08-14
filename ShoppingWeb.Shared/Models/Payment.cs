namespace ShoppingWeb.Shared.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        // Foreign Key to Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // Payment method details
        public string PaymentMethod { get; set; }
    }

}
