using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Store.Infrastructure.Extensions
{
    public static class ParamsExtensions
    {
        public static IQueryable<Category> Searching(this IQueryable<Category> categories, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return categories;

            return categories.Where(c =>
                    c.Name.ToLower().Contains(search.Trim().ToLower()));
        }

        public static IQueryable<T> Sorting<T>(this IQueryable<T> items,
            string orderBy, string orderDir)
        {
            if (string.IsNullOrWhiteSpace(orderBy) || !IsValidProperty<T>(orderBy))
                return items;
            if (string.IsNullOrWhiteSpace(orderDir))
            {
                orderDir = "ASC";
            }
            orderDir = (orderDir.ToLower() == "desc") ? "DESC" : "ASC";
            return items.OrderBy(String.Format($"{orderBy} {orderDir}"));
        }

        public static bool IsValidProperty<T>(string propertyName)
        {
            var prop = typeof(T).GetProperty(
                            propertyName,
                            BindingFlags.IgnoreCase |
                            BindingFlags.Public |
                            BindingFlags.Instance);
            if (prop == null)
            {
                throw new NotSupportedException($"Property {propertyName} doesn't exist in {typeof(T)}");
            }
            return prop != null;
        }

        public static IQueryable<Product> Searching(this IQueryable<Product> products, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return products;

            return products.Where(p =>
                    p.Name.ToLower().Contains(search.Trim().ToLower()));
        }

        public static IQueryable<Product> FilteringByPrice(this IQueryable<Product> products,
                                                           decimal minPrice,
                                                           decimal maxPrice)
            => products.Where(p => minPrice <= p.Price && p.Price <= maxPrice);

        public static IQueryable<Product> FilteringByCategories(this IQueryable<Product> products,
                                                                IEnumerable<int> categoryIds)
        {
            if (categoryIds == null)
            {
                return products;
            }
            return products.Where(p => categoryIds.Contains(p.CategoryId)); 
        }

        public static IQueryable<Provider> Searching(this IQueryable<Provider> providers, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return providers;

            return providers.Where(p =>
                    p.Name.ToLower().Contains(search.Trim().ToLower()));
        }

    }
}
