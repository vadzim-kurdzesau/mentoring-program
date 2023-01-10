using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using OrmFundamentals.Dapper.Extensions;
using OrmFundamentals.Shared;
using OrmFundamentals.Shared.Exceptions;
using OrmFundamentals.Shared.Models;

namespace OrmFundamentals.Dapper
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public void Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Insert(product);
            }
        }

        public Product? Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Get<Product>(id);
            }
        }

        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                if (!connection.Exists<Product>(product.Id))
                {
                    throw new EntryDoesNotExistException($"Product with id '{product.Id}' does not exist.");
                }

                connection.Update(product);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (!connection.Exists<Product>(id))
                {
                    throw new EntryDoesNotExistException($"Product with id '{id}' does not exist.");
                }

                connection.Delete(new Product { Id = id });
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<Product>();
            }
        }
    }
}
