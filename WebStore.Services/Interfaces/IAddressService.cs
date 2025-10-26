using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebStore.Model;
using WebStore.ViewModels.VM;

namespace WebStore.Services.Interfaces
{
    public interface IAddressService
    {
        AddressVm AddOrUpdateAddress(AddOrUpdateAddressVm addOrUpdateAddressVm);
        AddressVm GetAddress(Expression<Func<Address, bool>> filterExpression);
        IEnumerable<AddressVm> GetAddresses(Expression<Func<Address, bool>>? filterExpression = null);
        bool SetAsDefaultAddress(int addressId);
        bool DeleteAddress(int addressId);
        IEnumerable<AddressVm> GetAddressesByCustomer(int customerId);
        AddressVm? GetDefaultAddress(int customerId);
    }
}
