using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using System.Linq.Expressions;
using WebStore.Model;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;
    private readonly ILogger<InvoicesController> _logger;

    public InvoicesController(IInvoiceService invoiceService, ILogger<InvoicesController> logger)
    {
        _invoiceService = invoiceService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<InvoiceVm>> GetInvoices()
    {
        try
        {
            var invoices = _invoiceService.GetInvoices();
            return Ok(invoices);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting invoices");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<InvoiceVm> GetInvoice(int id)
    {
        try
        {
            Expression<Func<Invoice, bool>> filter = i => i.Id == id;
            var invoice = _invoiceService.GetInvoice(filter);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting invoice with id {InvoiceId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public ActionResult<InvoiceVm> CreateInvoice(AddOrUpdateInvoiceVm invoiceVm)
    {
        try
        {
            var createdInvoice = _invoiceService.AddOrUpdateInvoice(invoiceVm);
            return Ok(createdInvoice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating invoice");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public ActionResult<InvoiceVm> UpdateInvoice(int id, AddOrUpdateInvoiceVm invoiceVm)
    {
        try
        {
            var updatedInvoice = _invoiceService.AddOrUpdateInvoice(invoiceVm);
            return Ok(updatedInvoice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating invoice with id {InvoiceId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("from-order/{orderId}")]
    public ActionResult<InvoiceVm> CreateInvoiceFromOrder(int orderId)
    {
        try
        {
            var invoice = _invoiceService.CreateInvoiceFromOrder(orderId);
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating invoice from order {OrderId}", orderId);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("{id}/mark-paid")]
    public ActionResult MarkInvoiceAsPaid(int id, [FromBody] string paymentMethod)
    {
        try
        {
            var success = _invoiceService.MarkInvoiceAsPaid(id, paymentMethod);
            if (!success)
                return NotFound();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking invoice as paid with id {InvoiceId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("generate-number")]
    public ActionResult<string> GenerateInvoiceNumber()
    {
        try
        {
            var invoiceNumber = _invoiceService.GenerateInvoiceNumber();
            return Ok(invoiceNumber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating invoice number");
            return StatusCode(500, "Internal server error");
        }
    }
}
