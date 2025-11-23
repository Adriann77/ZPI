using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels.VM
{
    public class AddOrUpdateStationaryStoreVm
    {
        public int? Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public DateTime OpeningDate { get; set; }
        
        [Required]
        public int AddressId { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
