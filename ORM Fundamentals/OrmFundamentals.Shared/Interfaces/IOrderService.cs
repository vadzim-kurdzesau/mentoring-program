using OrmFundamentals.Shared.Models;
using OrmFundamentals.Shared.Services;

namespace OrmFundamentals.Shared
{
    public interface IOrderService : IRepository<Order>
    {
        IEnumerable<Order> GetByMonthCreated(Month month);

        IEnumerable<Order> GetByYearCreated(int year);

        IEnumerable<Order> GetByStatus(OrderStatus status);
    }
}
