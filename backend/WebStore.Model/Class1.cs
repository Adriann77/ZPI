using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Model;

// Podstawowe klasy modelu
public class Address
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public bool IsDefault { get; set; } = false;
    
    // Relacje
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
}

public class Customer : User
{
    public string CustomerNumber { get; set; } = string.Empty;
    public DateTime LastOrderDate { get; set; }
    public decimal TotalSpent { get; set; }
    
    // Relacje
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ParentCategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Relacje
    public virtual Category? ParentCategory { get; set; }
    public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string SKU { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
    
    // Relacje
    public virtual Category Category { get; set; } = null!;
    public virtual Supplier Supplier { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public virtual ProductStock? ProductStock { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public string ShippingAddress { get; set; } = string.Empty;
    public string BillingAddress { get; set; } = string.Empty;
    public int? InvoiceId { get; set; }
    
    // Relacje
    public virtual Customer Customer { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public virtual Invoice? Invoice { get; set; }
}

public class Invoice
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public string PaymentMethod { get; set; } = string.Empty;
    public DateTime? PaymentDate { get; set; }
    
    // Relacje
    public virtual Order Order { get; set; } = null!;
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    
    // Relacje
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}

// Klasy dla sklepów stacjonarnych
public class StationaryStore
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime OpeningDate { get; set; }
    public bool IsActive { get; set; } = true;
    public int AddressId { get; set; }
    
    // Relacje
    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<StationaryStoreEmployee> Employees { get; set; } = new List<StationaryStoreEmployee>();
}

public class StationaryStoreEmployee
{
    public int Id { get; set; }
    public int StationaryStoreId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Relacje
    public virtual StationaryStore StationaryStore { get; set; } = null!;
}

// Dodatkowe encje wymagane dla relacji
public class Supplier
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Relacje
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

public class ProductStock
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Location { get; set; } = string.Empty;
    
    // Relacje
    public virtual Product Product { get; set; } = null!;
}

// Klasa bazowa dla dziedziczenia TPH
public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string UserType { get; set; } = string.Empty; // Discriminator dla TPH
}

// Encja asocjacyjna dla relacji N:M Order-Product
public class OrderProduct
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    
    // Relacje
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
