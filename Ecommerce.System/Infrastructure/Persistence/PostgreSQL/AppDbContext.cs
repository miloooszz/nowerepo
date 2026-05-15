using Microsoft.EntityFrameworkCore;
using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Infrastructure.Persistence.PostgreSQL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> Variants { get; set; }
        public DbSet<VariantAttribute> Attributes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne()
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductVariant>()
                .HasMany(v => v.Attributes)
                .WithOne()
                .HasForeignKey(a => a.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.TotalAmount)
                    .HasPrecision(18, 2);

                entity.Property(o => o.Status)
                    .HasMaxLength(50);

                entity.HasMany(o => o.Items)
                    .WithOne()
                    .HasForeignKey("OrderId") 
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => new { oi.ProductId, oi.Quantity, oi.UnitPrice });

                entity.Property(oi => oi.UnitPrice)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);
        }
    }
}