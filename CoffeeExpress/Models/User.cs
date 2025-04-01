using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeExpress.Models
{

    [Table("Users")]
    public class User
    {
        [Key]//Clave primaria
        public int IdUser { get; set; }

        [Required]//No permite valores nulos
        [MaxLength(50)]//Longitud máxima de caracteres
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
        
        [Required]//Relación con UserRole
        public int IdUserRole { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool State { get; set; }
        
        [ForeignKey("IdUserRole")] //Define la clave foránea
        public UserRole UserRole { get; set; }
    }
}
