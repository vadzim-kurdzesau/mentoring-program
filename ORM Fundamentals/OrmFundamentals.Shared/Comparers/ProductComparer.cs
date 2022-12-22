using System.Diagnostics.CodeAnalysis;
using OrmFundamentals.Shared.Models;

namespace OrmFundamentals.Shared.Comparers
{
    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product? x, Product? y)
        {
            return (x, y) switch
            {
                (null, null) => true,
                (null, _) => false,
                (_, null) => false,
                _ => x.Id == y.Id
                  && string.Equals(x.Name, y.Name)
                  && string.Equals(x.Description, y.Description)
                  && x.Weight == y.Weight
                  && x.Height == y.Height
                  && x.Width == y.Width
                  && x.Length == y.Length
            };
        }

        public int GetHashCode([DisallowNull] Product obj)
        {
            return 1; // Skip hash comparison
        }
    }
}
