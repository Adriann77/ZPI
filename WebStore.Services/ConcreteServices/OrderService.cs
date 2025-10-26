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
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(WebStoreDbContext dbContext, IMapper mapper, ILogger<OrderService> logger)
            : base(dbContext, mapper, logger) { }

        public OrderVm AddOrUpdateOrder(AddOrUpdateOrderVm addOrUpdateOrderVm)
        {
            try
            {
                if (addOrUpdateOrderVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var orderEntity = Mapper.Map<Order>(addOrUpdateOrderVm);
                
                if (addOrUpdateOrderVm.Id.HasValue && addOrUpdateOrderVm.Id != 0)
                {
                    DbContext.Orders.Update(orderEntity);
                }
                else
                {
                    orderEntity.OrderDate = DateTime.Now;
                    orderEntity.TotalAmount = CalculateOrderTotal(addOrUpdateOrderVm);
                    DbContext.Orders.Add(orderEntity);
                }

                DbContext.SaveChanges();

                // Map order items
                if (addOrUpdateOrderVm.OrderItems.Any())
                {
                    var orderItems = Mapper.Map<List<OrderItem>>(addOrUpdateOrderVm.OrderItems);
                    foreach (var item in orderItems)
                    {
                        item.OrderId = orderEntity.Id;
                        item.TotalPrice = item.Quantity * item.UnitPrice;
                    }
                    DbContext.OrderItems.AddRange(orderItems);
                    DbContext.SaveChanges();
                }

                var orderVm = Mapper.Map<OrderVm>(orderEntity);
                return orderVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public OrderVm GetOrder(Expression<Func<Order, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");

                var orderEntity = DbContext.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                    .FirstOrDefault(filterExpression);

                var orderVm = Mapper.Map<OrderVm>(orderEntity);
                return orderVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<OrderVm> GetOrders(Expression<Func<Order, bool>>? filterExpression = null)
        {
            try
            {
                var ordersQuery = DbContext.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                    .AsQueryable();

                if (filterExpression != null)
                    ordersQuery = ordersQuery.Where(filterExpression);

                var orderVms = Mapper.Map<IEnumerable<OrderVm>>(ordersQuery);
                return orderVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool CancelOrder(int orderId)
        {
            try
            {
                var order = DbContext.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null) return false;

                order.Status = "Cancelled";
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool CompleteOrder(int orderId)
        {
            try
            {
                var order = DbContext.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null) return false;

                order.Status = "Completed";
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<OrderVm> GetOrdersByCustomer(int customerId)
        {
            try
            {
                return GetOrders(o => o.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public decimal CalculateOrderTotal(AddOrUpdateOrderVm orderVm)
        {
            try
            {
                return orderVm.OrderItems.Sum(item => item.Quantity * item.UnitPrice);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
