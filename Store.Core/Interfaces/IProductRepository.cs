using Store.Core.Entities;
using System.Collections.Generic;

namespace Store.Core.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get(int category_id);
        Product GetById(int category_id, int id);
        void Create(Product product);
        void Save();
        void Delete(Product product);
        void Update(Product product);
    }
}
