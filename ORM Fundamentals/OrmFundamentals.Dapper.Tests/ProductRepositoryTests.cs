using OrmFundamentals.Shared.Comparers;
using OrmFundamentals.Shared.Exceptions;
using OrmFundamentals.Shared.Models;
using OrmFundamentals.Tests.Shared;

namespace OrmFundamentals.Dapper.Tests
{
    [Collection("OrmFundamentals.Dapper.Tests")] // Include to collection to run tests sequentally, not in parallel
    public class ProductRepositoryTests
    {
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _productRepository = new ProductRepository(Constants.ConnectionString);

            new System.Data.SqlClient.SqlConnection(Constants.ConnectionString).ClearTable("Orders");
            new System.Data.SqlClient.SqlConnection(Constants.ConnectionString).ClearTable("Products");
        }

        [Fact]
        public void Add_AddsProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _productRepository.Add(expected);

            var actual = _productRepository.Get(expected.Id);
            Assert.Equal(expected, actual, new ProductComparer());
        }

        [Fact]
        public void Add_ProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _productRepository.Add(product: null));
        }

        [Fact]
        public void Update_UpdatesProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _productRepository.Add(expected);
            expected.Description = "TestDescription";

            _productRepository.Update(expected);

            var actual = _productRepository.Get(expected.Id);
            Assert.Equal(expected, actual, new ProductComparer());
        }

        [Fact]
        public void Update_ProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _productRepository.Update(product: null));
        }

        [Fact]
        public void Update_ProductWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _productRepository.Update(new Product() { Id = int.MaxValue, Name = "TestName" }));
        }

        [Fact]
        public void Delete_DeletesProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _productRepository.Add(expected);

            _productRepository.Delete(expected.Id);
            Assert.Null(_productRepository.Get(expected.Id));
        }

        [Fact]
        public void Delete_ProductWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _productRepository.Delete(id: int.MaxValue));
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
                _productRepository.Add(product);
            }

            var actual = _productRepository.GetAll();

            Assert.Equal(expected, actual, new ProductComparer());
        }
    }
}