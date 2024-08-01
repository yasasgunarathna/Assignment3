using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Requerd")]
        [Display(Name = "Name")]
        public string Name { get; set; }

    }
}
