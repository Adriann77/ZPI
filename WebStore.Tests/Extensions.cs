using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL;
using WebStore.Model;

namespace WebStore.Tests
{
    public static class Extensions
    {
        // Create sample data
        public static async void SeedData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<WebStoreDbContext>();

            // Suppliers
            var supplier1 = new Supplier
            {
                Id = 1,
                CompanyName = "Tech Supplies Ltd",
                ContactName = "Adam Bednarski",
                Email = "supp1@eg.eg",
                Phone = "123-456-789",
                Address = "Tech Street 123",
                City = "Warsaw",
                Country = "Poland",
                CreatedAt = new DateTime(2010, 1, 1),
                IsActive = true
            };
            await dbContext.Suppliers.AddAsync(supplier1);

            // Categories
            var category1 = new Category
            {
                Id = 1,
                Name = "Computers",
                Description = "Computer equipment and accessories",
                CreatedAt = new DateTime(2020, 1, 1),
                IsActive = true
            };
            await dbContext.Categories.AddAsync(category1);

            // Products
            var p1 = new Product
            {
                Id = 1,
                CategoryId = category1.Id,
                SupplierId = supplier1.Id,
                Description = "Bardzo praktyczny monitor 32 cale",
                Name = "Monitor Dell 32",
                Price = 1000,
                StockQuantity = 10,
                SKU = "MON-DELL-32",
                CreatedAt = new DateTime(2023, 1, 1),
                IsActive = true
            };
            await dbContext.Products.AddAsync(p1);

            var p2 = new Product
            {
                Id = 2,
                CategoryId = category1.Id,
                SupplierId = supplier1.Id,
                Description = "Precyzyjna mysz do pracy",
                Name = "Mysz Logitech",
                Price = 500,
                StockQuantity = 25,
                SKU = "MYS-LOG-001",
                CreatedAt = new DateTime(2023, 1, 1),
                IsActive = true
            };
            await dbContext.Products.AddAsync(p2);

            // ProductStock
            var ps1 = new ProductStock
            {
                Id = 1,
                ProductId = p1.Id,
                Quantity = 10,
                ReservedQuantity = 0,
                AvailableQuantity = 10,
                LastUpdated = DateTime.Now,
                Location = "Warehouse A"
            };
            await dbContext.ProductStocks.AddAsync(ps1);

            var ps2 = new ProductStock
            {
                Id = 2,
                ProductId = p2.Id,
                Quantity = 25,
                ReservedQuantity = 0,
                AvailableQuantity = 25,
                LastUpdated = DateTime.Now,
                Location = "Warehouse B"
            };
            await dbContext.ProductStocks.AddAsync(ps2);

            // Customers
            var customer1 = new Customer
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@example.com",
                UserType = "Customer",
                CustomerNumber = "CUST001",
                LastOrderDate = DateTime.Now.AddDays(-5),
                TotalSpent = 2500
            };
            await dbContext.Users.AddAsync(customer1);

            // Addresses
            var address1 = new Address
            {
                Id = 1,
                Street = "Main Street 123",
                City = "Warsaw",
                PostalCode = "00-001",
                Country = "Poland",
                State = "Mazovia",
                IsDefault = true,
                CustomerId = 1
            };
            await dbContext.Addresses.AddAsync(address1);

            var address2 = new Address
            {
                Id = 2,
                Street = "Second Street 456",
                City = "Krakow",
                PostalCode = "30-001",
                Country = "Poland",
                State = "Malopolska",
                IsDefault = false,
                CustomerId = 1
            };
            await dbContext.Addresses.AddAsync(address2);

            // Orders
            var order1 = new Order
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = DateTime.Now.AddDays(-5),
                TotalAmount = 2000,
                Status = "Pending",
                ShippingAddress = "Main Street 123, Warsaw",
                BillingAddress = "Main Street 123, Warsaw"
            };
            await dbContext.Orders.AddAsync(order1);

            var order2 = new Order
            {
                Id = 2,
                CustomerId = 1,
                OrderDate = DateTime.Now.AddDays(-3),
                TotalAmount = 1500,
                Status = "Completed",
                ShippingAddress = "Second Street 456, Krakow",
                BillingAddress = "Second Street 456, Krakow"
            };
            await dbContext.Orders.AddAsync(order2);

            // OrderItems
            var orderItem1 = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                UnitPrice = 1000,
                TotalPrice = 2000
            };
            await dbContext.OrderItems.AddAsync(orderItem1);

            var orderItem2 = new OrderItem
            {
                Id = 2,
                OrderId = 2,
                ProductId = 2,
                Quantity = 3,
                UnitPrice = 500,
                TotalPrice = 1500
            };
            await dbContext.OrderItems.AddAsync(orderItem2);

            // Invoices
            var invoice1 = new Invoice
            {
                Id = 1,
                OrderId = 1,
                InvoiceNumber = "INV/2024/01/0001",
                InvoiceDate = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(25),
                SubTotal = 2000,
                TaxAmount = 460,
                TotalAmount = 2460,
                Status = "Pending",
                PaymentMethod = "Bank Transfer"
            };
            await dbContext.Invoices.AddAsync(invoice1);

            var invoice2 = new Invoice
            {
                Id = 2,
                OrderId = 2,
                InvoiceNumber = "INV/2024/01/0002",
                InvoiceDate = DateTime.Now.AddDays(-3),
                DueDate = DateTime.Now.AddDays(27),
                SubTotal = 1500,
                TaxAmount = 345,
                TotalAmount = 1845,
                Status = "Paid",
                PaymentMethod = "Credit Card",
                PaymentDate = DateTime.Now.AddDays(-1)
            };
            await dbContext.Invoices.AddAsync(invoice2);

            // Stationary Stores
            var store1 = new StationaryStore
            {
                Id = 1,
                Name = "Tech Store Warsaw",
                Phone = "22-123-4567",
                Email = "warsaw@techstore.com",
                OpeningDate = new DateTime(2020, 1, 15),
                IsActive = true,
                AddressId = 1
            };
            await dbContext.StationaryStores.AddAsync(store1);

            var store2 = new StationaryStore
            {
                Id = 2,
                Name = "Tech Store Krakow",
                Phone = "12-345-6789",
                Email = "krakow@techstore.com",
                OpeningDate = new DateTime(2021, 3, 10),
                IsActive = true,
                AddressId = 2
            };
            await dbContext.StationaryStores.AddAsync(store2);

            // Stationary Store Employees
            var employee1 = new StationaryStoreEmployee
            {
                Id = 1,
                StationaryStoreId = 1,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna.nowak@techstore.com",
                Phone = "22-111-2222",
                Position = "Manager",
                HireDate = new DateTime(2020, 2, 1),
                Salary = 5000,
                IsActive = true
            };
            await dbContext.StationaryStoreEmployees.AddAsync(employee1);

            var employee2 = new StationaryStoreEmployee
            {
                Id = 2,
                StationaryStoreId = 1,
                FirstName = "Piotr",
                LastName = "Wiśniewski",
                Email = "piotr.wisniewski@techstore.com",
                Phone = "22-333-4444",
                Position = "Sales Associate",
                HireDate = new DateTime(2020, 6, 15),
                Salary = 3500,
                IsActive = true
            };
            await dbContext.StationaryStoreEmployees.AddAsync(employee2);

            // save changes
            await dbContext.SaveChangesAsync();
        }
    }
}
