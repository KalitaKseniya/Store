using Store.Core.Entities;
using Store.Core.RequestFeatures;

namespace Store.Core.Interfaces
{
    public interface IProductRepository
    {
        PagedList<Product> Get(int category_id, ProductParams productParams);
        Product GetById(int category_id, int id);
        void Create(Product product);
        void Save();
        void Delete(Product product);
        void Update(Product product);
        PagedList<Product> GetAllForAllCategories(ProductParams productParams);
    }
}
