using Microsoft.AspNetCore.Http;
using Store.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.ViewModels
{
    public class ProductFormViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }
        public List<Sex> Sexes { get; set; }

        [Display(Name = "Photo")]
        public IFormFile Photo { get; set; }

        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        public int Id { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category not selected")]
        public int CategoryId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name not provided")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price not provided")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression("([0-9,]+)", ErrorMessage = "Price must be a number. Use , instead of .")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Color not selected. If it is not on the list, add it.")]
        [Display(Name = "Color")]
        public int? ColorId { get; set; }

        [Required(ErrorMessage = "Brand not selected. If it is not on the list, add it.")]
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender not selected")]
        public int SexId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
