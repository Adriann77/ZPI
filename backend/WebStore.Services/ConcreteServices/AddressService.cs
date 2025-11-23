using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL;
using WebStore.Model;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Services.ConcreteServices
{
    public class AddressService : BaseService, IAddressService
    {
        public AddressService(WebStoreDbContext dbContext, IMapper mapper, ILogger<AddressService> logger)
            : base(dbContext, mapper, logger) { }

        public AddressVm AddOrUpdateAddress(AddOrUpdateAddressVm addOrUpdateAddressVm)
        {
            try
            {
                if (addOrUpdateAddressVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var addressEntity = Mapper.Map<Address>(addOrUpdateAddressVm);
                
                if (addOrUpdateAddressVm.Id.HasValue && addOrUpdateAddressVm.Id != 0)
                {
                    DbContext.Addresses.Update(addressEntity);
                }
                else
                {
                    // If this is set as default, unset other default addresses for this customer
                    if (addOrUpdateAddressVm.IsDefault)
                    {
                        var existingAddresses = DbContext.Addresses
                            .Where(a => a.CustomerId == addOrUpdateAddressVm.CustomerId);
                        foreach (var existing in existingAddresses)
                        {
                            existing.IsDefault = false;
                        }
                    }
                    
                    DbContext.Addresses.Add(addressEntity);
                }

                DbContext.SaveChanges();
                var addressVm = Mapper.Map<AddressVm>(addressEntity);
                return addressVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public AddressVm GetAddress(Expression<Func<Address, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");

                var addressEntity = DbContext.Addresses
                    .Include(a => a.Customer)
                    .FirstOrDefault(filterExpression);

                var addressVm = Mapper.Map<AddressVm>(addressEntity);
                return addressVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<AddressVm> GetAddresses(Expression<Func<Address, bool>>? filterExpression = null)
        {
            try
            {
                var addressesQuery = DbContext.Addresses
                    .Include(a => a.Customer)
                    .AsQueryable();

                if (filterExpression != null)
                    addressesQuery = addressesQuery.Where(filterExpression);

                var addressVms = Mapper.Map<IEnumerable<AddressVm>>(addressesQuery);
                return addressVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool SetAsDefaultAddress(int addressId)
        {
            try
            {
                var address = DbContext.Addresses.FirstOrDefault(a => a.Id == addressId);
                if (address == null) return false;

                // Unset other default addresses for this customer
                var otherAddresses = DbContext.Addresses
                    .Where(a => a.CustomerId == address.CustomerId && a.Id != addressId);
                foreach (var other in otherAddresses)
                {
                    other.IsDefault = false;
                }

                // Set this address as default
                address.IsDefault = true;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool DeleteAddress(int addressId)
        {
            try
            {
                var address = DbContext.Addresses.FirstOrDefault(a => a.Id == addressId);
                if (address == null) return false;

                DbContext.Addresses.Remove(address);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<AddressVm> GetAddressesByCustomer(int customerId)
        {
            try
            {
                return GetAddresses(a => a.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public AddressVm? GetDefaultAddress(int customerId)
        {
            try
            {
                var address = DbContext.Addresses
                    .Include(a => a.Customer)
                    .FirstOrDefault(a => a.CustomerId == customerId && a.IsDefault);

                return address != null ? Mapper.Map<AddressVm>(address) : null;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
