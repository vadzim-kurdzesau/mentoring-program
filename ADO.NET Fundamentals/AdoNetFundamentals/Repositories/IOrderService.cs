using System.Collections.Generic;
using AdoNetFundamentals.Models;

namespace AdoNetFundamentals.Repositories
{
    public interface IOrderService : IRepository<Order>
    {
        IEnumerable<Order> GetByMonth(Month month);

        IEnumerable<Order> GetByYear(int year);

        IEnumerable<Order> GetByProduct(int productId);
    }
}
