using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeExpress.Models
{
    [Table("UserRoles")]
    public class UserRole
    {
        [Key]//Clave primaria
        public int IdUserRole { get; set; }

        [Required]//No permite valores nulos
        [MaxLength(50)]//Longitud máxima de caracteres
        public required string UserRoleName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string UserRoleDescription { get; set; }    

        //Relación de uno a muchos
        public ICollection<User> User { get; } = new List<User>();
    }
}
