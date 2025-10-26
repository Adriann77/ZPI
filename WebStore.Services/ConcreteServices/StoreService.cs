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
    public class StoreService : BaseService, IStoreService
    {
        public StoreService(WebStoreDbContext dbContext, IMapper mapper, ILogger<StoreService> logger)
            : base(dbContext, mapper, logger) { }

        public StationaryStoreVm AddOrUpdateStore(AddOrUpdateStationaryStoreVm addOrUpdateStoreVm)
        {
            try
            {
                if (addOrUpdateStoreVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var storeEntity = Mapper.Map<StationaryStore>(addOrUpdateStoreVm);
                
                if (addOrUpdateStoreVm.Id.HasValue && addOrUpdateStoreVm.Id != 0)
                {
                    DbContext.StationaryStores.Update(storeEntity);
                }
                else
                {
                    DbContext.StationaryStores.Add(storeEntity);
                }

                DbContext.SaveChanges();
                var storeVm = Mapper.Map<StationaryStoreVm>(storeEntity);
                return storeVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StationaryStoreVm GetStore(Expression<Func<StationaryStore, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");

                var storeEntity = DbContext.StationaryStores
                    .Include(s => s.Address)
                    .Include(s => s.Employees)
                    .FirstOrDefault(filterExpression);

                var storeVm = Mapper.Map<StationaryStoreVm>(storeEntity);
                return storeVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<StationaryStoreVm> GetStores(Expression<Func<StationaryStore, bool>>? filterExpression = null)
        {
            try
            {
                var storesQuery = DbContext.StationaryStores
                    .Include(s => s.Address)
                    .Include(s => s.Employees)
                    .AsQueryable();

                if (filterExpression != null)
                    storesQuery = storesQuery.Where(filterExpression);

                var storeVms = Mapper.Map<IEnumerable<StationaryStoreVm>>(storesQuery);
                return storeVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool DeactivateStore(int storeId)
        {
            try
            {
                var store = DbContext.StationaryStores.FirstOrDefault(s => s.Id == storeId);
                if (store == null) return false;

                store.IsActive = false;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool ActivateStore(int storeId)
        {
            try
            {
                var store = DbContext.StationaryStores.FirstOrDefault(s => s.Id == storeId);
                if (store == null) return false;

                store.IsActive = true;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<StationaryStoreVm> GetActiveStores()
        {
            try
            {
                return GetStores(s => s.IsActive);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<StationaryStoreVm> GetStoresByCity(string city)
        {
            try
            {
                return GetStores(s => s.Address.City.ToLower().Contains(city.ToLower()));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
