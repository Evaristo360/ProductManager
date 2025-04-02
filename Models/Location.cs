using System.ComponentModel.DataAnnotations;

namespace ProductManager.Models
{
    public class Location
    {
        [Key]
        public int idLocation { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public bool status { get; set; }
    }
}
