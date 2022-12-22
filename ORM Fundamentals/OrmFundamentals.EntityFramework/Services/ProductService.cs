using OrmFundamentals.Shared;
using OrmFundamentals.Shared.Exceptions;
using OrmFundamentals.Shared.Models;

namespace OrmFundamentals.EntityFramework.Services
{
    public class ProductService : IRepository<Product>, IDisposable
    {
        private readonly OrderContext _orderContext;
        private bool _disposedValue;

        public ProductService(OrderContext orderContext)
        {
            _orderContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
        }

        public void Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _orderContext.Products.Add(product);
            _orderContext.SaveChanges();
        }

        public Product? Get(int id)
        {
            return _orderContext.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _orderContext.Products;
        }

        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (!_orderContext.Products.Any(p => p.Id == product.Id))
            {
                throw new EntryDoesNotExistException($"Product with id '{product.Id}' does not exist.");
            }

            _orderContext.Products.Update(product);
            _orderContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = Get(id);
            if (product == null)
            {
                throw new EntryDoesNotExistException($"Product with id '{id}' does not exist.");
            }

            _orderContext.Products.Remove(product);
            _orderContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _orderContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
