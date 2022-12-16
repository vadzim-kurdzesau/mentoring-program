using System.Data.SqlClient;
using AdoNetFundamentals.Exceptions;
using AdoNetFundamentals.Models;
using AdoNetFundamentals.Repositories;
using AdoNetFundamentals.Tests.Comparers;
using AdoNetFundamentals.Tests.Utilities;

namespace AdoNetFundamentals.Tests
{
    [Collection("AdoNetFundamentalsTests")] // Include to collection to run tests sequentally, not in parallel
    public sealed class OrderServiceTests
    {
        private readonly ProductRepository _productRepository;
        private readonly OrderService _orderRepository;

        public OrderServiceTests()
        {
            _productRepository = new ProductRepository(Constants.ConnectionString);
            _orderRepository = new OrderService(Constants.ConnectionString);
            new SqlConnection(Constants.ConnectionString).ClearTable("Orders");
            new SqlConnection(Constants.ConnectionString).ClearTable("Products");
        }

        [Fact]
        public void Add_AddsOrder()
        {
            var product = new Product
            {
                Name = "TestProduct"
            };

            _productRepository.Add(product);
            var expected = new Order
            {
                Id = 1,
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTime.Now,
                ProductId = product.Id,
            };

            _orderRepository.Add(expected);

            var actual = _orderRepository.Get(expected.Id);
            Assert.Equal(expected, actual, new OrderComparer());
        }

        [Fact]
        public void Add_ProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _orderRepository.Add(order: null));
        }

        [Fact]
        public void Update_UpdatesOrder()
        {
            var product = new Product
            {
                Name = "TestProduct"
            };

            _productRepository.Add(product);
            var expected = new Order
            {
                Id = 1,
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTime.Now,
                ProductId = product.Id,
            };

            _orderRepository.Add(expected);
            expected.UpdatedDate = DateTime.Now;

            _orderRepository.Update(expected);

            var actual = _orderRepository.Get(expected.Id);
            Assert.Equal(expected, actual, new OrderComparer());
        }

        [Fact]
        public void Update_OrderIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _orderRepository.Update(order: null));
        }

        [Fact]
        public void Update_OrderWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _orderRepository.Update(new Order() { Id = int.MaxValue }));
        }

        [Fact]
        public void Delete_DeletesOrder()
        {
            var product = new Product
            {
                Name = "TestProduct"
            };

            _productRepository.Add(product);
            var expected = new Order
            {
                Id = 1,
                Status = OrderStatus.NotStarted,
                CreatedDate = DateTime.Now,
                ProductId = product.Id,
            };

            _orderRepository.Add(expected);

            _orderRepository.Delete(expected.Id);
            
            Assert.Null(_orderRepository.Get(expected.Id));
        }

        [Fact]
        public void Delete_OrderWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _orderRepository.Delete(id: int.MaxValue));
        }

        [Fact]
        public void GetAll_GetsAllOrders()
        {
            var product = new Product
            {
                Name = "TestProduct"
            };

            _productRepository.Add(product);
            var expected = new Order[]
            {
                new() { Status = OrderStatus.NotStarted, CreatedDate = DateTime.Now, ProductId = product.Id },
                new() { Status = OrderStatus.Loading, CreatedDate = DateTime.Now, ProductId = product.Id },
                new() { Status = OrderStatus.InProgress, CreatedDate = DateTime.Now, ProductId = product.Id },
            };

            foreach (var order in expected)
            {
                _orderRepository.Add(order);
            }

            var actual = _orderRepository.GetAll();

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

            _productRepository.Add(product);
            var orders = new Order[]
            {
                new() { Status = OrderStatus.NotStarted, CreatedDate = new DateTime(2022, (int)expectedMonth, 1), ProductId = product.Id },
                new() { Status = OrderStatus.Loading, CreatedDate = new DateTime(2023, (int)expectedMonth, 1), ProductId = product.Id },
                new() { Status = OrderStatus.InProgress, CreatedDate = new DateTime(2022, 6, 1), ProductId = product.Id },
            };

            foreach (var order in orders)
            {
                _orderRepository.Add(order);
            }

            var actual = _orderRepository.GetByMonth(expectedMonth);

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

            _productRepository.Add(product);
            var orders = new Order[]
            {
                new() { Status = OrderStatus.NotStarted, CreatedDate = new DateTime(2022, 5, 1), ProductId = product.Id },
                new() { Status = OrderStatus.Loading, CreatedDate = new DateTime(expectedYear, 5, 1), ProductId = product.Id },
                new() { Status = OrderStatus.InProgress, CreatedDate = new DateTime(2022, 6, 1), ProductId = product.Id },
            };

            foreach (var order in orders)
            {
                _orderRepository.Add(order);
            }

            var actual = _orderRepository.GetByYear(expectedYear);

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
                _productRepository.Add(product);
            }

            var expectedProductId = products.First().Id;
            var orders = new Order[]
            {
                new() { Status = OrderStatus.NotStarted, CreatedDate = new DateTime(2022, 5, 1), ProductId = expectedProductId },
                new() { Status = OrderStatus.Loading, CreatedDate = new DateTime(2023, 5, 1), ProductId = expectedProductId },
                new() { Status = OrderStatus.InProgress, CreatedDate = new DateTime(2022, 6, 1), ProductId = expectedProductId + 1 },
            };

            foreach (var order in orders)
            {
                _orderRepository.Add(order);
            }

            var actual = _orderRepository.GetByProduct(expectedProductId);

            Assert.Equal(orders.Where(o => o.ProductId == expectedProductId), actual, new OrderComparer());
        }
    }
}
