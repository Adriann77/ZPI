using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using System.Linq.Expressions;
using WebStore.Model;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _addressService;
    private readonly ILogger<AddressesController> _logger;

    public AddressesController(IAddressService addressService, ILogger<AddressesController> logger)
    {
        _addressService = addressService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AddressVm>> GetAddresses()
    {
        try
        {
            var addresses = _addressService.GetAddresses();
            return Ok(addresses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting addresses");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<AddressVm> GetAddress(int id)
    {
        try
        {
            Expression<Func<Address, bool>> filter = a => a.Id == id;
            var address = _addressService.GetAddress(filter);
            if (address == null)
                return NotFound();

            return Ok(address);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting address with id {AddressId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public ActionResult<AddressVm> CreateAddress(AddOrUpdateAddressVm addressVm)
    {
        try
        {
            var createdAddress = _addressService.AddOrUpdateAddress(addressVm);
            return Ok(createdAddress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating address");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public ActionResult<AddressVm> UpdateAddress(int id, AddOrUpdateAddressVm addressVm)
    {
        try
        {
            var updatedAddress = _addressService.AddOrUpdateAddress(addressVm);
            return Ok(updatedAddress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating address with id {AddressId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteAddress(int id)
    {
        try
        {
            var success = _addressService.DeleteAddress(id);
            if (!success)
                return NotFound();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting address with id {AddressId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("{id}/set-default")]
    public ActionResult SetAsDefaultAddress(int id)
    {
        try
        {
            var success = _addressService.SetAsDefaultAddress(id);
            if (!success)
                return NotFound();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting address as default with id {AddressId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
