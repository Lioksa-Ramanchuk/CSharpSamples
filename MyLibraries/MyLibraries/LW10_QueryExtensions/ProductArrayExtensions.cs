using System.Collections.Generic;
using System.Linq;

namespace OOP.LW10_QueryExtensions
{
    using LW02_Product;

    public static class ProductArrayExtensions
    {
        public static IEnumerable<Product> WhereName(this IEnumerable<Product> products, string name)
        {
            return products.Where(p => p.Name.Equals(name));
        }

        public static IEnumerable<Product> WhereNameWithPriceLimit(this IEnumerable<Product> products, string name, decimal maxPrice)
        {
            return products.WhereName(name).Where(p => (p.Price ?? 0) <= maxPrice);
        }

        public static int CountIfPriceExceeds(this IEnumerable<Product> products, decimal priceLowerLimit)
        {
            return products.Count(p => (p.Price ?? 0) > priceLowerLimit);
        }

        public static Product? MaxByCost(this IEnumerable<Product> products)
        {
            return products.MaxBy(p => (p.Price ?? 0) * (p.Amount ?? 0));
        }

        public static IEnumerable<Product> OrderByProducerAndAmount(this IEnumerable<Product> products)
        {
            return products.OrderBy(p => p.Producer ?? string.Empty).ThenBy(p => p.Amount ?? 0);
        }
    }
}
