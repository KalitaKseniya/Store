using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;
using Store.Infrastructure.Extensions;
using System.Linq;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RepositoryContext repository;
        public CategoryRepository(RepositoryContext _repository)
        {
            repository = _repository;
        }
        public PagedList<Category> Get(CategoryParams categoryParams)
        {
            var categoriesAfterSearch = repository.Categories.ToList()
                                                        .Searching(categoryParams.Search);
            return PagedList<Category>.ToPagedList(categoriesAfterSearch, 
                categoryParams.PageSize, categoryParams.PageNumber);
        }

        public Category GetById(int id)
        {
            return repository.Categories.Find(id);
        }

        public void Create(Category category)
        {
            repository.Categories.Add(category);
        }
        public void Save()
        {
            repository.SaveChanges();
        }
        public void Delete(Category category)
        {
            repository.Categories.Remove(category);
        }
        public void Update(Category category)
        {
            repository.Categories.Update(category);
        }
    }
}

