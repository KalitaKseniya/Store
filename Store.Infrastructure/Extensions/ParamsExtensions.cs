using Store.Core.Entities;
using System;
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic.Core;

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

    }
}
