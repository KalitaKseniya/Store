﻿using Store.Core.Entities;
using Store.Core.RequestFeatures;
using System.Collections.Generic;

namespace Store.Core.Interfaces
{
    public interface IProductRepository
    {
        PagedList<Product> Get(int category_id, ProductParams productParams);
        Product GetById(int category_id, int id);
        Product GetById(int id);
        void Create(Product product);
        void Save();
        void Delete(Product product);
        void Update(Product product);
        PagedList<Product> GetAllForAllCategories(ProductParams productParams, IEnumerable<int> categoryIds);
    }
}
