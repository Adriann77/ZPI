using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using System.Linq.Expressions;
using WebStore.Model;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrderVm>> GetOrders()
    {
        try
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting orders");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<OrderVm> GetOrder(int id)
    {
        try
        {
            Expression<Func<Order, bool>> filter = o => o.Id == id;
            var order = _orderService.GetOrder(filter);
            if (order == null)
                return NotFound();

            return Ok(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting order with id {OrderId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public ActionResult<OrderVm> CreateOrder(AddOrUpdateOrderVm orderVm)
    {
        try
        {
            var createdOrder = _orderService.AddOrUpdateOrder(orderVm);
            return Ok(createdOrder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public ActionResult<OrderVm> UpdateOrder(int id, AddOrUpdateOrderVm orderVm)
    {
        try
        {
            var updatedOrder = _orderService.AddOrUpdateOrder(orderVm);
            return Ok(updatedOrder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating order with id {OrderId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("{id}/cancel")]
    public ActionResult CancelOrder(int id)
    {
        try
        {
            var success = _orderService.CancelOrder(id);
            if (!success)
                return NotFound();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error canceling order with id {OrderId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("{id}/complete")]
    public ActionResult CompleteOrder(int id)
    {
        try
        {
            var success = _orderService.CompleteOrder(id);
            if (!success)
                return NotFound();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing order with id {OrderId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
