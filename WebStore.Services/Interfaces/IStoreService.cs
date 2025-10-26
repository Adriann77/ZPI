using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebStore.Model;
using WebStore.ViewModels.VM;

namespace WebStore.Services.Interfaces
{
    public interface IStoreService
    {
        StationaryStoreVm AddOrUpdateStore(AddOrUpdateStationaryStoreVm addOrUpdateStoreVm);
        StationaryStoreVm GetStore(Expression<Func<StationaryStore, bool>> filterExpression);
        IEnumerable<StationaryStoreVm> GetStores(Expression<Func<StationaryStore, bool>>? filterExpression = null);
        bool DeactivateStore(int storeId);
        bool ActivateStore(int storeId);
        IEnumerable<StationaryStoreVm> GetActiveStores();
        IEnumerable<StationaryStoreVm> GetStoresByCity(string city);
    }
}
