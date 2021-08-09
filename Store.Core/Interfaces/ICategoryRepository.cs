using Store.Core.Entities;
using Store.Core.RequestFeatures;

namespace Store.Core.Interfaces
{
    public interface ICategoryRepository
    {
        PagedList<Category> Get(CategoryParams categoryParams);
        Category GetById(int id);
        void Create(Category category);
        void Save();
        void Delete(Category category);
        void Update(Category category);
    }
}
