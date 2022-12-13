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
        const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ado_net_fundamentals";
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            _repository = new ProductRepository(ConnectionString);
            new SqlConnection(ConnectionString).ClearTable("Orders");
            new SqlConnection(ConnectionString).ClearTable("Products");
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
    }
}
