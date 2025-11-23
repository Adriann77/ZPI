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
    public class InvoiceService : BaseService, IInvoiceService
    {
        public InvoiceService(WebStoreDbContext dbContext, IMapper mapper, ILogger<InvoiceService> logger)
            : base(dbContext, mapper, logger) { }

        public InvoiceVm AddOrUpdateInvoice(AddOrUpdateInvoiceVm addOrUpdateInvoiceVm)
        {
            try
            {
                if (addOrUpdateInvoiceVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var invoiceEntity = Mapper.Map<Invoice>(addOrUpdateInvoiceVm);
                
                if (addOrUpdateInvoiceVm.Id.HasValue && addOrUpdateInvoiceVm.Id != 0)
                {
                    DbContext.Invoices.Update(invoiceEntity);
                }
                else
                {
                    invoiceEntity.InvoiceNumber = GenerateInvoiceNumber();
                    invoiceEntity.InvoiceDate = DateTime.Now;
                    invoiceEntity.DueDate = DateTime.Now.AddDays(30);
                    
                    // Get order details for calculations
                    var order = DbContext.Orders.FirstOrDefault(o => o.Id == addOrUpdateInvoiceVm.OrderId);
                    if (order != null)
                    {
                        invoiceEntity.SubTotal = order.TotalAmount;
                        invoiceEntity.TotalAmount = invoiceEntity.SubTotal + invoiceEntity.TaxAmount;
                    }
                    
                    DbContext.Invoices.Add(invoiceEntity);
                }

                DbContext.SaveChanges();
                var invoiceVm = Mapper.Map<InvoiceVm>(invoiceEntity);
                return invoiceVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public InvoiceVm GetInvoice(Expression<Func<Invoice, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");

                var invoiceEntity = DbContext.Invoices
                    .Include(i => i.Order)
                    .ThenInclude(o => o.Customer)
                    .FirstOrDefault(filterExpression);

                var invoiceVm = Mapper.Map<InvoiceVm>(invoiceEntity);
                return invoiceVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<InvoiceVm> GetInvoices(Expression<Func<Invoice, bool>>? filterExpression = null)
        {
            try
            {
                var invoicesQuery = DbContext.Invoices
                    .Include(i => i.Order)
                    .ThenInclude(o => o.Customer)
                    .AsQueryable();

                if (filterExpression != null)
                    invoicesQuery = invoicesQuery.Where(filterExpression);

                var invoiceVms = Mapper.Map<IEnumerable<InvoiceVm>>(invoicesQuery);
                return invoiceVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public InvoiceVm CreateInvoiceFromOrder(int orderId)
        {
            try
            {
                var order = DbContext.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                    throw new ArgumentException("Order not found");

                var invoiceVm = new AddOrUpdateInvoiceVm
                {
                    OrderId = orderId,
                    Status = "Pending",
                    PaymentMethod = "Bank Transfer",
                    TaxAmount = order.TotalAmount * 0.23m // 23% VAT
                };

                return AddOrUpdateInvoice(invoiceVm);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool MarkInvoiceAsPaid(int invoiceId, string paymentMethod)
        {
            try
            {
                var invoice = DbContext.Invoices.FirstOrDefault(i => i.Id == invoiceId);
                if (invoice == null) return false;

                invoice.Status = "Paid";
                invoice.PaymentMethod = paymentMethod;
                invoice.PaymentDate = DateTime.Now;
                
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<InvoiceVm> GetInvoicesByCustomer(int customerId)
        {
            try
            {
                return GetInvoices(i => i.Order.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public string GenerateInvoiceNumber()
        {
            try
            {
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month.ToString("D2");
                var count = DbContext.Invoices.Count(i => i.InvoiceDate.Year == year && i.InvoiceDate.Month == DateTime.Now.Month) + 1;
                return $"INV/{year}/{month}/{count:D4}";
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
