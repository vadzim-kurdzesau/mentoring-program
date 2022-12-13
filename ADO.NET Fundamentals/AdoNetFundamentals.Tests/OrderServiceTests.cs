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
        const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ado_net_fundamentals";
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;

        public OrderServiceTests()
        {
            _productRepository = new ProductRepository(ConnectionString);
            _orderRepository = new OrderRepository(ConnectionString);
            new SqlConnection(ConnectionString).ClearTable("Orders");
            new SqlConnection(ConnectionString).ClearTable("Products");
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
    }
}
