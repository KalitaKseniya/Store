using Store.Core.Entities;
using Store.Core.RequestFeatures;
using System.Linq;

namespace Store.Core.Interfaces
{
    public interface IProviderRepository
    {
        PagedList<Provider> Get(ProviderParams providerParams);
        Provider GetById(int id);
        void Create(Provider provider);
        void Update(Provider provider);
        void Delete(Provider provider);
        void Save();
    }
}