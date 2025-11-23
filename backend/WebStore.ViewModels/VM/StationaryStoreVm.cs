namespace WebStore.ViewModels.VM
{
    public class StationaryStoreVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime OpeningDate { get; set; }
        public bool IsActive { get; set; }
        public string FullAddress { get; set; } = string.Empty;
        public int EmployeesCount { get; set; }
    }
}
