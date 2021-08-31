using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Linq;
using Store.Core.RequestFeatures;
using Store.Infrastructure.Extensions;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RepositoryContext repository;
        public ProductRepository(RepositoryContext _repository)
        {
            repository = _repository;
        }
        public Core.RequestFeatures.IQueryable<Product> Get(int category_id, ProductParams productParams)
        {
            var items = repository.Products.Where(p => p.CategoryId == category_id)
                                        .Searching(productParams.Search)
                                        .Sorting(productParams.OrderBy, productParams.OrderDir); 
            return Core.RequestFeatures.IQueryable<Product>.ToPagedList(items, productParams.PageSize, productParams.PageNumber);
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
        public Core.RequestFeatures.IQueryable<Product> GetAllForAllCategories(ProductParams productParams)
        {       
            var items = repository.Products
                                        .Searching(productParams.Search)
                                        .Sorting(productParams.OrderBy, productParams.OrderDir);
            return Core.RequestFeatures.IQueryable<Product>.ToPagedList(items, productParams.PageSize, productParams.PageNumber);
        }
    }
}
