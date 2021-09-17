using Store.Core.Entities;
using Store.Core.RequestFeatures;
using System.Collections.Generic;

namespace Store.Core.Interfaces
{
    public interface IProductRepository
    {
        PagedList<Product> GetForCategory(int category_id, ProductParams productParams);
        Product GetForCategoryById(int category_id, int id);
        Product GetById(int id);
        void Create(Product product);
        void Save();
        void Delete(Product product);
        void Update(Product product);
        PagedList<Product> GetForCategories(ProductParams productParams, IEnumerable<int> categoryIds);
    }
}
