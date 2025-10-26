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
    public class OrderServiceUnitTests : BaseUnitTests
    {
        private readonly IOrderService _orderService;

        public OrderServiceUnitTests(WebStoreDbContext dbContext, IOrderService orderService) : base(dbContext)
        {
            _orderService = orderService;
        }

        [Fact]
        public void GetOrderTest()
        {
            var order = _orderService.GetOrder(o => o.Id == 1);
            Assert.NotNull(order);
        }

        [Fact]
        public void GetMultipleOrdersTest()
        {
            var orders = _orderService.GetOrders(o => o.Id >= 1 && o.Id <= 2);
            Assert.NotNull(orders);
            Assert.NotEmpty(orders);
        }

        [Fact]
        public void GetAllOrdersTest()
        {
            var orders = _orderService.GetOrders();
            Assert.NotNull(orders);
        }

        [Fact]
        public void AddNewOrderTest()
        {
            var newOrderVm = new AddOrUpdateOrderVm()
            {
                CustomerId = 1,
                Status = "Pending",
                ShippingAddress = "Test Street 123, Warsaw",
                BillingAddress = "Test Street 123, Warsaw",
                OrderItems = new List<OrderItemVm>
                {
                    new OrderItemVm
                    {
                        ProductId = 1,
                        Quantity = 2,
                        UnitPrice = 1000
                    }
                }
            };
            var createdOrder = _orderService.AddOrUpdateOrder(newOrderVm);
            Assert.NotNull(createdOrder);
            Assert.Equal("Pending", createdOrder.Status);
        }

        [Fact]
        public void UpdateOrderTest()
        {
            var updateOrderVm = new AddOrUpdateOrderVm()
            {
                Id = 1,
                CustomerId = 1,
                Status = "Processing",
                ShippingAddress = "Updated Street 456, Warsaw",
                BillingAddress = "Updated Street 456, Warsaw",
                OrderItems = new List<OrderItemVm>()
            };
            var editedOrderVm = _orderService.AddOrUpdateOrder(updateOrderVm);
            Assert.NotNull(editedOrderVm);
            Assert.Equal("Processing", editedOrderVm.Status);
        }

        [Fact]
        public void CancelOrderTest()
        {
            var result = _orderService.CancelOrder(1);
            Assert.True(result);
        }

        [Fact]
        public void CompleteOrderTest()
        {
            var result = _orderService.CompleteOrder(1);
            Assert.True(result);
        }

        [Fact]
        public void GetOrdersByCustomerTest()
        {
            var orders = _orderService.GetOrdersByCustomer(1);
            Assert.NotNull(orders);
        }

        [Fact]
        public void CalculateOrderTotalTest()
        {
            var orderVm = new AddOrUpdateOrderVm
            {
                OrderItems = new List<OrderItemVm>
                {
                    new OrderItemVm { Quantity = 2, UnitPrice = 100 },
                    new OrderItemVm { Quantity = 1, UnitPrice = 50 }
                }
            };
            var total = _orderService.CalculateOrderTotal(orderVm);
            Assert.Equal(250, total);
        }
    }
}
