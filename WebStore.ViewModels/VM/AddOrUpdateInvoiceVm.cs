using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.VM
{
    public class AddOrUpdateInvoiceVm
    {
        public int? Id { get; set; }
        
        [Required]
        public int OrderId { get; set; }
        
        [Required]
        public string Status { get; set; } = string.Empty;
        
        [Required]
        public string PaymentMethod { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue, ErrorMessage = "Tax amount cannot be negative")]
        public decimal TaxAmount { get; set; }
        
        public DateTime? PaymentDate { get; set; }
    }
}
