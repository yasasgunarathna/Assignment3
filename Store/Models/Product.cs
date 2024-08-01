using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Column(TypeName = "decimal(15,2)")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Color")]
        public Color Color { get; set; }

        [Display(Name = "Color")]
        public int? ColorId { get; set; }

        public Brand Brand { get; set; }

        [Display(Name = "Brand")]
        public int? BrandId { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int SexId { get; set; }

        [Display(Name = "Gender")]
        public Sex Sex { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public ICollection<Stock> Stock { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
