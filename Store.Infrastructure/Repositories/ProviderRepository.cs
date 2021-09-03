using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public IQueryable<Provider> Get()
        {
            var items = _repository.Providers.AsQueryable();

            return items;
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