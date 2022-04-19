using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;
using Store.Infrastructure.Extensions;
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
        public PagedList<Product> GetForCategory(int category_id, ProductParams productParams)
        {
            var items = repository.Products.Where(p => p.CategoryId == category_id)
                                        .Search(productParams.Search)
                                        .FilterByPrice(productParams.MinPrice, productParams.MaxPrice)
                                        .Sort(productParams.OrderBy, productParams.OrderDir);
            return PagedList<Product>.ToPagedList(items, productParams.PageSize, productParams.PageNumber);
        }
        public Product GetForCategoryById(int category_id, int id)
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
        public PagedList<Product> GetForCategories(ProductParams productParams, IEnumerable<int> categoryIds)
        {
            var items = repository.Products
                                        .Search(productParams.Search)
                                        .FilterByPrice(productParams.MinPrice, productParams.MaxPrice)
                                        .FilterByCategories(categoryIds)
                                        .Sort(productParams.OrderBy, productParams.OrderDir);
            return PagedList<Product>.ToPagedList(items, productParams.PageSize, productParams.PageNumber);
        }
        public Product GetById(int id)
        {
            return repository.Products.FirstOrDefault(p => p.Id == id);
        }

    }
}
