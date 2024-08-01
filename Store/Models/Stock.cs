using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Size")]
        // TODO: Ensure that a product can have only one element with a given name
        public string Name { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Qty { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public bool IsLastElementOrdered { get; set; } = false;
    }
}
