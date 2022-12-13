using System;
using System.Data;
using System.Data.SqlClient;
using AdoNetFundamentals.Extensions;
using AdoNetFundamentals.Models;

namespace AdoNetFundamentals.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private const string OrdersTableName = "Orders";
        private readonly string _connectionString;
        private readonly DataSet _dataSet;

        public OrderRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;

            _dataSet = new DataSet();

            var ordersTable = new DataTable(OrdersTableName);

            var primaryKey = new DataColumn("Id", typeof(int));
            ordersTable.Columns.Add(primaryKey);
            ordersTable.PrimaryKey = new DataColumn[] { primaryKey };

            _dataSet.Tables.Add(ordersTable);
        }

        public void Add(Order order)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                FetchTableData(databaseConnection, OrdersTableName);

                var orderRow = _dataSet.Tables[OrdersTableName].NewRow();
                InsertOrderIntoRow(order, orderRow);

                _dataSet.Tables[OrdersTableName].Rows.Add(orderRow);
                var dataAdapter = new SqlDataAdapter($"SELECT * FROM {OrdersTableName}", databaseConnection);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                commandBuilder.GetInsertCommand();

                dataAdapter.Update(_dataSet, OrdersTableName);
            }
        }

        public Order Get(int id)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                FetchTableData(databaseConnection, OrdersTableName);

                var orderRow = _dataSet.Tables[OrdersTableName].Rows.Find(id);
                if (orderRow == null)
                {
                    return null;
                }

                return orderRow.ToOrder();
            }
        }

        public void Update(Order order)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                FetchTableData(databaseConnection, OrdersTableName);

                var dataAdapter = new SqlDataAdapter($"SELECT * FROM {OrdersTableName}", databaseConnection);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                commandBuilder.GetUpdateCommand();

                var orderRow = _dataSet.Tables[OrdersTableName].Rows.Find(order.Id);
                if (orderRow == null)
                {
                    return;
                }

                UpdateOrderRow(orderRow, order);
                dataAdapter.Update(_dataSet, OrdersTableName);
            }
        }

        public void Delete(int id)
        {
            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                databaseConnection.Open();
                FetchTableData(databaseConnection, OrdersTableName);

                var dataAdapter = new SqlDataAdapter($"SELECT * FROM {OrdersTableName}", databaseConnection);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                commandBuilder.GetDeleteCommand();

                var orderRow = _dataSet.Tables[OrdersTableName].Rows.Find(id);
                if (orderRow == null)
                {
                    return;
                }

                orderRow.Delete();
                dataAdapter.Update(_dataSet, OrdersTableName);
            }
        }

        private void FetchTableData(SqlConnection databaseConnection, string tableName)
        {
            var adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", databaseConnection);
            if (!_dataSet.Tables.Contains(tableName))
            {
                throw new ArgumentException($"DataSet doesn't contain the '{tableName}' table.");
            }

            _dataSet.Tables[tableName].Clear();
            adapter.Fill(_dataSet, tableName);
        }

        private static void InsertOrderIntoRow(Order order, DataRow row)
        {
            row[0] = order.Id;
            row[1] = order.Status.ToString();
            row[2] = order.CreatedDate;
            row[3] = order.UpdatedDate == null ? DBNull.Value : order.UpdatedDate;
            row[4] = order.ProductId;
        }

        private static void UpdateOrderRow(DataRow row, Order order)
        {
            row[1] = order.Status.ToString();
            row[2] = order.CreatedDate;
            row[3] = order.UpdatedDate == null ? DBNull.Value : order.UpdatedDate;
            row[4] = order.ProductId;
        }
    }
}
