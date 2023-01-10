using Microsoft.EntityFrameworkCore;
using OrmFundamentals.EntityFramework.Services;
using OrmFundamentals.EntityFramework.Tests.Comparers;
using OrmFundamentals.Shared.Exceptions;
using OrmFundamentals.Shared.Models;
using OrmFundamentals.Shared.Services;
using OrmFundamentals.Tests.Shared;

namespace OrmFundamentals.EntityFramework.Tests
{
    [Collection("OrmFundamentals.EntityFramework.Tests")] // Include to collection to run tests sequentally, not in parallel
    public sealed class OrderServiceTests : IDisposable
    {
        private readonly OrderContext _orderContext;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<OrderContext>()
                .UseSqlServer(Constants.ConnectionString)
                .Options;

            _orderContext = new OrderContext(dbContextOptions);
            _orderService = new OrderService(_orderContext);

            new System.Data.SqlClient.SqlConnection(Constants.ConnectionString).ClearTable("Orders");
            new System.Data.SqlClient.SqlConnection(Constants.ConnectionString).ClearTable("Products");
        }

        [Fact]
        public void Add_AddsOrder()
        {
            var product = new Product
            {
                Name = "TestProduct"
            };

            _orderContext.Products.Add(product);
            _orderContext.SaveChanges();

            var expected = new Order
            {
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTime.Now,
                ProductId = product.Id,
            };

            _orderService.Add(expected);

            var actual = _orderService.Get(expected.Id);
            Assert.Equal(expected, actual, new OrderComparer());
        }

        [Fact]
        public void Add_ProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _orderService.Add(order: null));
        }

        [Fact]
        public void Update_UpdatesOrder()
        {
            var product = new Product
            {
                Name = "TestProduct"
            };

            _orderContext.Products.Add(product);
            _orderContext.SaveChanges();

            var expected = new Order
            {
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTime.Now,
                ProductId = product.Id,
            };

            _orderService.Add(expected);
            expected.UpdatedDate = DateTime.Now;

            _orderService.Update(expected);

            var actual = _orderService.Get(expected.Id);
            Assert.Equal(expected, actual, new OrderComparer());
        }

        [Fact]
        public void Update_OrderIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _orderService.Update(order: null));
        }

        [Fact]
        public void Update_OrderWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _orderService.Update(new Order() { Id = int.MaxValue }));
        }

        [Fact]
        public void Delete_DeletesOrder()
        {
            var product = new Product
            {
                Name = "TestProduct"
            };

            _orderContext.Products.Add(product);
            _orderContext.SaveChanges();

            var expected = new Order
            {
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTime.Now,
                ProductId = product.Id,
            };

            _orderService.Add(expected);

            _orderService.Delete(expected.Id);
            
            Assert.Null(_orderService.Get(expected.Id));
        }

        [Fact]
        public void Delete_OrderWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _orderService.Delete(id: int.MaxValue));
        }

        [Fact]
        public void GetAll_GetsAllOrders()
        {
            var product = new Product
            {
                Name = "TestProduct"
            };

            _orderContext.Products.Add(product);
            _orderContext.SaveChanges();

            var expected = new Order[]
            {
                new() { Status = OrderStatus.NotStarted, CreatedDate = DateTime.Now, ProductId = product.Id },
                new() { Status = OrderStatus.Loading, CreatedDate = DateTime.Now, ProductId = product.Id },
                new() { Status = OrderStatus.InProgress, CreatedDate = DateTime.Now, ProductId = product.Id },
            };

            foreach (var order in expected)
            {
                _orderService.Add(order);
            }

            var actual = _orderService.GetAll();

            Assert.Equal(expected, actual, new OrderComparer());
        }

        [Fact]
        public void GetByMonth_GetsOrdersByMonthCreated()
        {
            const Month expectedMonth = Month.May;
            var product = new Product
            {
                Name = "TestProduct"
            };

            _orderContext.Products.Add(product);
            _orderContext.SaveChanges();

            var orders = new Order[]
            {
                new() { Status = OrderStatus.NotStarted, CreatedDate = new DateTime(2022, (int)expectedMonth, 1), ProductId = product.Id },
                new() { Status = OrderStatus.Loading, CreatedDate = new DateTime(2023, (int)expectedMonth, 1), ProductId = product.Id },
                new() { Status = OrderStatus.InProgress, CreatedDate = new DateTime(2022, 6, 1), ProductId = product.Id },
            };

            foreach (var order in orders)
            {
                _orderService.Add(order);
            }

            var actual = _orderService.GetByMonthCreated(expectedMonth);

            Assert.Equal(orders.Where(o => o.CreatedDate.Month == (int)expectedMonth), actual, new OrderComparer());
        }

        [Fact]
        public void GetByYear_GetsOrdersByYearCreated()
        {
            const int expectedYear = 2023;
            var product = new Product
            {
                Name = "TestProduct"
            };

            _orderContext.Products.Add(product);
            _orderContext.SaveChanges();

            var orders = new Order[]
            {
                new() { Status = OrderStatus.NotStarted, CreatedDate = new DateTime(2022, 5, 1), ProductId = product.Id },
                new() { Status = OrderStatus.Loading, CreatedDate = new DateTime(expectedYear, 5, 1), ProductId = product.Id },
                new() { Status = OrderStatus.InProgress, CreatedDate = new DateTime(2022, 6, 1), ProductId = product.Id },
            };

            foreach (var order in orders)
            {
                _orderService.Add(order);
            }

            var actual = _orderService.GetByYearCreated(expectedYear);

            Assert.Equal(orders.Where(o => o.CreatedDate.Year == expectedYear), actual, new OrderComparer());
        }

        [Fact]
        public void GetByProduct_GetsOrdersByProductId()
        {
            var products = new Product[]
            {
                new() { Name = "TestProduct1" },
                new() { Name = "ExpectedTestProduct" }
            };

            foreach (var product in products)
            {
                _orderContext.Products.Add(product);
            }

            _orderContext.SaveChanges();

            var expectedProductId = products.First().Id;
            var orders = new Order[]
            {
                new() { Status = OrderStatus.NotStarted, CreatedDate = new DateTime(2022, 5, 1), ProductId = expectedProductId },
                new() { Status = OrderStatus.Loading, CreatedDate = new DateTime(2023, 5, 1), ProductId = expectedProductId },
                new() { Status = OrderStatus.InProgress, CreatedDate = new DateTime(2022, 6, 1), ProductId = expectedProductId + 1 },
            };

            foreach (var order in orders)
            {
                _orderService.Add(order);
            }

            var actual = _orderService.GetByProduct(expectedProductId);

            Assert.Equal(orders.Where(o => o.ProductId == expectedProductId), actual, new OrderComparer());
        }

        public void Dispose()
        {
            _orderService.Dispose();
        }
    }
}
