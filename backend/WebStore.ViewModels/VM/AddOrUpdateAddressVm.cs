using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.VM
{
    public class AddOrUpdateAddressVm
    {
        public int? Id { get; set; }
        
        [Required]
        public string Street { get; set; } = string.Empty;
        
        [Required]
        public string City { get; set; } = string.Empty;
        
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        
        [Required]
        public string Country { get; set; } = string.Empty;
        
        public string State { get; set; } = string.Empty;
        
        public bool IsDefault { get; set; } = false;
        
        [Required]
        public int CustomerId { get; set; }
    }
}
