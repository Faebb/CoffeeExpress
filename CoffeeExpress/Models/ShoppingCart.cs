using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeExpress.Models
{
    [Table("ShoppingCarts")]
    public class ShoppingCart
    {
        [Key] //Clave primaria
        public int IdShoppingCart { get; set; }

        [Required] //Relación con Usuario
        public int IdUser { get; set; }

        [ForeignKey("IdUser")] //Define la clave foránea
        public User User { get; set; }

        [Required] //Relación con Coffee
        public int IdCoffee { get; set; }

        [ForeignKey("IdCoffee")] //Define la clave foránea
        public Coffee Coffee { get; set; }

        [Required]//No permite valores nulos
        public int Quantity { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
