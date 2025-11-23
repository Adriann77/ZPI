using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.VM
{
    public class AddOrUpdateOrderVm
    {
        public int? Id { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        public string Status { get; set; } = string.Empty;
        
        [Required]
        public string ShippingAddress { get; set; } = string.Empty;
        
        [Required]
        public string BillingAddress { get; set; } = string.Empty;
        
        public List<OrderItemVm> OrderItems { get; set; } = new List<OrderItemVm>();
    }

    public class OrderItemVm
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }
    }
}
