using System.ComponentModel.DataAnnotations;

namespace ProductManager.Models
{
    public class Category
    {
        [Key]
        public int idCategory { get; set; }
        [Display(Name = "Descripción")]
        public string description { get; set; }
        public bool status { get; set; }
    }
}
