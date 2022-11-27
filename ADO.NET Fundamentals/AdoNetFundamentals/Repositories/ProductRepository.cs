using AdoNetFundamentals.Extensions;
using AdoNetFundamentals.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetFundamentals.Repositories
{
    public class ProductRepository : IRepository<Product>, IDisposable
    {
        private readonly SqlConnection _databaseConnection;

        public ProductRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _databaseConnection = new SqlConnection(connectionString);
        }

        public void Add(Product product)
        {
            using (_databaseConnection)
            {
                _databaseConnection.Open();
                var command = _databaseConnection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Product (Name, Description, Weight, Height, Width, Length)"
                                    + "VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";

                InsertProductParameters(command, product);
                var result = command.ExecuteNonQuery();
            }
        }

        public Product Get(int id)
        {
            using (_databaseConnection)
            {
                _databaseConnection.Open();
                var command = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", _databaseConnection);
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
            using (_databaseConnection)
            {
                _databaseConnection.Open();
                var command = _databaseConnection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Product"
                                    + " SET Name = @Name,"
                                    + "     Description = @Description,"
                                    + "     Weight = @Weight,"
                                    + "     Height = @Height,"
                                    + "     Width = @Width,"
                                    + "     Length = @Length"
                                    + " WHERE Id = @Id";

                InsertProductParameters(command, product);
                command.Parameters.AddWithValue("@Id", product.Id);

                var result = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (_databaseConnection)
            {
                _databaseConnection.Open();
                var command = new SqlCommand("DELETE FROM Product WHERE Id = @Id", _databaseConnection);
                command.Parameters.AddWithValue("@Id", id);

                var result = command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _databaseConnection.Dispose();
        }

        private static SqlCommand InsertProductParameters(SqlCommand command, Product product)
        {
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Weight", product.Weight);
            command.Parameters.AddWithValue("@Height", product.Height);
            command.Parameters.AddWithValue("@Width", product.Width);
            command.Parameters.AddWithValue("@Length", product.Length);

            return command;
        }
    }
}
