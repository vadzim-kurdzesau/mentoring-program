using AdoNetFundamentals.Models;
using System;
using System.Data;

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
                Status = (string)row[1],
                CreatedDate = (DateTime)row[2],
                UpdatedDate = row.GetValueOrDefault<DateTime?>(3),
                ProductId = (int)row[4],
            };
        }
    }
}
