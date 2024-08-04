using EcomGalaxy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EcomGalaxy.Models
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> payments { get; set; }
        //public DbSet<Image> Images { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsUnicode(true);

            // Order
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Product
            modelBuilder.Entity<Product>()
               .Property(p => p.Name)
               .IsUnicode(true);

            modelBuilder.Entity<Product>()
               .Property(p => p.Description)
               .IsUnicode(true);

            modelBuilder.Entity<Product>()
               .Property(p => p.Price)
               .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
             .Property(p => p.AverageRating)
             .HasColumnType("decimal(18, 2)");

            // Review
            modelBuilder.Entity<Review>()
                .Property(r => r.Message)
                .IsUnicode(true);

            // Shopping Cart
            //modelBuilder.Entity<ShoppingCart>()
            //    .Property(s => s.TotalPrice)
            //    .HasColumnType("decimal(18,2)");

            // ApplicationUser

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Name)
                .IsUnicode(true);
            modelBuilder.Entity<ApplicationUser>()
                .Property(a => a.City)
                .IsUnicode(true);

            modelBuilder.Entity<ApplicationUser>()
                .Property(a => a.Country)
                .IsUnicode(true);

            modelBuilder.Entity<ApplicationUser>()
                .Property(a => a.Street)
                .IsUnicode(true);

        }
    }
}
