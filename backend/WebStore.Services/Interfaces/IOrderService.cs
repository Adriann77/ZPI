using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebStore.Model;
using WebStore.ViewModels.VM;

namespace WebStore.Services.Interfaces
{
    public interface IOrderService
    {
        OrderVm AddOrUpdateOrder(AddOrUpdateOrderVm addOrUpdateOrderVm);
        OrderVm GetOrder(Expression<Func<Order, bool>> filterExpression);
        IEnumerable<OrderVm> GetOrders(Expression<Func<Order, bool>>? filterExpression = null);
        bool CancelOrder(int orderId);
        bool CompleteOrder(int orderId);
        IEnumerable<OrderVm> GetOrdersByCustomer(int customerId);
        decimal CalculateOrderTotal(AddOrUpdateOrderVm orderVm);
    }
}
