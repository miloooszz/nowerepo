using Microsoft.EntityFrameworkCore;
using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Infrastructure.Persistence.PostgreSQL
{
    // Tworzenie Fizycznych Tabel
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definicja tabel
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> Variants { get; set; }
        public DbSet<VariantAttribute> Attributes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja relacji jeden-do-wielu dla Produkt -> Warianty
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne()
                .HasForeignKey(w => w.ProductId);

            // Ustawienie precyzji dla pól pieniężnych (wymagane w PostgreSQL)
            modelBuilder.Entity<ProductVariant>()
                .Property(w => w.Price).HasPrecision(18, 2);

            modelBuilder.Entity<OrderStatus>()
                .Property(p => p.Priceatthetimeofpurchase).HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(z => z.TotalValue).HasPrecision(18, 2);
        }
    }
}