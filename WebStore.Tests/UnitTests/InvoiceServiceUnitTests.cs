using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DAL;
using WebStore.Model;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using Xunit;

namespace WebStore.Tests.UnitTests
{
    public class InvoiceServiceUnitTests : BaseUnitTests
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceServiceUnitTests(WebStoreDbContext dbContext, IInvoiceService invoiceService) : base(dbContext)
        {
            _invoiceService = invoiceService;
        }

        [Fact]
        public void GetInvoiceTest()
        {
            var invoice = _invoiceService.GetInvoice(i => i.Id == 1);
            Assert.NotNull(invoice);
        }

        [Fact]
        public void GetMultipleInvoicesTest()
        {
            var invoices = _invoiceService.GetInvoices(i => i.Id >= 1 && i.Id <= 2);
            Assert.NotNull(invoices);
            Assert.NotEmpty(invoices);
        }

        [Fact]
        public void GetAllInvoicesTest()
        {
            var invoices = _invoiceService.GetInvoices();
            Assert.NotNull(invoices);
        }

        [Fact]
        public void AddNewInvoiceTest()
        {
            var newInvoiceVm = new AddOrUpdateInvoiceVm()
            {
                OrderId = 1,
                Status = "Pending",
                PaymentMethod = "Bank Transfer",
                TaxAmount = 230
            };
            var createdInvoice = _invoiceService.AddOrUpdateInvoice(newInvoiceVm);
            Assert.NotNull(createdInvoice);
            Assert.Equal("Pending", createdInvoice.Status);
        }

        [Fact]
        public void UpdateInvoiceTest()
        {
            var updateInvoiceVm = new AddOrUpdateInvoiceVm()
            {
                Id = 1,
                OrderId = 1,
                Status = "Paid",
                PaymentMethod = "Credit Card",
                TaxAmount = 230
            };
            var editedInvoiceVm = _invoiceService.AddOrUpdateInvoice(updateInvoiceVm);
            Assert.NotNull(editedInvoiceVm);
            Assert.Equal("Paid", editedInvoiceVm.Status);
        }

        [Fact]
        public void CreateInvoiceFromOrderTest()
        {
            var invoice = _invoiceService.CreateInvoiceFromOrder(1);
            Assert.NotNull(invoice);
            Assert.Equal(1, invoice.OrderId);
        }

        [Fact]
        public void MarkInvoiceAsPaidTest()
        {
            var result = _invoiceService.MarkInvoiceAsPaid(1, "Credit Card");
            Assert.True(result);
        }

        [Fact]
        public void GetInvoicesByCustomerTest()
        {
            var invoices = _invoiceService.GetInvoicesByCustomer(1);
            Assert.NotNull(invoices);
        }

        [Fact]
        public void GenerateInvoiceNumberTest()
        {
            var invoiceNumber = _invoiceService.GenerateInvoiceNumber();
            Assert.NotNull(invoiceNumber);
            Assert.NotEmpty(invoiceNumber);
            Assert.Contains("INV/", invoiceNumber);
        }
    }
}
