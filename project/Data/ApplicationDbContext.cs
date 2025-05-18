using AMAPP.API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMAPP.API.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Core Entity Sets
    public DbSet<User> Users { get; set; }
    public DbSet<ProducerInfo> ProducersInfo { get; set; }
    public DbSet<CoproducerInfo> CoproducersInfo { get; set; }
    public DbSet<AMAPAdministrator> AMAPAdministrators { get; set; }

    // Product Management
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory> Inventory { get; set; }

    // Order Management
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    // Payment and Delivery
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }

    // Other
    public DbSet<CheckingAccount> CheckingAccounts { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public object CoproducerInfos { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        #region USER MANAGEMENT
        // Relationships for ProducerInfo
        modelBuilder.Entity<ProducerInfo>()
            .HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<ProducerInfo>(p => p.UserId);

        // Relationships for CoproducerInfo
        modelBuilder.Entity<CoproducerInfo>()
            .HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<CoproducerInfo>(c => c.UserId);

        // Relationships for AMAPAdministrator
        modelBuilder.Entity<AMAPAdministrator>()
            .HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<AMAPAdministrator>(a => a.UserId);
        #endregion

        #region PRODUCT
        modelBuilder.Entity<Product>()
            .Property(p => p.Photo)
            .HasColumnType("BYTEA")
            .IsRequired(false);

        // Configure one-to-many relationship between Producer and Product
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProducerInfo)
            .WithMany(pr => pr.Products)
            .HasForeignKey(p => p.ProducerInfoId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configuration for Product -> Inventory
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Inventory)
            .WithOne(i => i.Product)
            .HasForeignKey<Inventory>(i => i.ProductId);
        #endregion

        #region ORDER
        // Relationship between Order and CoproducerInfo
        modelBuilder.Entity<Order>()
            .HasOne(o => o.CoproducerInfo)
            .WithMany(ci => ci.Orders)
            .HasForeignKey(o => o.CoproducerInfoId);

        // Relationship between Order and OrderItem
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        // Relationship between OrderItem and Product
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);
        #endregion
        #region ORDER
        // Relationship between Order and CoproducerInfo
        modelBuilder.Entity<Order>()
            .HasOne(o => o.CoproducerInfo)
            .WithMany(ci => ci.Orders)
            .HasForeignKey(o => o.CoproducerInfoId);

        // Relationship between Order and OrderItems with cascade delete
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relationship between OrderItem and Product
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);

        // Relationship between Order and Reservation with cascade delete
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Reservation)
            .WithOne(r => r.Order)
            .HasForeignKey<Reservation>(r => r.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion
        /* 
              #region PAYMENT AND DELIVERY
              // Relationship between Order and Payment
           modelBuilder.Entity<Payment>()
                  .HasOne(p => p.Order)
                  .WithOne(o => o.Payment)
                  .HasForeignKey<Payment>(p => p.OrderId);

              // Relationship between Order and Delivery
              modelBuilder.Entity<Delivery>()
                  .HasOne(d => d.Order)
                  .WithOne(o => o.Delivery)
                  .HasForeignKey<Delivery>(d => d.OrderId);
              #endregion
        */

        #region CHECKING ACCOUNT
        // Configuration for CheckingAccount
        modelBuilder.Entity<CheckingAccount>()
            .HasOne(c => c.Coproducer)
            .WithOne(c => c.CheckingAccount)
            .HasForeignKey<CheckingAccount>(c => c.CoproducerId);
        #endregion

        #region NOTIFICATION
        // Notification configuration
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Recipient)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.RecipientId);
        #endregion

        // Roles seeding
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Administrator", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "Amap", NormalizedName = "AMAP" },
            new IdentityRole { Name = "Producer", NormalizedName = "PROD" },
            new IdentityRole { Name = "CoProducer", NormalizedName = "COPR" }
        );
    }
}
