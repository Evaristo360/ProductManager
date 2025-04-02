using System.ComponentModel.DataAnnotations;

namespace ProductManager.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "ID")]
        public int idProduct { get; set; }
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [Display(Name = "Precio")]
        public double price { get; set; }
        [Display(Name = "Stock")]
        public int stock { get; set; }
        [Display(Name = "Categoría")]
        public int idCategory { get; set; }
        [Display(Name = "Ubicación")]
        public int idLocation { get; set; }
        [Display(Name = "Fecha")]
        public DateTime createDate { get; set; }
        public bool status { get; set; }
    }
}
