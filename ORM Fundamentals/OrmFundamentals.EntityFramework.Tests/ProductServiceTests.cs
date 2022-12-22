using Microsoft.EntityFrameworkCore;
using OrmFundamentals.EntityFramework.Services;
using OrmFundamentals.EntityFramework.Tests.Comparers;
using OrmFundamentals.EntityFramework.Tests.Utilities;
using OrmFundamentals.Shared.Exceptions;
using OrmFundamentals.Shared.Models;

namespace OrmFundamentals.EntityFramework.Tests
{
    [Collection("OrmFundamentals.EntityFramework.Tests")] // Include to collection to run tests sequentally, not in parallel
    public sealed class ProductServiceTests
    {
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<OrderContext>()
                .UseSqlServer(Constants.ConnectionString)
                .Options;

            var orderContext = new OrderContext(dbContextOptions);
            _productService = new ProductService(orderContext);

            new System.Data.SqlClient.SqlConnection(Constants.ConnectionString).ClearTable("Order");
            new System.Data.SqlClient.SqlConnection(Constants.ConnectionString).ClearTable("Product");
        }

        [Fact]
        public void Add_AddsProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _productService.Add(expected);

            var actual = _productService.Get(expected.Id);
            Assert.Equal(expected, actual, new ProductComparer());
        }

        [Fact]
        public void Add_ProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _productService.Add(product: null));
        }

        [Fact]
        public void Update_UpdatesProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _productService.Add(expected);
            expected.Description = "TestDescription";

            _productService.Update(expected);

            var actual = _productService.Get(expected.Id);
            Assert.Equal(expected, actual, new ProductComparer());
        }

        [Fact]
        public void Update_ProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _productService.Update(product: null));
        }

        [Fact]
        public void Update_ProductWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _productService.Update(new Product() { Id = int.MaxValue, Name = "TestName" }));
        }

        [Fact]
        public void Delete_DeletesProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _productService.Add(expected);

            _productService.Delete(expected.Id);
            Assert.Null(_productService.Get(expected.Id));
        }

        [Fact]
        public void Delete_ProductWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _productService.Delete(id: int.MaxValue));
        }

        [Fact]
        public void GetAll_GetsAllProducts()
        {
            var expected = new Product[]
            {
                new() { Name = "TestProduct1" },
                new() { Name = "TestProduct2" },
                new() { Name = "TestProduct3" },
            };

            foreach (var product in expected)
            {
                _productService.Add(product);
            }

            var actual = _productService.GetAll();

            Assert.Equal(expected, actual, new ProductComparer());
        }
    }
}
