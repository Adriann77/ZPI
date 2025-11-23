using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebStore.Model;
using WebStore.ViewModels.VM;

namespace WebStore.Services.Interfaces
{
    public interface IInvoiceService
    {
        InvoiceVm AddOrUpdateInvoice(AddOrUpdateInvoiceVm addOrUpdateInvoiceVm);
        InvoiceVm GetInvoice(Expression<Func<Invoice, bool>> filterExpression);
        IEnumerable<InvoiceVm> GetInvoices(Expression<Func<Invoice, bool>>? filterExpression = null);
        InvoiceVm CreateInvoiceFromOrder(int orderId);
        bool MarkInvoiceAsPaid(int invoiceId, string paymentMethod);
        IEnumerable<InvoiceVm> GetInvoicesByCustomer(int customerId);
        string GenerateInvoiceNumber();
    }
}
