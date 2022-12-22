using OrmFundamentals.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmFundamentals.EntityFramework.Tests.Comparers
{
    internal class OrderComparer : IEqualityComparer<Order>
    {
        public bool Equals(Order x, Order y)
        {
            return (x, y) switch
            {
                (null, null) => true,
                (null, _) => false,
                (_, null) => false,
                _ => x.Id == y.Id
                  && x.Status == y.Status
                  && x.CreatedDate == y.CreatedDate
                  && x.UpdatedDate == y.UpdatedDate
                  && x.ProductId == y.ProductId
            };
        }

        public int GetHashCode([DisallowNull] Order obj)
        {
            return 1; // Skip hash comparison
        }
    }
}
