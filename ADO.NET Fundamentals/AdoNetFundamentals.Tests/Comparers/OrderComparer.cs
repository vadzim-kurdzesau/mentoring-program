using System.Diagnostics.CodeAnalysis;
using AdoNetFundamentals.Models;

namespace AdoNetFundamentals.Tests.Comparers
{
    internal class OrderComparer : IEqualityComparer<Order?>
    {
        public bool Equals(Order? x, Order? y)
        {
            return (x, y) switch
            {
                (null, null) => true,
                (null, _) => false,
                (_, null) => false,
                _ => x.Status == y.Status
                  && x.CreatedDate.Date == y.CreatedDate.Date
                  && (Equals(GetDateOrNull(x.UpdatedDate), GetDateOrNull(y.UpdatedDate)))
                  && x.ProductId == y.ProductId
            };
        }

        public int GetHashCode([DisallowNull] Order obj)
        {
            return 1; // Skip hash comparison
        }

        private static DateTime? GetDateOrNull(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.Date;
            }

            return null;
        }
    }
}
