using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;
using Store.Infrastructure.Extensions;

namespace Store.Infrastructure.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly RepositoryContext _repository;
        public ProviderRepository(RepositoryContext repository)
        {
            _repository = repository;
        }
        public void Create(Provider provider)
        {
            _repository.Providers.Add(provider);
        }

        public void Delete(Provider provider)
        {
            _repository.Providers.Remove(provider);
        }

        public PagedList<Provider> Get(ProviderParams providerParams)
        {
            var items = _repository.Providers
                                   .Search(providerParams.Search)
                                   .Sort(providerParams.OrderBy, providerParams.OrderDir);

            return PagedList<Provider>.ToPagedList(items, providerParams.PageSize, providerParams.PageNumber);
        }

        public Provider GetById(int id)
        {
            return _repository.Providers.Find(id);
        }
        public void Update(Provider provider)
        {
            _repository.Providers.Update(provider);
        }
        public void Save()
        {
            _repository.SaveChanges();
        }
    }
}