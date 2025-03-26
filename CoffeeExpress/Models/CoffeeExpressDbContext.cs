using Microsoft.EntityFrameworkCore;

namespace CoffeeExpress.Models
{
    public class CoffeeEpxpressDBContext : DbContext
    {
        public CoffeeEpxpressDBContext(DbContextOptions<CoffeeEpxpressDBContext> options) : base(options) { }

        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts
        {
            get; set;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Datos por defecto para UserRole
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { IdUserRole = 1, UserRoleName = "Customer", UserRoleDescription = "Role of customers who will buy" },
                new UserRole { IdUserRole = 2, UserRoleName = "Administrator", UserRoleDescription = "Role of employees who will manage the stock" }
            );
        }

    }
}
