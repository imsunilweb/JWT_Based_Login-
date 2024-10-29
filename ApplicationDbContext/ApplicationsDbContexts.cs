using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT_Based_Login.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product ki Price property ke liye precision specify karna
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Order aur Product ke beech relationship ko configure karna
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()  // Agar yeh one-to-many relationship hai
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete
        }
    }
}




