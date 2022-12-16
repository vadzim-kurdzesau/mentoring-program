using System.Data.SqlClient;
using AdoNetFundamentals.Exceptions;
using AdoNetFundamentals.Models;
using AdoNetFundamentals.Repositories;
using AdoNetFundamentals.Tests.Comparers;
using AdoNetFundamentals.Tests.Utilities;

namespace AdoNetFundamentals.Tests
{
    [Collection("AdoNetFundamentalsTests")] // Include to collection to run tests sequentally, not in parallel
    public sealed class ProductRepositoryTests
    {
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            _repository = new ProductRepository(Constants.ConnectionString);
            new SqlConnection(Constants.ConnectionString).ClearTable("Orders");
            new SqlConnection(Constants.ConnectionString).ClearTable("Products");
        }

        [Fact]
        public void Add_AddsProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _repository.Add(expected);

            var actual = _repository.Get(expected.Id);
            Assert.Equal(expected, actual, new ProductComparer());
        }

        [Fact]
        public void Add_ProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Add(product: null));
        }

        [Fact]
        public void Update_UpdatesProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _repository.Add(expected);
            expected.Description = "TestDescription";

            _repository.Update(expected);

            var actual = _repository.Get(expected.Id);
            Assert.Equal(expected, actual, new ProductComparer());
        }

        [Fact]
        public void Update_ProductIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Update(product: null));
        }

        [Fact]
        public void Update_ProductWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _repository.Update(new Product() { Id = int.MaxValue, Name = "TestName" }));
        }

        [Fact]
        public void Delete_DeletesProduct()
        {
            var expected = new Product
            {
                Name = "TestProduct"
            };

            _repository.Add(expected);

            _repository.Delete(expected.Id);
            Assert.Null(_repository.Get(expected.Id));
        }

        [Fact]
        public void Delete_ProductWithSpecifiedIdDoesNotExist_ThrowsEntryDoesNotExistException()
        {
            Assert.Throws<EntryDoesNotExistException>(() => _repository.Delete(id: int.MaxValue));
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
                _repository.Add(product);
            }

            var actual = _repository.GetAll();

            Assert.Equal(expected, actual, new ProductComparer());
        }
    }
}
