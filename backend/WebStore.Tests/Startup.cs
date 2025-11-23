using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebStore.DAL;
using WebStore.Services.ConcreteServices;
using WebStore.Services.Interfaces;

namespace WebStore.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<WebStoreDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDb")
                );

            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));

            // service binding
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IAddressService, AddressService>();

            // … other bindings…
            services.SeedData();
        }
    }
}
