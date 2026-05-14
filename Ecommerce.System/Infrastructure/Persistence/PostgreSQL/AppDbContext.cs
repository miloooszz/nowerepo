using Microsoft.EntityFrameworkCore;
using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Infrastructure.Persistence.PostgreSQL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definicja tabel fizycznych w bazie danych
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> Variants { get; set; }
        public DbSet<VariantAttribute> Attributes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Relacja Produkt -> Warianty (Jeden do Wielu)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne()
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2. Relacja Wariant -> Atrybuty (Jeden do Wielu)
            modelBuilder.Entity<ProductVariant>()
                .HasMany(v => v.Attributes)
                .WithOne()
                .HasForeignKey(a => a.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Konfiguracja zamówienia (Order)
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                // Zsynchronizowane z nowym modelem
                entity.Property(o => o.TotalAmount)
                    .HasPrecision(18, 2);

                entity.Property(o => o.Status)
                    .HasMaxLength(50);

                // Konfiguracja relacji do pozycji zamówienia (Items)
                entity.HasMany(o => o.Items)
                    .WithOne()
                    .HasForeignKey("OrderId") // Shadow property w bazie danych
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // 4. Konfiguracja pozycji zamówienia (OrderItem)
            modelBuilder.Entity<OrderItem>(entity =>
            {
                // Tworzymy techniczny klucz główny, jeśli OrderItem go nie posiada
                entity.HasKey(oi => new { oi.ProductId, oi.Quantity, oi.UnitPrice });

                entity.Property(oi => oi.UnitPrice)
                    .HasPrecision(18, 2);
            });

            // 5. Precyzja cen w wariantach
            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);
        }
    }
}