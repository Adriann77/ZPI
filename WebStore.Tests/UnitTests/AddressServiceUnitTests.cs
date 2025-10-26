using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DAL;
using WebStore.Model;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using Xunit;

namespace WebStore.Tests.UnitTests
{
    public class AddressServiceUnitTests : BaseUnitTests
    {
        private readonly IAddressService _addressService;

        public AddressServiceUnitTests(WebStoreDbContext dbContext, IAddressService addressService) : base(dbContext)
        {
            _addressService = addressService;
        }

        [Fact]
        public void GetAddressTest()
        {
            var address = _addressService.GetAddress(a => a.Id == 1);
            Assert.NotNull(address);
        }

        [Fact]
        public void GetMultipleAddressesTest()
        {
            var addresses = _addressService.GetAddresses(a => a.Id >= 1 && a.Id <= 2);
            Assert.NotNull(addresses);
            Assert.NotEmpty(addresses);
        }

        [Fact]
        public void GetAllAddressesTest()
        {
            var addresses = _addressService.GetAddresses();
            Assert.NotNull(addresses);
        }

        [Fact]
        public void AddNewAddressTest()
        {
            var newAddressVm = new AddOrUpdateAddressVm()
            {
                Street = "New Street 123",
                City = "Krakow",
                PostalCode = "30-001",
                Country = "Poland",
                State = "Malopolska",
                IsDefault = false,
                CustomerId = 1
            };
            var createdAddress = _addressService.AddOrUpdateAddress(newAddressVm);
            Assert.NotNull(createdAddress);
            Assert.Equal("New Street 123", createdAddress.Street);
        }

        [Fact]
        public void UpdateAddressTest()
        {
            var updateAddressVm = new AddOrUpdateAddressVm()
            {
                Id = 1,
                Street = "Updated Street 456",
                City = "Warsaw",
                PostalCode = "00-001",
                Country = "Poland",
                State = "Mazovia",
                IsDefault = true,
                CustomerId = 1
            };
            var editedAddressVm = _addressService.AddOrUpdateAddress(updateAddressVm);
            Assert.NotNull(editedAddressVm);
            Assert.Equal("Updated Street 456", editedAddressVm.Street);
        }

        [Fact]
        public void SetAsDefaultAddressTest()
        {
            var result = _addressService.SetAsDefaultAddress(1);
            Assert.True(result);
        }

        [Fact]
        public void DeleteAddressTest()
        {
            var result = _addressService.DeleteAddress(1);
            Assert.True(result);
        }

        [Fact]
        public void GetAddressesByCustomerTest()
        {
            var addresses = _addressService.GetAddressesByCustomer(1);
            Assert.NotNull(addresses);
        }

        [Fact]
        public void GetDefaultAddressTest()
        {
            var address = _addressService.GetDefaultAddress(1);
            Assert.NotNull(address);
        }
    }
}
