using System.Collections.Generic;
using AdoNetFundamentals.Models;

namespace AdoNetFundamentals.Repositories
{
    public interface IOrderService : IRepository<Order>
    {
        IEnumerable<Order> GetByMonthCreated(Month month);

        IEnumerable<Order> GetByYearCreated(int year);

        IEnumerable<Order> GetByProduct(int productId);
    }
}
