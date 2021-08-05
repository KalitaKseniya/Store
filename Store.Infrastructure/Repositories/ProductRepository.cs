using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
        public void Delete(Product product)
        {
            repository.Products.Remove(product);
        } 
        public void Update(Product product)
        {
            repository.Products.Update(product);
        }
    }
}
