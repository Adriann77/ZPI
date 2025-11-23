using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using System.Linq.Expressions;
using WebStore.Model;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;
    private readonly ILogger<StoresController> _logger;

    public StoresController(IStoreService storeService, ILogger<StoresController> logger)
    {
        _storeService = storeService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<StationaryStoreVm>> GetStores()
    {
        try
        {
            var stores = _storeService.GetStores();
            return Ok(stores);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting stores");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<StationaryStoreVm> GetStore(int id)
    {
        try
        {
            Expression<Func<StationaryStore, bool>> filter = s => s.Id == id;
            var store = _storeService.GetStore(filter);
            if (store == null)
                return NotFound();

            return Ok(store);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting store with id {StoreId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public ActionResult<StationaryStoreVm> CreateStore(AddOrUpdateStationaryStoreVm storeVm)
    {
        try
        {
            var createdStore = _storeService.AddOrUpdateStore(storeVm);
            return Ok(createdStore);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating store");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public ActionResult<StationaryStoreVm> UpdateStore(int id, AddOrUpdateStationaryStoreVm storeVm)
    {
        try
        {
            var updatedStore = _storeService.AddOrUpdateStore(storeVm);
            return Ok(updatedStore);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating store with id {StoreId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("{id}/deactivate")]
    public ActionResult DeactivateStore(int id)
    {
        try
        {
            var success = _storeService.DeactivateStore(id);
            if (!success)
                return NotFound();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deactivating store with id {StoreId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("{id}/activate")]
    public ActionResult ActivateStore(int id)
    {
        try
        {
            var success = _storeService.ActivateStore(id);
            if (!success)
                return NotFound();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error activating store with id {StoreId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
