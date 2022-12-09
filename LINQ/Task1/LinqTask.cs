using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        /// <summary>
        /// Select the customers whose total turnover (the sum of all orders) exceeds a certain value. 
        /// </summary>
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Sum(o => o.Total) > limit);
        }

        /// <summary>
        /// For each customer make a list of suppliers located in the same country and the same city. Compose query without grouping.
        /// </summary>
        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.Select(c => (c, suppliers.Where(s
                    => s.Country.Equals(c.Country)
                    && s.City.Equals(c.City))));
        }

        /// <summary>
        /// For each customer make a list of suppliers located in the same country and the same city. Compose query with grouping.
        /// </summary>
        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.GroupJoin(
                suppliers,
                customer => (customer.Country, customer.City),
                supplier => (supplier.Country, supplier.City),
                (customer, supplier) => (customer, supplier));
        }

        /// <summary>
        /// Find all customers with the sum of all orders that exceed a certain value.
        /// </summary>
        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Any(o => o.Total > limit));
        }

        /// <summary>
        /// Select the clients, including the date of their first order. 
        /// </summary>
        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            return customers.Where(c => c.Orders.Any())
                .Select(c => (c, c.Orders.First().OrderDate));
        }

        /// <summary>
        /// Repeat the previous query but order the result by year, month, turnover (descending) and customer name. 
        /// </summary>
        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            return customers.Where(c => c.Orders.Any())
                .Select(customer => (customer, customer.Orders.First().OrderDate))
                .OrderBy(p => p.OrderDate.Year)
                .ThenBy(p => p.OrderDate.Month)
                .ThenByDescending(p => p.customer.Orders.Sum(o => o.Total));
        }

        /// <summary>
        /// Select the clients which either have a) non-digit postal code or b) undefined region or c) operator code in the phone is not specified (does not contain parentheses).
        /// </summary>
        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            const string operatorCodePattern = @"\(\d+\).+";

            return customers.Where(c
                => c.PostalCode.Any(c => !char.IsDigit(c))
                || c.Region == null
                || !Regex.Match(c.Phone, operatorCodePattern).Success);
        }

        /// <summary>
        /// Group the products by category, then by availability in stock with ordering by cost.
        /// </summary>
        /// <example>
        /// category - Beverages
        /// UnitsInStock - 39
        ///     price - 18.0000
        ///     price - 19.0000
        /// UnitsInStock - 17
        ///     price - 18.0000
        ///     price - 19.0000
        /// </example>
        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            return products.GroupBy(p => p.Category, (category, productsByCategory) => new Linq7CategoryGroup
            {
                Category = category,
                UnitsInStockGroup = productsByCategory.GroupBy(p => p.UnitsInStock, (unitsInStock, productsByUnits) => new Linq7UnitsInStockGroup
                {
                    UnitsInStock = unitsInStock,
                    Prices = productsByUnits.Select(p => p.UnitPrice)
                })
            });
        }

        /// <summary>
        /// Group the products by “cheap”, “average” and “expensive” following the rules:
        /// a. From 0 to cheap inclusive
        /// b. From cheap exclusive to average inclusive
        /// c. From average exclusive to expensive inclusive
        /// </summary>
        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            return products.GroupBy(p => GetProductCategory(p.UnitPrice))
                .Select(group => (group.Key, group.AsEnumerable()));

            decimal GetProductCategory(decimal unitPrice)
            {
                if (unitPrice > 0 && unitPrice <= cheap)
                {
                    return cheap;
                }

                if (unitPrice > cheap && unitPrice <= middle)
                {
                    return middle;
                }

                if (unitPrice > middle && unitPrice <= expensive)
                {
                    return expensive;
                }

                throw new ArgumentException("Product has invalid price category.");
            }
        }

        /// <summary>
        /// Calculate the average profitability of each city (average amount of orders per customer) and average rate (average number of orders per customer from each city). 
        /// </summary>
        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            return customers.GroupBy(
                c => c.City,
                (city, customersByCity) => (
                    city,
                    (int)Math.Round(customersByCity
                                        .Select(c => c.Orders)
                                        .Average(orders => orders.Sum(o => o.Total))),
                    (int)Math.Round(customersByCity.Select(c => c.Orders)
                                    .Average(orders => orders.Count()))));
        }

        /// <summary>
        /// Build a string of unique supplier country names, sorted first by length and then by country.
        /// </summary>
        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var countryNames = suppliers.Select(s => s.Country)
                .Distinct()
                .OrderBy(n => n.Length)
                .ThenBy(n => n);

            return string.Concat(countryNames);
        }
    }
}