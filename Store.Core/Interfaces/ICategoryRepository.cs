using Store.Core.Entities;
using System.Collections.Generic;

namespace Store.Core.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Get();
        Category GetById(int id);
        void Create(Category category);
        void Save();
        void Delete(Category category);
        void Update(Category category);
    }
}
