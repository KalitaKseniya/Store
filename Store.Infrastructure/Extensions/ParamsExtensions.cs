using Store.Core.Entities;
using System.Collections.Generic;

namespace Store.Infrastructure.Extensions
{
    public static class ParamsExtensions
    {
        public static List<Category> Searching(this List<Category> categories, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return categories;

            return categories.FindAll(c => 
                    c.Name.ToLower().Contains(search.Trim().ToLower()));
        }
    }
}
