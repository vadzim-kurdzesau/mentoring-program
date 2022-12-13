using System;
using System.Data;
using AdoNetFundamentals.Models;

namespace AdoNetFundamentals.Extensions
{
    public static class DataRowExtensions
    {
        public static T GetValueOrDefault<T>(this DataRow row, int index)
        {
            return !row.IsNull(index) ? (T)row[index] : default;
        }

        public static Order ToOrder(this DataRow row)
        {
            return new Order
            {
                Id = (int)row[0],
                Status = Enum.Parse<OrderStatus>((string)row[1]),
                CreatedDate = (DateTime)row[2],
                UpdatedDate = row.GetValueOrDefault<DateTime?>(3),
                ProductId = (int)row[4],
            };
        }
    }
}
