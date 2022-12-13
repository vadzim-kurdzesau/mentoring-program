using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AdoNetFundamentals.Exceptions;
using AdoNetFundamentals.Extensions;
using AdoNetFundamentals.Models;

namespace AdoNetFundamentals.Repositories
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
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Products (Name, Description, Weight, Height, Width, Length)"
                                    + "VALUES (@Name, @Description, @Weight, @Height, @Width, @Length);"
                                    + "SELECT SCOPE_IDENTITY()";

                InsertProductParameters(command, product);
                var result = command.ExecuteScalar()!;
                product.Id = Convert.ToInt32(result);
            }
        }

        public Product? Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        return reader.ToProduct();
                    }
                }
            }

            return null;
        }

        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Products"
                                    + " SET Name = @Name,"
                                    + "     Description = @Description,"
                                    + "     Weight = @Weight,"
                                    + "     Height = @Height,"
                                    + "     Width = @Width,"
                                    + "     Length = @Length"
                                    + " WHERE Id = @Id";

                InsertProductParameters(command, product);
                command.Parameters.AddWithValue("@Id", product.Id);

                var rowsUpdated = command.ExecuteNonQuery();
                if (rowsUpdated == 0)
                {
                    throw new EntryDoesNotExistException($"Product with id '{product.Id}' does not exist.");
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Products WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                var rowsDeleted = command.ExecuteNonQuery();
                if (rowsDeleted == 0)
                {
                    throw new EntryDoesNotExistException($"Product with id '{id}' does not exist.");
                }
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Products", connection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return reader.ToProduct();
                }
            }
        }

        private static SqlCommand InsertProductParameters(SqlCommand command, Product product)
        {
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description.GetValueOrDbNull());
            command.Parameters.AddWithValue("@Weight", product.Weight.GetValueOrDbNull());
            command.Parameters.AddWithValue("@Height", product.Height.GetValueOrDbNull());
            command.Parameters.AddWithValue("@Width", product.Width.GetValueOrDbNull());
            command.Parameters.AddWithValue("@Length", product.Length.GetValueOrDbNull());

            return command;
        }
    }
}
