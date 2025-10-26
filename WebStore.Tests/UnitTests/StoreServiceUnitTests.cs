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
    public class StoreServiceUnitTests : BaseUnitTests
    {
        private readonly IStoreService _storeService;

        public StoreServiceUnitTests(WebStoreDbContext dbContext, IStoreService storeService) : base(dbContext)
        {
            _storeService = storeService;
        }

        [Fact]
        public void GetStoreTest()
        {
            var store = _storeService.GetStore(s => s.Id == 1);
            Assert.NotNull(store);
        }

        [Fact]
        public void GetMultipleStoresTest()
        {
            var stores = _storeService.GetStores(s => s.Id >= 1 && s.Id <= 2);
            Assert.NotNull(stores);
            Assert.NotEmpty(stores);
        }

        [Fact]
        public void GetAllStoresTest()
        {
            var stores = _storeService.GetStores();
            Assert.NotNull(stores);
        }

        [Fact]
        public void AddNewStoreTest()
        {
            var newStoreVm = new AddOrUpdateStationaryStoreVm()
            {
                Name = "Test Store",
                Phone = "123-456-789",
                Email = "test@store.com",
                OpeningDate = DateTime.Now,
                AddressId = 1,
                IsActive = true
            };
            var createdStore = _storeService.AddOrUpdateStore(newStoreVm);
            Assert.NotNull(createdStore);
            Assert.Equal("Test Store", createdStore.Name);
        }

        [Fact]
        public void UpdateStoreTest()
        {
            var updateStoreVm = new AddOrUpdateStationaryStoreVm()
            {
                Id = 1,
                Name = "Updated Store",
                Phone = "987-654-321",
                Email = "updated@store.com",
                OpeningDate = DateTime.Now.AddDays(-30),
                AddressId = 1,
                IsActive = true
            };
            var editedStoreVm = _storeService.AddOrUpdateStore(updateStoreVm);
            Assert.NotNull(editedStoreVm);
            Assert.Equal("Updated Store", editedStoreVm.Name);
        }

        [Fact]
        public void DeactivateStoreTest()
        {
            var result = _storeService.DeactivateStore(1);
            Assert.True(result);
        }

        [Fact]
        public void ActivateStoreTest()
        {
            var result = _storeService.ActivateStore(1);
            Assert.True(result);
        }

        [Fact]
        public void GetActiveStoresTest()
        {
            var stores = _storeService.GetActiveStores();
            Assert.NotNull(stores);
        }

        [Fact]
        public void GetStoresByCityTest()
        {
            var stores = _storeService.GetStoresByCity("Warsaw");
            Assert.NotNull(stores);
        }
    }
}
