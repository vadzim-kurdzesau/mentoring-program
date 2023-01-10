using Microsoft.EntityFrameworkCore;
using OrmFundamentals.Shared;
using OrmFundamentals.Shared.Exceptions;
using OrmFundamentals.Shared.Models;
using OrmFundamentals.Shared.Services;

namespace OrmFundamentals.EntityFramework.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        private readonly OrderContext _orderContext;
        private bool disposedValue;

        public OrderService(OrderContext orderContext)
        {
            _orderContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
        }

        public void Add(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _orderContext.Orders.Add(order);
            _orderContext.SaveChanges();
        }

        public Order? Get(int id)
        {
            return _orderContext.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderContext.Orders;
        }

        public void Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (!_orderContext.Orders.Any(o => o.Id == order.Id))
            {
                throw new EntryDoesNotExistException($"Order with id '{order.Id}' does not exist.");
            }

            _orderContext.Orders.Update(order);
            _orderContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = Get(id);
            if (order == null)
            {
                throw new EntryDoesNotExistException($"Order with id '{id}' does not exist.");
            }

            _orderContext.Orders.Remove(order);
            _orderContext.SaveChanges();
        }

        public IEnumerable<Order> GetByMonthCreated(Month month)
        {
            return _orderContext.Orders.FromSql($"EXEC dbo.Orders_GetByMonthCreated {month}");
        }

        public IEnumerable<Order> GetByYearCreated(int year)
        {
            return _orderContext.Orders.FromSql($"EXEC dbo.Orders_GetByYearCreated {year}");
        }

        public IEnumerable<Order> GetByProduct(int productId)
        {
            return _orderContext.Orders.FromSql($"EXEC dbo.Orders_GetByProductId {productId}");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _orderContext.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
