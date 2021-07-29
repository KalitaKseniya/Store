﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Entities;
using Store.Core.Interfaces;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly RepositoryContext repository;
        public CategoryRepository(RepositoryContext _repository)
        {
            repository = _repository;
        }
        public IEnumerable<Category> Get()
        {
            return repository.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return repository.Categories.Find(id);
        }

        public void Create(Category category)
        {
            repository.Add(category);
        }
        public void Save()
        {
            repository.SaveChanges();
        }
    }
}

