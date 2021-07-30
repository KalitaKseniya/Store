using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Core.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get(int category_id);
        Product GetById(int category_id, int id);
        void Create(Product product);
        void Save();
    }
}
