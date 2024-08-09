using EcomGalaxy.Domain.Models.Product;
namespace EcomGalaxy.Domain.Models.Context
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<EcomGalaxy.Domain.Models.Product.Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<EcomGalaxy.Domain.Models.ShoppingCart.ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EcomGalaxy.Domain.Models.Order.Order> Orders { get; set; }
        public DbSet<EcomGalaxy.Domain.Models.Payment.Payment> Payments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsUnicode(true);

            // Order
            modelBuilder.Entity<EcomGalaxy.Domain.Models.Order.Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Product
            modelBuilder.Entity<EcomGalaxy.Domain.Models.Product.Product>()
               .Property(p => p.Name)
               .IsUnicode(true);

            modelBuilder.Entity<EcomGalaxy.Domain.Models.Product.Product>()
               .Property(p => p.Description)
               .IsUnicode(true);

            modelBuilder.Entity<EcomGalaxy.Domain.Models.Product.Product>()
               .Property(p => p.Price)
               .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<EcomGalaxy.Domain.Models.Product.Product>()
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
