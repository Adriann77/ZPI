using AutoMapper;
using WebStore.Model;
using WebStore.ViewModels.VM;

namespace WebStore.Services.ConcreteServices
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapowanie Product -> ProductVm
            CreateMap<Product, ProductVm>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => 0.0f)); // Domyślna wartość dla Weight

            // Mapowanie AddOrUpdateProductVm -> Product
            CreateMap<AddOrUpdateProductVm, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? 0))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => 0)) // Domyślna wartość
                .ForMember(dest => dest.SKU, opt => opt.MapFrom(src => string.Empty)) // Domyślna wartość
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Supplier, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.OrderProducts, opt => opt.Ignore())
                .ForMember(dest => dest.ProductStock, opt => opt.Ignore());

            // Order mappings
            CreateMap<Order, OrderVm>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : ""))
                .ForMember(dest => dest.ItemsCount, opt => opt.MapFrom(src => src.OrderItems.Count));
            CreateMap<AddOrUpdateOrderVm, Order>();
            CreateMap<OrderItem, OrderItemVm>();
            CreateMap<OrderItemVm, OrderItem>();

            // Invoice mappings
            CreateMap<Invoice, InvoiceVm>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Order != null && src.Order.Customer != null ? $"{src.Order.Customer.FirstName} {src.Order.Customer.LastName}" : ""))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Order != null ? $"ORD-{src.Order.Id:D6}" : ""));
            CreateMap<AddOrUpdateInvoiceVm, Invoice>();

            // Store mappings
            CreateMap<StationaryStore, StationaryStoreVm>()
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => $"{src.Address.Street}, {src.Address.PostalCode} {src.Address.City}, {src.Address.Country}"))
                .ForMember(dest => dest.EmployeesCount, opt => opt.MapFrom(src => src.Employees.Count));
            CreateMap<AddOrUpdateStationaryStoreVm, StationaryStore>();

            // Address mappings
            CreateMap<Address, AddressVm>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : ""))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => $"{src.Street}, {src.PostalCode} {src.City}, {src.Country}"));
            CreateMap<AddOrUpdateAddressVm, Address>();
        }
    }
}
