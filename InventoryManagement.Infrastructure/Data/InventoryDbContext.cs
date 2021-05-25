using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace InventoryManagement.Infrastructure.Data
{
    public class InventoryDbContext: IdentityDbContext<User, UserRole, Guid>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CategoryHierarchy> CategoryHierarchies { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<ReferralCode> ReferralCodes { get; set; }
        public DbSet<ReferralCodeTransaction> ReferralCodeTransactions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            OnModelCreatingProduct(builder);
            OnModelCreatingBrand(builder);
            OnModelCreatingCategory(builder);
            OnModelCreatingCategoryHierarchy(builder);
            OnModelCreatingCoupon(builder);
            OnModelCreatingProductImage(builder);
            OnModelCreatingProductStock(builder);
            OnModelCreatingReferralCode(builder);
            OnModelCreatingReferralCodeTransaction(builder);
            OnModelCreatingTransaction(builder);
            OnModelCreatingTransactionDetails(builder);
            OnModelCreatingUser(builder);
            base.OnModelCreating(builder);
        }
        private void OnModelCreatingProduct(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasKey(key => key.ProductId);
            
            builder.Entity<Product>()
                .HasOne<Category>(x => x.Category)
                .WithMany(prop => prop.Products)
                .HasForeignKey(key => key.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Product>()
                .HasOne(x => x.Currency)
                .WithMany(prop => prop.Products)
                .HasForeignKey(key => key.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Product>()
                .HasOne<Brand>(x => x.Brand)
                .WithMany(prop => prop.Products)
                .HasForeignKey(key => key.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Product>()
                .HasOne<User>(x => x.User)
                .WithMany(prop => prop.Products)
                .HasForeignKey(key => key.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingBrand(ModelBuilder builder)
        {
            builder.Entity<Brand>()
                .HasKey(key => key.BrandId);
            
            builder.Entity<Brand>()
                .HasOne<User>(x => x.User)
                .WithMany(prop => prop.Brands)
                .HasForeignKey(key => key.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingCategory(ModelBuilder builder) 
        {
            builder.Entity<Category>()
                .HasKey(key => key.CategoryId);
        }
        private void OnModelCreatingCategoryHierarchy(ModelBuilder builder) 
        {
            builder.Entity<CategoryHierarchy>()
                .HasKey(key => key.CategoryHierarchyId);
            
            builder.Entity<CategoryHierarchy>()
                .HasOne<Category>(x => x.Category)
                .WithMany(prop => prop.CategoryHierarchies)
                .HasForeignKey(key => key.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingProductStock(ModelBuilder builder) 
        {
            builder.Entity<ProductStock>()
                .HasKey(key => key.StockId);
            
            builder.Entity<ProductStock>()
                .HasOne(x => x.Product)
                .WithMany(prop => prop.ProductStocks)
                .HasForeignKey(key => key.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingProductImage(ModelBuilder builder) 
        {
            
            builder.Entity<ProductImage>()
                .HasKey(key => key.ImageId);
            
            builder.Entity<ProductImage>()
                .HasOne(x => x.Product)
                .WithMany(prop => prop.ProductImages)
                .HasForeignKey(key => key.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingCoupon(ModelBuilder builder) 
        {
            builder.Entity<Coupon>()
                .HasKey(key => key.CouponId);
        }
        private void OnModelCreatingReferralCode(ModelBuilder builder) 
        {
            builder.Entity<ReferralCode>()
                .HasKey(key => key.ReferralCodeId);
            
            builder.Entity<ReferralCode>()
                .HasOne<User>()
                .WithMany(prop => prop.ReferralCodes)
                .HasForeignKey(key => key.ReferredById)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingReferralCodeTransaction(ModelBuilder builder) 
        {
            builder.Entity<ReferralCodeTransaction>()
                .HasKey(key => key.ReferralTransactionId);
            
            builder.Entity<ReferralCodeTransaction>()
                .HasOne<ReferralCode>()
                .WithMany(prop => prop.ReferralCodeTransactions)
                .HasForeignKey(key => key.ReferralId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<ReferralCodeTransaction>()
                .HasOne<User>()
                .WithMany(prop => prop.ReferralCodeTransactions)
                .HasForeignKey(key => key.ReferredTo)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<ReferralCodeTransaction>()
                .HasOne<Transaction>()
                .WithOne(prop => prop.ReferralCodeTransaction).HasForeignKey<ReferralCodeTransaction>(x => x.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingTransaction(ModelBuilder builder) 
        {
            builder.Entity<Transaction>()
                .HasKey(key => key.TransactionId);
            
            builder.Entity<Transaction>()
                .HasOne<User>(prop => prop.Customer)
                .WithMany(prop => prop.CustomerTransactions)
                .HasForeignKey(key => key.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Transaction>()
                .HasOne<Coupon>(prop => prop.Coupon)
                .WithMany(prop => prop.Transactions)
                .HasForeignKey(key => key.CouponId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transaction>()
                .HasOne<User>(prop => prop.CreatedBy)
                .WithMany(prop => prop.CreatedByTransactions)
                .HasForeignKey(key => key.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingTransactionDetails(ModelBuilder builder)
        {
            builder.Entity<TransactionDetail>()
                .HasKey(key => key.TransactionDetailId);

            builder.Entity<TransactionDetail>()
                .HasOne<Transaction>(prop => prop.Transaction)
                .WithMany(prop => prop.TransactionDetails)
                .HasForeignKey(key => key.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TransactionDetail>()
                .HasOne<ProductStock>(prop => prop.ProductStock)
                .WithMany(prop => prop.TransactionDetails)
                .HasForeignKey(key => key.StockId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void OnModelCreatingUser(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(key => key.Id);
            builder.Entity<User>()
                .HasOne<Membership>(prop => prop.Membership)
                .WithMany(prop => prop.Users)
                .HasForeignKey(key => key.MembershipId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
