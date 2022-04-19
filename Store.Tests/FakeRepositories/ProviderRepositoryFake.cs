using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;
using Store.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Store.Tests.FakeRepositories
{
    public class ProviderRepositoryFake : IProviderRepository
    {
        private readonly List<Provider> _providerRepository;
        public ProviderRepositoryFake()
        {
            _providerRepository = new List<Provider>()
            {
                new Provider(){
                    Id = 1,
                    Name = "Manege Mall",
                    Info = "Provider of women's clothes",
                    ImagePath="https://static.tildacdn.com/tild3964-3063-4261-a464-326466656338/m220.jpg",
                    Latitude = 55.47255,
                    Longitude = 28.74973
                },
                new Provider(){
                    Id = 2,
                    Name = "ASOS",
                    Info = "ASOS plc is a British online fashion and cosmetic retailer.",
                    ImagePath="https://www.bitrefill.com/content/cn/b_rgb%3AFFFFFF%2Cc_pad%2Ch_720%2Cw_1280/v1593603665/asos.jpg",
                    Latitude = 51.53962,
                    Longitude = -0.13771
                },
                new Provider(){
                    Id = 3,
                    Name = "Wildberries",
                    Info = "Wildberries is the largest Russian online retailer.",
                    ImagePath="https://wbcon.ru/wp-content/uploads/2019/10/WBW.jpg",
                    Latitude = 55.86024,
                    Longitude = 37.55312
                }
            };
        }
        public void Create(Provider provider)
        {
            _providerRepository.Add(provider);
        }

        public void Delete(Provider provider)
        {
            _providerRepository.Remove(provider);
        }

        public PagedList<Provider> Get(ProviderParams providerParams)
        {
            var items = _providerRepository.AsQueryable()
                                            .Search(providerParams.Search)
                                            .Sort(providerParams.OrderBy, providerParams.OrderDir);

            return PagedList<Provider>.ToPagedList(items, providerParams.PageSize, providerParams.PageNumber);
        }

        public Provider GetById(int id)
        {
            return _providerRepository.Find(p => p.Id == id);
        }

        public void Update(Provider provider)
        {
            var providerFromDb = _providerRepository.FirstOrDefault(p => p.Id == provider.Id);
            providerFromDb.ImagePath = provider.ImagePath;
            providerFromDb.Info = provider.Info;
            providerFromDb.Latitude = provider.Latitude;
            providerFromDb.Longitude = provider.Longitude;
            providerFromDb.Name = providerFromDb.Name;
        }

        public void Save()
        {
            
        }
    }
}