using System.ComponentModel.DataAnnotations;

namespace ProductManager.Models
{
    public enum TypeTransaction
    {
        Entrada,
        Salida
    }
    public class Transaction
    {
        [Key]
        public int idTransaction { get; set; }
        public int idProduct { get; set; }
        public int pieces { get; set; }
        public TypeTransaction typeTransaction { get; set; }
        public string? comments { get; set; }
        public DateTime createdDate { get; set; }
    }
}
