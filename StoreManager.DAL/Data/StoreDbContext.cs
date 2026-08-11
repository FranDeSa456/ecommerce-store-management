using Microsoft.EntityFrameworkCore;
using StoreManager.DAL.Entities;
using System.Net.Sockets;

namespace StoreManager.DAL.Data
{
    internal class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.ProductId, oi.OrderId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId);

            modelBuilder.Entity<OrderItem>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_OrderItem_Quantity_Positive", "[Quantity] > 0");
                    t.HasCheckConstraint("CK_OrderItem_UnitPrice_Positive", "[UnitPrice] > 0");
                });

            modelBuilder.Entity<Payment>()
                .ToTable(t => t.HasCheckConstraint("CK_Payment_Amount_Positive", "[Amount] > 0"));

            modelBuilder.Entity<Product>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Product_Price_Positive", "[Price] > 0");
                    t.HasCheckConstraint("CK_Product_Stock_NonNegative", "[Stock] >= 0");
                });

            modelBuilder.Entity<Review>()
                .ToTable(t => t.HasCheckConstraint("CK_Review_Rating_Range", "[Rating] >= 1 AND [Rating] <= 5"));
        }
    }
}
