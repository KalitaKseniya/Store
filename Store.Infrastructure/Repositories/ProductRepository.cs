using Store.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RepositoryContext repository;
        public ProductRepository(RepositoryContext _repository)
        {
            repository = _repository;
        }
        public IEnumerable<Product> Get(int category_id)
        {
            return repository.Products.Where(p => p.CategoryId == category_id);
        }
        public Product GetById(int category_id, int id)
        {
            return repository.Products.FirstOrDefault(p => p.Id == id && p.CategoryId == category_id);
        }
        public void Create(Product product)
        {
            repository.Products.Add(product);
        }
        public void Save()
        {
            repository.SaveChanges();
        }
    }
}
