using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;

namespace Store.Core.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Get();
        Category GetById(int id);
        void Create(Category category);
        void Save();
    }
}
