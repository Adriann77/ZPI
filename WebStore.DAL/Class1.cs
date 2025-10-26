using Microsoft.EntityFrameworkCore;
using WebStore.Model;

namespace WebStore.DAL;

public class WebStoreDbContext : DbContext
{
    public WebStoreDbContext(DbContextOptions<WebStoreDbContext> options) : base(options)
    {
    }

    // DbSets dla wszystkich encji
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductStock> ProductStocks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<StationaryStore> StationaryStores { get; set; }
    public DbSet<StationaryStoreEmployee> StationaryStoreEmployees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfiguracja dziedziczenia TPH dla User -> Customer
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Customer>("Customer");

        // Relacje 1:M
        // Customer -> Order
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Customer -> Address
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Supplier -> Product
        modelBuilder.Entity<Supplier>()
            .HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product -> ProductStock
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductStock)
            .WithOne(ps => ps.Product)
            .HasForeignKey<ProductStock>(ps => ps.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Category -> Product
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Category -> Category (self-reference)
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Invoice -> Order
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Order)
            .WithOne(o => o.Invoice)
            .HasForeignKey<Order>(o => o.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        // StationaryStore -> Address
        modelBuilder.Entity<StationaryStore>()
            .HasOne(s => s.Address)
            .WithMany()
            .HasForeignKey(s => s.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

        // StationaryStore -> StationaryStoreEmployee
        modelBuilder.Entity<StationaryStore>()
            .HasMany(s => s.Employees)
            .WithOne(e => e.StationaryStore)
            .HasForeignKey(e => e.StationaryStoreId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order -> OrderItem
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Product -> OrderItem
        modelBuilder.Entity<Product>()
            .HasMany(p => p.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacje N:M Order-Product przez OrderProduct
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Konfiguracja indeksów
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.SKU)
            .IsUnique();

        modelBuilder.Entity<Invoice>()
            .HasIndex(i => i.InvoiceNumber)
            .IsUnique();

        // Konfiguracja wartości domyślnych dla SQLite
        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Product>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Category>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Supplier>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Order>()
            .Property(o => o.OrderDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Invoice>()
            .Property(i => i.InvoiceDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<ProductStock>()
            .Property(ps => ps.LastUpdated)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
