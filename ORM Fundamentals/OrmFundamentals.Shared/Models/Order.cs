using Dapper.Contrib.Extensions;

namespace OrmFundamentals.Shared.Models
{
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set;}

        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
