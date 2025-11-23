using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.VM
{
    public class OrderVm
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public int? InvoiceId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int ItemsCount { get; set; }
    }
}
