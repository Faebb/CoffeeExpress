using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeExpress.Models
{
    [Table("Coffees")]
    public class Coffee
    {
        [Key]//Clave Primaria
        public int IdCoffee { get; set; }

        [Required]//No permite valores nulos
        [MaxLength(50)]//Longitud máxima de caracteres
        public  string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [Column(TypeName ="decimal(10,2)")]//Define el tipo de dato en SQL
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string Origin { get; set; }

        [Required]
        [MaxLength(50)]
        public string Roast { get; set; }

        public int SalesCount { get; set; }

        //Relación uno-a-muchos con ShoppingCart
        //public ICollection<ShoppingCart>? ShoppingCarts { get; set; }
    }
}