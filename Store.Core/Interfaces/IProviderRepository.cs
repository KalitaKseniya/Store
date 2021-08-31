
using Store.Core.Entities;
using System.Linq;

namespace Store.Core.Interfaces
{
    public interface IProviderRepository
    {
        IQueryable<Provider> Get();
        Provider GetById(int id);
        void Create(Provider provider);
        void Update(Provider provider);
        void Delete(Provider provider);
        void Save();
    }
}
