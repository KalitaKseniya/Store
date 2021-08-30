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

        public static IQueryable<Category> Sorting(this IQueryable<Category> categories,
            string orderBy, string orderDir)
        {
            if (string.IsNullOrWhiteSpace(orderBy) || !IsValidProperty(orderBy))
                return categories;
            if (string.IsNullOrWhiteSpace(orderDir))
            {
                orderDir = "ASC";
            }
            orderDir = (orderDir.ToLower() == "desc") ? "DESC" : "ASC";
            return categories.OrderBy(String.Format($"{orderBy} {orderDir}"));
        }

        public static bool IsValidProperty(string propertyName)
        {
            var prop = typeof(Category).GetProperty(
                            propertyName,
                            BindingFlags.IgnoreCase |
                            BindingFlags.Public |
                            BindingFlags.Instance);
            if (prop == null)
            {
                throw new NotSupportedException($"Property {propertyName} doesn't exist in Category");
            }
            return prop != null;
        }
    }
}
