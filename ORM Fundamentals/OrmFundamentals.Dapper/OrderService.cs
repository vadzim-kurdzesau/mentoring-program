using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using OrmFundamentals.Dapper.Extensions;
using OrmFundamentals.Shared;
using OrmFundamentals.Shared.Exceptions;
using OrmFundamentals.Shared.Models;
using OrmFundamentals.Shared.Services;

namespace OrmFundamentals.Dapper.Services
{
    public class OrderService : IOrderService
    {
        private readonly string _connectionString;

        public OrderService(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public void Add(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Insert(order);
            }
        }

        public Order? Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Get<Order>(id);
            }
        }

        public void Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                if (!connection.Exists<Product>(order.Id))
                {
                    throw new EntryDoesNotExistException($"Order with id '{order.Id}' does not exist.");
                }

                connection.Update(order);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (!connection.Exists<Order>(id))
                {
                    throw new EntryDoesNotExistException($"Order with id '{id}' does not exist.");
                }

                connection.Delete(new Order { Id = id });
            }
        }

        public IEnumerable<Order> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<Order>();
            }
        }

        public IEnumerable<Order> GetByMonthCreated(Month month)
        {
            throw new NotImplementedException();
            //return _orderContext.Orders.FromSql($"EXEC dbo.Order_GetByMonthCreated {month}");
        }

        public IEnumerable<Order> GetByYearCreated(int year)
        {
            throw new NotImplementedException();
            //return _orderContext.Orders.FromSql($"EXEC dbo.Order_GetByYearCreated {year}");
        }

        public IEnumerable<Order> GetByProduct(int productId)
        {
            throw new NotImplementedException();
            //return _orderContext.Orders.FromSql($"EXEC dbo.Order_GetByProductId {productId}");
        }
    }
}
